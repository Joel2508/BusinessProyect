using BusinessProyect.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BusinessProyect.Service
{
    public class NavigationService
    {
        public async Task Navigation (string pageName)
        {
            switch (pageName)
            {
                case "BusinessPage":
                   await Application.Current.MainPage.Navigation.PushAsync(new BusinessPage());
                    break;
                case "TyperBusinessPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new TyperBusinessPage());
                    break;
                case "OneBusinessMapPage":
                    await Application.Current.MainPage.Navigation.PushAsync(new OneBusinessMapPage());
                    break;
                case "RegisterNewUserView":
                    App.Current.MainPage = new RegisterNewUserView();
                    break;

            }
        }

        public void Back()
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}
