using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiimaToostusKlient.PtServiceRef;

namespace PiimaToostusKlient.Models
{
    public class GraafikudModel : MsgModel
    {
        public IList<IsikGraafik> AllIsikGraafikud;
    }
}