using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Controls;

namespace StudienarbeitsProjekt.ContentControls {
    public class CollectionControlItemVM : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Poperty Content
        private object _content;

        public object Content {
            get { return _content; }
            set {
                _content = value;
                NotifyPropertyChanged("Content");
            }
        }


        #endregion

        #region Property Image

        private Panel _image;

        public Panel Image {
            get { return _image; }
            set {
                _image = value;
                NotifyPropertyChanged("Image");
            }
        }

        //private Panel _image;

        //public Panel Image {
        //    get { return _image; }
        //    set {
        //        _image = value;
        //        NotifyPropertyChanged("Image");
        //    }
        //}


        #endregion


        #region Property Path

        private string _path;

        public string Path {
            get { return _path; }
            set {
                _path = value;

                NotifyPropertyChanged("Path");
            }
        }


        #endregion


    }


}
