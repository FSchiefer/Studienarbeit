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
        private TagContent content;
  
        public ScatterOrientationControl() {
            InitializeComponent();
            
        }

        public void setMainscatt(ScatterView mainScatt, TagContent content) {
            this.mainScatt = mainScatt;
            this.content = content;
            
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
            ScatterMovement movement = new ScatterMovement(mainScatt);
           

                movement.ScatterItemsOrientateTo(this, content);
            


  
        }

        private void Positionierung_Click(object sender, RoutedEventArgs e) {

        }

     
    }
}
