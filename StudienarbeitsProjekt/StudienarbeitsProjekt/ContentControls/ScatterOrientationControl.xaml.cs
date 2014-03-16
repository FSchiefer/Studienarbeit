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
    public partial class ScatterOrientationControl : MovableScatterViewItem {

        private Boolean orientierung = false;
        private Boolean positionierung = false;
        private Boolean tagOrientierung = false;
        private TagContent content;

                public ScatterOrientationControl(ScatterView mainScatt, TagContent content):base(mainScatt){
            InitializeComponent();
          this.content = content;

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

           this.ScatterItemsOrientateAndMoveTo(content, true, true);
        }
        private void ScatterOrientation_TouchDown(object sender, TouchEventArgs e) {
            Console.WriteLine("Test");
            e.TouchDevice.Deactivated += new EventHandler(TouchDevice_Deactivated);
        }

        void TouchDevice_Deactivated(object sender, EventArgs e) {
            Console.WriteLine(content.Orientation);
            this.ScatterItemsOrientateAndMoveTo(content, orientierung, positionierung);

            if (tagOrientierung) {
                
                foreach (MovableScatterViewItem svi in content.Elements) {
                    if (svi.Name == "MainScatter") {
                        continue;
                    }

                    svi.ScatterOrientationAnimation(content.Orientation, TimeSpan.FromSeconds(0.5));
                }
            }

        }


    }
}




