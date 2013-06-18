using System;
using System.Collections;
using System.Reflection;
using NHibernate;

namespace PtService.NhibernateImpl.DAOs
{
    public class BaseDao
    {
        internal ISession _session;

        public BaseDao(ISession session)
        {
            _session = session;
        }

        public object Load(int Id, Type type)
        {
            return _session.Get(type, Id);
        }

        public virtual void Save(object obj)
        {
            _session.Save(obj);
            _session.Flush();
        }

        public void Delete(object obj)
        {
            _session.Delete(obj);
            _session.Flush();
        }

        public IList LoadAll(Type type)
        {
            IList objects = _session.CreateCriteria(type).SetFetchMode("HooneID", NHibernate.FetchMode.Eager).List();
            //IList objects = _session.CreateQuery("from " + type.Name).List();
            return objects;
        }

        public virtual void Update(object obj, object id)
        {
            doRecursUpdate(obj);
            _session.Update(obj, id);
            _session.Flush();
        }

        private void doRecursUpdate(object obj)
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (property.PropertyType.IsClass && !property.PropertyType.Equals(typeof(string)))
                {
                    object focusObj = property.GetValue(obj, null);
                    doRecursUpdate(focusObj);
                    _session.Update(focusObj);
                    _session.Flush();
                }
            }
        }
    }
}