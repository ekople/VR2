using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Kasutaja
    {
        private int _ID;
        private string _KasutajaNimi;
        private string _PsswdHash;
        private DateTime _AlgusKP;
        private DateTime? _LoppKP;
        private Roll _RollID;
        private Isik _IsikID;
        private string _SessionHandle;
        private DateTime? _SessionValidTo;

        public virtual int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public virtual string KasutajaNimi
        {
            get { return _KasutajaNimi; }
            set { _KasutajaNimi = value; }
        }

        public virtual string PsswdHash
        {
            get { return _PsswdHash; }
            set { _PsswdHash = value; }
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

        public virtual Roll RollID
        {
            get { return _RollID; }
            set { _RollID = value; }
        }

        public virtual Isik IsikID
        {
            get { return _IsikID; }
            set { _IsikID = value; }
        }

        public virtual string SessionHandle
        {
            get { return _SessionHandle; }
            set { _SessionHandle = value; }
        }

        public virtual DateTime? SessionValidTo
        {
            get { return _SessionValidTo; }
            set { _SessionValidTo = value; }
        }
    }
}