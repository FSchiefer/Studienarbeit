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

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für DocumentControl.xaml
    /// </summary>
    public partial class DocumentControl : MovableScatterViewItem {

        private FileHandler handler;
        private ScatterView mainScatt;
        private CollectionControl closeControl;
        private CollectionControlItemVM sLBI;



        public DocumentControl(ScatterView mainScatt, string dokumentPfad, Brush color)
            : base(mainScatt) {
                DefaultAction(mainScatt, dokumentPfad,color);
        }

        public DocumentControl(ScatterView mainScatt, string dokumentPfad, Brush color, CollectionControl closeControl,CollectionControlItemVM sLBI)
            : base(mainScatt) {
            DefaultAction(mainScatt, dokumentPfad, color);
            this.sLBI = sLBI;
            this.closeControl = closeControl;
            Close.Visibility= Visibility.Visible;
            Close.Click += Close_Click;
        }


        public void Close_Click(object sender, RoutedEventArgs e) {
            closeControl.contentNames.SelectedItems.Remove(sLBI);
       
        }


        private void DefaultAction (ScatterView mainScatt, string dokumentPfad, Brush color){
             InitializeComponent();
            this.mainScatt = mainScatt;
            handler = new FileHandler(dokumentPfad);
            this.BorderBrush = color;
            myDocViewer.BorderBrush = color;
        
            // nur mit "Speichern unter" erzeugte XPS Dokumente können verwendet werden.
            XpsDocument xpsDoc = new XpsDocument(dokumentPfad,
                                       FileAccess.Read);
           
            Title.Content = handler.titleViewer();
            myDocViewer.Document = xpsDoc.GetFixedDocumentSequence();
            xpsDoc.Close();
        }

        private void docViewer_SizeChanged(object sender, SizeChangedEventArgs e) {
            myDocViewer.FitToWidth();
        }

 

  
    }
}
