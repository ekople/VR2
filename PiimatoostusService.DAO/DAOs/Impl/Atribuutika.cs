using System;

namespace PtService.NhibernateImpl.DAOs.Impl
{
    public class Atribuutika
    {
        private int _ID;
        private string _Nimetus;
        private DateTime? _JargmineHooldusKP;
        private string _Kommentaar;
        private string _SeeriaNR_KereNR;
        private int? _Kood;
        private string _RegistriKood;
        private int? _MaxVeovoime;
        private string _VeovoimeYhikIndikaator;
        private AtribuutikaLiik _AtribuutikaLiikID;
        private string _Kategooria;

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

        public virtual DateTime? JargmineHooldusKP
        {
            get { return _JargmineHooldusKP; }
            set { _JargmineHooldusKP = value; }
        }

        public virtual string Kommentaar
        {
            get { return _Kommentaar; }
            set { _Kommentaar = value; }
        }

        public virtual string SeeriaNR_KereNR
        {
            get { return _SeeriaNR_KereNR; }
            set { _SeeriaNR_KereNR = value; }
        }

        public virtual int? Kood
        {
            get { return _Kood; }
            set { _Kood = value; }
        }

        public virtual string RegistriKood
        {
            get { return _RegistriKood; }
            set { _RegistriKood = value; }
        }

        public virtual int? MaxVeovoime
        {
            get { return _MaxVeovoime; }
            set { _MaxVeovoime = value; }
        }

        public virtual string VeovoimeYhikIndikaator
        {
            get { return _VeovoimeYhikIndikaator; }
            set { _VeovoimeYhikIndikaator = value; }
        }

        public virtual AtribuutikaLiik AtribuutikaLiikID
        {
            get { return _AtribuutikaLiikID; }
            set { _AtribuutikaLiikID = value; }
        }

        public virtual string Kategooria
        {
            get { return _Kategooria; }
            set { _Kategooria = value; }
        }
    }
}