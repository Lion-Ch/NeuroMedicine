using DataLayer.SqlServer.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models.Factories
{
    public class SettingsFactory
    {
        public static RefSettings Create(Settings.Settings oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new RefSettings();
            newOb.Id = oldObj.Id;
            newOb.NameOrganization = oldObj.NameOrganization;
            newOb.PathToNeuro = oldObj.PathToNeuro;
            newOb.phoneOrgTag = oldObj.phoneOrgTag;
            newOb.rsOrgTag = oldObj.rsOrgTag;
            newOb.addressOrgTag = oldObj.addressOrgTag;
            newOb.bankOrgTag = oldObj.bankOrgTag;
            newOb.bikOrgTag = oldObj.bikOrgTag;
            newOb.cityOrgTag = oldObj.cityOrgTag;
            newOb.fioDirector = oldObj.fioDirector;
            newOb.indexOrgTag = oldObj.indexOrgTag;
            newOb.innOrgTag = oldObj.innOrgTag;
            newOb.ksOrgTag = oldObj.ksOrgTag;

            return newOb;
        }
        public static Settings.Settings Create(RefSettings oldObj)
        {
            if (oldObj == null) return null;

            var newOb = new Settings.Settings();
            newOb.Id = oldObj.Id;
            newOb.NameOrganization = oldObj.NameOrganization;
            newOb.PathToNeuro = oldObj.PathToNeuro;
            newOb.phoneOrgTag = oldObj.phoneOrgTag;
            newOb.rsOrgTag = oldObj.rsOrgTag;
            newOb.addressOrgTag = oldObj.addressOrgTag;
            newOb.bankOrgTag = oldObj.bankOrgTag;
            newOb.bikOrgTag = oldObj.bikOrgTag;
            newOb.cityOrgTag = oldObj.cityOrgTag;
            newOb.fioDirector = oldObj.fioDirector;
            newOb.indexOrgTag = oldObj.indexOrgTag;
            newOb.innOrgTag = oldObj.innOrgTag;
            newOb.ksOrgTag = oldObj.ksOrgTag;

            return newOb;
        }
    }
}
