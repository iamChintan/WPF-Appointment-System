using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FinalProject
{
    //Validator class for owner name for GUI error
    class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            //Checking if owner name is greater than 2 digit or not
            if ((value.ToString().Trim().Length) < 2)
            {
                return new ValidationResult(false, "Please Enter Name");
            }
            else if ((value.ToString().Trim().Length) >= 2)
            {
                //RegularExpressions only for getting characters.
                //this RegularExpressions will not accept any special character, letter or whitspace
                Regex rgx = new Regex("[^A-Za-z]");
                bool containsSpecialCharacter = rgx.IsMatch(value.ToString());
                if (containsSpecialCharacter)
                {
                    return new ValidationResult(false, "Special characters/Whitespace not allowed!"); ;
                }

            }

            return ValidationResult.ValidResult;
        }
    }
}
