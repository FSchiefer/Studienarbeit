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

            
        }

        public DocumentControl(string dokumentPfad, Brush color)
        {

            InitializeComponent();
            this.BorderBrush = color;
            // nur mit "Speichern unter" erzeugte XPS Dokumente können verwendet werden.
            XpsDocument xpsDoc = new XpsDocument(dokumentPfad,
                                       FileAccess.Read);
            titleViewer(dokumentPfad);
            myDocViewer.Document = xpsDoc.GetFixedDocumentSequence();
            xpsDoc.Close();
        }


        private void docViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myDocViewer.FitToWidth();
        }

        // Funktion um den Namen des Dokuments auszulesen
        private void titleViewer(string dokumentPfad)
        {
            // Ausgabe des Dateinamens des Dokuments
            int beginFileName = dokumentPfad.LastIndexOf('\\') + 1;
            string name = dokumentPfad.Substring(beginFileName, dokumentPfad.LastIndexOf('.') - beginFileName );
            Title.Content = name;
 
         
        }

    }
}
