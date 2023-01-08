using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDataLibrary
{
   
    interface IVehicle 
    {

        //Interface Properties
        string OwnerName { get; set; }
        string ContactNumber { get; set; }
        double VehicleAge { get; set; }

        //Method declaration for gettting selected services
        string GetSelectedServices();
        //Method declaration for Mask Contact Number
        string MaskContactNumber();


    }
}
