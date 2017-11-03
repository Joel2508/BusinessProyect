using BusinessProyect.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProyect.Service
{
    public class NavigationService
    {
        public void SetPageNavigation (string pageName)
        {
            switch (pageName)
            {
                case "BusinessPage":
                    App.Current.MainPage.Navigation.PushAsync(new BusinessPage());
                    break;
                default:
                    break;
            }
        }
    }
}
