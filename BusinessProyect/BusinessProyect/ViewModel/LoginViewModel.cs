using BusinessProyect.Pages;
using BusinessProyect.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using BusinessProyect.Models;

namespace BusinessProyect.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attribute
        private bool isRunning;
        private bool isEnabled;
        private bool isToggled;
        private string email;
        private string password;
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
                if (isRunning != value)
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

        public bool IsToggled
        {
            get
            {
                return isToggled;
            }
            set
            {
                if (isToggled != value)
                {
                    isToggled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            apiService = new ApiService();
            Email = "joelarias2508@gmail.com";
            Password = "123456";
            IsEnabled = true;
            IsToggled = true;
        }
        #endregion

        public ICommand LoginCommand { get { return new RelayCommand(Login); } }

        private async void Login()
        {

            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowsMessage("Error", "You must enter a email");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowsMessage("Error", "You must enter a password");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", "You do not have an Internet connection.");
                return;
            }

            var isRechable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isRechable)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", "Internet connection failed, consult your network service operator.");
                return;
            }

            var response = await apiService.GetToken("http://www.xtudiaconstructor.somee.com",
                Email, Password);

            if (response==null)
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowsMessage("Error", "Not service the internet.");
                Email = null;
                Password = null;
                return;
            }

    
            if (string.IsNullOrEmpty(response.AccessToken))
            {
                IsEnabled = true;
                IsRunning = false;
                await dialogService.ShowsMessage("Error", response.ErrorDescription);
                Password = null;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = response;
            mainViewModel.TyperBusines = new TyperBusinessViewModel();
            await navigationService.Navigation("TyperBusinessPage");

            Email = null;
            Password = null;


            IsRunning = false;
            IsEnabled = true;

            
        }

    }
}
