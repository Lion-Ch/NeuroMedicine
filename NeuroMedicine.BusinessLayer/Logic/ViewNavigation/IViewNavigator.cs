using NeuroMedicine.BusinessLayer.ViewModels;

namespace BusinessLayer.Logic.ViewNavigation
{
    public interface IViewNavigator
    {
        void NavigateToView(BaseViewModel newView, bool inNewWindow = false);
        void NavigateToWindow(BaseViewModel newView);
    }
}
