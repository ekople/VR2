using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class HooneModel : MsgModel
    {

        public int? ID { get; set; }

        [Required]
        public string Nimetus { get; set; }

        [Required]
        public string Aadress { get; set; }

        public int? ValvelauaKontaktTelefon { get; set; }

        [Required]
        public int? LinnID { get; set; }

        public SelectList Linnad { get; set; }
    }
}