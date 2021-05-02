using BusinessLayer.Logic.ViewNavigation;
using DataLayer.Models.Enums;
using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.BusinessLayer.ViewModels.ServicesVM;
using NeuroMedicine.Views.ModalWindowView;
using NeuroMedicine.Views.WindowView;
using NeuroMedicine.Views.WindowView.ServicesViews;
using System;
using System.Collections.Generic;
using System.Windows;

namespace NeuroMedicine.Views.ViewNavigator
{
    public class ViewNavigator : IViewNavigator
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
            _viewModelToViewMap.Add(typeof(PhotoVM), typeof(PhotoModalWindow));
            _viewModelToViewMap.Add(typeof(SearchPatientVM), typeof(SearchPatientModalWindow));
            _viewModelToViewMap.Add(typeof(ConfirmationModalVM), typeof(ConfirmModalWindow));
            _viewModelToViewMap.Add(typeof(DiagnosisVM), typeof(DiagnosisModalWindow));
            _viewModelToViewMap.Add(typeof(RegistryVM), typeof(RegistryView));
            _viewModelToViewMap.Add(typeof(SettingsVM), typeof(SettingsView));
            _viewModelToViewMap.Add(typeof(ConsultationVM), typeof(ConsultationView));

            //Услуги
            _viewModelToViewMap.Add(typeof(BloodTestServiceVM), typeof(BloodTestView));
            _viewModelToViewMap.Add(typeof(UrineTestServiceVM), typeof(UrineTestServiceView));
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
        public void NavigateToView(BaseViewModel newView, bool inMasterWindow = true)
        {
            //var control = new DiagnosticsView();
            Type viewControlType;

            if (_viewModelToViewMap.TryGetValue(newView.GetType(), out viewControlType))
            {

                //_masterWindow.DataContext = control;
                //control.ViewModel = newView;

                //if (_mainWindow.DataContext != _masterWindow)
                //    _mainWindow.DataContext = _masterWindow;

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
        public void NavigateToModal(BaseViewModel newView, bool isMaximazed = false)
        {
            //var control = new DiagnosticsView();
            Type viewControlType;

            if (_viewModelToViewMap.TryGetValue(newView.GetType(), out viewControlType))
            {
                var control = Activator.CreateInstance(viewControlType) as BaseView;

                if (null == control) return;

                control.ViewModel = newView;

                var window = new ModalWindow();
                window.Title = newView.HeaderVM;
                window.Content = control;
                window.DataContext = newView;
                window.Owner = _mainWindow;
                window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (isMaximazed)
                {
                    control.Width = SystemParameters.PrimaryScreenWidth;
                    control.Height = SystemParameters.PrimaryScreenHeight;
                    window.WindowState = WindowState.Maximized;
                }
                window.ShowDialog();

            }
        }
        public void SetSettings(UserType userType)
        {
            FrameworkElement[] controls = new FrameworkElement[]
            {
                _masterWindow.Cabinet,
                _masterWindow.Neuro,
                _masterWindow.History,
                _masterWindow.AddNewPatient,
                _masterWindow.Settings,
                _masterWindow.Registr
            };
            foreach(var control in controls)
            {
                control.Visibility = Visibility.Collapsed;
            }

            switch(userType)
            {
                case UserType.Admin:
                    _masterWindow.Settings.Visibility = Visibility.Visible;
                    break;
                case UserType.Registratur:
                    _masterWindow.AddNewPatient.Visibility = Visibility.Visible;
                    _masterWindow.Registr.Visibility = Visibility.Visible;
                    break;
                case UserType.Doctor:
                    _masterWindow.Cabinet.Visibility = Visibility.Visible;
                    _masterWindow.Neuro.Visibility = Visibility.Visible;
                    _masterWindow.History.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
