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
using System.Windows.Xps.Packaging;
using System.IO;

namespace StudienarbeitsProjekt.ContentControls
{
    /// <summary>
    /// Interaktionslogik für DocumentControl.xaml
    /// </summary>
    public partial class DocumentControl : ScatterViewItem
    {
        public DocumentControl()
        {

            InitializeComponent();

            XpsDocument xpsDoc = new XpsDocument(@"C:\Studiengaenge\test.xps" ,
                                       FileAccess.Read);
            
            myDocViewer.Document = xpsDoc.GetFixedDocumentSequence();
            test.Width = myDocViewer.Width;
            test.Height = myDocViewer.Height;
            xpsDoc.Close();
        }
        private void docViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myDocViewer.FitToWidth();
        }
    }
}
