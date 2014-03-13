using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Surface.Presentation.Controls;
using System.Globalization;
using System.IO;
using System.Collections.ObjectModel;
using StudienarbeitsProjekt.ContentControls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Diagnostics;

namespace StudienarbeitsProjekt {
    /// <summary>
    /// Interaction logic for TagContent.xaml
    /// </summary>
    public partial class TagContent : TagVisualization {
        private ScatterOrientationControl orientationControl = new ScatterOrientationControl();

        #region readonly property Elements
        private ObservableCollection<object> _elements = new ObservableCollection<object>();
        public ObservableCollection<object> Elements { get { return _elements; } }
        #endregion

        private ScatterMovement move;
        private SurfaceWindow1 surWindow;
        public ScatterView mainScatt;
        private bool orientation;


        #region generated Code
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagContent() {
            InitializeComponent();
        }

        #endregion

        #region file reader functions

        public void ShowTagContent(SurfaceWindow1 surWindow) {
            this.surWindow = surWindow;
            this.mainScatt = surWindow.MainScatt;

            this.Message.Visibility = System.Windows.Visibility.Collapsed;

            move = new ScatterMovement(surWindow.MainScatt);

            string tagVal = GetTagValue();

            /// Auslesen der Dateien und festlegen eines Controls je nach Datentyp
            try {
                String[] ordnerPfad = Directory.GetDirectories(FileHandler.rootDir, "*", System.IO.SearchOption.TopDirectoryOnly);
                Console.WriteLine(ordnerPfad);

                String tagChooser;
                // Funktion zum auslesen der Tagnummer aus dem Ordnernamen
                for (int i = 0; i < ordnerPfad.Length; i++) {
                    int counter = ordnerPfad[i].LastIndexOf('\\') + 1;
                    // "-" ist das Trennzeichen zwischen dem in der Ordnerstruktur nummerierten TagValue und dem Namen
                    string start1 = ordnerPfad[i].Substring(counter, ordnerPfad[i].IndexOf('-') - counter);

                    if (start1 == tagVal) {
                        // TagChooser ist die Benennung des gewählten ordners.
                        tagChooser = ordnerPfad[i].Substring(counter);

                        GetTagContent(tagChooser);
                        orientationControl.SetBinding(ScatterOrientationControl.BorderBrushProperty,
                            new Binding("BorderBrush") { Source = this });
                        AddElement(orientationControl);
                        orientationControl.setMainscatt(this.surWindow.MainScatt, this);
                    }
                }
            } catch (FileNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            }
        }

        private void GetTagContent(string fileChooser) {
            try {
                Documents(FileHandler.getDocFiles(fileChooser));
                Images(FileHandler.getImageFiles(fileChooser));
                Videos(FileHandler.getVideoFiles(fileChooser));
                Collections(FileHandler.getCollections(fileChooser));
                String[] collectionPaths = FileHandler.getVideoFiles(fileChooser);
                if (collectionPaths != null)
                    Collections(collectionPaths);
            } catch (FileNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            }
        }

        /// <returns></returns>
        private string GetTagValue() {
            string tagVal = string.Empty;

            if (this.VisualizedTag.Value > 0) {
                tagVal = this.VisualizedTag.Value.ToString("X", CultureInfo.InvariantCulture);

            } else if (this.VisualizedTag.Series > 0) {
                tagVal = this.VisualizedTag.Series.ToString("X", CultureInfo.InvariantCulture);
            }
            return tagVal;
        }
        # endregion

        # region Array reader

        private void Videos(string[] pathNames) {
            foreach (string pfad in pathNames) {
                CreateVideo(pfad, BorderBrush);
            }
        }

        // Funktion zum Auslesen von Ordnern für die Ordnerdarstellung
        private void Collections(string[] paths) {
            if (paths != null)
                foreach (string path in paths) {
                    // Titel des Hauptordners auslesen
                    string name = new FileHandler(path).getFolderName();

                    if (Directory.Exists(path)) {
                        string[] subDirectories = Directory.GetDirectories(path, "*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string subDirectory in subDirectories)
                            CreateCollection(subDirectory, name, BorderBrush);
                    }
                }
        }


        private void Documents(string[] datenPfad) {
            foreach (String pfad in datenPfad) {
                CreateDocument(pfad, BorderBrush);
            }
        }

        private void Images(string[] paths) {
            foreach (string pfad in paths) {
                CreatePromotionImage(pfad, BorderBrush);
            }
        }
        #endregion

        #region Create functions

        public ScatterViewItem CreateDocument(string path, Brush color) {
            return AddElement(new DocumentControl(path, color));
        }


        public ScatterViewItem CreatePromotionImage(string path, Brush color) {
            Image promotionBild = new Image() { Source = new BitmapImage(new Uri(path, UriKind.Absolute)) };
            ScatterViewItem promoScatter = new ScatterViewItem();
            promoScatter.Padding = new System.Windows.Thickness(0);
            promoScatter.Content = promotionBild;
            promoScatter.BorderBrush = color;

            return AddElement(promoScatter);
        }


        // Funktion für den Aufruf von neuen Collections
        public ScatterViewItem CreateCollection(string path, string name, Brush color) {
            Debug.WriteLine("Hier wird die Collection: " + name + " geboren");
            return AddElement(new CollectionControl(path, name, this, color));
        }


        public ScatterViewItem CreateVideo(String path, Brush color) {
            return AddElement(new VideoControl(path, color));
        }

        #endregion

        # region Remove and add functions

        public void Remove(ScatterViewItem item) {
            move.removeScatterViewItem(item);
        }

        private ScatterViewItem AddElement(ScatterViewItem item) {
            Elements.Add(item);
            surWindow.Items.Add(item);
            return item;
        }
        #endregion

        # region  Naming, setter and Events




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
        #endregion
    }
}