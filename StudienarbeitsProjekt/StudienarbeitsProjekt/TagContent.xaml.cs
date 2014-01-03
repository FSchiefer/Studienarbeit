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
                String[] ordnerPfad = Directory.GetDirectories(@"C:\Studiengaenge\", "*", System.IO.SearchOption.TopDirectoryOnly);
                Console.WriteLine(ordnerPfad);

                String tagChooser;
                for (int i = 0; i < ordnerPfad.Length; i++)
                {
                    Console.WriteLine(i + ordnerPfad[i]);
                    string start1 = ordnerPfad[i].Substring(17, ordnerPfad[i].IndexOf('-') - 17);
                    Console.WriteLine("start1 " + start1 + " tagVal " + tagVal);
                    if (start1 == tagVal)
                    {

                        tagChooser = ordnerPfad[i].Substring(17);
                        Console.WriteLine(tagChooser);
                        getTagContent(tagChooser);
                    }
                }


                //String[] dataPath = Directory.GetFiles(@"C:\Studiengaenge\" + tagVal, "*.jpg");

                //PromotionBilder(dataPath);

                //String dokumentPfad = "C:\\Studiengaenge\\" + tagVal + "\\Seiten\\";
                //if(Directory.Exists(dokumentPfad)){
                //    dataPath = Directory.GetFiles(dokumentPfad, "*.jpg");
                //    dokumente(dataPath);
                //}

                //String modulPfad = "C:\\Studiengaenge\\" + tagVal + "\\Modulplan\\";
                //if (Directory.Exists(modulPfad))
                //{
                //    dataPath = Directory.GetFiles(modulPfad, "*.jpg");
                //    ModulPlanBilder(dataPath);
                //}
                //Console.WriteLine("Ich kam durch");

                //String videoPfad = "C:\\Studiengaenge\\" + tagVal + "\\Videos\\";
                //if (Directory.Exists(videoPfad))
                //{
                //    dataPath = Directory.GetFiles(videoPfad, "*.wmv");
                //    Videos(dataPath);
                //}






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

        private void getTagContent(string fileChooser)
        {
            
            try
            {
                String[] dataPath = Directory.GetFiles(@"C:\Studiengaenge\" + fileChooser, "*.jpg");

                PromotionBilder(dataPath);

                String dokumentPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Dokumente\\";
                if (Directory.Exists(dokumentPfad))
                {
                    dataPath = Directory.GetFiles(dokumentPfad, "*.xps");
                    dokumente(dataPath);
                }

                String modulPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Modulplan\\";
                if (Directory.Exists(modulPfad))
                {
                    dataPath = Directory.GetFiles(modulPfad, "*.jpg");
                    ModulPlanBilder(dataPath);
                }
                Console.WriteLine("Ich kam durch");

                String videoPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Videos\\";
                if (Directory.Exists(videoPfad))
                {
                    dataPath = Directory.GetFiles(videoPfad, "*.wmv");
                    Videos(dataPath);
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

        private void Videos(string[] datenPfad)
        {
            Console.WriteLine(datenPfad[0]);
            for (int i = 0; i < datenPfad.Length; i++)
            {
                String test = datenPfad[i];
                VideoControl videoScatter = new VideoControl(test);
                Bilder.Add(videoScatter);
            }
        }

        private void ModulPlanBilder(string[] bilderPfad)
        {
            throw new NotImplementedException();
        }

        private void dokumente(string[] datenPfad)
        {
            for (int i = 0; i < datenPfad.Length; i++)
            {
                //DocumentControl dokument = new DocumentControl();
                //Bilder.Add(dokument);
                String test = datenPfad[i];
                DocumentControl dokument = new DocumentControl(test);
                Bilder.Add(dokument);
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