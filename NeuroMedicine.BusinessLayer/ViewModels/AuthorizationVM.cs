

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
