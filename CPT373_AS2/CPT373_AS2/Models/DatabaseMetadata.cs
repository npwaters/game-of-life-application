using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;



namespace CPT373_AS2.Models
{
    public class UserGameMetadata
    {

        [ScriptIgnore(ApplyToOverrides =true)]
        public virtual User User { get; set; }
    }


    [MetadataType(typeof(UserGameMetadata))]
    //[Validator(typeof(UserGameValidator))]
    public partial class UserGame
    { }
}