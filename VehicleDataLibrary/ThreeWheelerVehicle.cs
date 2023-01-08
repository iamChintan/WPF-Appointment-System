using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
    public class ThreeWheelerVehicle : Vehicle
    {
        //Enum for Services
        public enum ThreeWheelerVehicleServices
        {
            OilChange = 1,
            RadiatorFlush = 2,
            TyresChange = 3,
            BrakesAndClutch = 4,
            ExhaustSystems = 5,
            GasKitRepair = 6,
            Grease = 7
        }

        //Empty Constructor
        public ThreeWheelerVehicle()
        {

        }

        public ThreeWheelerVehicle(string ownerName, string contactNumber, double vehicleAge, ThreeWheelerVehicleServices[] optedServices) : base(ownerName, contactNumber, vehicleAge)
        {
            serviceOpted = optedServices;
        }




        private ThreeWheelerVehicleServices[] serviceOpted;
        public ThreeWheelerVehicleServices[] ServiceOpted { get => serviceOpted; set => serviceOpted = value; }


        //function for selected services
        public override string GetSelectedServices()
        {
            StringBuilder service = new StringBuilder();
            foreach (var item in ServiceOpted)
            {
                service.Append(item + ", ");
            }
            service.Remove(service.Length - 2, 1);
            return service.ToString();
        }
    }
}
