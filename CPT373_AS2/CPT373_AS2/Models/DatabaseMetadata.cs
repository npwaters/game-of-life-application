using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using FluentValidation;
using FluentValidation.Attributes;
using CPT373_AS2.ValidationAttributes;



namespace CPT373_AS2.Models
{
    public class UserGameMetadata
    {
        [Required, StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class UserMetadata
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }


    public class UserTemplateMetadata
    {
        //[Required, StringLength(50, MinimumLength = 3)]
        //public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a number")]
        [Range(1, 15)]
        public int Height { get; set; }
        [Required(ErrorMessage = "Please enter a number")]
        [Range(1, 15)]
        public int Width { get; set; }




        [Required, DataType(DataType.MultilineText)]
        //[ValidTemplateCells]
        public string Cells { get; set; }
    }


    public class UserTemplateValidator : AbstractValidator<UserTemplate>
    {
        public UserTemplateValidator()
        {
            //RuleFor(p => p.Width)
            //    //.Must(p => int.Parse(p)).WithMessage("Must be a number");
            //    .InclusiveBetween(1, 15)
            //    .WithMessage("must be a number");

            //RuleFor(t => t.Name)
            //    .Must(name => !name.Contains("xxx"))
            //    .WithMessage("xxx not allowed!");
            //RuleFor(template => template.Cells)
            //    .NotEmpty().WithMessage("Cells is required!");
            //RuleFor(template => template.Cells)
            //    .Must(c => c == "hello") 
            //    .WithMessage("must be 'y'");

            RuleFor(template => template.Cells).Must(ContainValidChars).WithMessage("Only 'X' or 'O' allowed");
            RuleFor(template => template)
                .Must(AreCellsDimensionsValid)
                .WithMessage("Cell dimensions not valid!");
        }


        private bool ContainValidChars(string cells)
        {
            return checkCellString(cells);
        }

        public static bool checkCellString
            (string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != '\n' && input[i] != '\r')
                {
                    if ((input[i] != 'O') && (input[i] != 'X'))
                    {
                        return false;
                    }
                }


            }
            return true;
        }


        private bool AreCellsDimensionsValid(UserTemplate template)
        {

            
            string[] lines = template.Cells.Split
                (new string[] { Environment.NewLine }, StringSplitOptions.None);


            if (lines.Length != template.Height)
            {
                return false;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length != template.Width)
                {
                    return false;
                }
            }


            return true;


        }





    }


    [MetadataType(typeof(UserGameMetadata))]
    //[Validator(typeof(UserGameValidator))]
    public partial class UserGame
    { }

    [MetadataType(typeof(UserMetadata))]
    public partial class User
    { }


    [MetadataType(typeof(UserTemplateMetadata))]
    [Validator(typeof(UserTemplateValidator))]
    public partial class UserTemplate
    { }
}