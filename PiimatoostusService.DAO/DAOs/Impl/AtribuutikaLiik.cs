namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class AtribuutikaLiik
    {
        private int _ID;
        private string _Nimetus;
        private bool _IsikugaSeostatav;
        private string _Kommentaar;
        private string _Kirjeldus;

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

        public virtual bool IsikugaSeostatav
        {
            get { return _IsikugaSeostatav; }
            set { _IsikugaSeostatav = value; }
        }

        public virtual string Kommentaar
        {
            get { return _Kommentaar; }
            set { _Kommentaar = value; }
        }

        public virtual string Kirjeldus
        {
            get { return _Kirjeldus; }
            set { _Kirjeldus = value; }
        }
    }
}