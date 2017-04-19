using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CarouselsApp.Objects;
using System.Threading;

namespace CarouselsApp.ViewModel
{
    public class CarouselModel : INotifyPropertyChanged
    {
        private int _position;
        private int _delay = 2000; // 2000 ms (2 s)

        private ICommand _buttonCommand;

        public CancellationTokenSource _cancelToken;
        
        public int Position
        {
            get { return _position; }
            set
            {
                if (value != _position)
                {
                    _cancelToken.Cancel();

                    foreach (Country country in CountryList)
                    {
                        country.ImageIndex = 0;
                    }

                    _cancelToken = new CancellationTokenSource();
                    CarouselAutoPlay();

                    _position = value;

                    NotifyChanges();
                }
            }
        }

        public ICommand ButtonCommand
        {
            get
            {
                if (_buttonCommand == null)
                    _buttonCommand = new RelayCommand(ButtonAction);

                return _buttonCommand;
            }
        }
        
        public ObservableCollection<Country> CountryList { get; set; }

        public CarouselModel()
        {
            CountryList = new ObservableCollection<Country>();
            CountryList.Add(new Country() { Title = "Belgium", Images = new string[3] { "BE_1", "BE_2", "BE_3" }, Description = "Description", ImageIndex = 0 });
            CountryList.Add(new Country() { Title = "France", Images = new string[3] { "FR_1", "FR_2", "FR_3" }, Description = "Description", ImageIndex = 0 });
            CountryList.Add(new Country() { Title = "Italy", Images = new string[3] { "IT_1", "IT_2", "IT_3" }, Description = "Description", ImageIndex = 0 });
        }
        
        private void ButtonAction()
        {
            if (Position < CountryList.Count - 1)
                Position++;
            else
                Position = 0;
        }

        public void CarouselAutoPlay()
        {
            Task.Delay(_delay).ContinueWith(t =>
            {
                try
                {
                    _cancelToken.Token.ThrowIfCancellationRequested();

                    if (CountryList[Position].ImageIndex == CountryList[Position].Images.Count() - 1)
                        CountryList[Position].ImageIndex = 0;
                    else
                        CountryList[Position].ImageIndex++;

                    this.CarouselAutoPlay();
                }
                catch(OperationCanceledException e)
                {

                }
            }, _cancelToken.Token);
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
