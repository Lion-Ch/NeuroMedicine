using NeuroMedicine.BusinessLayer.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace NeuroMedicine.Views.WindowView
{
    public class BaseView : UserControl
    {
        public virtual BaseViewModel ViewModel { get; set; }
    }
}
