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

namespace StudienarbeitsProjekt.ContentControls {
    /// <summary>
    /// Interaktionslogik für VideoControl.xaml
    /// </summary>
    public partial class VideoControl : ScatterViewItem {

        private Boolean plays = false;
        private Boolean firstPlay = true;
        private FileHandler handler;
        private Image playImage;
        private Image stopImage;
        private Image pauseImage;

        public VideoControl(string videoPosition, Brush color) {

            InitializeComponent();
                       handler = new FileHandler(videoPosition);
            playImage = new Image();
            stopImage = new Image();
            pauseImage = new Image();
            playImage.Source = new BitmapImage(new Uri(handler.getPlayImage(), UriKind.Absolute));
            stopImage.Source = new BitmapImage(new Uri(handler.getStopImage(), UriKind.Absolute));
            pauseImage.Source = new BitmapImage(new Uri(handler.getPauseImage(), UriKind.Absolute)); 

   
    
            this.BorderBrush = color;
            Title.Content = handler.titleViewer();


            

            Play.Content = this.Resources["PlayButton"];
            Stop.Content = this.Resources["StopButton"];

            }



        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args) {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }



        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e) {
            myMediaElement.Stop();
        }

        void InitializePropertyValues() {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        private void Play_Click(object sender, RoutedEventArgs e) {


            if (plays == false) {
                if (firstPlay) {
                    myMediaElement.Source = new Uri(handler.getDokumentPfad(), UriKind.Absolute);
                    firstPlay = false;
                }
                myMediaElement.Play();
                InitializePropertyValues();

                plays = true;
                Play.Content = this.Resources["PauseButton"];
            } else {
                plays = false;
                myMediaElement.Pause();
                Play.Content = this.Resources["PlayButton"];
            }

        }



        private void Stop_Click(object sender, RoutedEventArgs e) {

          
            plays = false;
            Play.Content = this.Resources["PlayButton"];
            myMediaElement.Stop();

        }

    }
}
