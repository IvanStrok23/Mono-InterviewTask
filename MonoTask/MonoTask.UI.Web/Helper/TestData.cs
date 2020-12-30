using System;
using System.Collections.Generic;
using POCO = MonoTask.Core.Entities;


namespace MonoTask.UI.Web.Helper
{
    public class TestData
    {
        public List<POCO.VehicleMake> MakeList { get { return getHardCodedMakes(); } private set {} }



        private List<POCO.VehicleMake> getHardCodedMakes()
        {
            List<POCO.VehicleMake> tempList = new List<POCO.VehicleMake>();
            tempList.Add(new POCO.VehicleMake() { Name = "DOK-ING", Country = "Croatia", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Alfa Romeo", Country = "Italy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Dacia", Country = "Romania", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "FAP", Country = "Serbia", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Aston Martin", Country = "United Kingdom", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Bentley", Country = "United Kingdom", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Audi", Country = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "BMW", Country = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "MAN", Country = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Opel", Country = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });

            tempList.Add(new POCO.VehicleMake() { Name = "Volkswagen", Country = "Germany", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "SAZ", Country = "Uzbekistan", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            tempList.Add(new POCO.VehicleMake() { Name = "Thaco", Country = "Vietnam", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
            return tempList;
        }

        public List<POCO.VehicleModel> GetHardCodedModelsByMakeName(string name,int makeId)
        {
            List<POCO.VehicleModel> tempList = new List<POCO.VehicleModel>();

            switch (name)
            {
                case "DOK-ING":
                    tempList.Add(new POCO.VehicleModel() { Name = "MV-4", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "MV-10", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "MVF-5", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Alfa Romeo":
                    tempList.Add(new POCO.VehicleModel() { Name = "147", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "8C Competizione", MakeId = makeId, Year = 2009, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "GT", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Spider", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Dacia":
                    tempList.Add(new POCO.VehicleModel() { Name = "Logan", MakeId = makeId, Year = 2012, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Logan MCV", MakeId = makeId, Year = 2013, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Sandero", MakeId = makeId, Year = 2012, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "FAP":
                    tempList.Add(new POCO.VehicleModel() { Name = "FAP 1118", MakeId = makeId, Year = 2005, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "FAP 2228", MakeId = makeId, Year = 2006, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "FAP 3240", MakeId = makeId, Year = 2007, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Aston Martin":
                    tempList.Add(new POCO.VehicleModel() { Name = "Vantage", MakeId = makeId, Year = 2010, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "DB11", MakeId = makeId, Year = 2011, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "DBS SUPERLEGGERA", MakeId = makeId, Year = 2012, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Bentley":
                    tempList.Add(new POCO.VehicleModel() { Name = "Mulsanne", MakeId = makeId, Year = 2018, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Continental", MakeId = makeId, Year = 2019, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Bentayga", MakeId = makeId, Year = 2020, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Audi":
                    tempList.Add(new POCO.VehicleModel() { Name = "A4", MakeId = makeId, Year = 2001, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "A6", MakeId = makeId, Year = 2003, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "A8", MakeId = makeId, Year = 2008, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "BMW":
                    tempList.Add(new POCO.VehicleModel() { Name = "X1", MakeId = makeId, Year = 2008, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Z4", MakeId = makeId, Year = 2018, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "X7", MakeId = makeId, Year = 2019, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "MAN":
                    tempList.Add(new POCO.VehicleModel() { Name = "Lion's Coach", MakeId = makeId, Year = 2012, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Lion's City 12 E", MakeId = makeId, Year = 2009, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "TGE transporter van", MakeId = makeId, Year = 2005, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Opel":
                    tempList.Add(new POCO.VehicleModel() { Name = "Corsa", MakeId = makeId, Year = 2005, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Grandland X", MakeId = makeId, Year = 2008, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "MOKKA-E", MakeId = makeId, Year = 2021, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Volkswagen":
                    tempList.Add(new POCO.VehicleModel() { Name = "Tiguan", MakeId = makeId, Year = 2002, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Passat", MakeId = makeId, Year = 2003, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "Jetta", MakeId = makeId, Year = 2017, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "SAZ":
                    tempList.Add(new POCO.VehicleModel() { Name = "LE 60", MakeId = makeId, Year = 2004, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                case "Thaco":
                    tempList.Add(new POCO.VehicleModel() { Name = "DUMP SEMI TRAILER", MakeId = makeId, Year = 2005, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    tempList.Add(new POCO.VehicleModel() { Name = "STAKE CARGO SEMI TRAILER", MakeId = makeId, Year = 2015, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                    break;
                default:
                    break;
            }
           
            return tempList;
        }


    }
}