using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DoodleClassifier
{
    public class Drawing
    {
        public eType Type { get; set; }
        public byte[] ContentData { get; set; }
        public Image Picture
        {
            get
            {
                Bitmap img = new Bitmap(28, 28);
                for (int i = 0; i < img.Width * img.Height; i++)
                {
                    int x = i % img.Width;
                    int y = i / img.Width;
                    byte val = ContentData[i];
                    img.SetPixel(x, y, Color.FromArgb(val, val, val));
                }
                return img;
            }
        }

        public Drawing(byte[] pContentData, eType pType)
        {
            ContentData = pContentData;
            Type = pType;
        }

        public static List<Drawing> CreateFromNpyFile(string Path, eType pType)
        {
            List<Drawing> drawings = new List<Drawing>();
            byte[] data = File.ReadAllBytes(Path);
            int start = 80;
            int total = (data.Length - start) / (28 * 28);

            for (int j = 0; j < total; j++)
            {
                start = 80 + j * 28 * 28;
                byte[] ContentData = new byte[28 * 28];
                for (int i = 0; i < 28 * 28; i++)
                {
                    int index = i + start;
                    ContentData[i] = data[index];
                }
                drawings.Add(new Drawing(ContentData, pType));
            }
            return drawings;
        }
    }
}
