using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarouselsApp.Objects
{
    public class Country : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string[] Images { get; set; }
        public string Description { get; set; }

        private int _imageIndex;
        public int ImageIndex
        {
            get { return _imageIndex; }
            set
            {
                if (value != _imageIndex)
                {
                    _imageIndex = value;
                    NotifyChanges();
                }
            }
        }

        public Country()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyChanges([System.Runtime.CompilerServices.CallerMemberName]string propertyName = "")
        {
            if (!String.IsNullOrEmpty(propertyName))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
