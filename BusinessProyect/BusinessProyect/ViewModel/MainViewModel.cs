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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            TyperBusines = new TyperBusinessViewModel();
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
