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

namespace StudienarbeitsProjekt {
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow {

        private int TouchesOnMainScatter = 0;
        private ObservableCollection<object> elements = new ObservableCollection<object>();
        private ScatterMovement move;
        private Queue<Color> userColors = new Queue<Color>();
        private List<TagVisualization> pending = new List<TagVisualization>();
        private DocumentControl standardPresentation;
        private ImageControl standardMotivation;
        public ObservableCollection<object> Elements { get { return elements; } }

        #region generated Code

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1() {
            InitializeComponent();

            move = new ScatterMovement(MainScatt);
            MainScatterImage.ImageSource = new BitmapImage(new Uri(FileHandler.getMainscatterImage(), UriKind.Relative));
            MainScatt.Items.Add(standardPresentation);
            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            userColors.Enqueue(Colors.Red);
            userColors.Enqueue(Colors.Blue);
            //userColors.Enqueue(Colors.Green);
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
                foreach (ScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter")
                        continue;

                    Debug.WriteLine(svi.Name);

                    move.MoveAndOrientateScatterToClose(svi, e.TagVisualization.Center, e.TagVisualization.Orientation);
                }
            }

            if (userColors.Count == 2) {
                generateDefaultContent();
            }

        }


        private void StartVisualizer_VisualizationInitialized(object sender, TagVisualizerEventArgs e) {
            TagContent content = (TagContent)e.TagVisualization;

            if (userColors.Count > 0) {
                MainScatt.Items.Remove(standardPresentation);
                MainScatt.Items.Remove(standardMotivation);
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
            move.MoveToPosition(MainScatter, moveTo, TimeSpan.FromSeconds(0.5));
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
            standardPresentation = new DocumentControl(FileHandler.getDefaulViewPresentation(), Brushes.Beige);
        standardMotivation = new ImageControl(FileHandler.getMotivation(), Brushes.Beige);

        MainScatt.Items.Add(standardMotivation);
        MainScatt.Items.Add(standardPresentation);

        }
    }
}