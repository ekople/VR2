using NHibernate;
using NHibernate.Criterion;
using PtService.NhibernateImpl.DAOs.Impl;

namespace PtService.NhibernateImpl.DAOs
{
    public class KasutajaDAO : BaseDao
    {
        public KasutajaDAO(ISession session) : base(session)
        {
        }

        public Kasutaja GetKasutaja(string userName, string psswdHash)
        {
            ICriteria criteria = _session.CreateCriteria(typeof(Kasutaja))
                                         .Add(Restrictions.Eq("KasutajaNimi", @userName))
                                         .Add(Restrictions.Eq("PsswdHash", @psswdHash));
            return (Kasutaja)criteria.UniqueResult();
        }

        public Kasutaja GetKasutaja(string validationHandle)
        {
            ICriteria criteria = _session.CreateCriteria(typeof (Kasutaja))
                                         .Add(Restrictions.Eq("SessionHandle", @validationHandle));
            return criteria.UniqueResult<Kasutaja>();
        }
    }
}