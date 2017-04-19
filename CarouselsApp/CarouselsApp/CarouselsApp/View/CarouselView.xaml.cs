using CarouselsApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarouselsApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselView : ContentPage
    {
        public static CarouselModel ViewModel;

        public CarouselView()
        {
            BindingContext = ViewModel = new CarouselModel();

            InitializeComponent();

            ViewModel._cancelToken = new System.Threading.CancellationTokenSource();
            ViewModel.CarouselAutoPlay();
        }
    }
}
