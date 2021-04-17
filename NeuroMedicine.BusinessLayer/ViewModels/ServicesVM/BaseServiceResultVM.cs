using BusinessLayer.Commands;
using DataLayer.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels.ServicesVM
{
    public abstract class BaseServiceResultVM: BaseViewModel
    {
        private Patient _patient;
        public ICommand SaveServiceResultCommand { get { return new DelegateCommand(this.SaveServiceResult); } }
        public ICommand PrintDocCommand { get { return new DelegateCommand(this.PrintDoc); } }
        protected abstract void SaveServiceResult(object obj);
        protected virtual void PrintDoc(object obj) { }

        public BaseServiceResultVM(string nameService, Patient patient)
        {
            HeaderVM = nameService;
            _patient = patient;
        }
    }
}
