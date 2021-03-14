using BusinessLayer.Logic.ViewNavigation;
using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.Views.ModalWindowView;
using NeuroMedicine.Views.WindowView;
using System;
using System.Windows;

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
        public void NavigateToView(BaseViewModel newView, bool inNewWindow = false)
        {
            var control = new DiagnosticsView();

            if(inNewWindow)
            {
                var view = Activator.CreateInstance(typeof(PhotoModalWindow)) as BaseView;
                view.ViewModel = newView;

                var window = new ModalWindow();
                window.Title = newView.HeaderVM;
                //window.Content = view;
                window.DataContext = newView;
                window.Owner = _mainWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                window.ShowDialog();
            }
            else
            {
                //_masterWindow.DataContext = control;
                //control.ViewModel = newView;

                //if (_mainWindow.DataContext != _masterWindow)
                //    _mainWindow.DataContext = _masterWindow;

                _masterWindow.DataContext = control;
                control.ViewModel = newView;

                if (_mainWindow.DataContext != _masterWindow)
                    _mainWindow.DataContext = _masterWindow;
            }
        }

    }
}
