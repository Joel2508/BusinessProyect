
namespace BusinessProyect.Models
{
    using BusinessProyect.Service;
    using BusinessProyect.ViewModel;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class TypeBusiness
    {        

        public int TypeBusinessId { get; set; }
        public string Type { get; set; }
        public List<Business> Business { get; set; }

        #region Service
        private NavigationService navigationService;
        #endregion

        public TypeBusiness()
        {
            navigationService = new NavigationService();
        }

        #region Command
        public ICommand SelectBusinessCommand { get { return new RelayCommand(SelectBusiness); } }
        private async void SelectBusiness()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Business = new BusinesssViewModel(Business);
            await navigationService.Navigation("BusinessPage");
        }
        #endregion
    }
}
