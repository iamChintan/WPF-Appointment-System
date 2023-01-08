using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDataLibrary;

namespace FinalProject
{
    
    public class BookedAppointments : Vehicle
    {
        //Member variables 
        private string selectedServices;
        private string vehicleType;
        private string appointmentTime;

        //Member properties 
        public string SelectedServices { get => selectedServices; set => selectedServices = value; }
        public string VehicleType { get => vehicleType; set => vehicleType = value; }
        public string AppointmentTime { get => appointmentTime; set => appointmentTime = value; }

        //Function for get selected services
        public override string GetSelectedServices()
        {
            throw new NotImplementedException();
        }
    }
}
