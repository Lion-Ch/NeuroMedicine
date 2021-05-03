using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels.ServicesVM
{
    public class MeansurmentVM : BaseServiceResultVM
    {
        public RefMeasurment RefMeasurment { get; set; }
        public MeansurmentVM(Patient patient) : base("Измерение базовых показателей", patient)
        {
            RefMeasurment = new RefMeasurment()
            {
                Date = DateTime.Now,
                RefPatientId = patient.Id,
                RefUserId = AppContainer.Instance.CurrentUser.Id
            };
        }

        protected override void SaveServiceResult(object obj)
        {
            if (CheckValid())
            {
                AppContainer.Instance.SQLDataManager.AddMeansurment(RefMeasurment);
                AppContainer.Instance.NumPatients++;
                AppContainer.Instance.ViewNavigator.NavigateToView(new PersonalCabinetVM());
            }
        }
        protected override bool CheckValid()
        {
            Errors = "";
            if(!RefMeasurment.Height.HasValue && !RefMeasurment.Weight.HasValue && !RefMeasurment.Pulse.HasValue && !RefMeasurment.PressureSystolic.HasValue && !RefMeasurment.PressureDiastolic.HasValue)
            {
                Errors += "Укажите хотя бы одно измерение\n";
            }
            else
            {
                if (RefMeasurment.Height.HasValue && RefMeasurment.Height <= 50)
                {
                    Errors += "Указан неправильный рост\n";
                }
                if (RefMeasurment.Weight.HasValue && RefMeasurment.Weight <= 0)
                {
                    Errors += "Указан неправильный вес\n";
                }
                if (RefMeasurment.Pulse.HasValue && RefMeasurment.Pulse <= 0)
                {
                    Errors += "Указан неправильный пульс\n";
                }
                if (RefMeasurment.PressureSystolic.HasValue && RefMeasurment.PressureSystolic <= 0)
                {
                    Errors += "Указано неправильное систолическое давление\n";
                }
                if (RefMeasurment.PressureDiastolic.HasValue && RefMeasurment.PressureDiastolic.Value <= 0)
                {
                    Errors += "Указан неправильный диастолическое давление\n";
                }
            }
            
            return Errors == "";
        }
    }
}
