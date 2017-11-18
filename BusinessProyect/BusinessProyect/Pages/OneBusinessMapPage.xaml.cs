using BusinessProyect.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace BusinessProyect.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OneBusinessMapPage : ContentPage
    {
        private GeolocatorService geolocatorService;


        public OneBusinessMapPage()
        {
            InitializeComponent();
            geolocatorService = new GeolocatorService();
            MoveMapToCurrentPosition();
        }
        #region Methods
        async void MoveMapToCurrentPosition()
        {
            await geolocatorService.GetLocation();
            if (geolocatorService.Latitude != 0 ||
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                    position,
                    Distance.FromKilometers(.5)));
            }
        }
        #endregion

    }
}