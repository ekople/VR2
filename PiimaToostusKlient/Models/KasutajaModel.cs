using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class KasutajaModel : MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string KasutajaNimi { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string AlgusKP { get; set; }

        public string LoppKP { get; set; }

        [Required]
        public int? RollID { get; set; }

        public SelectList RollList { get; set; }

        [Required]
        public int? IsikID { get; set; }

        public SelectList Isikud { get; set; }
    }
}