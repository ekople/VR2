using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Osakond
    {
        private int _ID;
        private string _Nimetus;
        private DateTime? _AlgusKP;
        private DateTime? _LoppKP;
        private Hoone _HooneID;
        private OsakondLiik _OsakondLiikID;

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

        public virtual DateTime? AlgusKP
        {
            get { return _AlgusKP; }
            set { _AlgusKP = value; }
        }

        public virtual DateTime? LoppKP
        {
            get { return _LoppKP; }
            set { _LoppKP = value; }
        }

        public virtual Hoone HooneID
        {
            get { return _HooneID; }
            set { _HooneID = value; }
        }

        public virtual OsakondLiik OsakondLiikID
        {
            get { return _OsakondLiikID; }
            set { _OsakondLiikID = value; }
        }
    }
}