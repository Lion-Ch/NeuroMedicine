using BusinessLayer.Logic.ViewNavigation;
using DataLayer.DataManagers;

namespace NeuroMedicine.BusinessLayer
{
    public static class AppContainer
    {
        public static IViewNavigator ViewNavigator { get; set; }

        private static LocalDataManager _localDataManager;
        public static LocalDataManager LocalDataManager
        {
            get
            {
                if (_localDataManager == null)
                    _localDataManager = new LocalDataManager();

                return _localDataManager;
            }
        }
        private static SQLDataManager _sqlDataManager;
        public static SQLDataManager SQLDataManager
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
