using IA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using static IA.ConvolutionalNeuralNetwork;

namespace DoodleClassifier
{
    #region Enumérations
    public enum eType : byte
    {
        Cat,
        Car,
    }
    #endregion

    public partial class Doodle : Form
    {
        #region Constantes
        private const float POURCENT_OF_TESTING = 0.7f;
        #endregion

        #region Variables privées
        private float _correctTraining;
        private float _correctTesting;
        private List<Drawing> _trainingDataset;
        private List<Drawing> _testingDataset;
        private int _currentImage = 0;
        private ConvolutionalNeuralNetwork _neuralNetwork;
        private DataContractJsonSerializer ser;
        #endregion

        #region Constructeur
        public Doodle()
        {
            InitializeComponent();

            ser = new DataContractJsonSerializer(typeof(ConvolutionalNeuralNetwork));
            List<Drawing> images = new List<Drawing>();
            // Récupération des chats et division des données en 2 parties (Training et Testing set)
            images = Drawing.CreateFromNpyFile("cat.npy", eType.Cat);
            _trainingDataset = images.GetRange(0, (int)(images.Count * POURCENT_OF_TESTING));
            _testingDataset = images.GetRange((int)(images.Count * POURCENT_OF_TESTING), images.Count - (int)(images.Count * POURCENT_OF_TESTING));
            // Récupération des voitures et division des données en 2 parties (Training et Testing set)
            images = Drawing.CreateFromNpyFile("car.npy", eType.Car);
            _trainingDataset.AddRange(images.GetRange(0, (int)(images.Count * POURCENT_OF_TESTING)));
            _testingDataset.AddRange(images.GetRange((int)(images.Count * POURCENT_OF_TESTING), images.Count - (int)(images.Count * POURCENT_OF_TESTING)));
            // Mélange les données.
            _trainingDataset.Reverse();
            _testingDataset.Reverse();
            pictureBox.Image = _testingDataset[_currentImage].Picture;
        }
        #endregion

        #region Fonctions
        private void CreateBrain()
        {
            Random rnd = new Random();
            List<Layer> layers = new List<Layer>();
            Layer layer = new Layer(ePooling.Average, 3, 1, 1, ActivationFunctions.eActivationFunction.ELU);
            for (int i = 0; i < 4; i++)
            {
                layer.AddFilter(Layer.eFilterType.Hazard, rnd);
            }
            layers.Add(layer);
            
            layer = new Layer(ePooling.Average, 5, 0, 5, ActivationFunctions.eActivationFunction.ELU);
            for (int i = 0; i < 8; i++)
            {
                layer.AddFilter(Layer.eFilterType.Hazard, rnd);
            }
            layers.Add(layer);

            layer = new Layer(ePooling.Average, 3, 0, 3, ActivationFunctions.eActivationFunction.ELU);
            for (int i = 0; i < 16; i++)
            {
                layer.AddFilter(Layer.eFilterType.Hazard, rnd);
            }
            layers.Add(layer);

            layer = new Layer(ePooling.Max, 2, 0, 2, ActivationFunctions.eActivationFunction.TanH);
            for (int i = 0; i < 32; i++)
            {
                layer.AddFilter(Layer.eFilterType.Hazard, rnd);
            }
            layers.Add(layer);

            _neuralNetwork = new ConvolutionalNeuralNetwork(28, 28, layers, new int[] { 32, 16 }, 2, 0.2f, ActivationFunctions.eActivationFunction.TanH);
        }

        private void EnableDisableButtons(bool pState)
        {
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = pState;
                }
            }
        }
        #endregion

        #region Fenetre dessin
        private void pictureBox_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            _currentImage = rnd.Next(0, _testingDataset.Count);
            pictureBox.Image = _testingDataset[_currentImage].Picture;
        }
        #endregion

        #region Boutons
        private void btnTrain_Click(object sender, EventArgs e)
        {
            _trainingDataset.Reverse();
            EnableDisableButtons(false);
            _correctTraining = 0;
            Random rnd = new Random();
            for (int i = 0; i < _trainingDataset.Count; i++)
            {
                int index = rnd.Next(0, _trainingDataset.Count);
                Drawing d = _trainingDataset[index];
                float[] desired;
                switch (d.Type)
                {
                    case eType.Cat:
                        desired = new float[] { 1, 0 };
                        break;
                    case eType.Car:
                        desired = new float[] { 0, 1 };
                        break;
                    default:
                        desired = new float[2];
                        break;
                }
                float[] inputs = Array.ConvertAll(d.ContentData, data => data / 255f);
                float[] outputs = _neuralNetwork.Train(inputs, desired).Item1;
                eType type;
                if (outputs[0] > outputs[1])
                {
                    type = eType.Cat;
                }else
                {
                    type = eType.Car;
                }
                if (type == d.Type)
                {
                    _correctTraining++;
                }
            }
            lblTrainingDS.Text = "Training dataset : " + (100 * _correctTraining / _trainingDataset.Count).ToString() + " %";

            _correctTesting = 0;
            for (int i = 0; i < _testingDataset.Count; i++)
            {
                int index = rnd.Next(0, _testingDataset.Count);
                Drawing d = _testingDataset[index];
                float[] inputs = Array.ConvertAll(d.ContentData, data => data / 255f);
                float[] outputs = _neuralNetwork.FeedForward(inputs);
                eType type;
                if (outputs[0] > outputs[1])
                {
                    type = eType.Cat;
                }
                else
                {
                    type = eType.Car;
                }
                if (type == d.Type)
                {
                    _correctTesting++;
                }
            }
            lblTestingDS.Text = "Testing dataset : " + (100 * _correctTesting / _testingDataset.Count).ToString() + " %";

            EnableDisableButtons(true);
        }

        private void btnRecognize_Click(object sender, EventArgs e)
        {
            lblAnswer.Visible = true;
            Drawing d = _testingDataset[_currentImage];
            switch (d.Type)
            {
                case eType.Cat:
                    lblAnswer.Text = "The answer was a Cat";
                    break;
                case eType.Car:
                    lblAnswer.Text = "The answer was a Car";
                    break;
                default:
                    break;
            }
            float[] inputs = Array.ConvertAll(d.ContentData, data => data / 255f);
            float[] outputs = _neuralNetwork.FeedForward(inputs);
            int[] pourcent = Array.ConvertAll(outputs, output => (int)(output * 100));
            txtCat.Text = pourcent[0].ToString();
            txtCar.Text = pourcent[1].ToString();
        }

        private void btnExportBrain_Click(object sender, EventArgs e)
        {
            if (_neuralNetwork != null)
            {
                FileStream stream = File.Create(Program.FILE_PATH);
                ser.WriteObject(stream, _neuralNetwork);
                stream.Close();
            }
        }

        private void btnImportBrain_Click(object sender, EventArgs e)
        {
            if (File.Exists(Program.FILE_PATH))
            {
                MemoryStream stream = new MemoryStream(File.ReadAllBytes(Program.FILE_PATH));
                _neuralNetwork = (ConvolutionalNeuralNetwork)ser.ReadObject(stream);
            }
            else
            {
                CreateBrain();
            }
            if (_neuralNetwork != null)
            {
                EnableDisableButtons(true);
            }
        }

        private void btnCreateBrain_Click(object sender, EventArgs e)
        {
            CreateBrain();
            if (_neuralNetwork != null)
            {
                EnableDisableButtons(true);
            }
        }
        #endregion

    }
}
