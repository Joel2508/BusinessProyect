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
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                if(email != value)
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
                if(password != value)
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public string ConfirmPassword
        {
            get
            {
                return confirPassword;
            }
            set
            {
                if (ConfirmPassword != value)
                {
                    confirPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConfirmPassword)));
                }
            }
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
            if(Password.Length < 6)
            {
                await dialogService.ShowsMessage("Error", "The hiring must be greater than 6.");
                return;
            }
            if (Password != ConfirmPassword)
            {
                await dialogService.ShowsMessage("Error", "The password and confirm the password not equas.");
                return;
            }

            if (!CrossConnectivity.Current.IsConnected)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowsMessage("Error", "Not connection the internet");
                return;
            }
        }

        public ICommand CancelCommand { get { return new RelayCommand(Cancel); } }

        private void Cancel()
        {
            navigationService.Back();
        }
        #endregion
    }
}
