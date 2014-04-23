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
    /// 

    public partial class ImageControl : MovableScatterViewItem {

        private CollectionControl closeControl;
        private CollectionControlItemVM sLBI;
        private FileHandler handler;

        public ImageControl(ScatterView mainScatt, string imagePosition, Brush color, CollectionControl closeControl, CollectionControlItemVM sLBI)
            : base(mainScatt) {
            DefaultAction(mainScatt, imagePosition, color);
            this.sLBI = sLBI;
            this.closeControl = closeControl;
            Close.Visibility = Visibility.Visible;
            Close.Click += Close_Click;
        }


        public void Close_Click(object sender, RoutedEventArgs e) {
            closeControl.contentNames.SelectedItems.Remove(sLBI);

        }

        public ImageControl(ScatterView mainScatt, string imagePosition, Brush color)
            : base(mainScatt) {
            DefaultAction(mainScatt, imagePosition, color);
        }

        private void DefaultAction(ScatterView mainScatt, string imagePosition, Brush color) {


            InitializeComponent();
            handler = new FileHandler(imagePosition);
            Title.Content = handler.titleViewer();
            this.BorderBrush = color;
            Picture.Source = new BitmapImage(new Uri(imagePosition, UriKind.Absolute));
           
        }
    }
}
