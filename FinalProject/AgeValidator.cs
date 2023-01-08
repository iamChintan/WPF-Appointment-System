using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalProject
{
    //Validator class for vehicle age for GUI error
    public class AgeValidator : ValidationRule
    {
        //Member variables for min and max age
        double min;
        double max;

        //Property for set and get datas
        public double Min { get => min; set => min = value; }
        public double Max { get => max; set => max = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double numVal = 0;
            //If user enter characters into the age field
            if (!double.TryParse(value.ToString(), out numVal))
            {
                return new ValidationResult(false, "Wrong data, please give valid age");
            }

            //If user enters age beyond max value
            if (numVal < min || numVal > max)
            {
                return new ValidationResult(false, "Out of range, Vehicle's age shoud be between [0.2-150] years");
            }

            return ValidationResult.ValidResult;
        }

    }
}