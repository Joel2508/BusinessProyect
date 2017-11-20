using BusinessProyect.Models;
using BusinessProyect.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace BusinessProyect.ViewModel
{
    public class OneBusinessMapViewModel
    {

        #region Attribute
        #endregion

        #region Service
        private ApiService apiService;
        private DialogService dialogService;
        private List<Business> business;
        #endregion

        #region Observable
        public ObservableCollection<Pin> Pins { get; set; }
        #endregion


        public OneBusinessMapViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            business = new List<Business>();
            LoadPins();
        }

        private static OneBusinessMapViewModel instance;
        public static OneBusinessMapViewModel GetOneBusiness()
        {
            return instance;
        }

        public async Task LoadPins()
        {
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", connection.Message);
                return;
            }


            var mainViewModel = MainViewModel.GetInstance();
            var controller = string.Format("/Businesses");
            var response = await apiService.GetBusinessMap<Business>
                ("http://www.xtudiaconstructor.somee.com",
                "/api", controller,
                mainViewModel.Token.AccessToken,
                mainViewModel.Token.TokenType);

            if (!response.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", response.Message);
                return;
            }

            Pins = new ObservableCollection<Pin>();
            business = (List<Business>)response.Result;
            foreach (var busines in business)
            {
                Pins.Add(new Pin
                {
                    Address = busines.Address,
                    Label = busines.Name,
                    Position = new Position(
                    Convert.ToDouble(busines.Latituded),
                    Convert.ToDouble(busines.Longitud)),
                    Type = PinType.Place,
                });
            }
        }

      
    }
}
