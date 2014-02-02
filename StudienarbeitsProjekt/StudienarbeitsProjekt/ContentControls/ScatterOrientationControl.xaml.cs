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

namespace StudienarbeitsProjekt.ContentControls
{
    /// <summary>
    /// Interaktionslogik für ScatterOrientationControl.xaml
    /// </summary>
    public partial class ScatterOrientationControl : ScatterViewItem
    {
        private TagContent tagContent;

        public ScatterOrientationControl(TagContent tagContent)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.tagContent = tagContent;
        }

        private void FreieOrientierung_Checked(object sender, RoutedEventArgs e)
        {
            tagContent.root.UsesTagOrientation = false;
          
        }

        private void FreieOrientierung_Unchecked(object sender, RoutedEventArgs e)
        {
            
            tagContent.root.UsesTagOrientation = true;
         
        }

        private void Positionierung_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Positionierung_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
