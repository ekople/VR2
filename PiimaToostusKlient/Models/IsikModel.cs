using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiimaToostusKlient.Models
{
    public class IsikModel:MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string Eesnimi { get; set; }

        [Required]
        public string Perenimi { get; set; }

        [Required]
        public string Isikukood { get; set; }

        public string EMail { get; set; }

        public int? KontaktTelefon { get; set; }

        [Required]
        public string SynniKP { get; set; }
    }
}