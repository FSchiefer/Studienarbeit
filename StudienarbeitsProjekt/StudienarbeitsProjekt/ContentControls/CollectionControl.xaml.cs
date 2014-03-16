﻿using System;
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
using StudienarbeitsProjekt.ContentControls.Canvas;
using System.Windows.Media.Animation;

namespace StudienarbeitsProjekt.ContentControls {

    /// <summary>
    /// Interaktionslogik für CollectionControl.xaml
    /// </summary>
    ///
    public partial class CollectionControl : MovableScatterViewItem {
        private TagContent tagContent;
        private List<String> fileList = new List<String>();
        private List<String> folderList = new List<String>();
        private Dictionary<CollectionControlItemVM, MovableScatterViewItem> scatterList = new Dictionary<CollectionControlItemVM, MovableScatterViewItem>();
        private Dictionary<CollectionControlItemVM, MovableScatterViewItem> collectionList = new Dictionary<CollectionControlItemVM, MovableScatterViewItem>();
        private List<String> viewSources = new List<String>();
        private String title;
        private Brush color;

       
        // Konstruktor zum erstellen der Komponente und dem Festlegen des darzustellenden Titels
        public CollectionControl(ScatterView mainScatt, String dataPath, String name, TagContent tagContent, Brush color)
            : base(mainScatt) {
            InitializeComponent();
            this.BorderBrush = color;
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
                    //child = new SurfaceListBoxItem();
                    //child.Content = fh.titleViewer();
                    //child.Tag = path;

                    Panel image = null;

                    //if (fh.isValidDocType()) {
                    //    image = this.Resources["TextIcon"] as Panel;
                    //} else if (fh.isValidVideoType()) {
                    //    image = this.Resources["VideoIcon"] as Panel;
                    //} else if (fh.isValidImageType()) {
                    //    image = this.Resources["ImageIcon"] as Panel;
                    //    }



                    if (fh.isValidDocType()) {
                        image = new DocumentIcon().TextIcon;
                    } else if (fh.isValidVideoType()) {
                        image = new VideoIcon().Video;
                    } else if (fh.isValidImageType()) {
                        image = new ImageIcon().Image;
                    }
                    var child = new CollectionControlItemVM {
                        Content = fh.titleViewer(),
                        Image = image,
                        Path = path,
                    };

                    contentNames.Items.Add(child);
                }
            }
            foreach (String path in folderPath) {
                FileHandler fh = new FileHandler(path);
                folderList.Add(path);

                //child = new SurfaceListBoxItem();
                //child.Content = fh.getFolderName();
                //child.Tag = path;
                Panel image = new FolderIcon().Folder;

                var child = new CollectionControlItemVM {
                    Content = fh.getFolderName(),
                    Image = image,
                    Path = path,
                };

                contentNames.Items.Add(child);
            }
            contentNames.MaxHeight = contentNames.Items.Count * 70;
            collectionControl.MaxHeight = contentNames.MaxHeight + 30;
        }

        // Funktion zum Löschen von allen Elementen welche durch eine CollectionControl aufgerufen wurden.
        private void cleanAll() {
            foreach (CollectionControlItemVM sLBI in contentNames.SelectedItems) {
                viewSources.Remove(sLBI.Content.ToString());
                if (folderList.Contains(sLBI.Content.ToString())) {
                    CollectionControl item = (CollectionControl)collectionList[sLBI];
                    item.cleanAll();
                    ScatterViewItem sItem = collectionList[sLBI];
                    collectionList.Remove(sLBI);
                    item.MoveAndOrientateScatterToClose(this.ActualCenter, 0);
                } else {
                    MovableScatterViewItem item = scatterList[sLBI];
                    scatterList.Remove(sLBI);
                    item.MoveAndOrientateScatterToClose(this.ActualCenter, 0);
                }
            }
        }

        // Abfrage zum abgleich von aktivierten und deaktivierten SurfaceListBox Items
        private void contentNames_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Console.WriteLine("vor dem Nullvergleich");
            if (contentNames.Items != null) {

                foreach (CollectionControlItemVM sLBI in contentNames.Items) {
                    if (contentNames.SelectedItems != null && contentNames.SelectedItems.Contains(sLBI)) {
                        Console.WriteLine("Item gewählt");
                        if (!scatterList.ContainsKey(sLBI) && !collectionList.ContainsKey(sLBI)) {
                            Console.WriteLine("Key noch nicht eingetragen");
                            viewSources.Add(sLBI.Content.ToString());
                            foreach (String folder in folderList) {
                                if (folder.Contains(sLBI.Content.ToString())) {
                                    MovableScatterViewItem collection = tagContent.CreateCollection(folder, title, color);
                                    collection.Center = this.PointToScreen(new Point(0d, 0d));
                                    collectionList.Add(sLBI, collection);
                                }
                            }
                            foreach (String file in fileList) {
                                FileHandler fh = new FileHandler(file);
                                if (file.Contains(sLBI.Content.ToString())) {
                                    if (fh.isValidImageType()) {
                                        MovableScatterViewItem promoImage = tagContent.CreatePromotionImage(file, color);
                                        promoImage.Center = this.PointToScreen(new Point(0d, 0d));

                                        scatterList.Add(sLBI, promoImage);
                                    } else if (fh.isValidDocType()) {
                                        MovableScatterViewItem document = tagContent.CreateDocument(file, color);
                                        document.Center = this.PointToScreen(new Point(0d + 50, 0d + 50));
                                        scatterList.Add(sLBI, document);
                                    } else if (fh.isValidVideoType()) {
                                        MovableScatterViewItem video = tagContent.CreateVideo(file, color);
                                        video.Center = this.PointToScreen(new Point(0d + 5, 0d + 5));
                                        scatterList.Add(sLBI, video);
                                    }
                                }
                            }
                        }
                    } else {
                        if (scatterList.ContainsKey(sLBI)) {
                            viewSources.Remove(sLBI.Content.ToString());
                            MovableScatterViewItem item = scatterList[sLBI];
                            scatterList.Remove(sLBI);
                            item.MoveAndOrientateScatterToClose(this.ActualCenter, this.ActualOrientation);
                        } else if (collectionList.ContainsKey(sLBI)) {
                            CollectionControl item = (CollectionControl)collectionList[sLBI];
                            item.cleanAll();
                            collectionList.Remove(sLBI);
                            item.MoveAndOrientateScatterToClose(this.ActualCenter, this.ActualOrientation);
                        }
                    }
                }
            }
        }


    }
}
