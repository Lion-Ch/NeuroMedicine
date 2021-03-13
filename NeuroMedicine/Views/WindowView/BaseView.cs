using NeuroMedicine.BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NeuroMedicine.Views.WindowView
{
    public abstract class BaseView : UserControl
    {
        public virtual BaseViewModel ViewModel { get; set; }
    }
}
