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

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für VideoControl.xaml
    /// </summary>
    public partial class ImageControl : ScatterViewItem {

        


        public ImageControl( string imagePosition, Brush color ) {

            InitializeComponent();

   
      Image promotionBild = new Image() { Source = new BitmapImage(new Uri(imagePosition, UriKind.Absolute)) };
            this.BorderBrush = color;
            this.Content = promotionBild;
        
            


            }

    }
}
