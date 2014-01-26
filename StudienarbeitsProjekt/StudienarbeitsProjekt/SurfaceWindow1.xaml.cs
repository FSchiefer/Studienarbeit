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

namespace StudienarbeitsProjekt
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {

        private int TouchesOnMainScatter = 0;
        private ObservableCollection<object> elements = new ObservableCollection<object>();
        public ObservableCollection<object> Elements { get { return elements; } }
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();


            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
          
           
             
            startVisualizer.VisualizationInitialized += 
                new TagVisualizerEventHandler (StartVisualizer_VisualizationInitialized);
        }


        void StartVisualizer_VisualizationInitialized(object sender, TagVisualizerEventArgs e)
        {
            TagContent content = e.TagVisualization as TagContent;
         
            if (content != null)
            {

                content.ShowTagContent(MainScatt);
               
                //for (int i = 0; i < content.Elements.Count; i++)
                //{
                //    Elements.Add(content.Elements[i]);
                //}
                // Console.WriteLine(Elements.Count);
            }
        }

        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
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
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }

        private void MainScatter_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            double s = Math.Sqrt(Math.Pow(MainScatter.ActualWidth, 2) / 2);
            MainContentGrid.Width = s;
            MainContentGrid.Height = s;
        }

        private void MainScatter_ContainerManipulationCompleted(object sender, ContainerManipulationCompletedEventArgs e)
        {
            Point moveTo = new Point(Host.ActualWidth / 2, Host.ActualHeight / 2);
            ScatterPositionAnimation(MainScatter, moveTo, TimeSpan.FromSeconds(0.5));
        }


        private void MainScatter_TouchDown(object sender, TouchEventArgs e)
        {
            e.TouchDevice.Deactivated += new EventHandler(TouchDevice_Deactivated);
            if (++TouchesOnMainScatter >= 2)
            {
                MainScatter.CanMove = true;
            }
        }

        void TouchDevice_Deactivated(object sender, EventArgs e)
        {
            if (--TouchesOnMainScatter < 2)
            {
                MainScatter.CanMove = false;
            }
        }

        private void ScatterPositionAnimation(ScatterViewItem svi, Point moveTo, TimeSpan timeSpan)
        {
            PointAnimation positionAnimation = new PointAnimation(svi.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
            positionAnimation.AccelerationRatio = 0.5;
            positionAnimation.DecelerationRatio = 0.5;
            positionAnimation.FillBehavior = FillBehavior.Stop;
            positionAnimation.Completed += delegate(object sender, EventArgs e)
            {
                svi.Center = moveTo;
            };
            svi.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
        }

     

      


        public RoutedEventHandler OnLostTag { get; set; }
    }
}