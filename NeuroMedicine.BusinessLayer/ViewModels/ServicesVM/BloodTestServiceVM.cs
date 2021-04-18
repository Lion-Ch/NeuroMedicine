using BusinessLayer.Commands;
using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NeuroMedicine.BusinessLayer.ViewModels.ServicesVM
{
    public class BloodTestServiceVM : BaseServiceResultVM
    {

        public RefBloodTest RefBloodTest { get; set; }
        public BloodTestServiceVM(Patient patient) : base("Общий анализ крови", patient)
        {
            RefBloodTest = new RefBloodTest()
            {
                Date = DateTime.Now,
                RefPatientId = patient.Id,
                RefUserId = AppContainer.Instance.CurrentUser.Id
            };
        }
        protected override void PrintDoc(object obj)
        {
            if(CheckValid())
            {
                AppContainer.Instance.DocPrinter.PrintBloodTest(_patient, RefBloodTest);
            }
        }
        protected override void SaveServiceResult(object obj)
        {
            if(CheckValid())
            {
                AppContainer.Instance.SQLDataManager.AddBloodTest(RefBloodTest);
                AppContainer.Instance.NumPatients++;
                AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
            }
        }
        protected override bool CheckValid()
        {
            Errors = "";
            if(RefBloodTest.ESR <= 0)
            {
                Errors += "Не указано СОЭ через час\n";
            }
            if(RefBloodTest.HB <= 0)
            {
                Errors += "Не указан HB\n";
            }
            if (RefBloodTest.Leukocytes <= 0)
            {
                Errors += "Не указано количество лейкоцитов\n";
            }
            if (RefBloodTest.Erythrocytes <= 0)
            {
                Errors += "Не указано количество эритроцитов\n";
            }
            if (RefBloodTest.ErythrocyteIndex <= 0)
            {
                Errors += "Не указано эритроцитарный индекс\n";
            }

            return Errors == "";
        }

    }
}
