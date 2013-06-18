using System;
using PtService.NhibernateImpl;

namespace PiimaToostusService
{
    public class ValidationUtil
    {
        public static void ValidateAtribuutika(Atribuutika atribuutika)
        {
            if (atribuutika == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(atribuutika.Nimetus))
            {
                throw new Exception("Atribuutika nimetuse sisestamine on kohustuslik!");
            }
            if (atribuutika.JargmineHooldusKP == null)
            {
                throw new Exception("Atribuutika järgmise hoolduse kuupäeva sisestamine on kohustuslik!");
            }
            if (Utils.IsNullOrEmptyWhitespace(atribuutika.SeeriaNR_KereNR))
            {
                throw new Exception("Atribuutika seeria/kere numbri sisestamine on kohustuslik!");
            }
            if (atribuutika.Nimetus.Length > 150)
            {
                throw new Exception("Atribuutika nimi saab olla vaid 150 tähemärki!");
            }
            if (atribuutika.RegistriKood != null && atribuutika.RegistriKood.Length > 20)
            {
                throw new Exception("Atribuutika registrikood saab olla vaid 20 tähemärki!");
            }
            if (atribuutika.VeovoimeYhikIndikaator.Length > 3)
            {
                throw new Exception("Atribuutika veovõime/tootmismahu ühik saab olla vaid 3 tähemärki!");
            }
            if (atribuutika.AtribuutikaLiikID == null)
            {
                throw new Exception("Atribuutika liigi sisestamine on kohustuslik!");
            }
            if (atribuutika.Kategooria != null && atribuutika.Kategooria.Length > 5)
            {
                throw new Exception("Atribuutika kategooria saab olla vaid 5 tähemärki!");
            }
        }

        public static void ValidateAtribuutikaLiik(AtribuutikaLiik atribuutikaLiik)
        {
            if (atribuutikaLiik == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(atribuutikaLiik.Nimetus))
            {
                throw new Exception("Atribuutika liigi nimetuse sisestamine on kohustuslik!");
            }
            if (atribuutikaLiik.Nimetus.Length > 150)
            {
                throw new Exception("Atribuutika liigi nimetus saab olla vaid 150 tähemärki!");
            }

            if (atribuutikaLiik.Kirjeldus != null && atribuutikaLiik.Kirjeldus.Length > 300)
            {
                throw new Exception("Atribuutika liigi kirjeldus saab olla vaid 300 tähemärki!");
            }
        }

        public static void ValidateHoone(Hoone hoone)
        {
            if (hoone == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(hoone.Nimetus))
            {
                throw new Exception("Hoone nimetuse sisestamine on kohustuslik!");
            }
            if (hoone.Nimetus.Length > 150)
            {
                throw new Exception("Hoone nimi saab olla vaid 150 tähemärki!");
            }
            if (Utils.IsNullOrEmptyWhitespace(hoone.Aadress))
            {
                throw new Exception("Hoone aadressi sisestamine on kohustuslik!");
            }
            if (hoone.Aadress.Length > 200)
            {
                throw new Exception("Hoone aadress saab olla vaid 200 tähemärki!");
            }
            if (hoone.LinnID == null)
            {
                throw new Exception("Hoone linna sisestamine on kohustuslik!");
            }
        }

        public static void ValidateIsik(Isik isik)
        {
            if (isik == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(isik.Eesnimi))
            {
                throw new Exception("Isiku eesnime sisestamine on kohustuslik!");
            }
            if (Utils.IsNullOrEmptyWhitespace(isik.Perenimi))
            {
                throw new Exception("Isiku perenime sisestamine on kohustuslik!");
            }
            if (Utils.IsNullOrEmptyWhitespace(isik.Isikukood))
            {
                throw new Exception("Isiku isikukoodi sisestamine on kohustuslik!");
            }
            if (isik.SynniKP == null)
            {
                throw new Exception("Isiku sünnikuupäeva sisestamine on kohustuslik!");
            }
            if (isik.Eesnimi.Length > 40)
            {
                throw new Exception("Isiku eesnime pikkus saab olla vaid 40 tähemärki!");
            }
            if (isik.Perenimi.Length > 40)
            {
                throw new Exception("Isiku perenime pikkus saab olla vaid 40 tähemärki!");
            }
            if (!Utils.IsNullOrEmptyWhitespace(isik.EMail) && isik.EMail.Length > 50)
            {
                throw new Exception("Isiku emaili pikkus saab olla vaid 50 tähemärki!");
            }
        }

        public static void ValidateIsikGraafik(IsikGraafik isikGraafik)
        {
            if (isikGraafik == null)
            {
                return;
            }
            if (isikGraafik.IsikID == null)
            {
                throw new Exception("Isiku graafiku isiku sisestamine on kohustuslik!");
            }
            if (isikGraafik.OsakondID == null)
            {
                throw new Exception("Isiku graafiku osakonna sisestamine on kohustuslik!");
            }
            if (isikGraafik.AlgusKP == null)
            {
                throw new Exception("Isiku graafiku alguskuupäeva sisestamine on kohustuslik!");
            }
            if (isikGraafik.LoppKP == null)
            {
                throw new Exception("Isiku graafiku lõppkuupäeva sisestamine on kohustuslik!");
            }
        }

        public static void ValidateKasutaja(Kasutaja kasutaja, bool uuendamine)
        {
            if (kasutaja == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(kasutaja.KasutajaNimi))
            {
                throw new Exception("Kasutajanime sisestamine on kohustuslik!");
            }
            //if (Utils.IsNullOrEmptyWhitespace(kasutaja.PsswdHash))
            //{
            //    throw new Exception("Kasutaja parooli sisestamine on kohustuslik!");
            //}
            if (kasutaja.RollID == null)
            {
                throw new Exception("Kasutaja rolli sisestamine on kohustuslik!");
            }
            if (kasutaja.IsikID == null)
            {
                throw new Exception("Kasutaja isiku sisestamine on kohustuslik!");
            }
            if (!uuendamine && kasutaja.LoppKP != null)
            {
                throw new Exception("Uue kasutaja sisestamisel ei saa kasutajal olla lõpp kuupäeva!");
            }
        }

        public static void ValidateLinn(Linn linn)
        {
            if (linn == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(linn.Nimetus))
            {
                throw new Exception("Linna nimetuse sisestamine on kohustuslik!");
            }
            if (linn.Nimetus.Length > 150)
            {
                throw new Exception("Linna nime pikkus saab olla vaid 150 tähemärki!");
            }
            if (linn.RiikID == null)
            {
                throw new Exception("Linna riigi sisestamine on kohustuslik!");
            }
        }

        public static void ValidateOsakond(Osakond osakond)
        {
            if (osakond == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(osakond.Nimetus))
            {
                throw new Exception("Osakonna nimetuse sisestamine on kohustuslik!");
            }
            if (osakond.Nimetus.Length > 150)
            {
                throw new Exception("Osakonna nime pikkus saab olla vaid 150 tähemärki!");
            }
            if (osakond.AlgusKP == null)
            {
                throw new Exception("Osakonna alguse kuupäeva sisestamine on kohustuslik ");
            }
            if (osakond.HooneID == null)
            {
                throw new Exception("Osakonna hoone sisestamine on kohustuslik!");
            }
            if (osakond.OsakondLiikID == null)
            {
                throw new Exception("Osakonna liigi sisestamine on kohustuslik!");
            }
        }

        public static void ValidateOsakondLiik(OsakondLiik osakondLiik)
        {
            if (osakondLiik == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(osakondLiik.Nimetus))
            {
                throw new Exception("Osakonna liigi nimetuse sisestamine on kohustuslik!");
            }
            if (osakondLiik.Nimetus.Length > 150)
            {
                throw new Exception("Osakonna liigi nimetus saab olla vaid 150 tähemärki!");
            }
            if (osakondLiik.Kirjeldus != null && osakondLiik.Kirjeldus.Length > 300)
            {
                throw new Exception("Osakonna liigi kirjeldus saab olla vaid 300 tähemärki!");
            }
        }

        public static void ValidateRiik(Riik riik)
        {
            if (riik == null)
            {
                return;
            }
            if (Utils.IsNullOrEmptyWhitespace(riik.Nimetus))
            {
                throw new Exception("Riigi nimetuse sisestamine on kohustuslik!");
            }
            if (riik.Nimetus.Length > 150)
            {
                throw new Exception("Riigi nimetus saab olla vaid 150 tähemärki!");
            }
            if (Utils.IsNullOrEmptyWhitespace(riik.Tahis))
            {
                throw new Exception("Riigi tahise sisestamine on kohustuslik!");
            }
            if (riik.Tahis.Length > 3)
            {
                throw new Exception("Riigi tahis saab olla vaid 3 tähemärki!");
            }
        }

        public static void ValidateAuthInput(string userName, string psswdHash)
        {
            if (Utils.IsNullOrEmptyWhitespace(userName))
            {
                throw new Exception("Kasutajanimi ei saa olla tühi!");
            }
            if (Utils.IsNullOrEmptyWhitespace(psswdHash))
            {
                throw new Exception("Kasutaja parool ei saa olla tühi!");
            }
        }

        public static PtService.NhibernateImpl.DAOs.Impl.Kasutaja CheckValidateHandle(string validateHandle,
                                                                                      DaoConnContext connContext)
        {
            if (Utils.IsNullOrEmptyWhitespace(validateHandle))
            {
                throw new Exception("Valideerimise ID on tühi!");
            }
            if (!Utils.IsDigitsOnly(validateHandle))
            {
                throw new Exception("Valideerimise ID formaat ei vasta nõuetele!");
            }
            if (validateHandle.Length != 40)
            {
                throw new Exception("Valideerimise ID pikkus ei ole sobiv!");
            }

            PtService.NhibernateImpl.DAOs.Impl.Kasutaja kasutaja = connContext._KasutajaDAO.GetKasutaja(validateHandle);
            if (kasutaja == null)
            {
                throw new Exception("Valideerimise ebaõnnestus kasutaja sessiooni puudumise tõttu!");
            }
            if (kasutaja.SessionValidTo != null && kasutaja.SessionValidTo > DateTime.Now)
            {
                throw new Exception("Valideerimise ebaõnnestus kasutaja sessiooni lõppnevuse tõttu!");
            }
            if (kasutaja.LoppKP != null && kasutaja.LoppKP >= DateTime.Now)
            {
                throw new Exception("Valideerimise ebaõnnestus kasutaja lõppnevuse tõttu!");
            }
            return kasutaja;
        }
    }
}