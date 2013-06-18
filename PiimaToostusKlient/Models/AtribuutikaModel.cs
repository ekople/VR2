using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace PiimaToostusKlient.Models
{
    public class AtribuutikaModel:MsgModel
    {
        public int? ID { get; set; }

        [Required]
        public string Nimetus { get; set; }

        [Required]
        public string JargmineHooldusKP { get; set; }

        public string Kommentaar { get; set; }

        [Required]
        public string SeeriaNR_KereNR { get; set; }

        public int? Kood { get; set; }

        public string RegistriKood { get; set; }

        public int? MaxVeovoime { get; set; }

        public string VeovoimeYhikIndikaator { get; set; }

        [Required]
        public int? AtribuutikaLiikID { get; set; }

        public SelectList AtribuutikaLiigid { get; set; }

        public string Kategooria { get; set; }
    }
}