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

namespace StudienarbeitsProjekt.ContentControls
{
    /// <summary>
    /// Interaktionslogik für VideoControl.xaml
    /// </summary>
    public partial class VideoControl : ScatterViewItem
    {
        Boolean plays = false;
        Boolean firstPlay = true;
        String videoPosition;
        public VideoControl(string videoPosition, Brush color)
        {

            InitializeComponent();
            this.BorderBrush = color;
            this.videoPosition = videoPosition;
            titleViewer();
            Image playImage = new Image();
            playImage.Source = new BitmapImage(new Uri(@"C:\Studiengaenge\Play.jpg", UriKind.Absolute));
            Play.Content = playImage;
            Image stopImage = new Image();
            stopImage.Source = new BitmapImage(new Uri(@"C:\Studiengaenge\Stop.jpg", UriKind.Absolute));
         
            Stop.Content = stopImage;
            stopImage.Stretch = Stretch.Fill;
        

          
        }

 

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }


      
        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMediaElement.Stop();
        }

       

 

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMediaElement.Volume = (double)volumeSlider.Value;
  
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {

            if (plays == false)
            {
                if (firstPlay) {
                    myMediaElement.Source = new Uri(videoPosition, UriKind.Absolute);
                    firstPlay = false;
                }
                myMediaElement.Play();


                InitializePropertyValues();

                plays = true;
                Image pauseImage = new Image();
                pauseImage.Source = new BitmapImage(new Uri(@"C:\Studiengaenge\Pause.jpg", UriKind.Absolute));
                Play.Content = pauseImage;
            }
            else
            {
                plays = false;
                myMediaElement.Pause();
                Image playImage = new Image();
                playImage.Source = new BitmapImage(new Uri(@"C:\Studiengaenge\Play.jpg", UriKind.Absolute));
                Play.Content = playImage;
            }

        }

  

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            plays = false;
            Image playImage = new Image();
            playImage.Source = new BitmapImage(new Uri(@"C:\Studiengaenge\Play.jpg", UriKind.Absolute));
            Play.Content = playImage;
            // The Stop method stops and resets the media to be played from
            // the beginning.
            myMediaElement.Stop();

        }

        // Funktion um den Namen des Dokuments auszulesen
        private void titleViewer() {
            // Ausgabe des Dateinamens des Dokuments
            int beginFileName = videoPosition.LastIndexOf('\\') + 1;
            string name = videoPosition.Substring(beginFileName, videoPosition.LastIndexOf('.') - beginFileName);
            Title.Content = name;


        }
    }
}
