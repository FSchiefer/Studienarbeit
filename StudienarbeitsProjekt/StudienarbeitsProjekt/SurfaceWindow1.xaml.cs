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
using System.Windows.Media.Animation;
using System.Collections.ObjectModel;
using System.Collections;

namespace StudienarbeitsProjekt {
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow {


        private int TouchesOnMainScatter = 0;
        private ObservableCollection<object> elements = new ObservableCollection<object>();
        private ScatterMovement move;
        private Queue<Color> userColors = new Queue<Color>();

        public ObservableCollection<object> Elements { get { return elements; } }

        #region generated Code

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1() {
            InitializeComponent();
            move = new ScatterMovement( MainScatt );
            MainScatterImage.ImageSource = new BitmapImage(new Uri(FileHandler.getMainscatterImage(),UriKind.Relative));
            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            userColors.Enqueue( Colors.Red );
            userColors.Enqueue( Colors.Blue );
            userColors.Enqueue( Colors.Green );
            userColors.Enqueue( Colors.Yellow );
            startVisualizer.VisualizationRemoved += new TagVisualizerEventHandler( startVisualizer_VisualizationRemoved );
            startVisualizer.VisualizationInitialized +=
                new TagVisualizerEventHandler( StartVisualizer_VisualizationInitialized );
        }




        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed( EventArgs e ) {
            base.OnClosed( e );

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
        private void OnWindowInteractive( object sender, EventArgs e ) {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive( object sender, EventArgs e ) {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable( object sender, EventArgs e ) {
            //TODO: disable audio, animations here
        }

        #endregion


        #region events

        private void MainScatter_SizeChanged( object sender, SizeChangedEventArgs e ) {

            double s = Math.Sqrt( Math.Pow( MainScatter.ActualWidth, 2 ) / 2 );
            MainContentGrid.Width = s;
            MainContentGrid.Height = s;
        }


        void startVisualizer_VisualizationRemoved( object sender, TagVisualizerEventArgs e ) {
            TagContent content = e.TagVisualization as TagContent;

            userColors.Enqueue( content.Circle.Color );
            foreach (ScatterViewItem svi in content.Elements) {
                if (svi.Name == "MainScatter") {
                    continue;
                }
                Console.WriteLine( svi.Name );
                move.MoveAndOrientateScatterToClose( svi, e.TagVisualization.Center, e.TagVisualization.Orientation );
            }
        }


        void StartVisualizer_VisualizationInitialized( object sender, TagVisualizerEventArgs e ) {
            TagContent content = e.TagVisualization as TagContent;

            if (content != null && userColors.Count > 0) {
                Color color = userColors.Dequeue();
                Brush brush = new SolidColorBrush( color );
                ObservableCollection<object> tagElements = content.ShowTagContent( this, brush );

                content.Circle.Color = color;
                
            } else if (userColors.Count.Equals( 0 )) {
                content.Message.Content = "Bitte warten bis ein anderer Tag abgehoben wird";
                content.Message.Foreground = Brushes.Red;
                content.Message.Background = Brushes.White;
            }
        }


        private void MainScatter_ContainerManipulationCompleted( object sender, ContainerManipulationCompletedEventArgs e ) {
            Point moveTo = new Point( Host.ActualWidth / 2, Host.ActualHeight / 2 );
            move.ScatterPositionAnimation( MainScatter, moveTo, TimeSpan.FromSeconds( 0.5 ) );
        }


        private void MainScatter_TouchDown( object sender, TouchEventArgs e ) {
            e.TouchDevice.Deactivated += new EventHandler( TouchDevice_Deactivated );
            if (++TouchesOnMainScatter >= 2) {
                MainScatter.CanMove = true;
            }
        }

        void TouchDevice_Deactivated( object sender, EventArgs e ) {

            if (--TouchesOnMainScatter < 2) {
                MainScatter.CanMove = false;
            }
        }
        #endregion
    }
}