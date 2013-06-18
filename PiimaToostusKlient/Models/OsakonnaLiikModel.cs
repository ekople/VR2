using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class OsakonnaLiikModel : MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string Nimetus { get; set; }

        public string Kirjeldus { get; set; }

        public string Kommentaar { get; set; }
    }
}