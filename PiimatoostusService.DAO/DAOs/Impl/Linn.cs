using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Linn
    {
        private int _ID;
        private string _Nimetus;
        private Riik _RiikID;

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string Nimetus
        {
            get { return _Nimetus; }
            set { _Nimetus = value; }
        }

        public virtual Riik RiikID
        {
            get { return _RiikID; }
            set { _RiikID = value; }
        }
    }
}