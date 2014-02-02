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
    /// Interaktionslogik für CollectionControl.xaml
    /// </summary>
    ///
    public partial class CollectionControl : ScatterViewItem
    {
        private SurfaceListBoxItem child;
        private TagContent tagContent;
        private List<String> fileList = new List<String>();
        private Dictionary<SurfaceButton, ScatterViewItem> scatterList = new Dictionary<SurfaceButton, ScatterViewItem>();



        public CollectionControl()
        {
            InitializeComponent();
        }

        public CollectionControl(String dataPath,String name ,TagContent tagContent)
        {
            InitializeComponent();
            this.tagContent = tagContent;
            String title = name +": "+ getFolderName(dataPath);
            Title.Content = title;
            fileTable(dataPath);
        }



        private void fileTable(string dataPath)
        {
            String[] filePath = Directory.GetFiles(dataPath);
            foreach (String path in filePath)
            {
                String type = fileType(path);
                if (type == "jpg" || type == "xps" || type == "wmv")
                {
                    String name = titleViewer(path);
                    SurfaceButton contentViewer = new SurfaceButton();
                    contentViewer.Content = name;
                    contentViewer.Name = name;

                    fileList.Add(path);
                    contentViewer.Click += new RoutedEventHandler(Child_Click);

                    child = new SurfaceListBoxItem();
                    child.Content = contentViewer;
                    contentViewer.Width = child.Width;
                    contentViewer.Height = child.Height;


                    contentNames.Items.Add(child);
                }



            }
        }


        private void Child_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as SurfaceButton;
            String buttonName = button.Name;

            if (button.Background == Brushes.Red)
            {
                button.Background = Brushes.Gray;
                ScatterViewItem item = scatterList[button];
                scatterList.Remove(button);
                tagContent.Remove(item);
            }
            else
            {
                button.Background = Brushes.Red;

                foreach (String file in fileList)
                {

                    if (file.Contains(buttonName))
                    {
                        Console.WriteLine(buttonName);
                        String type = fileType(file);

                        if (type == "jpg")
                        {
                            scatterList.Add(button, tagContent.createPromotionImage(file));
                        }
                        else if (type == "xps")
                        {
                            scatterList.Add(button, tagContent.createDocument(file));
                        
                           
                        }
                        else if (type == "wmv")
                        {
                         
                            scatterList.Add(button, tagContent.createVideo(file));
                        }
                    }
                }
            }

        }



        private String getFolderName(String dokumentPfad)
        {
            // Ausgabe des Ordnernamens des Dokuments
            int beginDirectoryName = dokumentPfad.LastIndexOf('\\') + 1;
            String name = dokumentPfad.Substring(beginDirectoryName, dokumentPfad.Length - beginDirectoryName);

            return name;


        }
        private String titleViewer(String dokumentPfad)
        {
            // Ausgabe des Dateinamens des Dokuments
            int beginFileName = dokumentPfad.LastIndexOf('\\') + 1;
            String name = dokumentPfad.Substring(beginFileName, dokumentPfad.LastIndexOf('.') - beginFileName);
            return name;

        }

        private String fileType(String dokumentPfad)
        {
            // Ausgabe des Dateityps einer Datei

            String name = dokumentPfad.Substring(dokumentPfad.LastIndexOf('.') + 1, dokumentPfad.Length - dokumentPfad.LastIndexOf('.') - 1);
            return name;

        }
    }
}
