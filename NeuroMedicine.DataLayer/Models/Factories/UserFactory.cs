using DataLayer.Models.Classes;
using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    internal static class UserFactory
    {
        public static RefUser Create(User oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefUser();
            newOb.Id = oldObj.Id;
            newOb.FullName = oldObj.FullName;
            newOb.UserType = oldObj.UserType;

            return newOb;
        }

        public static User Create(RefUser oldObj)
        {
            if (oldObj == null) return null;

            var newObj = new User();
            newObj.Id = oldObj.Id;
            newObj.FullName = oldObj.FullName;
            newObj.UserType = oldObj.UserType;
            newObj.UserTypeName = UserTypeDictionary.GetString(oldObj.UserType);

            return newObj;
        }
    }
}
