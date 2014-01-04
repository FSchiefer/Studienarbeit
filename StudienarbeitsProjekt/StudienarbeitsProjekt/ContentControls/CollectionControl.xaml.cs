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
        private TreeViewItem child;
        private TreeViewItem contentChild;



        public CollectionControl()
        {
            InitializeComponent();
        }

        public CollectionControl(string dataPath)
        {
            InitializeComponent();
            //categoryChooser(dataPath);
        }

        private void categoryChooser(string dataPath)
        {
            String[] folderPfad = Directory.GetDirectories(dataPath, "*", System.IO.SearchOption.TopDirectoryOnly);
            for (int i = 0; i < folderPfad.Length; i++)
            {
               
                String name = getFolderName(folderPfad[i]);
                fillChild(name, folderPfad[i]);
               
                

                
            }
        }


        private void fillChild(string name,string dataPath)
        {
            child = new TreeViewItem();
            child.Header = name;
            Contentbaum.Items.Add(child);
            try
            {
                if (Directory.Exists(dataPath))
                {
                   String[] picturePath = Directory.GetFiles(dataPath, "*.jpg");
                   foreach (string path in picturePath)
                   {
                       contentChild = new TreeViewItem();
                       contentChild.Header = titleViewer(path);
                       child.Items.Add(contentChild);
                   }

               
                   String[] documentPath = Directory.GetFiles(dataPath, "*.xps");
                   foreach (string path in documentPath)
                   {
                       contentChild = new TreeViewItem();
                       contentChild.Header = titleViewer(path);
                       child.Items.Add(contentChild);
                   }

                   String[] videoPath = Directory.GetFiles(dataPath, "*.wmv");
                    foreach (string path in videoPath)
                    {
                        contentChild = new TreeViewItem();
                        contentChild.Header = titleViewer(path);
                        child.Items.Add(contentChild);
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("No Folder" + ex);
            }

        }

        private string getFolderName(string dokumentPfad)
        {
            // Ausgabe des Ordnernamens des Dokuments
            int beginDirectoryName = dokumentPfad.LastIndexOf('\\') + 1;
            string name = dokumentPfad.Substring(beginDirectoryName, dokumentPfad.Length - beginDirectoryName);

            return name;


        }
        private string titleViewer(string dokumentPfad)
        {
            // Ausgabe des Dateinamens des Dokuments
            int beginFileName = dokumentPfad.LastIndexOf('\\') + 1;
            string name = dokumentPfad.Substring(beginFileName, dokumentPfad.LastIndexOf('.') - beginFileName);
            return name;

        }
    }
}
