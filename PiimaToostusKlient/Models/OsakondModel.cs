using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PiimaToostusKlient.Models
{
    public class OsakondModel: MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string Nimetus { get; set; }

        [Required]
        public string AlgusKP { get; set; }

        public string LoppKP { get; set; }

        [Required]
        public int? HooneID { get; set; }

        public SelectList Hooned { get; set; }

        [Required]
        public int? OsakondLiik { get; set; }

        public SelectList OsakondLiigid { get; set; }
    }
}