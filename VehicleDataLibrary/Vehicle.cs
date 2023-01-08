using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
    // Abstract Vehicle class and implement IVehicle interface
    public abstract class Vehicle : IVehicle
    {

        //Member variables
        private string ownerName;
        private string contactNumber;
        private double vehicleAge;

        //Default Constructor
        public Vehicle()
        {
        }


        //Parameterized Constructor
        public Vehicle(string ownerName, string contactNumber, double vehicleAge)
        {
            this.ownerName = ownerName;
            this.contactNumber = contactNumber;
            this.vehicleAge = vehicleAge;
        }



        //Member Properties
        public string OwnerName { get => ownerName; set => ownerName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public double VehicleAge { get => vehicleAge; set => vehicleAge = value; }
        

        //Abstarct Method
        public abstract string GetSelectedServices();
      
        //public abstract string MaskContactNumber();
        public string MaskContactNumber()
        {
            StringBuilder stringBuilder = new StringBuilder(ContactNumber.ToString());

            for (int i = 2; i < 8; i++)
            {
                stringBuilder[i] = 'X';
            }
            return stringBuilder.ToString();
        }
    }
}
