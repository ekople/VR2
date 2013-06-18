using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace PtService.NhibernateImpl.DAOs
{
    public class AtribuutikaDAO : BaseDao
    {
        public AtribuutikaDAO(ISession session) : base(session)
        {
        }
    }
}