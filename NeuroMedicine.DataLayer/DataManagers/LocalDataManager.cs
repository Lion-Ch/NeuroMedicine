using System.Collections.Generic;
using DataLayer.Models.Classes;
using DataLayer.Models.PresentationVM;

namespace DataLayer.DataManagers
{
    /// <summary>
    /// Класс для выбора данных из кода
    /// </summary>
    public class LocalDataManager
    {
        public List<ListItem> GetDiagnosticTypes()
        {
            var dictionary = new DiagnosticTypeDictionary().Dictionary;
            List<ListItem> list = new List<ListItem>();
            foreach(var item in dictionary)
            {
                list.Add(new ListItem { Id = (int)item.Key, Name =  item.Value });
            }

            return list;
        }
    }
}
