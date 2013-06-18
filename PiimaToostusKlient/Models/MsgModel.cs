using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiimaToostusKlient.Models
{
    public class MsgModel
    {
        public UserMessages UserMsg;

        public class UserMessages
        {
            public enum MsgType
            {
                Teade,
                Kinnitus,
                Viga
            };

            public MsgType MessageType { get; set; }
            public string Pealkiri { get; set; }
            public string Msg { get; set; }
        }
    }
}