using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiimaToostusKlient.PtServiceRef;

namespace PiimaToostusKlient.Models
{
    public class OsakonnadModel : MsgModel
    {
        public IList<Osakond> AllOsakonnad;
    }
}