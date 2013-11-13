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
using System.Globalization;

namespace StudienarbeitsProjekt
{
    /// <summary>
    /// Interaction logic for TagContent.xaml
    /// </summary>
    public partial class TagContent : TagVisualization
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public TagContent()
        {
            InitializeComponent();

     
        }

        public void ShowTagContent()
        {

        
            string tagVal = GetTagValue();
            if (tagVal == "1")
            {
                PDFView view = new PDFView("sample.pdf");
                erstScatter.Content = view;
                ZweitFeld.Content = "Ja verdammt";

            }
            if (tagVal == "2")
            
                Acrobat.AcroAppClass test = new Acrobat.AcroAppClass();
                erstScatter.Background = Brushes.BlanchedAlmond;
                ZweitScatter.Background = System.Windows.Media.Brushes.Yellow;
            }


        }

        /// <returns></returns>
        private string GetTagValue()
        {
            string tagVal = string.Empty;
            if (this.VisualizedTag.Value > 0)
            {
                tagVal = this.VisualizedTag.Value.ToString("X", CultureInfo.InvariantCulture);
            }
            else if (this.VisualizedTag.Series > 0)
            {
                tagVal = this.VisualizedTag.Series.ToString("X", CultureInfo.InvariantCulture);
            }
            return tagVal;
        }
        
    }
}