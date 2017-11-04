using BusinessProyect.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BusinessProyect.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attribute
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Service
        private NavigationService navigationService;
        private DialogService dialogService;
        private ApiService apiService;
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if(isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }

            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        public string Email { get; set; }
        public string Password { get; set; }

        #endregion


        #region Constructor
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();

        }
        #endregion

        public ICommand LoginCommand { get {return new RelayCommand(Login); } }

        private async void Login()
        {
           var internet = await apiService.CheckConnection();
            if (!internet.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", "Not connection the internet");
                return;
            }

            if (!string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowsMessage("Error", "Enter your must a email");
                return;
            }

            if (!string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowsMessage("Error", "Enter your must a password");
                return;
            }

            var response = await apiService.GetToken("http://www.xtudia.somee.com","/api","");

        }
    }
}
