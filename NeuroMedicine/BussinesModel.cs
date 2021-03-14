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
        public BussinesModel(MainWindow mw)
        {
            //Инициализация + передача в бизнес
            AppContainer.ViewNavigator = new ViewNavigator(mw);

            //Загрузка начальной страницы
            AuthorizationVM _startWindow = new AuthorizationVM();
            mw.DataContext = _startWindow;
            _startWindow.Test();

        }
    }
}
