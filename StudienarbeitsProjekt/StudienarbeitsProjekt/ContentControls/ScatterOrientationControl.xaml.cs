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
using Microsoft.Surface.Presentation.Controls;
using System.Windows.Media.Animation;

namespace StudienarbeitsProjekt.ContentControls
{
    /// <summary>
    /// Interaktionslogik für ScatterOrientationControl.xaml
    /// </summary>
    public partial class ScatterOrientationControl : ScatterViewItem
    {
    
        private ScatterView mainScatt;
  
        public ScatterOrientationControl() {
            InitializeComponent();
            
        }

        public void setMainscatt(ScatterView mainScatt) {
            this.mainScatt = mainScatt;
        }

        private void FreieOrientierung_Checked(object sender, RoutedEventArgs e)
        {
      

        
          
        }

        private void FreieOrientierung_Unchecked(object sender, RoutedEventArgs e)
        {
            

            
            
        }

        private void Positionierung_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Positionierung_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Orientierung_Click(object sender, RoutedEventArgs e) {
            Console.WriteLine("Orientierung geklickt");
            ScatterItemsOrientateTo();
        }

        private void ScatterItemsOrientateTo() {
            ScatterViewItem control = this;
            Console.WriteLine(control.Name);
            double x = 0, y = 0, sX = 0, sY = 0, maxHeight = 0;

            Console.WriteLine(control.ActualOrientation);
            if (control.ActualOrientation >= 315 && control.ActualOrientation < 45) {
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in mainScatt.Items) {
                    Console.WriteLine(svi.Name);
                    
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    if (Math.Round(x + svi.ActualWidth) > mainScatt.ActualWidth) {
                        x = 0;
                        y -= maxHeight;
                        maxHeight = 0;
                    }
                    maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                    sX = x + svi.ActualWidth / 2;
                    sY = y - svi.ActualHeight / 2;
                    MoveAndOrientateScatter(svi, new Point(sX, sY), 0);
                    x += svi.ActualWidth;
                }
            } else if (control.ActualOrientation >= 135 && control.ActualOrientation < 225) {
              
                x = mainScatt.ActualWidth;
                foreach (ScatterViewItem svi in mainScatt.Items) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    Console.WriteLine(svi.Name);
                    if (Math.Round(x - svi.ActualWidth) < 0) {
                        x = mainScatt.ActualWidth;
                        y += maxHeight;
                        maxHeight = 0;
                    }
                    maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                    sX = x - svi.ActualWidth / 2;
                    sY = y + svi.ActualHeight / 2;
                    MoveAndOrientateScatter(svi, new Point(sX, sY), 180);
                    x -= svi.ActualWidth;
                }
            } else if (control.ActualOrientation >= 45 && control.ActualOrientation < 135) {
             
                foreach (ScatterViewItem svi in mainScatt.Items) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    Console.WriteLine(svi.Name);
                    if (Math.Round(y + svi.ActualWidth) > mainScatt.ActualHeight) {
                        y = 0;
                        x += maxHeight;
                        maxHeight = 0;
                    }
                    maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                    sY = y + svi.ActualWidth / 2;
                    sX = x + svi.ActualHeight / 2;
                    MoveAndOrientateScatter(svi, new Point(sX, sY), 90);
                    y += svi.ActualWidth;
                }

            } else if (control.ActualOrientation >= 225 && control.ActualOrientation < 315) {
                x = mainScatt.ActualWidth;
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in mainScatt.Items) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    Console.WriteLine(svi.Name);
                    if (Math.Round(y - svi.ActualWidth) < 0) {
                        x -= maxHeight;
                        y = mainScatt.ActualHeight;
                        maxHeight = 0;
                    }
                    maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                    sY = y - svi.ActualWidth / 2;
                    sX = x - svi.ActualHeight / 2;
                    MoveAndOrientateScatter(svi, new Point(sX, sY), 270);
                    y -= svi.ActualWidth;
                }
            } else {
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in mainScatt.Items) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }
                    Console.WriteLine(svi.Name);
                    if (Math.Round(x + svi.ActualWidth) > mainScatt.ActualWidth) {
                        x = 0;
                        y -= maxHeight;
                        maxHeight = 0;
                    }
                    maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                    sX = x + svi.ActualWidth / 2;
                    sY = y - svi.ActualHeight / 2;
                    MoveAndOrientateScatter(svi, new Point(sX, sY), 0);
                    x += svi.ActualWidth;
                }
            }


        }



        private void MoveAndOrientateScatter(ScatterViewItem svi, Point moveTo, double orientation) {
            ScatterPositionAnimation(svi, moveTo, TimeSpan.FromSeconds(0.5));
            ScatterOrientationAnimation(svi, orientation, TimeSpan.FromSeconds(0.5));
        }

        private void ScatterPositionAnimation(ScatterViewItem svi, Point moveTo, TimeSpan timeSpan) {
            PointAnimation positionAnimation = new PointAnimation(svi.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
            positionAnimation.AccelerationRatio = 0.5;
            positionAnimation.DecelerationRatio = 0.5;
            positionAnimation.FillBehavior = FillBehavior.Stop;
            positionAnimation.Completed += delegate(object sender, EventArgs e) {
                svi.Center = moveTo;
            };
            svi.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
        }

        private void ScatterOrientationAnimation(ScatterViewItem svi, double orientation, TimeSpan timeSpan) {

            orientation = ((360 + orientation) % 360);
            if ((orientation - ((360 + svi.ActualOrientation) % 360)) < -180) {
                orientation += 360;
            } else if ((orientation - ((360 + svi.ActualOrientation) % 360)) > 180) {
                orientation -= 360;
            }


            DoubleAnimation orientationAnimation = new DoubleAnimation(svi.ActualOrientation, orientation, TimeSpan.FromSeconds(0.5));
            orientationAnimation.AccelerationRatio = 0.5;
            orientationAnimation.DecelerationRatio = 0.5;
            orientationAnimation.FillBehavior = FillBehavior.Stop;
            orientationAnimation.Completed += delegate(object sender, EventArgs e) {
                svi.Orientation = orientation;
            };
            svi.BeginAnimation(ScatterViewItem.OrientationProperty, orientationAnimation);
        }

        private void Positionierung_Click(object sender, RoutedEventArgs e) {

        }

     
    }
}
