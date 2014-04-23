using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using StudienarbeitsProjekt.ContentControls;
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace StudienarbeitsProjekt {
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow, ContentList {

        private int TouchesOnMainScatter = 0;
        private ObservableCollection<object> elements = new ObservableCollection<object>();
        private Queue<Color> userColors = new Queue<Color>();
        private List<TagVisualization> pending = new List<TagVisualization>();
        private List<MovableScatterViewItem> defaultContent = new List<MovableScatterViewItem>();


        public ObservableCollection<object> Elements { get { return elements; } }

        #region generated Code

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1() {
            InitializeComponent();
            try {
                MainScatterImage.ImageSource = new BitmapImage(new Uri(FileHandler.getMainscatterImage(), UriKind.Relative));
            } catch (FileNotFoundException ex) {
                Debug.WriteLine("No File" + ex);
               
            } 
  
            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            userColors.Enqueue(Colors.Red);
            userColors.Enqueue(Colors.Blue);
            //userColors.Enqueue(Colors.Green);

            generateDefaultContent();
            //userColors.Enqueue(Colors.Yellow);
            startVisualizer.VisualizationRemoved += new TagVisualizerEventHandler(startVisualizer_VisualizationRemoved);
            startVisualizer.VisualizationInitialized +=
                new TagVisualizerEventHandler(StartVisualizer_VisualizationInitialized);
        }




        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers() {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers() {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e) {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e) {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e) {
            //TODO: disable audio, animations here
        }

        #endregion

        #region events

        private void MainScatter_SizeChanged(object sender, SizeChangedEventArgs e) {

            double s = Math.Sqrt(Math.Pow(MainScatter.ActualWidth, 2) / 2);
            MainContentGrid.Width = s;
            MainContentGrid.Height = s;
        }


        void startVisualizer_VisualizationRemoved(object sender, TagVisualizerEventArgs e) {
            TagContent content = (TagContent)e.TagVisualization;

            if (pending.Contains(content))
                pending.Remove(content);
            else
                if (pending.Any()) {
                    TagContent waiting = (TagContent)pending.ElementAt(0);
                    pending.RemoveAt(0);

                    waiting.Circle.Color = content.Circle.Color;
                    waiting.BorderBrush = content.BorderBrush;

                    waiting.ShowTagContent(this);
                } else
                    userColors.Enqueue(content.Circle.Color);

            if (content.Elements != null) {
                foreach (MovableScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter")
                        continue;

                    Debug.WriteLine(svi.Name);

                    svi.MoveAndOrientateScatterToClose(e.TagVisualization.Center, e.TagVisualization.Orientation);
                }
            }

            if (userColors.Count == 2) {
                generateDefaultContent();
            }

        }


        private void StartVisualizer_VisualizationInitialized(object sender, TagVisualizerEventArgs e) {
            TagContent content = (TagContent)e.TagVisualization;

            if (userColors.Count > 0) {
                removeDefaultContent();
                Color color = userColors.Dequeue();
                content.BorderBrush = new SolidColorBrush(color);
                content.ShowTagContent(this);
                content.Circle.Color = color;

            } else {
                content.Message.Content = "Bitte warten bis ein anderer Tag abgehoben wird";
                content.Message.Foreground = Brushes.Red;
                content.Message.Background = Brushes.White;
                pending.Add(content);
            }
        }



        private void MainScatter_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e) {
            Point moveTo = new Point(host.ActualWidth / 2, host.ActualHeight / 2);
            MainScatter.MoveToPosition(moveTo, TimeSpan.FromSeconds(0.5));
        }


        private void MainScatter_TouchDown(object sender, TouchEventArgs e) {
            e.TouchDevice.Deactivated += new EventHandler(TouchDevice_Deactivated);
            if (++TouchesOnMainScatter >= 2)
                MainScatter.CanMove = true;
        }

        private void TouchDevice_Deactivated(object sender, EventArgs e) {
            if (--TouchesOnMainScatter < 2)
                MainScatter.CanMove = false;
        }
        #endregion

        private void generateDefaultContent() {
            generateContent(FileHandler.getDefaultConent(), this);
        }

       public void generateContent(String pfad, ContentList content) {
            try {

                Documents(FileHandler.getDocFiles(pfad), content);
                Images(FileHandler.getImageFiles(pfad), content);
                Videos(FileHandler.getVideoFiles(pfad), content);
                Collections(FileHandler.getCollections(pfad),content);
                Debug.WriteLine(FileHandler.getMailFile(pfad) + "  " + File.Exists(FileHandler.getMailFile(pfad)));
                if (FileHandler.existsMailFile(pfad)) {
                    CreateInformationControl(FileHandler.getMailFile(pfad),content);
                }
            } catch (FileNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            } catch (DirectoryNotFoundException ex) {
                Console.WriteLine("No Folder" + ex);
            }         }

        private void removeDefaultContent() {
    

            if (defaultContent != null) {
                foreach (MovableScatterViewItem svi in defaultContent) {
                    if (svi.Name == "MainScatter")
                        continue;

                    Debug.WriteLine(svi.Name);

                    svi.MoveAndOrientateScatterToClose(MainScatter.ActualCenter, MainScatter.ActualOrientation);
                }
            }
            
            }

        # region Array reader

        private void Videos(string[] pathNames, ContentList content) {
            foreach (string pfad in pathNames) {
                CreateVideo(pfad, content);
            }
        }

        // Funktion zum Auslesen von Ordnern für die Ordnerdarstellung
        private void Collections(string[] paths, ContentList content) {
            if (paths != null)
                foreach (string path in paths) {
                    // Titel des Hauptordners auslesen
                    string name = new FileHandler(path).getFolderName();

                    if (Directory.Exists(path)) {
                        string[] subDirectories = Directory.GetDirectories(path, "*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string subDirectory in subDirectories)
                            CreateCollection(subDirectory, name, content);
                    }
                }
        }


        private void Documents(string[] datenPfad, ContentList content) {
            foreach (String pfad in datenPfad) {
                CreateDocument(pfad, content);
            }
        }

        private void Images(string[] paths, ContentList content) {
            foreach (string pfad in paths) {
                CreatePromotionImage(pfad, content);
            }
        }
        #endregion

        #region Create functions

        public MovableScatterViewItem CreateDocument(string path, ContentList content) {
            return content.AddElement(new DocumentControl(this.MainScatt, path, content.getBrush()));
        }

        public MovableScatterViewItem CreateDocument(string path, ContentList content, CollectionControl collectionControl, CollectionControlItemVM sLBI) {
            return content.AddElement(new DocumentControl(this.MainScatt, path, content.getBrush(), collectionControl, sLBI));
        }

        public MovableScatterViewItem CreateInformationControl(string path, ContentList content) {
            return content.AddElement(new InformationControl(this.MainScatt, path, content.getBrush()));
        }

        public MovableScatterViewItem CreateInformationControl(string path, ContentList content, CollectionControl collectionControl, CollectionControlItemVM sLBI) {
            return content.AddElement(new InformationControl(this.MainScatt, path, content.getBrush(), collectionControl, sLBI));
        }

        public MovableScatterViewItem CreatePromotionImage(string path, ContentList content) {
            return content.AddElement(new ImageControl(this.MainScatt, path, content.getBrush()));
        }

        public MovableScatterViewItem CreatePromotionImage(string path, ContentList content, CollectionControl collectionControl, CollectionControlItemVM sLBI) {
            return content.AddElement(new ImageControl(this.MainScatt, path, content.getBrush(), collectionControl, sLBI));
        }


        // Funktion für den Aufruf von neuen Collections
        public MovableScatterViewItem CreateCollection(string path, string name,ContentList content) {
            Debug.WriteLine("Hier wird die Collection: " + name + " geboren");
            return content.AddElement(new CollectionControl(this, path, name, content, content.getBrush()));
        }
        public MovableScatterViewItem CreateCollection(string path, string name, ContentList content, CollectionControl collectionControl, CollectionControlItemVM sLBI) {
            return content.AddElement(new CollectionControl(this, path, name, content, content.getBrush(), collectionControl, sLBI));
        }


        public MovableScatterViewItem CreateVideo(String path, ContentList content) {
            return content.AddElement(new VideoControl(this.MainScatt, path, content.getBrush()));
        }
        public MovableScatterViewItem CreateVideo(string path, ContentList content, CollectionControl collectionControl, CollectionControlItemVM sLBI) {
            return content.AddElement(new VideoControl(this.MainScatt, path, content.getBrush(), collectionControl, sLBI));
        }

        #endregion

        # region Remove and add functions


        public MovableScatterViewItem AddElement(MovableScatterViewItem item) {
            defaultContent.Add(item);
            return item;
        }

        public Brush getBrush() {
            return Brushes.Beige;
        }
        #endregion
    }
}