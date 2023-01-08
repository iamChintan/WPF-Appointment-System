using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleDataLibrary
{
    [XmlRoot("AppointmentList")]
    [XmlInclude(typeof(TwoWheelerVehicle))]
    [XmlInclude(typeof(ThreeWheelerVehicle))]
    [XmlInclude(typeof(FourWheelerVehicle))]
    [XmlInclude(typeof(HeavyDutyVehicle))]
    public class AppointmentList : IDisposable
    {
        //Generic Collection list of type Appointment class
        private List<Appointment> appointments;

        [XmlArray("Appointments")]
        [XmlArrayItem("Appointment", typeof(Appointment))]

        //Property for set and get data of List
        public List<Appointment> Appointments { get => appointments; set => appointments = value; }

        //Empty Constructor
        public AppointmentList()
        {
            //Initialization of list
            Appointments = new List<Appointment>();
        }

        //For add appointment
        public void Add(Appointment ap)
        {
            Appointments.Add(ap);
        }
        //For remove appointment
        public void Remove(Appointment ap)
        {
            Appointments.Remove(ap);
        }

        public void Sort()
        {
            Appointments.Sort();
        }

        //To get total count of list
        public int Count
        {
            get
            {
                return Appointments.Count;
            }
        }


        //For set appointment and get appointment
        public Appointment this[int i]
        {
            get
            {
                return Appointments[i];
            }
            set
            {
                Appointments[i] = value;
            }
        }

       
        public IEnumerator<Appointment> GetEnumerator()
        {
            return ((IEnumerable<Appointment>)Appointments).GetEnumerator();
        }

        //Disposable
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
