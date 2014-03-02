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
using System.Windows.Media.Animation;

namespace StudienarbeitsProjekt.ContentControls {

    /// <summary>
    /// Interaktionslogik für CollectionControl.xaml
    /// </summary>
    ///
    public partial class CollectionControl : ScatterViewItem {
        private SurfaceListBoxItem child;
        private TagContent tagContent;
        private List<String> fileList = new List<String>();
        private List<String> folderList = new List<String>();
        private Dictionary<SurfaceListBoxItem, ScatterViewItem> scatterList = new Dictionary<SurfaceListBoxItem, ScatterViewItem>();
        private Dictionary<SurfaceListBoxItem, ScatterViewItem> collectionList = new Dictionary<SurfaceListBoxItem, ScatterViewItem>();
        private List<String> viewSources = new List<String>();
        private String title;
        private ScatterMovement move;

        // TODO Icons für Dateiarten einbauen

        public CollectionControl() {
            InitializeComponent();
        }

        // Konstruktor zum erstellen der Komponente und dem Festlegen des darzustellenden Titels
        public CollectionControl(String dataPath, String name, TagContent tagContent) {
            InitializeComponent();
            move = new ScatterMovement(tagContent.mainScatt);
            this.tagContent = tagContent;
            if (name.Contains(":")) {
                    name = name.Substring(0, name.LastIndexOf(':') - 0);
                    title = name + ": " + getFolderName(dataPath);
            } else {
                title = name + ": " + getFolderName(dataPath);
            }
            Title.Content = title;
            fileTable(dataPath);
        }
        private void fileTable(string dataPath) {
            String[] filePath = Directory.GetFiles(dataPath);
            String[] folderPath = Directory.GetDirectories(dataPath);
            foreach (String path in filePath) {
                String type = fileType(path);
                if (type == "jpg" || type == "xps" || type == "wmv") {

                    fileList.Add(path);
                    child = new SurfaceListBoxItem();
                    child.Content = titleViewer(path);
                    child.Tag = path;

                    contentNames.Items.Add(child);
                }
            }
            foreach (String path in folderPath) {

                folderList.Add(path);


                child = new SurfaceListBoxItem();
                child.Content = getFolderName(path);
                child.Tag = path;

                contentNames.Items.Add(child);

            }
            contentNames.MaxHeight = contentNames.Items.Count * 55; 
            collectionControl.MaxHeight = contentNames.MaxHeight + 25;
        }




        private String getFolderName(String dokumentPfad) {
            // Ausgabe des Ordnernamens des Dokuments
            int beginDirectoryName = dokumentPfad.LastIndexOf('\\') + 1;
            String name = dokumentPfad.Substring(beginDirectoryName, dokumentPfad.Length - beginDirectoryName);

            return name;


        }
        private String titleViewer(String dokumentPfad) {
            // Ausgabe des Dateinamens des Dokuments
            String name = String.Empty;
            int beginFileName = dokumentPfad.LastIndexOf('\\') + 1;
            if (dokumentPfad.Contains('.')) {
                name = dokumentPfad.Substring(beginFileName, dokumentPfad.LastIndexOf('.') - beginFileName);
            }
            return name;

        }

        private String fileType(String dokumentPfad) {
            // Ausgabe des Dateityps einer Datei
            String name = dokumentPfad.Substring(dokumentPfad.LastIndexOf('.') + 1, dokumentPfad.Length - dokumentPfad.LastIndexOf('.') - 1);
            return name;

        }
        // Funktion zum Löschen von allen Elementen welche durch eine CollectionControl aufgerufen wurden.
        private void cleanAll() {
            foreach (SurfaceListBoxItem sLBI in contentNames.SelectedItems) {
                viewSources.Remove(sLBI.Content.ToString());
                if (folderList.Contains(sLBI.Content.ToString())) {
                    CollectionControl item = (CollectionControl)collectionList[sLBI];
                    item.cleanAll();
                    ScatterViewItem sItem = collectionList[sLBI];
                    collectionList.Remove(sLBI);
                    move.MoveAndOrientateScatterToClose(item, this.ActualCenter, 0);
                } else {
                    ScatterViewItem item = scatterList[sLBI];
                    scatterList.Remove(sLBI);
                    move.MoveAndOrientateScatterToClose(item, this.ActualCenter, 0);
            
                }
            }

        }

        // Abfrage zum abgleich von aktivierten und deaktivierten SurfaceListBox Items
        private void contentNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("vor dem Nullvergleich");
            if (contentNames.Items != null) {
                foreach (SurfaceListBoxItem sLBI in contentNames.Items) {
                    if (contentNames.SelectedItems != null && contentNames.SelectedItems.Contains(sLBI)) {
                        Console.WriteLine("Item gewählt");
                        if (!scatterList.ContainsKey(sLBI) && !collectionList.ContainsKey(sLBI)) {
                            Console.WriteLine("Key noch nicht eingetragen");
                            viewSources.Add(sLBI.Content.ToString());
                            foreach (String folder in folderList) {
                                if (folder.Contains(sLBI.Content.ToString())) {
                                    collectionList.Add(sLBI, tagContent.createCollection(folder, title));
                                }
                            }
                            foreach (String file in fileList) {
                                if (file.Contains(sLBI.Content.ToString())) {
                                    String type = fileType(file);
                                    if (type == "jpg") {
                                        scatterList.Add(sLBI, tagContent.createPromotionImage(file));
                                    } else if (type == "xps") {
                                        scatterList.Add(sLBI, tagContent.createDocument(file));
                                    } else if (type == "wmv") {
                                        scatterList.Add(sLBI, tagContent.createVideo(file));
                                    }
                                }
                            }
                        }
                    } else if (scatterList.ContainsKey(sLBI)) {
                        viewSources.Remove(sLBI.Content.ToString());
                        ScatterViewItem item = scatterList[sLBI];
                        scatterList.Remove(sLBI);
                        move.MoveAndOrientateScatterToClose(item, this.ActualCenter, this.ActualOrientation);
                    } else if (collectionList.ContainsKey(sLBI)) {
                        CollectionControl item = (CollectionControl)collectionList[sLBI];
                        item.cleanAll();
                        ScatterViewItem sItem = collectionList[sLBI];
                        collectionList.Remove(sLBI);
                        move.MoveAndOrientateScatterToClose(item, this.ActualCenter, this.ActualOrientation);
                    }
                }
            }
        }

    
    
    }


}
