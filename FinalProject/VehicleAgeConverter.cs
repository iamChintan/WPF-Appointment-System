using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using VehicleDataLibrary;

namespace FinalProject
{
    //Vehicle's age Conveters class  for DataGrid
    public class VehicleAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Vehicle)
            {
                //If vehicle's age is greater than 20 then show datas in the red color into DataGrid or else data will be in the white color
                if (((Vehicle)value).VehicleAge > 20)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.White;
                }
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

     
    }
}