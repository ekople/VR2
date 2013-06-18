using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class RollidModel : MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string Nimetus { get; set; }

        [Required]
        public string Kirjeldus { get; set; }
    }
}