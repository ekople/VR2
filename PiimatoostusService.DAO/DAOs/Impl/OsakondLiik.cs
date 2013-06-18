using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class OsakondLiik
    {
        private int _ID;
        private string _Nimetus;
        private string _Kirjeldus;
        private string _Kommentaar;

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

        public virtual string Kirjeldus
        {
            get { return _Kirjeldus; }
            set { _Kirjeldus = value; }
        }

        public virtual string Kommentaar
        {
            get { return _Kommentaar; }
            set { _Kommentaar = value; }
        }
    }
}