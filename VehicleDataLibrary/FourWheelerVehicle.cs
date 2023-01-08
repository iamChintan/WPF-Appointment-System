using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
    //Child class of Vehicle class
    public class FourWheelerVehicle : Vehicle
    {
        //Enum for Services
        public enum FourWheelerVehicleServices
        {
            OilChange = 1,
            RadiatorFlush = 2,
            TyresChange = 3,
            BrakesAndClutch = 4,
            EngineDiagnostics = 5,
            BeltsAndHoses = 6,
            AirAndHydraulic = 7
        }

        //Empty Constructor
        public FourWheelerVehicle()
        {

        }

        public FourWheelerVehicle(string ownerName, string contactNumber, double vehicleAge, FourWheelerVehicleServices[] optedServices) : base(ownerName, contactNumber, vehicleAge)
        {
            serviceOpted = optedServices;
        }




        private FourWheelerVehicleServices[] serviceOpted;
        public FourWheelerVehicleServices[] ServiceOpted { get => serviceOpted; set => serviceOpted = value; }

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
