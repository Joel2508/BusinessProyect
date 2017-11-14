namespace BusinessProyect.ViewModel
{
    using BusinessProyect.Models;
    using BusinessProyect.Service;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using System;

    public class TyperBusinessViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private bool isRefreshing;
        private List<TypeBusiness> typeBusiness;
        private ObservableCollection<TypeBusiness> typerBusiness;
        #endregion

        #region Service
        private NavigationService navigationService;
        private ApiService apiService;
        private DialogService dialogService;
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        #endregion

        #region Observable
        public ObservableCollection<TypeBusiness> TyperBusiness
        {
            get
            {
                return typerBusiness;
            }
            set
            {
                if(typerBusiness != value)
                {
                    typerBusiness = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TyperBusiness)));
                }
            }
        }
        #endregion

        #region Constructor
        public TyperBusinessViewModel()
        {
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
            LoadTyperBusiness();
        }

        #endregion

        #region Method
        private async void LoadTyperBusiness()
        {

            IsRefreshing = true;
        
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = new TokenResponse();

            var response = await apiService.GetList<TypeBusiness>
                ("http://www.xtudia.somee.com",
                "/api", 
                "/TypeBusinesses",
                mainViewModel.Token.AccessToken,
                mainViewModel.Token.TokenType);

            if (!response.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", response.Message);
                return;
            }

            typeBusiness = (List<TypeBusiness>)response.Result;
            TyperBusiness = new ObservableCollection<TypeBusiness>();

            IsRefreshing = false;
        }

        private async void LoadConnection(Responses connection)
        {
            if (!connection.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", connection.Message);
                return;
            }

        }
        #endregion

        #region Command
        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }
        private void Refresh()
        {
            LoadTyperBusiness();
        }       
        #endregion
    }
}
