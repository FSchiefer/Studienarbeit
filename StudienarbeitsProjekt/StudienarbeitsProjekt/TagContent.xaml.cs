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


                //String[] bilderPfad = Directory.GetFiles(@"C:\Studiengaenge\" + tagVal, "*.jpg");

                //PromotionBilder(bilderPfad);

                //String seitenPfad = "C:\\Studiengaenge\\" + tagVal + "\\Seiten\\";
                //if(Directory.Exists(seitenPfad)){
                //    bilderPfad = Directory.GetFiles(seitenPfad, "*.jpg");
                //    einzelSeitenBilder(bilderPfad);
                //}

                //String modulPfad = "C:\\Studiengaenge\\" + tagVal + "\\Modulplan\\";
                //if (Directory.Exists(modulPfad))
                //{
                //    bilderPfad = Directory.GetFiles(modulPfad, "*.jpg");
                //    ModulPlanBilder(bilderPfad);
                //}
                //Console.WriteLine("Ich kam durch");

                //String videoPfad = "C:\\Studiengaenge\\" + tagVal + "\\Videos\\";
                //if (Directory.Exists(videoPfad))
                //{
                //    bilderPfad = Directory.GetFiles(videoPfad, "*.wmv");
                //    Videos(bilderPfad);
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
            //DocumentControl dokument = new DocumentControl();
            //Bilder.Add(dokument);
            try
            {
                String[] bilderPfad = Directory.GetFiles(@"C:\Studiengaenge\" + fileChooser, "*.jpg");

                PromotionBilder(bilderPfad);

                String seitenPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Seiten\\";
                if (Directory.Exists(seitenPfad))
                {
                    bilderPfad = Directory.GetFiles(seitenPfad, "*.jpg");
                    einzelSeitenBilder(bilderPfad);
                }

                String modulPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Modulplan\\";
                if (Directory.Exists(modulPfad))
                {
                    bilderPfad = Directory.GetFiles(modulPfad, "*.jpg");
                    ModulPlanBilder(bilderPfad);
                }
                Console.WriteLine("Ich kam durch");

                String videoPfad = "C:\\Studiengaenge\\" + fileChooser + "\\Videos\\";
                if (Directory.Exists(videoPfad))
                {
                    bilderPfad = Directory.GetFiles(videoPfad, "*.wmv");
                    Videos(bilderPfad);
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

        private void Videos(string[] bilderPfad)
        {
            Console.WriteLine(bilderPfad[0]);
            for (int i = 0; i < bilderPfad.Length; i++)
            {
                String test = bilderPfad[i];
                MediaElement videoDarstellung = new MediaElement();
                videoDarstellung.Source = new Uri(test, UriKind.Absolute);
                VideoControl videoScatter = new VideoControl(videoDarstellung);
                Bilder.Add(videoScatter);
            }
        }

        private void ModulPlanBilder(string[] bilderPfad)
        {
            throw new NotImplementedException();
        }

        private void einzelSeitenBilder(string[] bilderPfad)
        {
            for (int i = 0; i < bilderPfad.Length; i++)
            {
                String test = bilderPfad[i];
                Image promotionBild = new Image() { Source = new BitmapImage(new Uri(test, UriKind.Absolute)) };
                ScatterViewItem promoScatter = new ScatterViewItem();
                promoScatter.Content = promotionBild;
                Bilder.Add(promoScatter);
            }
        }

        private void PromotionBilder(string[] bilderPfad)
        {
            for (int i = 0; i < bilderPfad.Length; i++)
            {
                String test = bilderPfad[i];
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