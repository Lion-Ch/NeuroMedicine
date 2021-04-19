using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using NeuroMedicine.BusinessLayer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels.ServicesVM
{
    public class UrineTestServiceVM: BaseServiceResultVM
    {
        public RefUrineTest RefUrineTest { get; set; }
        public UrineTestServiceVM(Patient patient) : base("Анализ мочи", patient)
        {
            RefUrineTest = new RefUrineTest()
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
                AppContainer.Instance.DocPrinter.PrintUrineTest(_patient, RefUrineTest);
            }
        }
        protected override void SaveServiceResult(object obj)
        {
            if(CheckValid())
            {
                AppContainer.Instance.SQLDataManager.AddUrineTest(RefUrineTest);
            }
        }
        protected override bool CheckValid()
        {
            Errors = "";
            if (String.IsNullOrEmpty(RefUrineTest.Color))
            {
                Errors += "Не указано цвет\n";
            }
            if (String.IsNullOrEmpty(RefUrineTest.Transparency))
            {
                Errors += "Не указана прозрачность\n";
            }
            if (RefUrineTest.SpecificGravity == 0)
            {
                Errors += "Не указан удельный вес\n";
            }
            if (String.IsNullOrEmpty(RefUrineTest.Reaction))
            {
                Errors += "Не указана реакция\n";
            }

            return Errors == "";
        }
    }
}
