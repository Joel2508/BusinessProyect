namespace BusinessProyect.Service
{
    using BusinessProyect.Pages;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class NavigationService
    {
        public async Task Navigation(string pageName)
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
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterNewUserView());
                    break;
            }
        }
        public void TyperBusinessNavigation(string pageName)
        {
            switch (pageName)
            {
                case "TyperBusinessPage":
                    App.Current.MainPage = new TyperBusinessPage();
                    break;            
            }
        }
        public async Task Back()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public void BackLogin()
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}
