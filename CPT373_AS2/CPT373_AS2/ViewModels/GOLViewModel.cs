using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CPT373_AS2.Models;
using FluentValidation.Attributes;

namespace CPT373_AS2.ViewModels
{

    //[Validator(typeof(UserGameValidator))]
    public partial class GOLViewModel
    {


        public UserTemplate UserTemplates { get; set; }
        public UserGame UserGame { get; set; }





    }
}