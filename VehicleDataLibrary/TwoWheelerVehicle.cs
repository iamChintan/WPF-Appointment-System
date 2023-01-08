using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
    //Child class of Vehicle
    public class TwoWheelerVehicle : Vehicle
    {
        //Enum for Services
        public enum TwoWheelerVehicleServices
        {
            OilChange = 1,
            RadiatorFlush = 2,
            TyresChange = 3,
            BrakesAndClutch = 4,
            SparkPlug = 5,
            Carburetor = 6,
            FuelPipes = 7
        }

        //Empty Constructor
        public TwoWheelerVehicle()
        {

        }

        public TwoWheelerVehicle(string ownerName, string contactNumber, double vehicleAge, TwoWheelerVehicleServices[] optedServices) : base(ownerName, contactNumber, vehicleAge)
        {
            serviceOpted = optedServices;
        }

       


        private TwoWheelerVehicleServices[] serviceOpted;
        public TwoWheelerVehicleServices[] ServiceOpted { get => serviceOpted; set => serviceOpted = value; }

     
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
