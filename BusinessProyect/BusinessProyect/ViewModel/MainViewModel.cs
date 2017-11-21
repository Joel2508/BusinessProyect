using BusinessProyect.Models;
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
        public TokenResponse Token { get; set; }
        public BusinesssViewModel Business { get; set; }
        public LoginViewModel Login { get; set; }
        public OneBusinessMapViewModel OneBusinessMap { get; set; }
        public RegisterNewUserViewModel RegisterNewUser { get; set; }
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
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

    }
}
