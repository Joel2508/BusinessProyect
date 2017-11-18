using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessProyect.Models;
using System.ComponentModel;
using BusinessProyect.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace BusinessProyect.ViewModel
{
    public class BusinesssViewModel :INotifyPropertyChanged
    {
        #region Attribute
        private List<Business> business;
        private bool isRefreshing;
        private string filter;
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
                    Search();
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

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Service
        private NavigationService navigationService;
        private ApiService apiService;
        #endregion

        #region Observable
        public ObservableCollection<Business> businessItemSource;

        public ObservableCollection<Business> BusinessItemSource
        {
            get
            {
                return businessItemSource;
            }
            set
            {
                if(businessItemSource != value)
                {
                    businessItemSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BusinessItemSource)));
                }
            }
        }
        #endregion

        public BusinesssViewModel(List<Business> business)
        {
            apiService = new ApiService();
            navigationService = new NavigationService();
            this.business = business;
            BusinessItemSource = new ObservableCollection<Business>();
            LoadBusiness(business);
        }

        private void LoadBusiness(List<Business> business)
        {
            IsRefreshing = true;

            BusinessItemSource.Clear();
            foreach (var busines in business.OrderBy(b=>b.Name))
            {
                BusinessItemSource.Add(new Business
                {
                    Address=busines.Address,
                    Name = busines.Name,
                    BusinessId = busines.BusinessId,
                    Email = busines.Email,
                    Image = busines.Image,
                    Latituded = busines.Latituded,
                    Longitud = busines.Longitud,
                });
            }
            IsRefreshing = false;
        }

        public ICommand SearchCommand { get { return new RelayCommand(Search); } }

        private void Search()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Filter))
            {
                BusinessItemSource = new ObservableCollection<Business>
                    (business
                    .OrderBy(b => b.Name)
                    .ToList());
            }
            else
            {
                BusinessItemSource = new ObservableCollection<Business>
                    (business
                    .Where(b => b.Name.ToLower()
                    .Contains(Filter)).ToList());
            }
            IsRefreshing = false;
        }

        public ICommand RefreshCommand { get { return new RelayCommand(Refresh); } }


        private void Refresh()
        {
            LoadBusiness(business);
        }
    }
}
