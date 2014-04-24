using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Surface.Presentation.Controls;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using StudienarbeitsProjekt.ContentControls.Addons;
using System.IO;
using System.Collections.Generic;

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für DescriptionControl.xaml
    /// </summary>
    public partial class InformationControl : MovableScatterViewItem {

        private Boolean nameNotEmpty = false;
        private Boolean mailNotEmpty = false;
        private Boolean courseNotEmpty = false;
        private ScatterView mainScatt;
        private XmlDocument reader;


        private System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        private string kontaktDatei;
               private CollectionControl closeControl;
        private CollectionControlItemVM sLBI;

        public InformationControl(ScatterView mainScatt, string textPosition, Brush color, CollectionControl closeControl, CollectionControlItemVM sLBI)
            : base(mainScatt) {
            DefaultAction(mainScatt, textPosition, color);
            this.sLBI = sLBI;
            this.closeControl = closeControl;
            Close.Visibility = Visibility.Visible;
            Close.Click += Close_Click;
        }


        public void Close_Click(object sender, RoutedEventArgs e) {
            closeControl.contentNames.SelectedItems.Remove(sLBI);

        }



        public InformationControl(ScatterView mainScatt, string textPosition, Brush color)
            : base(mainScatt) {
                DefaultAction(mainScatt, textPosition, color);
         
        }


        private void DefaultAction(ScatterView mainScatt, string textPosition, Brush color) {

            InitializeComponent();
            this.mainScatt = mainScatt;
            this.kontaktDatei = FileHandler.getKontakte();

            try {
                if (!File.Exists(kontaktDatei)) {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(kontaktDatei, true)) {
                        file.WriteLine("Name;E-Mail;Studiengang");

                    }
                }
                checkButton();
                this.BorderBrush = color;
                Scroller.BorderBrush = this.BorderBrush;
                ConfirmationButton.IsEnabled = false;
                reader = new XmlDocument();
                reader.Load(textPosition);
                List<string> lines = new List<string>();

                //while (reader.Read()) {
                //    switch (reader.NodeType) {
                //        case XmlNodeType.Element: // The node is an element.
                //            Console.Write("<" + reader.Name);

                //            while (reader.MoveToNextAttribute()) // Read the attributes.
                //              Console.WriteLine(">");
                //            break;
                //        case XmlNodeType.Text: //Display the text in each element.
                //            Console.WriteLine(reader.Value);
                //            lines.Add(reader.Value);
                //            Console.Write(" " + reader.Name + "='" + reader.Value + "");
                //            break;
                //        case XmlNodeType.EndElement: //Display the end of the element.
                //            Console.Write("</" + reader.Name);
                //            Console.WriteLine(">");
                //            break;
                //    }

                //}


                XmlNode nodes = reader.SelectSingleNode("Text");

                if (nodes.ChildNodes != null) {
                    foreach (XmlNode nodeWalk in nodes.ChildNodes) {
                        if (nodeWalk.Name == "Titel") {
                            Titel.Content = nodeWalk.InnerText;
                        }
                        if (nodeWalk.Name == "Interessengebiet") {
                            lines.Add(nodeWalk.InnerText);
                        }
                        if (nodeWalk.Name == "Definition") {
                            Definition.Content = nodeWalk.InnerText;
                        }

                    }
                }

                if (lines.Count > 0)
                    foreach (string line in lines) {
                        SurfaceCheckBox studiengangCheckBox = new SurfaceCheckBox();
                        studiengangCheckBox.Content = line;
                        studiengangCheckBox.Checked += new RoutedEventHandler(studiengangCheckBox_Checked);
                        studiengangCheckBox.Unchecked += new RoutedEventHandler(studiengangCheckBox_Checked);
                        AuswahlStudiengaenge.Children.Add(studiengangCheckBox);
                    }
            } catch (NullReferenceException e) {
                Debug.WriteLine(e);
             
                this.Visibility = Visibility.Collapsed;
            }
        
        
        }

        private void studiengangCheckBox_Checked(object sender, RoutedEventArgs e) {
            checkButton();
        }

        private void Confirmation_Clicked(object sender, RoutedEventArgs e) {
            string besucherDaten = VisitorName.Text + ";" + Email.Text + ";";

            string daten = string.Empty;
            foreach (SurfaceCheckBox chkbox in AuswahlStudiengaenge.Children) {
                if (chkbox.IsChecked.Equals(true)) {
                    daten = besucherDaten + chkbox.Content.ToString();
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(kontaktDatei, true)) {
                        file.WriteLine(daten);

                    }
                }
                chkbox.IsChecked = false;
             }
            Email.Text = "";
            VisitorName.Text = "";
            ConfirmationControl confirmation = new ConfirmationControl(mainScatt, "Speichern Erfolgreich", Brushes.Green, this.BorderBrush );
            confirmation.Center = this.Center;
            confirmation.Orientation = this.Orientation;

            confirmation.fadeOut(2);

            
            checkButton();

        }

        public void Timer_Callback(object args) 
        {
            
        }


        private void Email_TextChanged(object sender, TextChangedEventArgs e) {
            Debug.WriteLine(Email.Text);

            //txtmail is name/object of textbox
            if (Email.Text.Length > 0) {

                //rEMail is object of Regex class located in System.Text.RegularExpressions
                if (rEMail.IsMatch(Email.Text)) {
                    mailNotEmpty = true;
                    checkButton();
                } else {
                    mailNotEmpty = false;
                }
            } else {
                mailNotEmpty = false;
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e) {
            Debug.WriteLine(Email.Text);

            //txtmail is name/object of textbox
            if (VisitorName.Text.Length > 0) {
                nameNotEmpty = true;
                checkButton();

            } else {
                nameNotEmpty = false;
            }
        }


        private void checkButton() {
            Boolean oneChecked = false ;
            foreach (SurfaceCheckBox chkbox in AuswahlStudiengaenge.Children) {
                if (chkbox.IsChecked.Equals(true)) {
                    oneChecked = true;
                }
            }
            if (oneChecked) {
                courseNotEmpty = true;
            } else {
                courseNotEmpty = false;
            }

            if (nameNotEmpty && mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = true;
                Confirmation.Content = "Weitere Informationen Bestellen.";
            } else if (!nameNotEmpty && mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen Eingeben.";
            } else if (!nameNotEmpty && !mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen und Mail-Addresse Eingeben.";
            } else if (!nameNotEmpty && !mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen, Mail-Addresse und Interesse Eingeben.";
            } else if (nameNotEmpty && !mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Mail-Addresse Eingeben.";
            } else if (nameNotEmpty && mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Kursintresse Eingeben.";
            } else if (nameNotEmpty && !mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Mail-Addresse und Interesse Eingeben.";
            } else if (!nameNotEmpty && !mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen und Mail-Addresse Eingeben.";
            } else if (!nameNotEmpty && mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen und Interesse Eingeben.";
            }
               

        }
    
     

    }
}
