using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Isik
    {
        private int _ID;
        private string _Eesnimi;
        private string _Perenimi;
        private string _Isikukood;
        private string _EMail;
        private int _KontaktTelefon;
        private DateTime? _SynniKP;

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string Eesnimi
        {
            get { return _Eesnimi; }
            set { _Eesnimi = value; }
        }

        public virtual string Perenimi
        {
            get { return _Perenimi; }
            set { _Perenimi = value; }
        }

        public virtual string Isikukood
        {
            get { return _Isikukood; }
            set { _Isikukood = value; }
        }

        public virtual string EMail
        {
            get { return _EMail; }
            set { _EMail = value; }
        }

        public virtual int KontaktTelefon
        {
            get { return _KontaktTelefon; }
            set { _KontaktTelefon = value; }
        }

        public virtual DateTime? SynniKP
        {
            get { return _SynniKP; }
            set { _SynniKP = value; }
        }
    }
}