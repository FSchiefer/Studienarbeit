using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Surface.Presentation.Controls;
using System.Globalization;
using System.IO;
using System.Collections.ObjectModel;
using StudienarbeitsProjekt.ContentControls;



namespace StudienarbeitsProjekt
{
    /// <summary>
    /// Interaction logic for TagContent.xaml
    /// </summary>
    public partial class TagContent : TagVisualization
    {
        private static const String rootDir = @"C:\Studiengaenge\";

        private ObservableCollection<object> bilder = new ObservableCollection<object>();
        public ObservableCollection<object> Bilder { get { return bilder; } }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagContent()
        {
            InitializeComponent();
            mainView.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            mainView.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
        }

        public void ShowTagContent()
        {
            string tagVal = GetTagValue();

            /// Auslesen der Dateien und festlegen eines Controls je nach Datentyp
            try
            {
                String[] ordnerPfad = Directory.GetDirectories(rootDir, "*", System.IO.SearchOption.TopDirectoryOnly);
                Console.WriteLine(ordnerPfad);

                String tagChooser;
                // Funktion zum auslesen der Tagnummer aus dem Ordnernamen
                for (int i = 0; i < ordnerPfad.Length; i++)
                {
                    int counter = ordnerPfad[i].LastIndexOf('\\') +1;
                    // "-" ist das Trennzeichen zwischen dem in der Ordnerstruktur nummerierten TagValue und dem Namen
                    string start1 = ordnerPfad[i].Substring(counter, ordnerPfad[i].IndexOf('-') - counter);

                    if (start1 == tagVal)
                    {

                        // TagChooser ist die Benennung des gewählten ordners.
                        tagChooser = ordnerPfad[i].Substring(counter);
                        Console.WriteLine(tagChooser);
                        getTagContent(tagChooser);
                    }
                }


            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }
        }

        private void getTagContent(String fileChooser)
        {
            try
            {
                String baseDir = rootDir + fileChooser;

                String[] dataPath = Directory.GetFiles(rootDir + fileChooser, "*.jpg");
                PromotionBilder(dataPath);

                String dokumentPfad = baseDir + "\\Dokumente\\";
                if (Directory.Exists(dokumentPfad))
                {
                    dataPath = Directory.GetFiles(dokumentPfad, "*.xps");
                    dokumente(dataPath);
                }

                String videoPfad = baseDir + "\\Videos\\";
                if (Directory.Exists(videoPfad))
                {
                    dataPath = Directory.GetFiles(videoPfad, "*.wmv");
                    videos(dataPath);
                }

                // Wählt die Ordner der einzelnen Sammlungen aus
                String collectionPath = baseDir + "\\Sammlung\\";
                if (Directory.Exists(collectionPath))
                {
                    dataPath = Directory.GetDirectories(collectionPath, "*", System.IO.SearchOption.TopDirectoryOnly);
                    collections(dataPath);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }

            ScatterOrientationControl control = new ScatterOrientationControl(this);
            Bilder.Add(control);

        }

        private void videos(String[] pathNames)
        {
            Console.WriteLine(pathNames[0]);

            foreach (String pfad in pathNames)
            {
                Bilder.Add(new VideoControl(pfad));
            }
        }

        private void collections(string[] collectionPfad)
        {
            foreach (String pfad in collectionPfad)
                Bilder.Add(new CollectionControl(pfad));
        }

        private void dokumente(string[] datenPfad)
        {
            foreach (String pfad in datenPfad)
            {
                Bilder.Add(new DocumentControl(pfad));
            }
        }

        private void PromotionBilder(string[] datenPfad)
        {
            for (int i = 0; i < datenPfad.Length; i++)
            {
                String test = datenPfad[i];
                Image promotionBild = new Image() { Source = new BitmapImage(new Uri(test, UriKind.Absolute)) };
                ScatterViewItem promoScatter = new ScatterViewItem();
                promoScatter.Content = promotionBild;
                Bilder.Add(promoScatter);
            }
        }



        /// <returns></returns>
        private string GetTagValue()
        {
            string tagVal = string.Empty;
            if (this.VisualizedTag.Value > 0)
            {
                tagVal = this.VisualizedTag.Value.ToString("X", CultureInfo.InvariantCulture);
            }
            else if (this.VisualizedTag.Series > 0)
            {
                tagVal = this.VisualizedTag.Series.ToString("X", CultureInfo.InvariantCulture);
            }
            return tagVal;
        }

    }
}