using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FinalProject
{
    //Conveter class for vehicle's age for GUI error
    [ValueConversion(typeof(double), typeof(Brush))]
    public class AgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            double i = double.Parse(value.ToString());
            //If age is greater than 150 then make brush's color red else make color black 
            if (i < 150)
            {
                return Brushes.Black;
            }
            else
            {
                return Brushes.Red;
            }
        }

     

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
