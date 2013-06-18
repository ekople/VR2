using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace PiimaToostusService
{
    [ServiceContract]
    public interface IPiimaToostusService
    {
        //User Authentication
        [OperationContract]
        AuthResponse AuthUser(string userName, string psswdHash);

        #region Intermediate Data interactions

        #region Isik interactions

        [OperationContract]
        ModIsikResponse AddIsik(string sessionHandle, Isik isik);

        [OperationContract]
        StandardResponse RemoveIsik(string sessionHandle, Int32 isikId);

        [OperationContract]
        ModIsikResponse UpdateIsik(string sessionHandle, Isik isik);

        #endregion

        #region User interactions

        [OperationContract]
        ModUserResponse AddUser(string sessionHandle, Kasutaja user);

        [OperationContract]
        StandardResponse RemoveUser(string sessionHandle, Int32 userId);

        [OperationContract]
        ModUserResponse UpdateUser(string sessionHandle, Kasutaja user);

        #endregion

        #region Atribuutika interactions

        [OperationContract]
        ModAtribuutikaResponse AddAtribuutika(string sessionHandle, Atribuutika atribuutika);

        [OperationContract]
        StandardResponse RemoveAtribuutika(string sessionHandle, Int32 atribuutikaId);

        [OperationContract]
        ModAtribuutikaResponse UpdateAtribuutika(string sessionHandle, Atribuutika atribuutika);

        #endregion

        #region AtribuutikaLiik interactions

        [OperationContract]
        ModAtribuutikaLiikResponse AddAtribuutikaLiik(string sessionHandle, AtribuutikaLiik atribuutikaLiik);

        [OperationContract]
        StandardResponse RemoveAtribuutikaLiik(string sessionHandle, Int32 atribuutikaLiikId);

        [OperationContract]
        ModAtribuutikaLiikResponse UpdateAtribuutikaLiik(string sessionHandle, AtribuutikaLiik atribuutikaLiik);

        #endregion

        #region Hoone interactions

        [OperationContract]
        ModHooneResponse AddHoone(string sessionHandle, Hoone hoone);

        [OperationContract]
        StandardResponse RemoveHoone(string sessionHandle, Int32 hooneId);

        [OperationContract]
        ModHooneResponse UpdateHoone(string sessionHandle, Hoone hoone);

        #endregion

        #region IsikGraafik interactions

        [OperationContract]
        ModIsikGraafikResponse AddIsikGraafik(string sessionHandle, IsikGraafik isikGraafik);

        [OperationContract]
        StandardResponse RemoveIsikGraafik(string sessionHandle, Int32 isikGraafikId);

        [OperationContract]
        ModIsikGraafikResponse UpdateIsikGraafik(string sessionHandle, IsikGraafik isikGraafik);

        #endregion

        #region Linn interactions

        [OperationContract]
        ModLinnResponse AddLinn(string sessionHandle, Linn linn);

        [OperationContract]
        StandardResponse RemoveLinn(string sessionHandle, Int32 linnId);

        [OperationContract]
        ModLinnResponse UpdateLinn(string sessionHandle, Linn linn);

        #endregion

        #region Osakond interactions

        [OperationContract]
        ModOsakondResponse AddOsakond(string sessionHandle, Osakond osakond);

        [OperationContract]
        StandardResponse RemoveOsakond(string sessionHandle, Int32 osakond);

        [OperationContract]
        ModOsakondResponse UpdateOsakond(string sessionHandle, Osakond osakond);

        #endregion

        #region OsakondLiik interactions

        [OperationContract]
        ModOsakondLiikResponse AddOsakondLiik(string sessionHandle, OsakondLiik osakondLiik);

        [OperationContract]
        StandardResponse RemoveOsakondLiik(string sessionHandle, Int32 osakondLiikId);

        [OperationContract]
        ModOsakondLiikResponse UpdateOsakondLiik(string sessionHandle, OsakondLiik osakondLiik);

        #endregion

        #region OsakondLiik interactions

        [OperationContract]
        ModRiikResponse AddRiik(string sessionHandle, Riik riik);

        [OperationContract]
        StandardResponse RemoveRiik(string sessionHandle, Int32 riikId);

        [OperationContract]
        ModRiikResponse UpdateRiik(string sessionHandle, Riik riik);

        #endregion

        #endregion

        #region List Operations

        [OperationContract]
        ModGetAllAtribuutikaResponse GetAllAtribuutika(string sessionHandle);

        [OperationContract]
        ModGetAllAtribuutikaLiigidResponse GetAllAtribuutikaLiigid(string sessionHandle);

        [OperationContract]
        ModGetAllHoonedResponse GetAllHooned(string sessionHandle);

        [OperationContract]
        ModGetAllIsikudResponse GetAllIsikud(string sessionHandle);

        [OperationContract]
        ModGetAllIsikuGraafikudResponse GetAllIsikuGraafikud(string sessionHandle);

        [OperationContract]
        ModGetAllLinnadResponse GetAllLinnad(string sessionHandle);

        [OperationContract]
        ModGetAllOsakonnadResponse GetAllOsakonnad(string sessionHandle);

        [OperationContract]
        ModGetAllOsakonnaLiigidResponse GetAllOsakonnaLiigid(string sessionHandle);

        [OperationContract]
        ModGetAllRiigidResponse GetAllRiigid(string sessionHandle);

        [OperationContract]
        ModGetAllRollidResponse GetAllRollid(string sessionHandle);

        [OperationContract]
        ModGetAllUsersResponse GetAllUsers(string sessionHandle);
        #endregion
    }

    [DataContract]
    public class AuthResponse
    {
        [DataMember] public bool IsAuthenticated;
        [DataMember] public string SessionHandle;
        [DataMember] public DateTime? SessionValidFrom;
        [DataMember] public DateTime? SessionValidTo;
        [DataMember] public Exception AuthException;
        [DataMember] public Kasutaja Kasustaja;
    }

    [DataContract]
    public class StandardResponse
    {
        [DataMember] public AuthResponse AuthResponse;
        [DataMember] public bool Successful;
        [DataMember] public Exception Exception;
    }

    #region mod Responses

    [DataContract]
    public class ModUserResponse : StandardResponse
    {
        [DataMember] public Kasutaja ModifiedUser;
    }

    [DataContract]
    public class ModIsikResponse : StandardResponse
    {
        [DataMember] public Isik ModifiedIsik;
    }

    [DataContract]
    public class ModAtribuutikaResponse : StandardResponse
    {
        [DataMember] public Atribuutika ModifiedAtribuutika;
    }

    [DataContract]
    public class ModAtribuutikaLiikResponse : StandardResponse
    {
        [DataMember] public AtribuutikaLiik ModifiedAtribuutikaLiik;
    }

    [DataContract]
    public class ModHooneResponse : StandardResponse
    {
        [DataMember] public Hoone ModifiedHoone;
    }

    [DataContract]
    public class ModIsikGraafikResponse : StandardResponse
    {
        [DataMember] public IsikGraafik ModifiedIsikGraafik;
    }

    [DataContract]
    public class ModLinnResponse : StandardResponse
    {
        [DataMember] public Linn ModifiedLinn;
    }

    [DataContract]
    public class ModOsakondResponse : StandardResponse
    {
        [DataMember] public Osakond ModifiedOsakond;
    }

    [DataContract]
    public class ModOsakondLiikResponse : StandardResponse
    {
        [DataMember] public OsakondLiik ModifiedOsakondLiik;
    }

    [DataContract]
    public class ModRiikResponse : StandardResponse
    {
        [DataMember] public Riik ModifiedRiik;
    }

    #endregion

    #region GetAll mod responses

    [DataContract]
    public class ModGetAllAtribuutikaResponse : StandardResponse
    {
        [DataMember] public IList<Atribuutika> AllAtribuutika;
    }

    [DataContract]
    public class ModGetAllAtribuutikaLiigidResponse : StandardResponse
    {
        [DataMember] public IList<AtribuutikaLiik> AllAtribuutikaLiigid;
    }

    [DataContract]
    public class ModGetAllHoonedResponse : StandardResponse
    {
        [DataMember] public IList<Hoone> AllHooned;
    }

    [DataContract]
    public class ModGetAllIsikudResponse : StandardResponse
    {
        [DataMember] public IList<Isik> AllIsikud;
    }

    [DataContract]
    public class ModGetAllIsikuGraafikudResponse : StandardResponse
    {
        [DataMember] public IList<IsikGraafik> AllIsikuGraafikud;
    }

    [DataContract]
    public class ModGetAllLinnadResponse : StandardResponse
    {
        [DataMember] public IList<Linn> AllLinnad;
    }

    [DataContract]
    public class ModGetAllOsakonnadResponse : StandardResponse
    {
        [DataMember] public IList<Osakond> AllOsakonnad;
    }

    [DataContract]
    public class ModGetAllOsakonnaLiigidResponse : StandardResponse
    {
        [DataMember] public IList<OsakondLiik> AllOsakonnaLiigid;
    }

    [DataContract]
    public class ModGetAllRiigidResponse : StandardResponse
    {
        [DataMember] public IList<Riik> AllRiigid;
    }

    [DataContract]
    public class ModGetAllRollidResponse : StandardResponse
    {
        [DataMember] public IList<Roll> AllRollid;
    }

    [DataContract]
    public class ModGetAllUsersResponse : StandardResponse
    {
        [DataMember]
        public IList<Kasutaja> AllUsers;
    }

    #endregion

    #region DAO objektidest tulenevad DataContractid

    [DataContract]
    public class Kasutaja
    {
        [DataMember] public int ID;
        [DataMember] public string KasutajaNimi;
        [DataMember] public string PsswdHash;
        [DataMember] public DateTime AlgusKP;
        [DataMember] public DateTime? LoppKP;
        [DataMember] public Roll RollID;
        [DataMember] public Isik IsikID;
        //[DataMember] public string SessionHandle;
        //[DataMember] public DateTime? SessionValidTo;
    }

    [DataContract]
    public class Roll
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public string Kirjeldus;
    }

    [DataContract]
    public class Atribuutika
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public DateTime? JargmineHooldusKP;
        [DataMember] public string Kommentaar;
        [DataMember] public string SeeriaNR_KereNR;
        [DataMember] public int? Kood;
        [DataMember] public string RegistriKood;
        [DataMember] public int? MaxVeovoime;
        [DataMember] public string VeovoimeYhikIndikaator;
        [DataMember] public AtribuutikaLiik AtribuutikaLiikID;
        [DataMember] public string Kategooria;
    }

    [DataContract]
    public class AtribuutikaLiik
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public bool IsikugaSeostatav;
        [DataMember] public string Kommentaar;
        [DataMember] public string Kirjeldus;
    }

    [DataContract]
    public class Hoone
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public string Aadress;
        [DataMember] public int? ValvelauaKontaktTelefon;
        [DataMember] public Linn LinnID;
    }

    [DataContract]
    public class Isik
    {
        [DataMember] public int ID;
        [DataMember] public string Eesnimi;
        [DataMember] public string Perenimi;
        [DataMember] public string Isikukood;
        [DataMember] public string EMail;
        [DataMember] public int KontaktTelefon;
        [DataMember] public DateTime? SynniKP;
    }

    [DataContract]
    public class IsikGraafik
    {
        [DataMember] public int ID;
        [DataMember] public Atribuutika AtribuutikaID;
        [DataMember] public Osakond OsakondID;
        [DataMember] public DateTime AlgusKP;
        [DataMember] public DateTime? LoppKP;
        [DataMember] public string Kommentaar;
        [DataMember] public Isik IsikID;
    }

    [DataContract]
    public class Linn
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public Riik RiikID;
    }

    [DataContract]
    public class Osakond
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public DateTime? AlgusKP;
        [DataMember] public DateTime? LoppKP;
        [DataMember] public Hoone HooneID;
        [DataMember] public OsakondLiik OsakondLiikID;
    }


    [DataContract]
    public class OsakondLiik
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public string Kirjeldus;
        [DataMember] public string Kommentaar;
    }


    [DataContract]
    public class Riik
    {
        [DataMember] public int ID;
        [DataMember] public string Nimetus;
        [DataMember] public string Tahis;
    }

    #endregion
}