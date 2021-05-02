using BusinessLayer.Logic.ViewNavigation;
using DataLayer.DataManagers;
using DataLayer.Models.Classes;
using DataLayer.Settings;
using NeuroMedicine.BusinessLayer.Logic;

namespace NeuroMedicine.BusinessLayer
{
    public class AppContainer
    {
        private User _currUser;
        public User CurrentUser
        {
            get { return _currUser; }
            set
            {
                ViewNavigator.SetSettings(value.UserType);
                _currUser = value;
            }
        }
        public int NumPatients = 0;

        private static AppContainer _instance;
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
        private DocPrinter _docPrinter;
        public DocPrinter DocPrinter
        {
            get
            {
                if (_docPrinter == null)
                    _docPrinter = new DocPrinter();

                return _docPrinter;
            }
        }
        private Settings settings; 
        public Settings Settings
        {
            get
            {
                if (settings == null)
                    settings = new Settings();

                return settings;
            }
        }
        public AppContainer()
        {
            SQLDataManager.CheckConnect();
        }
    }
}
