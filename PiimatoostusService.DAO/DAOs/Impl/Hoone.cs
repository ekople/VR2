using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Hoone
    {
        private int _ID;
        private string _Nimetus;
        private string _Aadress;
        private int? _ValvelauaKontaktTelefon;
        private Linn _LinnID;

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

        public virtual string Aadress
        {
            get { return _Aadress; }
            set { _Aadress = value; }
        }

        public virtual int? ValvelauaKontaktTelefon
        {
            get { return _ValvelauaKontaktTelefon; }
            set { _ValvelauaKontaktTelefon = value; }
        }

        public virtual Linn LinnID
        {
            get { return _LinnID; }
            set { _LinnID = value; }
        }
    }
}