using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System;
using BusinessProyect.ViewModel;
using BusinessProyect.Service;
using Xamarin.Forms;

namespace BusinessProyect.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public int TypeBusinessId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public double Latituded { get; set; }
        public double Longitud { get; set; }

        private NavigationService navigationService;

        public Business()
        {
            navigationService = new NavigationService();
        }

        public string FullImage
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "business.png";
                }

                return string.Format("http://businesssearch.somee.com{0}", Image.Substring(1));
            }
        }



        public ICommand OneBusinessCommand { get { return new RelayCommand(OneBusiness); } }

        private async void OneBusiness()
        {
            MainViewModel.GetInstance().OneBusinessMap = new OneBusinessMapViewModel(BusinessId);
            await navigationService.Navigation("OneBusinessMapPage");
        }
    }
}
