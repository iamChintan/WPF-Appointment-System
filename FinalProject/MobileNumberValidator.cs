using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalProject
{
    //Validator class for mobile number field for GUI error
    class MobileNumberValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //Checking if mobile number is exact 10 digits or not
            if ((value.ToString().Trim().Length) != 10)
            {
                return new ValidationResult(false, "Invalid Mobile Number, Enter 10 digit Number");
            }

            return ValidationResult.ValidResult;
        }

    }
}
