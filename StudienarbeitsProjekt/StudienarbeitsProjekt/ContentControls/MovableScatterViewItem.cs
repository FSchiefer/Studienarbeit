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

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für MovableScatterViewItem.xaml
    /// </summary>
    public class MovableScatterViewItem : ScatterViewItem {

        enum Direction { Right, Left, Top, Bottom };

        private ScatterView mainScatt;

        public MovableScatterViewItem() {
          
        }

        public MovableScatterViewItem(ScatterView mainScatt) {
            this.mainScatt = mainScatt;
            mainScatt.Items.Add(this);
        }

        #region Sort and decision functions

        // Funktion zum Anordnen der Scatter zu einem "Vater" Element
        public void ScatterItemsOrientateAndMoveTo(TagContent content, Boolean rotation, Boolean moving) {

            Console.WriteLine(this.Name);
            Console.WriteLine(this.ActualOrientation);
            double x = 0, y = 0, sX = 0, sY = 0, maxHeight = 0;

            Direction d = (this.ActualOrientation >= 315 || this.ActualOrientation < 45)
                          ? Direction.Bottom
                          : (this.ActualOrientation >= 45 && this.ActualOrientation < 135)
                            ? Direction.Left
                            : (this.ActualOrientation >= 135 && this.ActualOrientation < 225)
                              ? Direction.Top
                              : Direction.Right;     // (control.ActualOrientation >= 225 && control.ActualOrientation < 315)

            int winkel = 0;
            switch (d) { // Initialisierung
                case Direction.Bottom:
                    y = mainScatt.ActualHeight;
                    winkel = 0;
                    break;
                case Direction.Top:
                    x = mainScatt.ActualWidth;
                    winkel = 180;
                    break;
                case Direction.Right:
                    x = mainScatt.ActualWidth;
                    y = mainScatt.ActualHeight;
                    winkel = 270;
                    break;
                case Direction.Left:
                    winkel = 90;
                    break;
            }

            foreach (MovableScatterViewItem svi in content.Elements) {
                if (svi.Name == "MainScatter") {
                    continue;
                }
                Console.WriteLine(svi.Name);

                switch (d) {
                    case Direction.Bottom:
                        if (Math.Round(x + svi.ActualWidth) > mainScatt.ActualWidth) {
                            x = 0;
                            y -= maxHeight;
                            maxHeight = 0;
                        }
                        maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                        sX = x + svi.ActualWidth / 2;
                        sY = y - svi.ActualHeight / 2;
                        x += svi.ActualWidth;

                        break;
                    case Direction.Top:
                        if (Math.Round(x - svi.ActualWidth) < 0) {
                            x = mainScatt.ActualWidth;
                            y += maxHeight;
                            maxHeight = 0;
                        }
                        maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                        sX = x - svi.ActualWidth / 2;
                        sY = y + svi.ActualHeight / 2;
                        x -= svi.ActualWidth;
                        break;
                    case Direction.Right:
                        if (Math.Round(y - svi.ActualWidth) < 0) {
                            x -= maxHeight;
                            y = mainScatt.ActualHeight;
                            maxHeight = 0;
                        }
                        maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                        sY = y - svi.ActualWidth / 2;
                        sX = x - svi.ActualHeight / 2;
                        y -= svi.ActualWidth;
                        break;
                    case Direction.Left:
                        if (Math.Round(y + svi.ActualWidth) > mainScatt.ActualHeight) {
                            y = 0;
                            x += maxHeight;
                            maxHeight = 0;
                        }
                        maxHeight = Math.Max(maxHeight, svi.ActualHeight);
                        sY = y + svi.ActualWidth / 2;
                        sX = x + svi.ActualHeight / 2;
                        y += svi.ActualWidth;
                        break;
                }

                svi.entscheider(new Point(sX, sY), (moving ? winkel : (int)this.ActualOrientation), rotation, moving);
            }
        }


        private void entscheider(Point point, int orientation, bool rotation, bool moving) {
            if (rotation && moving) {
                MoveAndOrientateScatter(point, orientation);
            } else if (!rotation && moving) {
                MoveToPosition(point, TimeSpan.FromSeconds(0.5));
            } else if (!moving && rotation) {
                ScatterOrientationAnimation(orientation, TimeSpan.FromSeconds(0.5));
            }
        }

        #endregion

        #region Move animations

        public void MoveAndOrientateScatter(Point moveTo, double orientation) {
            MoveToPosition(moveTo, TimeSpan.FromSeconds(0.5));
            ScatterOrientationAnimation(orientation, TimeSpan.FromSeconds(0.5));
        }

        public void MoveToPosition(Point moveTo, TimeSpan timeSpan) {
            PointAnimation positionAnimation = new PointAnimation(this.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
            positionAnimation.AccelerationRatio = 0.5;
            positionAnimation.DecelerationRatio = 0.5;
            positionAnimation.FillBehavior = FillBehavior.Stop;
            positionAnimation.Completed += delegate(object sender, EventArgs e) {
                this.Center = moveTo;
            };
            this.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
        }

        public void ScatterOrientationAnimation(double orientation, TimeSpan timeSpan) {

            orientation = ((360 + orientation) % 360);
            if ((orientation - ((360 + this.ActualOrientation) % 360)) < -180) {
                orientation += 360;
            } else if ((orientation - ((360 + this.ActualOrientation) % 360)) > 180) {
                orientation -= 360;
            }

            DoubleAnimation orientationAnimation = new DoubleAnimation(this.ActualOrientation, orientation, TimeSpan.FromSeconds(0.5));
            orientationAnimation.AccelerationRatio = 0.5;
            orientationAnimation.DecelerationRatio = 0.5;
            orientationAnimation.FillBehavior = FillBehavior.Stop;
            orientationAnimation.Completed += delegate(object sender, EventArgs e) {
                this.Orientation = orientation;
            };
            this.BeginAnimation(ScatterViewItem.OrientationProperty, orientationAnimation);
        }
        #endregion

        #region Close functions
        public void MoveAndOrientateScatterToClose(Point moveTo, double orientation) {
            ScatterPositionAnimationToClose(moveTo, TimeSpan.FromSeconds(0.5));
            ScatterOrientationAnimation(orientation, TimeSpan.FromSeconds(0.5));
        }

        private void ScatterPositionAnimationToClose(Point moveTo, TimeSpan timeSpan) {
            if (moveTo != null && this.ActualCenter != null) {
                PointAnimation positionAnimation = new PointAnimation(this.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
                positionAnimation.AccelerationRatio = 0.5;
                positionAnimation.DecelerationRatio = 0.5;
                positionAnimation.FillBehavior = FillBehavior.Stop;
                positionAnimation.Completed += delegate(object sender, EventArgs e) {
                    this.Center = moveTo;
                };
                positionAnimation.Completed += new EventHandler((sender, e) => scatterAnimationCompleted(sender, e));
                this.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
            }
        }

        private void scatterAnimationCompleted(object sender, EventArgs e) {

            if (this.Name != "MainScatter") {
                removeScatterViewItem();
            }
        }

        private void removeScatterViewItem() {

            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            fadeOut.FillBehavior = FillBehavior.Stop;
            fadeOut.AccelerationRatio = 0.5;
            fadeOut.DecelerationRatio = 0.5;
            fadeOut.Completed += delegate(object sender, EventArgs e) {
                this.Opacity = 1;
                mainScatt.Items.Remove(this);
            };
            this.BeginAnimation(ScatterViewItem.OpacityProperty, fadeOut);
        }
        #endregion



    }
}

