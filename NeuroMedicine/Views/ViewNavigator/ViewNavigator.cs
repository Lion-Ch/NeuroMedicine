using BusinessLayer.Logic.ViewNavigation;
using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.Views.ModalWindowView;
using NeuroMedicine.Views.WindowView;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NeuroMedicine.Views.ViewNavigator
{
    public class ViewNavigator: IViewNavigator
    {
        private MainWindow _mainWindow;
        private MasterWindowView _masterWindow;

        private readonly Dictionary<Type, Type> _viewModelToViewMap = new Dictionary<Type, Type>();


        public ViewNavigator(MainWindow mw)
        {
            _mainWindow = mw;
            _masterWindow = new MasterWindowView();

            _viewModelToViewMap.Add(typeof(DiagnosticVM), typeof(DiagnosticsView));
            _viewModelToViewMap.Add(typeof(HistotyReceptionVM), typeof(HistoryReceptionView));
            _viewModelToViewMap.Add(typeof(PatientsVM), typeof(PatientsView));
            _viewModelToViewMap.Add(typeof(PersonalCabinetVM), typeof(PersonalCabinet));
            _viewModelToViewMap.Add(typeof(AuthorizationVM), typeof(AuthorizationView));

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
        public void NavigateToView(BaseViewModel newView, bool inNewWindow = false, bool inMasterWindow = true)
        {
            //var control = new DiagnosticsView();

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
                Type viewControlType;
                if (_viewModelToViewMap.TryGetValue(newView.GetType(), out viewControlType))
                {

                    var control = Activator.CreateInstance(viewControlType) as BaseView;
                    if (inMasterWindow)
                        _masterWindow.DataContext = control;
                    else
                        _mainWindow.DataContext = control;
                    control.ViewModel = newView;

                    if (_mainWindow.DataContext != _masterWindow && inMasterWindow)
                        _mainWindow.DataContext = _masterWindow;
                }
            }
        }

    }
}
