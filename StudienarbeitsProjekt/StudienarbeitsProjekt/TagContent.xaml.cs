using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Surface.Presentation.Controls;
using System.Globalization;
using System.IO;
using System.Collections.ObjectModel;
using StudienarbeitsProjekt.ContentControls;
using System.Windows.Media;

namespace StudienarbeitsProjekt
{
    /// <summary>
    /// Interaction logic for TagContent.xaml
    /// </summary>
    public partial class TagContent : TagVisualization
    {
        private static String rootDir = @"C:\Studiengaenge\";

        private ScatterOrientationControl orientationControl = new ScatterOrientationControl();

        private ObservableCollection<object> elements = new ObservableCollection<object>();
        public ObservableCollection<object> Elements { get { return elements; } }
        public ScatterView mainScatt;
        private ScatterMovement move;
        private SurfaceWindow1 surWindow;
        private bool orientation;
        private Brush color;


        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagContent()
        {
            InitializeComponent();
            
          
           
        }

        

        public ObservableCollection<object> ShowTagContent(SurfaceWindow1 surWindow, Brush color)
        {
            this.surWindow = surWindow;
            this.mainScatt = surWindow.MainScatt;
            this.color = color;
            move = new ScatterMovement(this.mainScatt);
        
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
                    int counter = ordnerPfad[i].LastIndexOf('\\') + 1;
                    // "-" ist das Trennzeichen zwischen dem in der Ordnerstruktur nummerierten TagValue und dem Namen
                    string start1 = ordnerPfad[i].Substring(counter, ordnerPfad[i].IndexOf('-') - counter);

                    if (start1 == tagVal)
                    {
                        // TagChooser ist die Benennung des gew�hlten ordners.
                        tagChooser = ordnerPfad[i].Substring(counter);
                       
                        getTagContent(tagChooser);
                        orientationControl.BorderBrush = color;
                        addElement(orientationControl);
                        orientationControl.setMainscatt(mainScatt, this);    
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
            return Elements;
        }

        private void getTagContent(String fileChooser)
        {
            try
            {
                String baseDir = rootDir + fileChooser;

                String[] dataPath = Directory.GetFiles(baseDir, "*.jpg");
                PromotionBilder(dataPath);


                dataPath = Directory.GetFiles(baseDir, "*.xps");
                dokumente(dataPath);

                dataPath = Directory.GetFiles(baseDir, "*.wmv");
                videos(dataPath);

                dataPath = Directory.GetFiles(baseDir, "*.mp4");
                videos(dataPath);

                dataPath = Directory.GetFiles(baseDir, "*.avi");
                videos(dataPath);
                dataPath = Directory.GetFiles(baseDir, "*.mpg");
                videos(dataPath);
                dataPath = Directory.GetFiles(baseDir, "*.MTS");
                videos(dataPath);

                // W�hlt die Ordner der einzelnen Sammlungen aus
                String collectionPath = baseDir + "\\collections\\";
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

            
        
        }

        private void videos(String[] pathNames)
        {
      

            foreach (String pfad in pathNames)
            {
                createVideo(pfad, color);
            }
        }

        public ScatterViewItem createVideo(String pfad, Brush color)
        {
            return addElement(new VideoControl(pfad, color));
        }

        
        // Funktion zum auslesen von Ordnern f�r die Ordnerdarstellung
        private void collections(string[] collectionPfad)
        {
            foreach (String pfad in collectionPfad)
            {
                // Titel des Hauptordners auslesen
                String name = getFolderName(pfad);
                if (Directory.Exists(pfad))
                {
                    string[] dataPath = Directory.GetDirectories(pfad, "*", System.IO.SearchOption.TopDirectoryOnly);
                    foreach (String path in dataPath)
                        createCollection(path, name, color);
                 
                }

            }
        }

        // Funktion f�r den Aufruf von neuen Collections
        public ScatterViewItem createCollection(String path, String name, Brush color) {
            Console.WriteLine("Hier wird die Collection: " + name + " geboren");
            return addElement(new CollectionControl(path, name, this, color));
        }

        private void dokumente(string[] datenPfad)
        {
            foreach (String pfad in datenPfad)
            {
                createDocument(pfad, color);
            }
        }

        public ScatterViewItem createDocument(String pfad, Brush color)
        {
            return addElement(new DocumentControl(pfad, color));
        }

        private void PromotionBilder(string[] datenPfad)
        {
            foreach (String pfad in datenPfad)
            {
                createPromotionImage(pfad, color);
            }

        }
        public ScatterViewItem createPromotionImage(String pfad, Brush color)
        {
            Image promotionBild = new Image() { Source = new BitmapImage(new Uri(pfad, UriKind.Absolute)) };
            ScatterViewItem promoScatter = new ScatterViewItem();
            promoScatter.Padding = new System.Windows.Thickness(0);
            promoScatter.Content = promotionBild;
            promoScatter.BorderBrush = color;
           
            return addElement(promoScatter);
        }

        private ScatterViewItem addElement(ScatterViewItem item)
        {
            Elements.Add(item);
            mainScatt.Items.Add(item);
            return item;
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

        private String getFolderName(String dokumentPfad)
        {
            // Ausgabe des Ordnernamens des Dokuments
            int beginDirectoryName = dokumentPfad.LastIndexOf('\\') + 1;
            String name = dokumentPfad.Substring(beginDirectoryName, dokumentPfad.Length - beginDirectoryName);

            return name;
        }

        public void Remove(ScatterViewItem item)
        {
            move.removeScatterViewItem(item);
         
        }

        public void setTagOrientation(bool orientation) {
            this.orientation = orientation;
        }

        private void startVisualizer_VisualizationMoved(object sender, TagVisualizerEventArgs e) {
            Console.WriteLine(e.TagVisualization.Orientation);
            Console.WriteLine(orientation);

            if (orientation) {
                TagContent content = e.TagVisualization as TagContent;
                foreach (ScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }

                    move.ScatterOrientationAnimation(svi, e.TagVisualization.Orientation, TimeSpan.FromSeconds(0.5));
                }
            }
        }
        
    }
}