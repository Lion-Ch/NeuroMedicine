using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public class ServiceFactory
    {
        public static Service Create(RefService oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new Service();
            newOb.Id = oldObj.Id;
            newOb.IsUseNeuralNetwork = oldObj.IsUseNeuralNetwork;
            newOb.Name = oldObj.Name;
            newOb.PathToNeuralNetwork = oldObj.PathToNeuralNetwork;
            newOb.Price = oldObj.Price;
            newOb.Duration = oldObj.Duration;

            return newOb;
        }
        public static RefService Create(Service oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefService();
            newOb.Id = oldObj.Id;
            newOb.IsUseNeuralNetwork = oldObj.IsUseNeuralNetwork;
            newOb.Name = oldObj.Name;
            newOb.PathToNeuralNetwork = oldObj.PathToNeuralNetwork;
            newOb.Price = oldObj.Price;
            newOb.Duration = oldObj.Duration;

            return newOb;
        }
    }
}
