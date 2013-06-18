using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using PtService.NhibernateImpl.DAOs;

namespace PtService.NhibernateImpl
{
    public class DaoConnContext
    {
        private static ISession _session;
        private ISessionFactory _sessionFactory;
        private string _connString;

        public AtribuutikaDAO _AtribuutikaDao;
        public AtribuutikaLiikDAO _AtribuutikaLiikDAO;
        public HooneDAO _HooneDAO;
        public IsikDAO _IsikDAO;
        public IsikGraafikDAO _IsikGraafikDAO;
        public KasutajaDAO _KasutajaDAO;
        public LinnDAO _LinnDAO;
        public OsakondDAO _OsakondDAO;
        public OsakondLiikDAO _OsakondLiikDAO;
        public RiikDAO _RiikDAO;
        public RollDAO _RollDAO;

        public DaoConnContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString) || string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("NHibernate andmebaasi ühenduse ühendussõne ei saa olla väärtuseta!");
            }
            _connString = connectionString;
            _sessionFactory = configure(connectionString);
            _session = createSession(_sessionFactory);
            initDaos(_session);
        }

        private void initDaos(ISession session)
        {
            _AtribuutikaDao = new AtribuutikaDAO(session);
            _AtribuutikaLiikDAO = new AtribuutikaLiikDAO(session);
            _HooneDAO = new HooneDAO(session);
            _IsikDAO = new IsikDAO(session);
            _IsikGraafikDAO = new IsikGraafikDAO(session);
            _KasutajaDAO = new KasutajaDAO(session);
            _LinnDAO = new LinnDAO(session);
            _OsakondDAO = new OsakondDAO(session);
            _OsakondLiikDAO = new OsakondLiikDAO(session);
            _RiikDAO = new RiikDAO(session);
            _RollDAO = new RollDAO(session);
        }

        public DaoConnContext CheckDBConn()
        {
            //kui ühendus on mingil põhjusel kinni pandud initsialiseeritakse see uuesti
            if (_session == null || !_session.IsOpen)
            {
                return new DaoConnContext(_connString);
            }
            else
            {
                return this;
            }
        }

        private ISessionFactory configure(string connectionString)
        {
            ISessionFactory sessionFactory;

            var configuration = new Configuration();
            configuration.SetProperty("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            configuration.SetProperty("dialect", "NHibernate.Dialect.MsSql2005Dialect");
            configuration.SetProperty("connection.driver_class", "NHibernate.Driver.SqlClientDriver");
            configuration.SetProperty("connection.connection_string", connectionString);
            configuration.AddAssembly("PtService.NhibernateImpl");

            try
            {
                sessionFactory = configuration.BuildSessionFactory();
            }
            catch (Exception e)
            {
                throw new Exception("NHibernate sessioonilooja koostamisel tekkis tõrge: " + e.Message);
            }
            return sessionFactory;
        }

        private ISession createSession(ISessionFactory sessFactory)
        {
            if (sessFactory == null)
            {
                throw new Exception("NHibernate sessioonilooja initsialiseerimata");
            }

            ISession session;

            try
            {
                session = sessFactory.OpenSession();
            }
            catch (Exception e)
            {
                throw new Exception("Nhibernate sessiooni loomine ebaõnnestus! " + e.Message);
            }

            session.FlushMode = FlushMode.Commit;
            return session;
        }
    }
}