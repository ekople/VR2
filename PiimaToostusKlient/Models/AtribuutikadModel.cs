using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PiimaToostusKlient.PtServiceRef;

namespace PiimaToostusKlient.Models
{
    public class AtribuutikadModel : MsgModel
    {
        public IList<Atribuutika> AllAtribuutikad;
    }
}