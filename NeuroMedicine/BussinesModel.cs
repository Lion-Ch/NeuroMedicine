using NeuroMedicine.BusinessLayer;
using NeuroMedicine.BusinessLayer.ViewModels;
using NeuroMedicine.Views.ViewNavigator;
using NeuroMedicine.Views.WindowView;

namespace NeuroMedicine
{
    /// <summary>
    /// Старт приложения + передача в бизнес уровень
    /// </summary>
    internal class BussinesModel
    {
        public static ViewNavigator ViewNavigator;
        public BussinesModel(MainWindow mw)
        {
            ViewNavigator = new ViewNavigator(mw);
            //Инициализация + передача в бизнес
            AppContainer.Instance.ViewNavigator = ViewNavigator;

            //Загрузка начальной страницы
            ViewNavigator.NavigateToView(new DiagnosticVM(),true);
            //AuthorizationVM _startWindow = new AuthorizationVM();
            //mw.DataContext = _startWindow;
            //_startWindow.Test();

        }
    }
}
