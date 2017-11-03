
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
        public List<Businesss> Businesss { get; set; }

        #region Service
        private NavigationService navigationService;
        #endregion

        #region Command
        public ICommand SelectBusinessCommand { get { return new RelayCommand(SelectBusiness); } }
        private void SelectBusiness()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Businesss = new BusinesssViewModel();
            navigationService.SetPageNavigation("BusinessPage");
        }
        #endregion
    }
}
