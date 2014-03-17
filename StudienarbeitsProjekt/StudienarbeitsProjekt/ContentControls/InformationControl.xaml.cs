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
using System.Diagnostics;

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für DescriptionControl.xaml
    /// </summary>
    public partial class InformationControl : MovableScatterViewItem {

        private Boolean nameNotEmpty = false;
        private Boolean mailNotEmpty = false;
        private Boolean courseNotEmpty = false;


        private System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        string kontaktDatei;
        public InformationControl(ScatterView mainScatt, string textPosition, string kontaktDatei, Brush color)
            : base(mainScatt) {

            InitializeComponent();
            this.kontaktDatei = kontaktDatei;
            checkButton();
            this.BorderBrush = color;
            ConfirmationButton.IsEnabled = false;
            string[] lines = System.IO.File.ReadAllLines(textPosition);
            foreach (string line in lines) {
                SurfaceCheckBox studiengangCheckBox = new SurfaceCheckBox();
                studiengangCheckBox.Content = line;
                studiengangCheckBox.Checked += new RoutedEventHandler(studiengangCheckBox_Checked);
                studiengangCheckBox.Unchecked += new RoutedEventHandler(studiengangCheckBox_Checked);
                AuswahlStudiengaenge.Children.Add(studiengangCheckBox);
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
            checkButton();
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
                Confirmation.Content = "Bitte Namen, Mail-Addresse und Kursintresse Eingeben.";
            } else if (nameNotEmpty && !mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Mail-Addresse Eingeben.";
            } else if (nameNotEmpty && mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Kursintresse Eingeben.";
            } else if (nameNotEmpty && !mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Mail-Addresse und Kursintresse Eingeben.";
            } else if (!nameNotEmpty && !mailNotEmpty && courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen und Mail-Addresse Eingeben.";
            } else if (!nameNotEmpty && mailNotEmpty && !courseNotEmpty) {
                ConfirmationButton.IsEnabled = false;
                Confirmation.Content = "Bitte Namen und Kursintresse Eingeben.";
            }
               

        }
    
     

    }
}
