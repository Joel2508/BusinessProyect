using BusinessProyect.Models;
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

namespace BusinessProyect.ViewModel
{
    public class RegisterNewUserViewModel : INotifyPropertyChanged
    {
        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attribute
        private string email;
        private string password;
        private string confirPassword;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Service
        private NavigationService navigationService;
        private ApiService apiService;
        private DialogService dialogService;
        #endregion

        #region Properties
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }

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
        #endregion

        #region Constructor
        public RegisterNewUserViewModel()
        {
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
            IsEnabled = true;
        }
        #endregion

        #region Command
        public ICommand SalveCommand { get { return new RelayCommand(Salve); } }

        private async void Salve()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowsMessage("Error", "Enter must your first name.");
                return;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowsMessage("Error", "Enter must your last name.");
                return;
            }
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowsMessage("Error", "Enter must your email.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowsMessage("Error", "Enter an password for your register.");
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await dialogService.ShowsMessage("Error", "Enter an password for your register.");
                return;
            }
            if (Password.Length < 6)
            {
                await dialogService.ShowsMessage("Error", "The hiring must be greater than 6.");
                return;
            }
            if (!Password.Equals(ConfirmPassword))
            {
                await dialogService.ShowsMessage("Error", "The password and confirm the password not equas.");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", "Not connection the internet");
                return;
            }



            var customer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
            };
            var response = await apiService.PostRegister(
                 "http://businesssearch.somee.com",
                "/api", "/Customers", customer);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", response.Message);
                return;
            }

            var responseToken = await apiService.GetToken(
                "http://businesssearch.somee.com", Email, Password);
            if (responseToken == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", "The service is not available, please try latter.");
                Password = null;
                return;
            }
            if (string.IsNullOrEmpty(responseToken.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", responseToken.ErrorDescription);
                Password = null;
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = responseToken;
            mainViewModel.TyperBusines = new TyperBusinessViewModel();
            await navigationService.Back();
            await navigationService.Navigation("TyperBusinessPage");

            IsRunning = false;
            IsEnabled = true;
        }      
        #endregion
    }
}
