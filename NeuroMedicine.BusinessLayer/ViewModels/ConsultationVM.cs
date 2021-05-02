using DataLayer.Models.PresentationVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels
{
    public class ConsultationVM : DiagnosisVM
    {
        public ConsultationVM(PatientPVM patient): base(patient)
        {
            HeaderVM = "Консультация";
        }
        protected override void Save(object obj)
        {
            PatientPVM.Date = DateTime.Now;
            AppContainer.Instance.SQLDataManager.SaveConsultation(PatientPVM);
            AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
        }
    }
}
