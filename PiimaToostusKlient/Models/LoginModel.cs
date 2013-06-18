using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace PiimaToostusKlient.Models
{
    public class LoginModel : MsgModel
    {
        [Required]
        [Display(Name = "Kasutajanimi")]
        [StringLength(25, ErrorMessage = "{0} peab olema vähemalt {2} tähemärki!", MinimumLength = 4)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parool")]
        [StringLength(25, ErrorMessage = "{0} peab olema vähemalt {2} tähemärki!", MinimumLength = 4)]
        public string Password { get; set; }

        [Display(Name = "Jäta mind meelde")]
        public bool RememberMe { get; set; }
    }
}