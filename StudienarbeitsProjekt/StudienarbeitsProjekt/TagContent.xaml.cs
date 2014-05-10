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
    public partial class TagContent : TagVisualization,ContentList {
        private ScatterOrientationControl orientationControl;

        #region readonly property Elements
        private ObservableCollection<MovableScatterViewItem> _elements = new ObservableCollection<MovableScatterViewItem>();
        public ObservableCollection<MovableScatterViewItem> Elements { get { return _elements; } }
        #endregion

        private MainWindow surWindow;
        public ScatterView mainScatt;
        private bool orientation;
        private bool movement;


        #region generated Code
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagContent() {
            InitializeComponent();
        }

        #endregion

        #region file reader functions

        public void ShowTagContent(MainWindow surWindow) {
            this.surWindow = surWindow;
            this.mainScatt = surWindow.MainScatt;


            this.Message.Visibility = System.Windows.Visibility.Collapsed;

            string tagVal = GetTagValue();

            /// Auslesen der Dateien und festlegen eines Controls je nach Datentyp
            try {
                String[] ordnerPfad = Directory.GetDirectories(FileHandler.rootDir, "*", System.IO.SearchOption.TopDirectoryOnly);
                Debug.WriteLine(ordnerPfad);


                

                String tagChooser;
                // Funktion zum auslesen der Tagnummer aus dem Ordnernamen

                for (int i = 0; i < ordnerPfad.Length; i++) {
                    int indexOfMinus = ordnerPfad[i].IndexOf('-');
                    if (indexOfMinus > -1) {
                        int counter = ordnerPfad[i].LastIndexOf('\\') + 1;
                        Debug.WriteLine("Counter " + indexOfMinus + " " + ordnerPfad[i]);
                        // "-" ist das Trennzeichen zwischen dem in der Ordnerstruktur nummerierten TagValue und dem Namen
                        string start1 = ordnerPfad[i].Substring(counter, indexOfMinus - counter);
                        if (start1 == tagVal) {
                            // TagChooser ist die Benennung des gewählten ordners.
                            tagChooser = ordnerPfad[i].Substring(counter);
                            orientationControl = new ScatterOrientationControl(this.surWindow.MainScatt, this);
                            orientationControl.SetBinding(ScatterOrientationControl.BorderBrushProperty,
                                new Binding("BorderBrush") { Source = this });
                            AddElement(orientationControl);

                            GetTagContent(tagChooser);
                        }
                    }
                }
            } catch (FileNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            }
        }

        private void GetTagContent(string fileChooser) {
            surWindow.generateContent(FileHandler.rootDir + fileChooser, this);
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

       
        # region Remove and add functions


        public MovableScatterViewItem AddElement(MovableScatterViewItem item) {
            Elements.Add(item);
            return item;
        }

        public Brush getBrush() {
            return BorderBrush;
        }
        #endregion

        # region  Naming, setter and Events




        public void setTagOrientation(bool orientation) {
            this.orientation = orientation;

        }

        public void setMovement(bool movement) {
            this.movement = movement;
        }

        private void startVisualizer_VisualizationMoved(object sender, TagVisualizerEventArgs e) {
            Debug.WriteLine(e.TagVisualization.Orientation);
            Debug.WriteLine(orientation);

            if (orientation) {
                TagContent content = e.TagVisualization as TagContent;
                foreach (MovableScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }

                    svi.ScatterOrientationAnimation(e.TagVisualization.Orientation, TimeSpan.FromSeconds(0.5));
                }
            }
        }
        #endregion
    }
}