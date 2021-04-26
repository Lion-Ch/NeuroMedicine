using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Classes
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsUseNeuralNetwork { get; set; }
        public string PathToNeuralNetwork { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
    }
}
