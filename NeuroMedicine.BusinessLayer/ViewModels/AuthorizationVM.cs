using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class AuthorizationVM : BaseViewModel
    {
        public AuthorizationVM()
        {

        }
        public void Test()
        {
            AppContainer.ViewNavigator.NavigateToView(new DiagnosticVM());
        }
    }
}
