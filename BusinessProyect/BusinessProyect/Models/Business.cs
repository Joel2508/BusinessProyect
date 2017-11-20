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
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Latituded { get; set; }
        public decimal Longitud { get; set; }

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

                return string.Format("http://www.xtudiaconstructor.somee.com{0}", Image.Substring(1));
            }
        }



        public ICommand OneBusinessCommand { get { return new RelayCommand(OneBusiness); } }

        private async void OneBusiness()
        {
            MainViewModel.GetInstance().OneBusinessMap = new OneBusinessMapViewModel();
            await navigationService.Navigation("OneBusinessMapPage");
        }
    }
}
