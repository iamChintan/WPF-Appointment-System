using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleDataLibrary
{

    [XmlRoot("AppointmentLists")]
    [XmlInclude(typeof(TwoWheelerVehicle))]
    [XmlInclude(typeof(ThreeWheelerVehicle))]
    [XmlInclude(typeof(FourWheelerVehicle))]
    [XmlInclude(typeof(HeavyDutyVehicle))]
    public class Appointment
    {
        //Member variables
        private string appointmentTime;
        //Variable of type Vehicle class
        private Vehicle vehicle;

        //Member Properties
        public string AppointmentTime { get => appointmentTime; set => appointmentTime = value; }
        public Vehicle Vehicle { get => vehicle; set => vehicle = value; }

   
    }
}
