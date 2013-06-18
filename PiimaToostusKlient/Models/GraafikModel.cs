using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class GraafikModel : MsgModel
    {
        public int? ID { get; set; }

        public int? AtribuutikaID { get; set; }

        public SelectList Atribuutikad { get; set; }

        [Required]
        public int? OsakondID { get; set; }

        public SelectList Osakonnad { get; set; }

        [Required]
        public string AlgusKP { get; set; }

        public string LoppKP { get; set; }

        public string Kommentaar { get; set; }

        [Required]
        public int? IsikID { get; set; }

        public SelectList Isikud { get; set; }
    }
}