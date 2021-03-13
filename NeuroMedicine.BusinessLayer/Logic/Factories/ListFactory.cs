using BusinessLayer.ViewModels.PresentationVM;
using System;
using System.Collections.Generic;
using DataLayer.Models.Classes;

namespace BusinessLayer.Logic.Factories
{
    internal class ListFactory
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
