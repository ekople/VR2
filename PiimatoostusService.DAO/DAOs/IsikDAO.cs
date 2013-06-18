using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using PtService.NhibernateImpl.DAOs.Impl;

namespace PtService.NhibernateImpl.DAOs
{
    public class IsikDAO : BaseDao
    {
        public IsikDAO(ISession session) : base(session)
        {
        }
    }
}