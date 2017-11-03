using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProyect.Service
{
    public class DialogService
    {
        public async Task ShowsMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Accept");
            return;
        }
    }
}
