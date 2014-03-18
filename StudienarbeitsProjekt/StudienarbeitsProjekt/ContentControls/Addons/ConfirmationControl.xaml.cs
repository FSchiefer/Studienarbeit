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

namespace StudienarbeitsProjekt.ContentControls.Addons {
    /// <summary>
    /// Interaktionslogik für ConfirmationControl.xaml
    /// </summary>
    public partial class ConfirmationControl : MovableScatterViewItem {
        public ConfirmationControl() {
            InitializeComponent();
        }

          public ConfirmationControl(ScatterView mainScatt, string Meldung, Brush Background, Brush color)
            : base(mainScatt) {

            InitializeComponent();
            Message.Content = Meldung;
            this.BorderBrush = color;
            this.Background = Background;
            }
        }
    }

