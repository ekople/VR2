﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace PtService.NhibernateImpl.DAOs
{
    public class HooneDAO : BaseDao
    {
        public HooneDAO(ISession session) : base(session)
        {
        }
    }
}