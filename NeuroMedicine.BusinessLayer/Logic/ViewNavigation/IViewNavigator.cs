using DataLayer.Models.Enums;
using NeuroMedicine.BusinessLayer.ViewModels;

namespace BusinessLayer.Logic.ViewNavigation
{
    public interface IViewNavigator
    {
        void NavigateToView(BaseViewModel newView, bool inMasterWindow = true);
        void NavigateToModal(BaseViewModel newView, bool isMaximazed = false);
        void NavigateToWindow(BaseViewModel newView);
        void SetSettings(UserType userType);
    }
}
