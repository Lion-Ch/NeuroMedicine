using BusinessLayer.Logic.ViewNavigation;
using DataLayer.DataManagers;
using DataLayer.Models.Classes;

namespace NeuroMedicine.BusinessLayer
{
    public class AppContainer
    {
        private static AppContainer _instance;
        public User CurrentUser { get; set; }

        public static AppContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppContainer();
                }

                return _instance;
            }
        }

        public IViewNavigator ViewNavigator { get; set; }

        private LocalDataManager _localDataManager;
        public LocalDataManager LocalDataManager
        {
            get
            {
                if (_localDataManager == null)
                    _localDataManager = new LocalDataManager();

                return _localDataManager;
            }
        }
        private SQLDataManager _sqlDataManager;
        public SQLDataManager SQLDataManager
        {
            get
            {
                if (_sqlDataManager == null)
                    _sqlDataManager = new SQLDataManager();

                return _sqlDataManager;
            }
        }
    }
}
