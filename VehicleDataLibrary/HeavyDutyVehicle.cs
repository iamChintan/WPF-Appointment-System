using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
    //Child class of Vehicle class
    public class HeavyDutyVehicle : Vehicle
    {
        //Enum for Services
        public enum HeavyDutyVehicleServices
        {
            OilChange = 1,
            RadiatorFlush = 2,
            TyresChange = 3,
            BrakesAndClutch = 4,
            HydraulicFilter = 5,
            RotaryShaft = 6,
            ExcavatorPump = 7
        }

        //Empty Constructor
        public HeavyDutyVehicle()
        {

        }

        public HeavyDutyVehicle(string ownerName, string contactNumber, double vehicleAge, HeavyDutyVehicleServices[] optedServices) : base(ownerName, contactNumber, vehicleAge)
        {
            serviceOpted = optedServices;
        }




        private HeavyDutyVehicleServices[] serviceOpted;
        public HeavyDutyVehicleServices[] ServiceOpted { get => serviceOpted; set => serviceOpted = value; }


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
