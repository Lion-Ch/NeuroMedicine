using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuroMedicine.BusinessLayer.ViewModels.ServicesVM
{
    public class BloodTestServiceVM : BaseServiceResultVM
    {
        
        public RefBloodTest RefBloodTest { get; set; }
        public BloodTestServiceVM(Patient patient) : base("Общий анализ крови", patient)
        {
            RefBloodTest = new RefBloodTest();
        }

        protected override void SaveServiceResult(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
