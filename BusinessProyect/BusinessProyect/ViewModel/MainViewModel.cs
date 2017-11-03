using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProyect.ViewModel
{
    public class MainViewModel
    {

        #region Properties
        public TyperBusinessViewModel TyperBusines { get; set; }
        public BusinesssViewModel Businesss { get; set; }
        public LoginViewModel Login { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }
        #endregion

    }
}
