using System;
using System.Windows.Forms;

namespace DoodleClassifier
{
    static class Program
    {
        public const string FILE_PATH = "Neural Network.json";

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            //Application.Run(new Draw());

            /* Pour exécuter le formulaire "Doodle" qui sert à créer et entrainer un réseau de neurone à reconnaitre des dessins,
             * il est necéssaire d'ajouter les fichiers npy correspondant aux chats et aux voitures.
             * Ils sont téléchargeables ici (il faudra toutefois les renommer en "cat.npy" et "car.npy" :
             * https://console.cloud.google.com/storage/browser/quickdraw_dataset/full/numpy_bitmap
             * 
             * Ils sont à placer dans le répertoire :
             * DoodleClassifier\bin\Debug
             * 
             * Puis commenter l'appel au formulaire "Draw".
             */
            Application.Run(new Doodle());
        }
    }
}
