using MonoTask.Infrastructure.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoTask.UI.Web.Helper
{
    public class TestData
    {
        public List<VehicleMake> MakeList { get { return getHardCodedMakes(); } private set {} }



        private List<VehicleMake> getHardCodedMakes()
        {
            List<VehicleMake> tempList = new List<VehicleMake>();
            return tempList;
        }
    }
}