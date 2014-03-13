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
        private Brush color;

        // TODO Icons für Dateiarten einbauen

        public CollectionControl() {
            InitializeComponent();
        }

        // Konstruktor zum erstellen der Komponente und dem Festlegen des darzustellenden Titels
        public CollectionControl(String dataPath, String name, TagContent tagContent, Brush color) {
            InitializeComponent();
            this.BorderBrush = color;
            move = new ScatterMovement(tagContent.mainScatt);
            this.tagContent = tagContent;
            this.color = color;

            FileHandler fh = new FileHandler(dataPath);
            if (name.Contains(":")) {
                name = name.Substring(0, name.LastIndexOf(':') - 0);
                title = name + ": " + fh.getFolderName();
            } else {
                title = name + ": " + fh.getFolderName();
            }
            Title.Content = title;
            fileTable(dataPath);
        }

        private void fileTable(string dataPath) {
            String[] filePath = Directory.GetFiles(dataPath);
            String[] folderPath = Directory.GetDirectories(dataPath);
            foreach (String path in filePath) {
                FileHandler fh = new FileHandler(path);

                if (fh != null && fh.isValidFileType()) {
                    fileList.Add(path);
                    child = new SurfaceListBoxItem();
                    child.Content = fh.titleViewer();
                    child.Tag = path;

                    contentNames.Items.Add(child);
                }
            }
            foreach (String path in folderPath) {
                FileHandler fh = new FileHandler(path);
                folderList.Add(path);

                child = new SurfaceListBoxItem();
                child.Content = fh.getFolderName();
                child.Tag = path;

                contentNames.Items.Add(child);
            }
            contentNames.MaxHeight = contentNames.Items.Count * 55;
            collectionControl.MaxHeight = contentNames.MaxHeight + 25;
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
                                    ScatterViewItem collection = tagContent.CreateCollection(folder, title, color);
                                    collection.Center = this.PointToScreen(new Point(0d, 0d));
                                    collectionList.Add(sLBI, collection);
                                }
                            }
                            foreach (String file in fileList) {
                                FileHandler fh = new FileHandler(file);
                                if (file.Contains(sLBI.Content.ToString())) {
                                    if (fh.isValidImageType()) {
                                        ScatterViewItem promoImage = tagContent.CreatePromotionImage(file, color);
                                      promoImage.Center = this.PointToScreen(new Point (0d,0d));
                                        
                                        scatterList.Add(sLBI, promoImage);
                                    } else if (fh.isValidDocType()) {
                                        ScatterViewItem document =  tagContent.CreateDocument(file, color);
                                        document.Center = this.PointToScreen(new Point (0d+50,0d+50));
                                        scatterList.Add(sLBI,document);
                                    } else if (fh.isValidVideoType()) {
                                        ScatterViewItem video = tagContent.CreateVideo(file, color);
                                        video.Center = this.PointToScreen(new Point(0d+5, 0d+5));
                                        scatterList.Add(sLBI,video);
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
