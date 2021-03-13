using NeuroMedicine.BusinessLayer.ViewModels;

namespace BusinessLayer.Logic.ViewNavigation
{
    public interface IViewNavigator
    {
        void NavigateToView(BaseViewModel newView);
        void NavigateToWindow(BaseViewModel newView);
    }
}
