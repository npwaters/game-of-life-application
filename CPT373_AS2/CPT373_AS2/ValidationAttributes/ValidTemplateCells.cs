using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CPT373_AS2.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidTemplateCells : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                //if ((string)value != "hello")
                if(!checkCellString((string)value))
                {
                    return new ValidationResult("failed cell validation!");
                }
            }

            return ValidationResult.Success;



        }




        public static bool checkCellString
            (string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] != 'O') && (input[i] != 'X'))
                {
                    return false;
                }

            }
            return true;
        }
    }
}