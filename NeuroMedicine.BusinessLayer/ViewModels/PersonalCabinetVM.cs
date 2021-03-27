using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class PersonalCabinetVM : BaseViewModel
    {
        public PersonalCabinetVM()
        {
            HeaderVM = "Личный кабинет";
            GC.Collect(1);
            GC.WaitForPendingFinalizers();
        }
    }
}
