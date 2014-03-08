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
    /// Interaktionslogik für ScatterOrientationControl.xaml
    /// </summary>
    public partial class ScatterOrientationControl : ScatterViewItem {

        private Boolean orientierung = false;
        private Boolean positionierung = false;
        private Boolean tagOrientierung = false;
        private ScatterMovement movement;
        private ScatterView mainScatt;
        private TagContent content;

        public ScatterOrientationControl() {
            InitializeComponent();

        }

        public void setMainscatt(ScatterView mainScatt, TagContent content) {
            this.mainScatt = mainScatt;
            this.content = content;
            movement = new ScatterMovement(mainScatt);
        }

        private void ScatterOrientierung_Checked(object sender, RoutedEventArgs e) {
            tagOrientierung = false;
            content.setTagOrientation(tagOrientierung);
            orientierung = true;


        }

        private void FreieOrientierung_Checked(object sender, RoutedEventArgs e) {

            try {
                tagOrientierung = false;
                content.setTagOrientation(tagOrientierung);
                orientierung = false;
            } catch (NullReferenceException ex){
                Console.WriteLine(ex);
            }

        }


        private void TagOrientierung_Checked(object sender, RoutedEventArgs e) {

            orientierung = false;
            tagOrientierung = true;
            content.setTagOrientation(tagOrientierung);
            


        }

        private void Positionierung_Unchecked(object sender, RoutedEventArgs e) {
            positionierung = false;

        }

        private void Positionierung_Checked(object sender, RoutedEventArgs e) {
            positionierung = true;
        }

        private void Orientierung_Click(object sender, RoutedEventArgs e) {

            movement.ScatterItemsOrientateAndMoveTo(this, content, true, true);
        }
        private void ScatterOrientation_TouchDown(object sender, TouchEventArgs e) {
            Console.WriteLine("Test");
            e.TouchDevice.Deactivated += new EventHandler(TouchDevice_Deactivated);
        }

        void TouchDevice_Deactivated(object sender, EventArgs e) {
            Console.WriteLine(content.Orientation);
            movement.ScatterItemsOrientateAndMoveTo(this, content, orientierung, positionierung);

            if (tagOrientierung) {
                
                foreach (ScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }

                    movement.ScatterOrientationAnimation(svi, content.Orientation, TimeSpan.FromSeconds(0.5));
                }
            }

        }


    }
}
