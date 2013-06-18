ALTER TABLE Kasutaja DROP CONSTRAINT fk1_Kasutaja_to_Roll;
ALTER TABLE Kasutaja DROP CONSTRAINT fk2_Kasutaja_to_Isik;
ALTER TABLE Linn DROP CONSTRAINT fk1_Linn_to_Riik;
ALTER TABLE Hoone DROP CONSTRAINT fk1_HooneLinn;
ALTER TABLE Tootamine DROP CONSTRAINT fk1_TootamineIsik;
ALTER TABLE Tootamine DROP CONSTRAINT fk2_TootamineAmet;
ALTER TABLE IsikGraafik DROP CONSTRAINT fk1_IsikGraafikIsik;
ALTER TABLE TarnijaPiirkond DROP CONSTRAINT fk1_TarnijaPiirkondLinn;
ALTER TABLE TarnijaPiirkond DROP CONSTRAINT fk2_TarnijaPiirkondTarnija;
ALTER TABLE Tellimus DROP CONSTRAINT fk1_TellimusTarnija;
ALTER TABLE TellimuseRida DROP CONSTRAINT fk1_TellimuseRidaTellimus;
ALTER TABLE TellimuseRida DROP CONSTRAINT fk2_TellimuseRidaTooraine;
ALTER TABLE SaatelehtRida DROP CONSTRAINT fk1_SaatelehtRidaTooraine;
ALTER TABLE SaatelehtRida DROP CONSTRAINT fk2_SaatelehtRidaSaateleht;
ALTER TABLE Saateleht DROP CONSTRAINT fk1_SaatelehtTarnija;
ALTER TABLE TarnijaTarnitavKaup DROP CONSTRAINT fk1_TarnijaTarnitavKaupTarnija;
ALTER TABLE TarnijaTarnitavKaup DROP CONSTRAINT fk2_TarnijaTooraine;
ALTER TABLE Saateleht DROP CONSTRAINT fk2_SaatelehtHoone;
ALTER TABLE Osakond DROP CONSTRAINT fk1_OsakondHoone;
ALTER TABLE Amet DROP CONSTRAINT fk1_AmetOsakond;
ALTER TABLE TarneSobivus DROP CONSTRAINT fk1_TarneSobivusSaatelehtRida;
ALTER TABLE TarneSobivus DROP CONSTRAINT fk2_TarneSobivusTooraineTestid;
ALTER TABLE TooraineTestid DROP CONSTRAINT fk1_TooraineTestidTooraine;
ALTER TABLE ArveRida DROP CONSTRAINT fk1_ArveRidaArve;
ALTER TABLE Klient DROP CONSTRAINT fk1_KlientKliendigrupp;
ALTER TABLE Arve DROP CONSTRAINT fk1_ArveKlient;
ALTER TABLE TooAtribuutika DROP CONSTRAINT fk1_TooAtribuutikaIsik;
ALTER TABLE VedavadAutod DROP CONSTRAINT fk1_VedavadAutodIsikGraafik;
ALTER TABLE VedavadAutod DROP CONSTRAINT fk2_VedavadAutodKaubaVedu ;
ALTER TABLE KlientKontakt DROP CONSTRAINT fk1_KlientKontaktKlient;
ALTER TABLE KlientKontakt DROP CONSTRAINT fk2_KlientKontaktKontakt;
ALTER TABLE KaubaVedu DROP CONSTRAINT fk1_KaubaVeduKontakt;
ALTER TABLE KaubaVedu DROP CONSTRAINT fk2_KaubaVeduHoone;
ALTER TABLE TellimusAutos DROP CONSTRAINT fk1_TellimusAutosVeoTellimus;
ALTER TABLE TellimusAutos DROP CONSTRAINT fk2_TellimusAutosVedavadAutod;
ALTER TABLE VeoTellimus DROP CONSTRAINT fk1_VeoTellimusKontakt;
ALTER TABLE VeoTellimus DROP CONSTRAINT fk2_VeoTellimusHoone;
ALTER TABLE Kontakt DROP CONSTRAINT fk1_KontaktLinn;
ALTER TABLE Hooldus DROP CONSTRAINT fk1_HooldusAsutus;
ALTER TABLE TehtudTood DROP CONSTRAINT fk1_TehtudToodHooldus;
ALTER TABLE AtribuutikaOsakonnas DROP CONSTRAINT fk1_AtribuutikaOsakond;
ALTER TABLE AtribuutikaOsakonnas DROP CONSTRAINT fk2_AtribuutikaOsakonnas;
ALTER TABLE Hooldus DROP CONSTRAINT fk2_HooldusAtribuutika;
ALTER TABLE Hooldus DROP CONSTRAINT fk3_HooldusIsik;
ALTER TABLE Osakond DROP CONSTRAINT fk2_OsakondOsakondLiik;
ALTER TABLE KutusekaardiValdaja DROP CONSTRAINT fk1_KutusekaardiValdajaIsik;
ALTER TABLE KutusekaardiValdaja DROP CONSTRAINT fk2_KkaardiValdKutusekaart;
ALTER TABLE Kutusekaart DROP CONSTRAINT fk1_KutusekaartAsutus;
ALTER TABLE KutuseOst DROP CONSTRAINT fk1_KutuseOstKutusekaart;
ALTER TABLE Asutus DROP CONSTRAINT fk1_AsutusLiik;
ALTER TABLE TooraineLiikumine DROP CONSTRAINT fk1_TooraineLiikumineOsakond;
ALTER TABLE TooraineLiikumine DROP CONSTRAINT fk2_TLSRida;
ALTER TABLE OsakondValjund DROP CONSTRAINT fk1_OsakondValjundOsakond;
ALTER TABLE TooteLiikumine DROP CONSTRAINT fk1_LiikumineOsakond;
ALTER TABLE TooteLiikumine DROP CONSTRAINT fk2_TooteLiikumineOsakond;
ALTER TABLE OsakondValjund DROP CONSTRAINT fk2_OsakondValjundTooteLiik;
ALTER TABLE ArveRida DROP CONSTRAINT fk2_ArveRidaTooteLiik;
ALTER TABLE TooteHind DROP CONSTRAINT fk1_TooteHindTooteLiik;
ALTER TABLE Puudumine DROP CONSTRAINT fk1_PuuduminePuudumineLiik;
ALTER TABLE Puudumine DROP CONSTRAINT fk2_PuudumineIsik;
ALTER TABLE Atribuutika DROP CONSTRAINT fk1_AtribuutikaAtribuutikaLiik;
ALTER TABLE OsakondValjund DROP CONSTRAINT fk3_OsakValjKogMahtKL;
ALTER TABLE IsikGraafik DROP CONSTRAINT fk2_IsikGraafikAtribuutika;
ALTER TABLE TooAtribuutika DROP CONSTRAINT fk2_TooAtribuutikaAtribuutika;
ALTER TABLE IsikGraafik DROP CONSTRAINT fk3_IsikGraafikOsakond;
ALTER TABLE TooraineTestid DROP CONSTRAINT fk2_TooraineTestidTestid;

DROP TABLE Roll;

DROP TABLE Kasutaja;

DROP TABLE Riik;

DROP TABLE Linn;

DROP TABLE Hoone;

DROP TABLE OsakondLiik;

DROP TABLE Amet;

DROP TABLE Tootamine;

DROP TABLE Isik;

DROP TABLE IsikGraafik;

DROP TABLE TellitavTooraine;

DROP TABLE TellimuseRida;

DROP TABLE Tellimus;

DROP TABLE TarnijaPiirkond;

DROP TABLE Tarnija;

DROP TABLE SaatelehtRida;

DROP TABLE Saateleht;

DROP TABLE TarnijaTarnitavKaup;

DROP TABLE Osakond;

DROP TABLE TarneSobivus;

DROP TABLE TooraineTestid;

DROP TABLE Klient;

DROP TABLE Kliendigrupp;

DROP TABLE TooteHind;

DROP TABLE Arve;

DROP TABLE ArveRida;

DROP TABLE TooAtribuutika;

DROP TABLE VeoTellimus;

DROP TABLE KaubaVedu;

DROP TABLE VedavadAutod;

DROP TABLE KlientKontakt;

DROP TABLE Kontakt;

DROP TABLE TellimusAutos;

DROP TABLE Asutus;

DROP TABLE Hooldus;

DROP TABLE TehtudTood;

DROP TABLE Atribuutika;

DROP TABLE AtribuutikaOsakonnas;

DROP TABLE Kutusekaart;

DROP TABLE KutusekaardiValdaja;

DROP TABLE KutuseOst;

DROP TABLE Liik;

DROP TABLE TooraineLiikumine;

DROP TABLE OsakondValjund;

DROP TABLE TooteLiikumine;

DROP TABLE TooteLiik;

DROP TABLE Puudumine;

DROP TABLE PuudumineLiik;

DROP TABLE AtribuutikaLiik;

DROP TABLE ValjundKogusMahtKL;

DROP TABLE Testid;
