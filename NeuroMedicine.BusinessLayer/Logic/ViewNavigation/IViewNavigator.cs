using NeuroMedicine.BusinessLayer.ViewModels;

namespace BusinessLayer.Logic.ViewNavigation
{
    public interface IViewNavigator
    {
        void NavigateToView(BaseViewModel newView, bool inMasterWindow = true);
        void NavigateToModal(BaseViewModel newView, bool inNewWindow = false);
        void NavigateToWindow(BaseViewModel newView);
    }
}
