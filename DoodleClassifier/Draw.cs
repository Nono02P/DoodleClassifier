using IA;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;

namespace DoodleClassifier
{
    public partial class Draw : Form
    {
        #region Variables privées
        private Color _penColor = Color.White;
        private Bitmap _img;
        private Point _oldPos;
        private bool _mouseDown;
        private ConvolutionalNeuralNetwork _neuralNetwork;
        private DataContractJsonSerializer ser;
        #endregion

        #region Constructeur
        public Draw()
        {
            InitializeComponent();
            _img = new Bitmap(picBoxDrawing.Width, picBoxDrawing.Height);
            ClearImage();
            ser = new DataContractJsonSerializer(typeof(ConvolutionalNeuralNetwork));
            MemoryStream stream = new MemoryStream(File.ReadAllBytes(Program.FILE_PATH));
            _neuralNetwork = (ConvolutionalNeuralNetwork)ser.ReadObject(stream);
        }
        #endregion

        #region Fonctions
        private void ClearImage()
        {
            for (int i = 0; i < picBoxDrawing.Width; i++)
            {
                for (int j = 0; j < picBoxDrawing.Height; j++)
                {
                    _img.SetPixel(i, j, Color.Black);
                }
            }
            picBoxDrawing.Image = _img;
        }
        #endregion

        #region Boutons

        private void btnSaveDrawing_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Bitmap|*.bmp";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _img.Save(fileDialog.FileName);
            }
        }

        private void btnOpenDrawing_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Bitmap|*.bmp";
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _img = new Bitmap(fileDialog.FileName);
                picBoxDrawing.Image = _img;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearImage();
            txtCat.Text = string.Empty;
            txtCar.Text = string.Empty;
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(_img, 28, 28);
            picBoxScaled.Image = img;
            float[] data = new float[img.Height * img.Width];
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    data[i + img.Width * j] = img.GetPixel(i, j).GetBrightness();
                }
            }
            float[] outputs = _neuralNetwork.FeedForward(data);
            int[] pourcent = Array.ConvertAll(outputs, output => (int)(output * 100));
            txtCat.Text = pourcent[0].ToString();
            txtCar.Text = pourcent[1].ToString();
        }
        #endregion

        #region Fenetre dessin
        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(picBoxDrawing.Image, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
        }

        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            picBoxDrawing.Capture = true;
            _oldPos = new Point(e.X, e.Y);                    // point de départ du nouveau tracé
        }

        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
            picBoxDrawing.Capture = false;
        }

        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                Graphics g = Graphics.FromImage(_img);
                Pen pen = new Pen(_penColor, 4);
                if (_oldPos != new Point(-1, -1))
                    g.DrawEllipse(pen, _oldPos.X, _oldPos.Y, pen.Width, pen.Width);                      // tracé de la ligne

                // Calcul de la zone à redessiner (recherche du rectangle circonscrit)
                int offsetx = Math.Min(e.X, _oldPos.X);
                int offsety = Math.Min(e.Y, _oldPos.Y);
                Rectangle rInvalid = new Rectangle(offsetx, offsety, Math.Abs(_oldPos.X - e.X), Math.Abs(_oldPos.Y - e.Y));
                rInvalid.Inflate((int)pen.Width, (int)pen.Width);
                picBoxDrawing.Invalidate(rInvalid, true);
                _oldPos = new Point(e.X, e.Y);
            };
        }
        #endregion
    }
}
