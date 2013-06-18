using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using System.Web.UI.WebControls;
using PtService.NhibernateImpl;

namespace PiimaToostusService
{
    public class PiimaToostusService : IPiimaToostusService
    {
        //andmebaasi ühendussõne
        private static readonly string connStr = ConfigurationManager.AppSettings["DB.ConnectionString"];
        //andmebaasi suhtluse konteksti loomine
        private DaoConnContext _connContext = new DaoConnContext(connStr);
        //autentimise ebaõnnestumise veateade
        private const string authFailedMsg = "Kasutaja autentimine ebaõnnestus!";

        #region IPiimaToostusService Members

        public AuthResponse AuthUser(string userName, string psswdHash)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new AuthResponse();

            try
            {
                ValidationUtil.ValidateAuthInput(userName, psswdHash);
                PtService.NhibernateImpl.DAOs.Impl.Kasutaja principal =
                    _connContext._KasutajaDAO.GetKasutaja(userName, psswdHash);
                if (principal == null)
                {
                    throw new Exception("Kasutaja autentimine ebaõnnestus!");
                }
                else if (principal.LoppKP > DateTime.Now)
                {
                    throw new Exception("Puudub kehtiv kasutaja!");
                }
                else if (principal.IsikID == null)
                {
                    throw new Exception("Kasutajal puudub isik!");
                }

                string validHandle = Utils.GenCryptoRndStr();
                DateTime sessValidTo = DateTime.Now.AddMinutes(15);

                principal.SessionHandle = validHandle;
                principal.SessionValidTo = sessValidTo;
                _connContext._KasutajaDAO.Update(principal, principal.ID);

                resp.IsAuthenticated = true;
                resp.SessionValidFrom = DateTime.Now;
                resp.SessionHandle = validHandle;
                resp.SessionValidTo = sessValidTo;
                resp.Kasustaja = new Kasutaja();
                resp.Kasustaja = Utils.CopyTo(principal, resp.Kasustaja);
            }
            catch (Exception exception)
            {
                resp = new AuthResponse {IsAuthenticated = false, AuthException = exception};
            }

            return resp;
        }

        private AuthResponse validateAuth(string sessionHandle)
        {
            var tulem = new AuthResponse();
            PtService.NhibernateImpl.DAOs.Impl.Kasutaja daoKasutaja = null;
            try
            {
                daoKasutaja = ValidationUtil.CheckValidateHandle(sessionHandle, _connContext);
            }
            catch (Exception e)
            {
                tulem.IsAuthenticated = false;
                tulem.AuthException = e;
                return tulem;
            }

            DateTime newValidTo = DateTime.Now.AddMinutes(15);

            tulem.IsAuthenticated = true;
            tulem.Kasustaja = new Kasutaja();
            tulem.Kasustaja = Utils.CopyTo(daoKasutaja, tulem.Kasustaja);
            //salvesta ajapikendus baasi
            daoKasutaja.SessionValidTo = newValidTo;
            _connContext._KasutajaDAO.Update(daoKasutaja, daoKasutaja.ID);
            tulem.SessionValidFrom = DateTime.Now;
            tulem.SessionValidTo = newValidTo;
            tulem.SessionHandle = daoKasutaja.SessionHandle;
            //ära edasta klientsüsteemile psswdHas välja
            tulem.Kasustaja.PsswdHash = null;
            return tulem;
        }

        public ModIsikResponse AddIsik(string sessionHandle, Isik isik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModIsikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isik == null)
                {
                    throw new Exception("Lisatav isik puudub!");
                }
                ValidationUtil.ValidateIsik(isik);
                var lisatavIsik = new PtService.NhibernateImpl.DAOs.Impl.Isik();
                lisatavIsik = Utils.CopyTo(isik, lisatavIsik);
                lisatavIsik.ID = 0;
                _connContext._IsikDAO.Save(lisatavIsik);
                resp.ModifiedIsik = Utils.CopyTo(lisatavIsik, resp.ModifiedIsik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModIsikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveIsik(string sessionHandle, int isikId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isikId == 0)
                {
                    throw new Exception("Isiku eemaldamiseks peab isiku ID olema sisestatud!");
                }
                var isikToDel =
                    _connContext._IsikDAO.Load(isikId, typeof (PtService.NhibernateImpl.DAOs.Impl.Isik))
                    as PtService.NhibernateImpl.DAOs.Impl.Isik;
                _connContext._IsikDAO.Delete(isikToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModIsikResponse UpdateIsik(string sessionHandle, Isik isik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModIsikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isik == null)
                {
                    throw new Exception("Isiku uuendamiseks peab isik olema sisestatud!");
                }
                if (isik.ID == 0)
                {
                    throw new Exception("Isiku uuendamiseks peab isikul olema ID!");
                }
                ValidationUtil.ValidateIsik(isik);
                var isikToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Isik();
                isikToUpdate = Utils.CopyTo(isik, isikToUpdate);
                _connContext._IsikDAO.Update(isikToUpdate, isikToUpdate.ID);
                var updatedIsik = new PtService.NhibernateImpl.DAOs.Impl.Isik();
                updatedIsik =
                    _connContext._IsikDAO.Load(isikToUpdate.ID, typeof (PtService.NhibernateImpl.DAOs.Impl.Isik))
                    as PtService.NhibernateImpl.DAOs.Impl.Isik;
                resp.ModifiedIsik = new Isik();
                resp.ModifiedIsik = Utils.CopyTo(updatedIsik, resp.ModifiedIsik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModIsikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModUserResponse AddUser(string sessionHandle, Kasutaja user)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModUserResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (user == null)
                {
                    throw new Exception("Lisatav kasutaja puudub!");
                }
                ValidationUtil.ValidateKasutaja(user, false);
                var lisatavKasutaja = new PtService.NhibernateImpl.DAOs.Impl.Kasutaja();
                lisatavKasutaja = Utils.CopyTo(user, lisatavKasutaja);
                lisatavKasutaja.ID = 0;
                _connContext._KasutajaDAO.Save(lisatavKasutaja);
                resp.ModifiedUser = new Kasutaja();
                resp.ModifiedUser = Utils.CopyTo(lisatavKasutaja, resp.ModifiedUser);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModUserResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveUser(string sessionHandle, int userId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (userId == 0)
                {
                    throw new Exception("Kasutaja eemaldamiseks peab kasutaja ID olema sisestatud!");
                }
                var UserToDel =
                    _connContext._KasutajaDAO.Load(userId, typeof (PtService.NhibernateImpl.DAOs.Impl.Kasutaja))
                    as PtService.NhibernateImpl.DAOs.Impl.Kasutaja;
                _connContext._KasutajaDAO.Delete(UserToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModUserResponse UpdateUser(string sessionHandle, Kasutaja user)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModUserResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (user == null)
                {
                    throw new Exception("Kasutaja uuendamiseks peab kasutaja olema sisestatud!");
                }
                if (user.ID == 0)
                {
                    throw new Exception("Kasutaja uuendamiseks peab kasutajal olema ID!");
                }
                ValidationUtil.ValidateKasutaja(user, true);
                var kasutajaToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Kasutaja();
                kasutajaToUpdate = Utils.CopyTo(user, kasutajaToUpdate);
                _connContext._IsikDAO.Update(kasutajaToUpdate, kasutajaToUpdate.ID);
                var updatedIsik = new PtService.NhibernateImpl.DAOs.Impl.Kasutaja();
                updatedIsik =
                    _connContext._KasutajaDAO.Load(kasutajaToUpdate.ID,
                                                   typeof (PtService.NhibernateImpl.DAOs.Impl.Kasutaja))
                    as PtService.NhibernateImpl.DAOs.Impl.Kasutaja;
                resp.ModifiedUser = new Kasutaja();
                resp.ModifiedUser = Utils.CopyTo(updatedIsik, resp.ModifiedUser);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModUserResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModAtribuutikaResponse AddAtribuutika(string sessionHandle, Atribuutika atribuutika)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModAtribuutikaResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutika == null)
                {
                    throw new Exception("Lisatav atribuutika puudub!");
                }
                ValidationUtil.ValidateAtribuutika(atribuutika);
                var lisatavAtribuutika = new PtService.NhibernateImpl.DAOs.Impl.Atribuutika();
                lisatavAtribuutika = Utils.CopyTo(atribuutika, lisatavAtribuutika);
                lisatavAtribuutika.ID = 0;
                _connContext._AtribuutikaDao.Save(lisatavAtribuutika);
                resp.ModifiedAtribuutika = new Atribuutika();
                resp.ModifiedAtribuutika = Utils.CopyTo(lisatavAtribuutika, resp.ModifiedAtribuutika);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModAtribuutikaResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveAtribuutika(string sessionHandle, int atribuutikaId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutikaId == 0)
                {
                    throw new Exception("Atribuutika eemaldamiseks peab atribuutika ID olema sisestatud!");
                }
                var AtribuutikaToDel =
                    _connContext._AtribuutikaDao.Load(atribuutikaId,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.Atribuutika))
                    as PtService.NhibernateImpl.DAOs.Impl.Atribuutika;
                _connContext._AtribuutikaDao.Delete(AtribuutikaToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModAtribuutikaResponse UpdateAtribuutika(string sessionHandle, Atribuutika atribuutika)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModAtribuutikaResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutika == null)
                {
                    throw new Exception("Atribuutika uuendamiseks peab atribuutika olema sisestatud!");
                }
                if (atribuutika.ID == 0)
                {
                    throw new Exception("Atribuutika uuendamiseks peab atribuutikal olema ID!");
                }
                ValidationUtil.ValidateAtribuutika(atribuutika);
                var atribuutikaToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Atribuutika();
                atribuutikaToUpdate = Utils.CopyTo(atribuutika, atribuutikaToUpdate);
                _connContext._AtribuutikaDao.Update(atribuutikaToUpdate, atribuutikaToUpdate.ID);
                var updatedAtribuutika = new PtService.NhibernateImpl.DAOs.Impl.Atribuutika();
                updatedAtribuutika =
                    _connContext._AtribuutikaDao.Load(atribuutikaToUpdate.ID,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.Atribuutika))
                    as PtService.NhibernateImpl.DAOs.Impl.Atribuutika;
                resp.ModifiedAtribuutika = new Atribuutika();
                resp.ModifiedAtribuutika = Utils.CopyTo(updatedAtribuutika, resp.ModifiedAtribuutika);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModAtribuutikaResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModAtribuutikaLiikResponse AddAtribuutikaLiik(string sessionHandle, AtribuutikaLiik atribuutikaLiik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModAtribuutikaLiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutikaLiik == null)
                {
                    throw new Exception("Lisatav atribuutika liik puudub!");
                }
                ValidationUtil.ValidateAtribuutikaLiik(atribuutikaLiik);
                var lisatavAtribuutikaLiik = new PtService.NhibernateImpl.DAOs.Impl.Atribuutika();
                lisatavAtribuutikaLiik = Utils.CopyTo(atribuutikaLiik, lisatavAtribuutikaLiik);
                lisatavAtribuutikaLiik.ID = 0;
                _connContext._AtribuutikaLiikDAO.Save(lisatavAtribuutikaLiik);
                resp.ModifiedAtribuutikaLiik = new AtribuutikaLiik();
                resp.ModifiedAtribuutikaLiik = Utils.CopyTo(lisatavAtribuutikaLiik, resp.ModifiedAtribuutikaLiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModAtribuutikaLiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveAtribuutikaLiik(string sessionHandle, Int32 atribuutikaLiikId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutikaLiikId == 0)
                {
                    throw new Exception("Atribuutika liigi eemaldamiseks peab atribuutika liigi ID olema sisestatud!");
                }
                var AtribuutikaLiikToDel =
                    _connContext._AtribuutikaLiikDAO.Load(atribuutikaLiikId,
                                                          typeof (PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik))
                    as PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik;
                _connContext._AtribuutikaLiikDAO.Delete(AtribuutikaLiikToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModAtribuutikaLiikResponse UpdateAtribuutikaLiik(string sessionHandle, AtribuutikaLiik atribuutikaLiik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModAtribuutikaLiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (atribuutikaLiik == null)
                {
                    throw new Exception("Atribuutika liigi uuendamiseks peab atribuutika liik olema sisestatud!");
                }
                if (atribuutikaLiik.ID == 0)
                {
                    throw new Exception("Atribuutika liigi uuendamiseks peab atribuutika liigil olema ID!");
                }
                ValidationUtil.ValidateAtribuutikaLiik(atribuutikaLiik);
                var atribuutikaLiikToUpdate = new PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik();
                atribuutikaLiikToUpdate = Utils.CopyTo(atribuutikaLiik, atribuutikaLiikToUpdate);
                _connContext._AtribuutikaDao.Update(atribuutikaLiikToUpdate, atribuutikaLiikToUpdate.ID);
                var updatedAtribuutikaLiik = new PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik();
                updatedAtribuutikaLiik =
                    _connContext._AtribuutikaLiikDAO.Load(atribuutikaLiikToUpdate.ID,
                                                          typeof (PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik))
                    as PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik;
                resp.ModifiedAtribuutikaLiik = new AtribuutikaLiik();
                resp.ModifiedAtribuutikaLiik = Utils.CopyTo(updatedAtribuutikaLiik, resp.ModifiedAtribuutikaLiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModAtribuutikaLiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModHooneResponse AddHoone(string sessionHandle, Hoone hoone)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModHooneResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (hoone == null)
                {
                    throw new Exception("Lisatav hoone puudub!");
                }
                ValidationUtil.ValidateHoone(hoone);
                var lisatavHoone = new PtService.NhibernateImpl.DAOs.Impl.Hoone();
                lisatavHoone = Utils.CopyTo(hoone, lisatavHoone);
                lisatavHoone.ID = 0;
                _connContext._HooneDAO.Save(lisatavHoone);
                resp.ModifiedHoone = new Hoone();
                resp.ModifiedHoone = Utils.CopyTo(lisatavHoone, resp.ModifiedHoone);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModHooneResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveHoone(string sessionHandle, int hooneId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (hooneId == 0)
                {
                    throw new Exception("Hoone eemaldamiseks peab hoone ID olema sisestatud!");
                }
                var hooneIdToDel =
                    _connContext._HooneDAO.Load(hooneId, typeof (PtService.NhibernateImpl.DAOs.Impl.Hoone))
                    as PtService.NhibernateImpl.DAOs.Impl.Hoone;
                _connContext._HooneDAO.Delete(hooneIdToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModHooneResponse UpdateHoone(string sessionHandle, Hoone hoone)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModHooneResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (hoone == null)
                {
                    throw new Exception("Hoone uuendamiseks peab hoone olema sisestatud!");
                }
                if (hoone.ID == 0)
                {
                    throw new Exception("Hoone uuendamiseks peab hoonel olema ID!");
                }
                ValidationUtil.ValidateHoone(hoone);
                var hooneToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Hoone();
                hooneToUpdate = Utils.CopyTo(hoone, hooneToUpdate);
                _connContext._HooneDAO.Update(hooneToUpdate, hooneToUpdate.ID);
                var updatedHoone = new PtService.NhibernateImpl.DAOs.Impl.Hoone();
                updatedHoone =
                    _connContext._HooneDAO.Load(hooneToUpdate.ID, typeof (PtService.NhibernateImpl.DAOs.Impl.Hoone))
                    as PtService.NhibernateImpl.DAOs.Impl.Hoone;
                resp.ModifiedHoone = new Hoone();
                resp.ModifiedHoone = Utils.CopyTo(updatedHoone, resp.ModifiedHoone);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModHooneResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModIsikGraafikResponse AddIsikGraafik(string sessionHandle, IsikGraafik isikGraafik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModIsikGraafikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isikGraafik == null)
                {
                    throw new Exception("Lisatav isiku graafik puudub!");
                }
                ValidationUtil.ValidateIsikGraafik(isikGraafik);
                var lisatavGraafik = new PtService.NhibernateImpl.DAOs.Impl.IsikGraafik();
                lisatavGraafik = Utils.CopyTo(isikGraafik, lisatavGraafik);
                lisatavGraafik.ID = 0;
                _connContext._IsikGraafikDAO.Save(lisatavGraafik);
                resp.ModifiedIsikGraafik = new IsikGraafik();
                resp.ModifiedIsikGraafik = Utils.CopyTo(lisatavGraafik, resp.ModifiedIsikGraafik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModIsikGraafikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveIsikGraafik(string sessionHandle, int isikGraafikId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isikGraafikId == 0)
                {
                    throw new Exception("Isiku graafiku eemaldamiseks peab isiku graafiku ID olema sisestatud!");
                }
                var isikGraafikToDel =
                    _connContext._IsikGraafikDAO.Load(isikGraafikId,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.IsikGraafik))
                    as PtService.NhibernateImpl.DAOs.Impl.IsikGraafik;
                _connContext._IsikGraafikDAO.Delete(isikGraafikToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModIsikGraafikResponse UpdateIsikGraafik(string sessionHandle, IsikGraafik isikGraafik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModIsikGraafikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (isikGraafik == null)
                {
                    throw new Exception("Isiku graafiku uuendamiseks peab isiku graafik olema sisestatud!");
                }
                if (isikGraafik.ID == 0)
                {
                    throw new Exception("Isiku graafiku  uuendamiseks peab isiku graafikul olema ID!");
                }
                ValidationUtil.ValidateIsikGraafik(isikGraafik);
                var isikGraafikToUpdate = new PtService.NhibernateImpl.DAOs.Impl.IsikGraafik();
                isikGraafikToUpdate = Utils.CopyTo(isikGraafik, isikGraafikToUpdate);
                _connContext._IsikGraafikDAO.Update(isikGraafikToUpdate, isikGraafikToUpdate.ID);
                var updatedIsikGraafik = new PtService.NhibernateImpl.DAOs.Impl.IsikGraafik();
                updatedIsikGraafik =
                    _connContext._IsikGraafikDAO.Load(isikGraafikToUpdate.ID,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.IsikGraafik))
                    as PtService.NhibernateImpl.DAOs.Impl.IsikGraafik;
                resp.ModifiedIsikGraafik = new IsikGraafik();
                resp.ModifiedIsikGraafik = Utils.CopyTo(updatedIsikGraafik, resp.ModifiedIsikGraafik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModIsikGraafikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModLinnResponse AddLinn(string sessionHandle, Linn linn)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModLinnResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (linn == null)
                {
                    throw new Exception("Lisatav linn puudub!");
                }
                ValidationUtil.ValidateLinn(linn);
                var lisatavLinn = new PtService.NhibernateImpl.DAOs.Impl.Linn();
                lisatavLinn = Utils.CopyTo(linn, lisatavLinn);
                lisatavLinn.ID = 0;
                _connContext._LinnDAO.Save(lisatavLinn);
                resp.ModifiedLinn = new Linn();
                resp.ModifiedLinn = Utils.CopyTo(lisatavLinn, resp.ModifiedLinn);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModLinnResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveLinn(string sessionHandle, int linnId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (linnId == 0)
                {
                    throw new Exception("Linna eemaldamiseks peab linna ID olema sisestatud!");
                }
                var linnToDel =
                    _connContext._LinnDAO.Load(linnId, typeof (PtService.NhibernateImpl.DAOs.Impl.Linn))
                    as PtService.NhibernateImpl.DAOs.Impl.Linn;
                _connContext._LinnDAO.Delete(linnToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModLinnResponse UpdateLinn(string sessionHandle, Linn linn)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModLinnResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (linn == null)
                {
                    throw new Exception("Linna uuendamiseks peab linn olema sisestatud!");
                }
                if (linn.ID == 0)
                {
                    throw new Exception("Linna uuendamiseks peab linnal olema ID!");
                }
                ValidationUtil.ValidateLinn(linn);
                var LinnToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Linn();
                LinnToUpdate = Utils.CopyTo(linn, LinnToUpdate);
                _connContext._LinnDAO.Update(LinnToUpdate, LinnToUpdate.ID);
                var updatedLinn = new PtService.NhibernateImpl.DAOs.Impl.Linn();
                updatedLinn =
                    _connContext._LinnDAO.Load(LinnToUpdate.ID, typeof (PtService.NhibernateImpl.DAOs.Impl.Linn))
                    as PtService.NhibernateImpl.DAOs.Impl.Linn;
                resp.ModifiedLinn = new Linn();
                resp.ModifiedLinn = Utils.CopyTo(updatedLinn, resp.ModifiedLinn);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModLinnResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModOsakondResponse AddOsakond(string sessionHandle, Osakond osakond)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModOsakondResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakond == null)
                {
                    throw new Exception("Lisatav osakond puudub!");
                }
                ValidationUtil.ValidateOsakond(osakond);
                var lisatavOsakond = new PtService.NhibernateImpl.DAOs.Impl.Osakond();
                lisatavOsakond = Utils.CopyTo(osakond, lisatavOsakond);
                lisatavOsakond.ID = 0;
                _connContext._OsakondDAO.Save(lisatavOsakond);
                resp.ModifiedOsakond = new Osakond();
                resp.ModifiedOsakond = Utils.CopyTo(lisatavOsakond, resp.ModifiedOsakond);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModOsakondResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveOsakond(string sessionHandle, int osakond)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakond == 0)
                {
                    throw new Exception("Osakonna eemaldamiseks peab osakonna ID olema sisestatud!");
                }
                var osakondToDel =
                    _connContext._OsakondDAO.Load(osakond, typeof (PtService.NhibernateImpl.DAOs.Impl.Osakond))
                    as PtService.NhibernateImpl.DAOs.Impl.Osakond;
                _connContext._OsakondDAO.Delete(osakondToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModOsakondResponse UpdateOsakond(string sessionHandle, Osakond osakond)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModOsakondResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakond == null)
                {
                    throw new Exception("Osakonna uuendamiseks peab osakond olema sisestatud!");
                }
                if (osakond.ID == 0)
                {
                    throw new Exception("Osakonna uuendamiseks peab osakonnal olema ID!");
                }
                ValidationUtil.ValidateOsakond(osakond);
                var osakondToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Osakond();
                osakondToUpdate = Utils.CopyTo(osakond, osakondToUpdate);
                _connContext._OsakondDAO.Update(osakondToUpdate, osakondToUpdate.ID);
                var updatedOsakond = new PtService.NhibernateImpl.DAOs.Impl.Osakond();
                updatedOsakond =
                    _connContext._OsakondDAO.Load(osakondToUpdate.ID,
                                                  typeof (PtService.NhibernateImpl.DAOs.Impl.Osakond))
                    as PtService.NhibernateImpl.DAOs.Impl.Osakond;
                resp.ModifiedOsakond = new Osakond();
                resp.ModifiedOsakond = Utils.CopyTo(updatedOsakond, resp.ModifiedOsakond);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModOsakondResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModOsakondLiikResponse AddOsakondLiik(string sessionHandle, OsakondLiik osakondLiik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModOsakondLiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakondLiik == null)
                {
                    throw new Exception("Lisatav osakonna liik puudub!");
                }
                ValidationUtil.ValidateOsakondLiik(osakondLiik);
                var lisatavOsakonnaLiik = new PtService.NhibernateImpl.DAOs.Impl.OsakondLiik();
                lisatavOsakonnaLiik = Utils.CopyTo(osakondLiik, lisatavOsakonnaLiik);
                lisatavOsakonnaLiik.ID = 0;
                _connContext._OsakondLiikDAO.Save(lisatavOsakonnaLiik);
                resp.ModifiedOsakondLiik = new OsakondLiik();
                resp.ModifiedOsakondLiik = Utils.CopyTo(lisatavOsakonnaLiik, resp.ModifiedOsakondLiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModOsakondLiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveOsakondLiik(string sessionHandle, int osakondLiikId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakondLiikId == 0)
                {
                    throw new Exception("Osakonna liigi eemaldamiseks peab osakonna liik ID olema sisestatud!");
                }
                var osakonnaLiikToDel =
                    _connContext._OsakondLiikDAO.Load(osakondLiikId,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.OsakondLiik))
                    as PtService.NhibernateImpl.DAOs.Impl.OsakondLiik;
                _connContext._OsakondLiikDAO.Delete(osakonnaLiikToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModOsakondLiikResponse UpdateOsakondLiik(string sessionHandle, OsakondLiik osakondLiik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModOsakondLiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (osakondLiik == null)
                {
                    throw new Exception("Osakonna liigi uuendamiseks peab osakonna liik olema sisestatud!");
                }
                if (osakondLiik.ID == 0)
                {
                    throw new Exception("Osakonna liigi uuendamiseks peab osakonna liigil olema ID!");
                }
                ValidationUtil.ValidateOsakondLiik(osakondLiik);
                var osakondLiikToUpdate = new PtService.NhibernateImpl.DAOs.Impl.OsakondLiik();
                osakondLiikToUpdate = Utils.CopyTo(osakondLiik, osakondLiikToUpdate);
                _connContext._OsakondLiikDAO.Update(osakondLiikToUpdate, osakondLiikToUpdate.ID);
                var updatedOsakondLiik = new PtService.NhibernateImpl.DAOs.Impl.OsakondLiik();
                updatedOsakondLiik =
                    _connContext._OsakondLiikDAO.Load(osakondLiikToUpdate.ID,
                                                      typeof (PtService.NhibernateImpl.DAOs.Impl.OsakondLiik))
                    as PtService.NhibernateImpl.DAOs.Impl.OsakondLiik;
                resp.ModifiedOsakondLiik = new OsakondLiik();
                resp.ModifiedOsakondLiik = Utils.CopyTo(updatedOsakondLiik, resp.ModifiedOsakondLiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModOsakondLiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModRiikResponse AddRiik(string sessionHandle, Riik riik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModRiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (riik == null)
                {
                    throw new Exception("Lisatav riik puudub!");
                }
                ValidationUtil.ValidateRiik(riik);
                var lisatavRiik = new PtService.NhibernateImpl.DAOs.Impl.Riik();
                lisatavRiik = Utils.CopyTo(riik, lisatavRiik);
                lisatavRiik.ID = 0;
                _connContext._RiikDAO.Save(lisatavRiik);
                resp.ModifiedRiik = new Riik();
                resp.ModifiedRiik = Utils.CopyTo(lisatavRiik, resp.ModifiedRiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModRiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public StandardResponse RemoveRiik(string sessionHandle, int riikId)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new StandardResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (riikId == 0)
                {
                    throw new Exception("Riigi eemaldamiseks peab riigi ID olema sisestatud!");
                }
                var riikToDel =
                    _connContext._RiikDAO.Load(riikId, typeof (PtService.NhibernateImpl.DAOs.Impl.Riik))
                    as PtService.NhibernateImpl.DAOs.Impl.Riik;
                _connContext._RiikDAO.Delete(riikToDel);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new StandardResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModRiikResponse UpdateRiik(string sessionHandle, Riik riik)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModRiikResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                if (riik == null)
                {
                    throw new Exception("Riigi uuendamiseks peab riik olema sisestatud!");
                }
                if (riik.ID == 0)
                {
                    throw new Exception("Riigi uuendamiseks peab riigil olema ID!");
                }
                ValidationUtil.ValidateRiik(riik);
                var RiikToUpdate = new PtService.NhibernateImpl.DAOs.Impl.Riik();
                RiikToUpdate = Utils.CopyTo(riik, RiikToUpdate);
                _connContext._RiikDAO.Update(RiikToUpdate, RiikToUpdate.ID);
                var updatedRiik = new PtService.NhibernateImpl.DAOs.Impl.Riik();
                updatedRiik =
                    _connContext._RiikDAO.Load(RiikToUpdate.ID, typeof (PtService.NhibernateImpl.DAOs.Impl.Riik))
                    as PtService.NhibernateImpl.DAOs.Impl.Riik;
                resp.ModifiedRiik = new Riik();
                resp.ModifiedRiik = Utils.CopyTo(updatedRiik, resp.ModifiedRiik);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModRiikResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllAtribuutikaResponse GetAllAtribuutika(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllAtribuutikaResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._AtribuutikaDao.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Atribuutika));
                resp.AllAtribuutika = Utils.ConvertToType<Atribuutika>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllAtribuutikaResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllAtribuutikaLiigidResponse GetAllAtribuutikaLiigid(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllAtribuutikaLiigidResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._AtribuutikaLiikDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.AtribuutikaLiik));
                resp.AllAtribuutikaLiigid = Utils.ConvertToType<AtribuutikaLiik>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllAtribuutikaLiigidResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllHoonedResponse GetAllHooned(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllHoonedResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._HooneDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Hoone));
                resp.AllHooned = Utils.ConvertToType<Hoone>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllHoonedResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllIsikudResponse GetAllIsikud(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllIsikudResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._IsikDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Isik));
                resp.AllIsikud = Utils.ConvertToType<Isik>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllIsikudResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllIsikuGraafikudResponse GetAllIsikuGraafikud(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllIsikuGraafikudResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._IsikGraafikDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.IsikGraafik));
                resp.AllIsikuGraafikud = Utils.ConvertToType<IsikGraafik>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllIsikuGraafikudResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllLinnadResponse GetAllLinnad(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllLinnadResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._LinnDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Linn));
                resp.AllLinnad = Utils.ConvertToType<Linn>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllLinnadResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllOsakonnadResponse GetAllOsakonnad(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllOsakonnadResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._OsakondDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Osakond));
                resp.AllOsakonnad = Utils.ConvertToType<Osakond>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllOsakonnadResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllOsakonnaLiigidResponse GetAllOsakonnaLiigid(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllOsakonnaLiigidResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._OsakondLiikDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.OsakondLiik));
                resp.AllOsakonnaLiigid = Utils.ConvertToType<OsakondLiik>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllOsakonnaLiigidResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllRiigidResponse GetAllRiigid(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllRiigidResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._RiikDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Riik));
                resp.AllRiigid = Utils.ConvertToType<Riik>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllRiigidResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllRollidResponse GetAllRollid(string sessionHandle)
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllRollidResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._RollDAO.LoadAll(typeof (PtService.NhibernateImpl.DAOs.Impl.Roll));
                resp.AllRollid = Utils.ConvertToType<Roll>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllRollidResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        public ModGetAllUsersResponse GetAllUsers(string sessionHandle) 
        {
            _connContext = _connContext.CheckDBConn();
            var resp = new ModGetAllUsersResponse();
            resp.AuthResponse = validateAuth(sessionHandle);
            if (resp.AuthResponse.IsAuthenticated == false)
            {
                resp.Successful = false;
                resp.Exception = new AuthenticationException(authFailedMsg);
                return resp;
            }

            try
            {
                IList elems =
                    _connContext._KasutajaDAO.LoadAll(typeof(PtService.NhibernateImpl.DAOs.Impl.Kasutaja));
                resp.AllUsers = Utils.ConvertToType<Kasutaja>(elems);
                resp.Successful = true;
            }
            catch (Exception e)
            {
                resp = new ModGetAllUsersResponse();
                resp.Successful = false;
                resp.Exception = e;
            }

            return resp;
        }

        #endregion
    }
}