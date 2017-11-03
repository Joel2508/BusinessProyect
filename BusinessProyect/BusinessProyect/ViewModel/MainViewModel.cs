using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessProyect.ViewModel
{
    public class MainViewModel
    {
        public TyperBusinessViewModel TyperBusiness { get; set; }

        public MainViewModel()
        {
            TyperBusiness = new TyperBusinessViewModel();
        }
    }
}
