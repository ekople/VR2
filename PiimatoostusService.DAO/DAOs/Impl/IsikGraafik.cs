using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class IsikGraafik
    {
        private int _ID;
        private Atribuutika _AtribuutikaID;
        private Osakond _OsakondID;
        private DateTime _AlgusKP;
        private DateTime? _LoppKP;
        private string _Kommentaar;
        private Isik _IsikID;

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual Atribuutika AtribuutikaID
        {
            get { return _AtribuutikaID; }
            set { _AtribuutikaID = value; }
        }

        public virtual Osakond OsakondID
        {
            get { return _OsakondID; }
            set { _OsakondID = value; }
        }

        public virtual DateTime AlgusKP
        {
            get { return _AlgusKP; }
            set { _AlgusKP = value; }
        }

        public virtual DateTime? LoppKP
        {
            get { return _LoppKP; }
            set { _LoppKP = value; }
        }

        public virtual string Kommentaar
        {
            get { return _Kommentaar; }
            set { _Kommentaar = value; }
        }

        public virtual Isik IsikID
        {
            get { return _IsikID; }
            set { _IsikID = value; }
        }
    }
}