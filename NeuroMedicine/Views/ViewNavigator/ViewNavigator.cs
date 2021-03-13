using BusinessLayer.Logic.ViewNavigation;
using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.Views.WindowView;
using System;

namespace NeuroMedicine.Views.ViewNavigator
{
    public class ViewNavigator: IViewNavigator
    {
        private MainWindow _mainWindow;
        private MasterWindowView _masterWindow;

        public ViewNavigator(MainWindow mw)
        {
            _mainWindow = mw;
            _masterWindow = new MasterWindowView();
        }
        //Перемещение по самому приложению
        public void NavigateToWindow(BaseViewModel newView)
        {
            _mainWindow.DataContext = newView;
        }
        /// <summary>
        /// Перемещение по MasterVM
        /// </summary>
        /// <param name="newView"></param>
        public void NavigateToView(BaseViewModel newView)
        {
            var control = new DiagnosticsView();
            //_masterWindow.Content = control;
            //control.DataContext = newView;
            _masterWindow.DataContext = control;
            control.ViewModel = newView;

            if (_mainWindow.DataContext != _masterWindow)
                _mainWindow.DataContext = _masterWindow;
        }
    }
}
