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
        private Queue<Brush> userColors = new Queue<Brush>();

        public ObservableCollection<object> Elements { get { return elements; } }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1() {
            InitializeComponent();

            move = new ScatterMovement(MainScatt);

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            userColors.Enqueue(Brushes.Red);
            userColors.Enqueue(Brushes.Blue);
            userColors.Enqueue(Brushes.Green);
            userColors.Enqueue(Brushes.Yellow);


            startVisualizer.VisualizationRemoved += new TagVisualizerEventHandler(startVisualizer_VisualizationRemoved);
            startVisualizer.VisualizationInitialized +=
                new TagVisualizerEventHandler(StartVisualizer_VisualizationInitialized);
        }


        void startVisualizer_VisualizationRemoved(object sender, TagVisualizerEventArgs e) {
            TagContent content = e.TagVisualization as TagContent;

            foreach (ScatterViewItem svi in content.Elements) {
                if (svi.Name == "MainScatter") {
                    continue;
                }
                Console.WriteLine(svi.Name);


                if (!userColors.Contains(Brushes.Red) && svi.BorderBrush == Brushes.Red) {

                    userColors.Enqueue(Brushes.Red);


                } else if (!userColors.Contains(Brushes.Green) && svi.BorderBrush == Brushes.Green) {

                    userColors.Enqueue(Brushes.Green);


                } else if (!userColors.Contains(Brushes.Yellow) && svi.BorderBrush == Brushes.Yellow) {
                    userColors.Enqueue(Brushes.Yellow);

                } else if (!userColors.Contains(Brushes.Blue) && svi.BorderBrush == Brushes.Blue) {
                    userColors.Enqueue(Brushes.Blue);

                }


                move.MoveAndOrientateScatterToClose(svi, MainScatter.ActualCenter, MainScatter.ActualOrientation);
            }




        }



        void StartVisualizer_VisualizationInitialized(object sender, TagVisualizerEventArgs e) {
            TagContent content = e.TagVisualization as TagContent;


            if (content != null && userColors.Count > 0) {
                Brush color = userColors.Dequeue();
                ObservableCollection<object> tagElements = content.ShowTagContent(this, color);

                foreach (ScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    Console.WriteLine(svi.Name);

                     if (svi.BorderBrush == Brushes.Red) {

                
                content.Circle.Color = Colors.Red ;
                

                } else if (svi.BorderBrush == Brushes.Green) {

                    
                content.Circle.Color = Colors.Green ;
               

                } else if ( svi.BorderBrush == Brushes.Yellow) {
              
                content.Circle.Color = Colors.Yellow ;
                

                } else if (svi.BorderBrush == Brushes.Blue) {

                content.Circle.Color = Colors.Blue ;
               
                }
                } 



            }else if (userColors.Count.Equals(0)){
                content.Message.Content = "Bitte warten bis ein anderer Tag abgehoben wird";
                content.Message.Foreground = Brushes.Red;
                content.Message.Background = Brushes.White;
                }
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

        private void MainScatter_SizeChanged(object sender, SizeChangedEventArgs e) {

            double s = Math.Sqrt(Math.Pow(MainScatter.ActualWidth, 2) / 2);
            MainContentGrid.Width = s;
            MainContentGrid.Height = s;
        }

        private void MainScatter_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e) {
            Point moveTo = new Point(Host.ActualWidth / 2, Host.ActualHeight / 2);
            move.ScatterPositionAnimation(MainScatter, moveTo, TimeSpan.FromSeconds(0.5));
        }


        private void MainScatter_TouchDown(object sender, TouchEventArgs e) {
            e.TouchDevice.Deactivated += new EventHandler(TouchDevice_Deactivated);
            if (++TouchesOnMainScatter >= 2) {
                MainScatter.CanMove = true;
            }
        }

        void TouchDevice_Deactivated(object sender, EventArgs e) {

            if (--TouchesOnMainScatter < 2) {
                MainScatter.CanMove = false;
            }
        }

         }
}