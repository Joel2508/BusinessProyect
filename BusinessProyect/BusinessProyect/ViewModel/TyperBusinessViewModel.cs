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
    using Plugin.Connectivity;
    using System.Linq;

    public class TyperBusinessViewModel : INotifyPropertyChanged
    {

        #region Attributes
        private string filter;
        private bool isRefreshing;
        private List<TypeBusiness> typeBusiness;
        private ObservableCollection<TypeBusiness> typerObservarBusiness;
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
        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (filter != value)
                {
                    filter = value;
                    ReadTypeBusiness(typeBusiness);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }
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
                return typerObservarBusiness;
            }
            set
            {
                if (typerObservarBusiness != value)
                {
                    typerObservarBusiness = value;
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
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowsMessage("Error", connection.Message);
                return;
            }


            var mainViewModel = MainViewModel.GetInstance();           
            var response = await apiService.GetList<TypeBusiness>
                ("http://www.xtudiaconstructor.somee.com", 
                "/api", "/TypeBusinesses",
                mainViewModel.Token.AccessToken, 
                mainViewModel.Token.TokenType);

            if (!response.IsSuccess)
            {
                await dialogService.ShowsMessage("Error", response.Message);
                return;
            }

            typeBusiness = (List<TypeBusiness>)response.Result;
            TyperBusiness = new ObservableCollection<TypeBusiness>(typeBusiness.OrderBy(t=>t.Type));
            IsRefreshing = false;
        }

        private void ReadTypeBusiness(List<TypeBusiness> typeBusiness)
        {
            TyperBusiness.Clear();
            foreach (var typeBusines in typeBusiness.OrderBy(t => t.Type))
            {
                TyperBusiness.Add(new TypeBusiness
                {
                    Business= typeBusines.Business,
                    Type=typeBusines.Type,
                    TypeBusinessId=typeBusines.TypeBusinessId,
                });
            }
        }

        public ICommand SearchCommand { get { return new RelayCommand(Search); } }

        private void Search()
        {

            ReadTypeBusiness(typeBusiness.Where(t => t.Type.ToLower() == Filter.ToLower()).ToList());
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
