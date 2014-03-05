﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using Microsoft.Surface.Presentation.Controls;
using System.Windows;

namespace StudienarbeitsProjekt {
    class ScatterMovement {
        private ScatterView mainScatt;
        public ScatterMovement(ScatterView mainScatt) {
            this.mainScatt = mainScatt;
        }

        // Funktion zum Anordnen der Scatter zu einem "Vater" Element
        public void ScatterItemsOrientateAndMoveTo(ScatterViewItem control, TagContent content, Boolean rotation,Boolean moving) {

            Console.WriteLine(control.Name);
            double x = 0, y = 0, sX = 0, sY = 0, maxHeight = 0;
            Console.WriteLine(control.ActualOrientation);
            if (control.ActualOrientation >= 315 && control.ActualOrientation < 45) {
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in content.Elements) {
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
                    if (moving) {
                        entscheider(svi, new Point(sX, sY), 0, rotation, moving);
                    } else {
                        entscheider(svi, new Point(sX, sY), (int) control.ActualOrientation, rotation, moving);
                    }
                    x += svi.ActualWidth;
                }
            } else if (control.ActualOrientation >= 135 && control.ActualOrientation < 225) {

                x = mainScatt.ActualWidth;
                foreach (ScatterViewItem svi in content.Elements) {
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
                    if (moving) {
                        entscheider(svi, new Point(sX, sY), 180, rotation, moving);
                    } else {
                        entscheider(svi, new Point(sX, sY), (int)control.ActualOrientation, rotation, moving);
                    }
                    x -= svi.ActualWidth;
                }
            } else if (control.ActualOrientation >= 45 && control.ActualOrientation < 135) {

                foreach (ScatterViewItem svi in content.Elements) {
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
                    if (moving) {
                        entscheider(svi, new Point(sX, sY), 90, rotation, moving);
                    } else {
                        entscheider(svi, new Point(sX, sY), (int)control.ActualOrientation, rotation, moving);
                    }
                    y += svi.ActualWidth;
                }

            } else if (control.ActualOrientation >= 225 && control.ActualOrientation < 315) {
                x = mainScatt.ActualWidth;
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in content.Elements) {
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
                    if (moving) {
                        entscheider(svi, new Point(sX, sY), 270, rotation, moving);
                    } else {
                        entscheider(svi, new Point(sX, sY), (int)control.ActualOrientation, rotation, moving);
                    }
                    y -= svi.ActualWidth;
                }
            } else {
                y = mainScatt.ActualHeight;
                foreach (ScatterViewItem svi in content.Elements) {
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
                    if (moving) {
                        entscheider(svi, new Point(sX, sY), 0, rotation, moving);
                    } else {
                        entscheider(svi, new Point(sX, sY), (int)control.ActualOrientation, rotation, moving);
                    }
                    x += svi.ActualWidth;
                }
            }


        }

        private void entscheider(ScatterViewItem svi, Point point, int orientation, bool rotation, bool moving) {
              if (rotation && moving) {
                        MoveAndOrientateScatter(svi, point, orientation);
                    } else if (!rotation && moving) {
                        ScatterPositionAnimation(svi, point, TimeSpan.FromSeconds(0.5));
              } else if (!moving && rotation) {
                  ScatterOrientationAnimation(svi,orientation,TimeSpan.FromSeconds(0.5));
              }
        }

        public void MoveAndOrientateScatter(ScatterViewItem svi, Point moveTo, double orientation) {
            ScatterPositionAnimation(svi, moveTo, TimeSpan.FromSeconds(0.5));
            ScatterOrientationAnimation(svi, orientation, TimeSpan.FromSeconds(0.5));
        }

        public void ScatterPositionAnimation(ScatterViewItem svi, Point moveTo, TimeSpan timeSpan) {
            PointAnimation positionAnimation = new PointAnimation(svi.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
            positionAnimation.AccelerationRatio = 0.5;
            positionAnimation.DecelerationRatio = 0.5;
            positionAnimation.FillBehavior = FillBehavior.Stop;
            positionAnimation.Completed += delegate(object sender, EventArgs e) {
                svi.Center = moveTo;
            };
            svi.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
        }

        public void ScatterOrientationAnimation(ScatterViewItem svi, double orientation, TimeSpan timeSpan) {

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

        public void MoveAndOrientateScatterToClose(ScatterViewItem svi, Point moveTo, double orientation) {
            ScatterPositionAnimationToClose(svi, moveTo, TimeSpan.FromSeconds(0.5));
            ScatterOrientationAnimation(svi, orientation, TimeSpan.FromSeconds(0.5));
        }

        private void ScatterPositionAnimationToClose(ScatterViewItem svi, Point moveTo, TimeSpan timeSpan) {
            PointAnimation positionAnimation = new PointAnimation(svi.ActualCenter, moveTo, TimeSpan.FromSeconds(0.5));
            positionAnimation.AccelerationRatio = 0.5;
            positionAnimation.DecelerationRatio = 0.5;
            positionAnimation.FillBehavior = FillBehavior.Stop;
            positionAnimation.Completed += delegate(object sender, EventArgs e) {
                svi.Center = moveTo;
            };
            positionAnimation.Completed += new EventHandler((sender, e) => scatterAnimationCompleted(sender, e, svi));
            svi.BeginAnimation(ScatterViewItem.CenterProperty, positionAnimation);
        }

        private void scatterAnimationCompleted(object sender, EventArgs e, ScatterViewItem svi) {

            if (svi.Name != "MainScatter") {
                removeScatterViewItem(svi);
            }


        }

        public void removeScatterViewItem(ScatterViewItem svi) {

            DoubleAnimation fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            fadeOut.FillBehavior = FillBehavior.Stop;
            fadeOut.AccelerationRatio = 0.5;
            fadeOut.DecelerationRatio = 0.5;
            fadeOut.Completed += delegate(object sender, EventArgs e) {
                svi.Opacity = 1;
                mainScatt.Items.Remove(svi);
            };
            svi.BeginAnimation(ScatterViewItem.OpacityProperty, fadeOut);
        }

   
    }
}