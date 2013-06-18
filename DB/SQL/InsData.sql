/*--  Obligatory data  --*/
insert into Roll(ID,Nimetus,Kirjeldus) values (1,'Haldur','Haldav personal');
insert into Roll(ID,Nimetus,Kirjeldus) values (2,'Tootaja','Tavatootajad');

/*--  Test data  --*/
insert into isik(ID,Eesnimi,Perenimi,Isikukood,EMail,KontaktTelefon,SynniKP) values (1,'Eesnimi1','Perenimi1','09878915601','email1@mail.nan',583720101,convert(DATETIME,'1991/05/03 00:00:00'));
insert into isik(ID,Eesnimi,Perenimi,Isikukood,EMail,KontaktTelefon,SynniKP) values (2,'Eesnimi2','Perenimi2','09878915602','email2@mail.nan',583720102,convert(DATETIME,'1992/05/03 00:00:00'));
insert into isik(ID,Eesnimi,Perenimi,Isikukood,EMail,KontaktTelefon,SynniKP) values (3,'Eesnimi3','Perenimi3','09878915603','email3@mail.nan',583720103,convert(DATETIME,'1993/05/03 00:00:00'));

insert into Kasutaja(ID, KasutajaNimi, PsswdHash, AlgusKP, LoppKP, RollID, IsikID, SessionHandle, SessionValidTo) 
values (1,'admin','b71be083c4cccbbcc6e8b9f892289d0681ba280fe799e3db88a9ddb1b3270e4c',convert(DATETIME,'1991/05/03 00:00:00'),NULL,1,1,NULL,NULL);
insert into Kasutaja(ID, KasutajaNimi, PsswdHash, AlgusKP, LoppKP, RollID, IsikID, SessionHandle, SessionValidTo) 
values (2,'kasutaja','6280526b99c0b06c4552db6dc47053e1593998b5618e1d685bf548edafd67609',convert(DATETIME,'1991/05/03 00:00:00'),NULL,2,2,NULL,NULL);

insert into riik (ID,Nimetus,Tahis) values (1,'Riik1','T1');
insert into riik (ID,Nimetus,Tahis) values (2,'Riik2','T2');
insert into riik (ID,Nimetus,Tahis) values (3,'Riik3','T3');

insert into linn (ID,Nimetus,RiikID) values (1,'Linn1',1);
insert into linn (ID,Nimetus,RiikID) values (2,'Linn2',2);
insert into linn (ID,Nimetus,RiikID) values (3,'Linn3',3);

insert into hoone (ID,Nimetus,Aadress,ValvelauaKontaktTelefon,LinnID) values  (1,'Hoone1','Aadress1','000001',1);
insert into hoone (ID,Nimetus,Aadress,ValvelauaKontaktTelefon,LinnID) values  (2,'Hoone2','Aadress2','000002',2);
insert into hoone (ID,Nimetus,Aadress,ValvelauaKontaktTelefon,LinnID) values  (3,'Hoone3','Aadress3','000003',3);

insert into osakondliik (ID,Nimetus,Kirjeldus,Kommentaar) values (1,'OsakondLiik1','OsakondLiikKirjeldus1','OsakondLiikKommnetaar1');
insert into osakondliik (ID,Nimetus,Kirjeldus,Kommentaar) values (2,'OsakondLiik2','OsakondLiikKirjeldus2','OsakondLiikKommnetaar2');
insert into osakondliik (ID,Nimetus,Kirjeldus,Kommentaar) values (3,'OsakondLiik3','OsakondLiikKirjeldus3','OsakondLiikKommnetaar3');
insert into osakondliik (ID,Nimetus,Kirjeldus,Kommentaar) values (4,'OsakondLiik4','OsakondLiikKirjeldus4','OsakondLiikKommnetaar4');

insert into osakond(ID,Nimetus,AlgusKP,LoppKP,HooneID,OsakondLiikID) values (1,'Osakon1',convert(DATETIME,'1991/05/03 00:00:00'),NULL,1,1);
insert into osakond(ID,Nimetus,AlgusKP,LoppKP,HooneID,OsakondLiikID) values (2,'Osakon2',convert(DATETIME,'1992/05/03 00:00:00'),NULL,1,2);
insert into osakond(ID,Nimetus,AlgusKP,LoppKP,HooneID,OsakondLiikID) values (3,'Osakon3',convert(DATETIME,'1993/05/03 00:00:00'),NULL,1,3);
insert into osakond(ID,Nimetus,AlgusKP,LoppKP,HooneID,OsakondLiikID) values (4,'Osakon4',convert(DATETIME,'1994/05/03 00:00:00'),NULL,1,4);

insert into tarnija(ID,Nimetus,KontaktIsik,EMail,Telefon,Faks,Kommentaar) Values(1,'Tarnija1','TarnijaKontaktisik1','EMail1',0000001,00000010,'TarnijaKommentaar1');
insert into tarnija(ID,Nimetus,KontaktIsik,EMail,Telefon,Faks,Kommentaar) Values(2,'Tarnija2','TarnijaKontaktisik2','EMail2',0000002,00000020,'TarnijaKommentaar2');
insert into tarnija(ID,Nimetus,KontaktIsik,EMail,Telefon,Faks,Kommentaar) Values(3,'Tarnija3','TarnijaKontaktisik3','EMail3',0000003,00000030,'TarnijaKommentaar3');

insert into tarnijaPiirkond(ID,LinnID,TarnijaID,Kommentaar) values (1,1,1,'Kommentaar1');
insert into tarnijaPiirkond(ID,LinnID,TarnijaID,Kommentaar) values (2,2,2,'Kommentaar2');
insert into tarnijaPiirkond(ID,LinnID,TarnijaID,Kommentaar) values (3,3,3,'Kommentaar3');

insert into tellitavtooraine(ID,Nimetus,Kirjeldus,Kommentaar,TooraineMaxTempC) values (1,'Nimetus1','Kirjeldus1','Kommentaar1',1);
insert into tellitavtooraine(ID,Nimetus,Kirjeldus,Kommentaar,TooraineMaxTempC) values (2,'Nimetus2','Kirjeldus2','Kommentaar2',2);
insert into tellitavtooraine(ID,Nimetus,Kirjeldus,Kommentaar,TooraineMaxTempC) values (3,'Nimetus3','Kirjeldus3','Kommentaar3',3);

insert into tarnijatarnitavkaup(ID,TellitavTooraineID,TarnijaID,MinKogusMass,MaxKogusMass,KogusMassIndikaator,MaxtarneAegPaevad,MaxtarneAegTunnid,Kommentaarid,TarnijaHind,TarnijaHindValuuta,TarnijaHindKogusIndikaator,TarnijaHindKogus)
	values (1,1,1,1,1000,'KG',10,1,'Kommentaar1',100,'EUR','KG',1);
insert into tarnijatarnitavkaup(ID,TellitavTooraineID,TarnijaID,MinKogusMass,MaxKogusMass,KogusMassIndikaator,MaxtarneAegPaevad,MaxtarneAegTunnid,Kommentaarid,TarnijaHind,TarnijaHindValuuta,TarnijaHindKogusIndikaator,TarnijaHindKogus)
	values (2,2,2,2,2000,'KG',20,2,'Kommentaar2',200,'EUR','KG',2);
insert into tarnijatarnitavkaup(ID,TellitavTooraineID,TarnijaID,MinKogusMass,MaxKogusMass,KogusMassIndikaator,MaxtarneAegPaevad,MaxtarneAegTunnid,Kommentaarid,TarnijaHind,TarnijaHindValuuta,TarnijaHindKogusIndikaator,TarnijaHindKogus)
	values (3,3,3,3,3000,'KG',30,3,'Kommentaar3',300,'EUR','KG',3);

insert into tellimus(ID,TarnijaID,Esitaja,AlgusKP,TellimusKood) values (1,1,'Esitaja1',convert(DATETIME,'1991/05/03 00:00:00'),'TEL000001');
insert into tellimus(ID,TarnijaID,Esitaja,AlgusKP,TellimusKood) values (2,2,'Esitaja2',convert(DATETIME,'1991/05/03 00:00:00'),'TEL000002');
insert into tellimus(ID,TarnijaID,Esitaja,AlgusKP,TellimusKood) values (3,3,'Esitaja2',convert(DATETIME,'1991/05/03 00:00:00'),'TEL000003');

insert into tellimuseRida(ID,TellimusID,TellitavTooraineID,KogusMass,KogusMassIndikaator,Kommentaar,Hind,HindValuuta) values (1,1,1,1,'KG','Kommentaar1',1,'EUR');
insert into tellimuseRida(ID,TellimusID,TellitavTooraineID,KogusMass,KogusMassIndikaator,Kommentaar,Hind,HindValuuta) values (2,2,2,2,'KG','Kommentaar2',2,'EUR');
insert into tellimuseRida(ID,TellimusID,TellitavTooraineID,KogusMass,KogusMassIndikaator,Kommentaar,Hind,HindValuuta) values (3,3,3,3,'KG','Kommentaar3',3,'EUR');

insert into saateleht(ID,TarnijaID,HooneID,AlgusKP,VastuVotja,Kommentaarid) values(1,1,1,convert(DATETIME,'1991/05/03 00:00:00'),'VastuVõtja1','Kommentaarid1');
insert into saateleht(ID,TarnijaID,HooneID,AlgusKP,VastuVotja,Kommentaarid) values(2,2,2,convert(DATETIME,'1991/05/03 00:00:00'),'VastuVõtja2','Kommentaarid2');
insert into saateleht(ID,TarnijaID,HooneID,AlgusKP,VastuVotja,Kommentaarid) values(3,3,3,convert(DATETIME,'1991/05/03 00:00:00'),'VastuVõtja3','Kommentaarid3'); 

insert into saatelehtrida(ID,SaatelehtID,TellitavTooraineID,TooraineKogusMass,KogusMassIndikaator) values (1,1,1,1,'KG');
insert into saatelehtrida(ID,SaatelehtID,TellitavTooraineID,TooraineKogusMass,KogusMassIndikaator) values (2,2,2,2,'KG');
insert into saatelehtrida(ID,SaatelehtID,TellitavTooraineID,TooraineKogusMass,KogusMassIndikaator) values (3,3,3,3,'KG');

insert into testid(ID,Nimetus,Kirjeldus,Kommentaar) values (1,'Nimetus1','Kirjeldus1','Kommentaar1');
insert into testid(ID,Nimetus,Kirjeldus,Kommentaar) values (2,'Nimetus2','Kirjeldus2','Kommentaar2');
insert into testid(ID,Nimetus,Kirjeldus,Kommentaar) values (3,'Nimetus3','Kirjeldus3','Kommentaar3');

insert into toorainetestid(ID,Kommentaar,LubatavNominaal,TestidID,TellitavTooraineID) values (1,'Kommenaar1',1,1,1);
insert into toorainetestid(ID,Kommentaar,LubatavNominaal,TestidID,TellitavTooraineID) values (2,'Kommenaar1',2,2,2);
insert into toorainetestid(ID,Kommentaar,LubatavNominaal,TestidID,TellitavTooraineID) values (3,'Kommenaar1',3,3,3);

insert into tarnesobivus(ID,SaatelehtRidaID,TooraineTestidID,LabisTesti,Kontrollija,TestTeostatudKP) values(1,1,1,1,'Kontrollija1',convert(DATETIME,'2001/05/03 00:00:00'));
insert into tarnesobivus(ID,SaatelehtRidaID,TooraineTestidID,LabisTesti,Kontrollija,TestTeostatudKP) values(2,2,2,2,'Kontrollija2',convert(DATETIME,'2002/05/03 00:00:00'));
insert into tarnesobivus(ID,SaatelehtRidaID,TooraineTestidID,LabisTesti,Kontrollija,TestTeostatudKP) values(3,3,3,3,'Kontrollija3',convert(DATETIME,'2003/05/03 00:00:00'));

insert into tooraineliikumine(ID,OsakondID,SaateleheRidaID,LiikumiseKP,LiigutatavMahtKogus,LiigutatavMahtKogusIndikaator) values(1,1,1,convert(DATETIME,'2001/05/03 00:00:00'),1,'KG');
insert into tooraineliikumine(ID,OsakondID,SaateleheRidaID,LiikumiseKP,LiigutatavMahtKogus,LiigutatavMahtKogusIndikaator) values(2,2,2,convert(DATETIME,'2002/05/03 00:00:00'),2,'KG');
insert into tooraineliikumine(ID,OsakondID,SaateleheRidaID,LiikumiseKP,LiigutatavMahtKogus,LiigutatavMahtKogusIndikaator) values(3,3,3,convert(DATETIME,'2003/05/03 00:00:00'),3,'KG');

insert into AtribuutikaLiik(ID,Nimetus,IsikugaSeostatav,Kommentaar,Kirjeldus) values (1,'Nimetus1',1,'Kommetaar1','Kirjeldus1');
insert into AtribuutikaLiik(ID,Nimetus,IsikugaSeostatav,Kommentaar,Kirjeldus) values (2,'Nimetus2',1,'Kommetaar2','Kirjeldus2');
insert into AtribuutikaLiik(ID,Nimetus,IsikugaSeostatav,Kommentaar,Kirjeldus) values (3,'Nimetus3',1,'Kommetaar3','Kirjeldus3');

insert into Atribuutika(ID,Nimetus,JargmineHooldusKP,Kommentaar,SeeriaNR_KereNR,Kood,RegistriKood,MaxVeovoime,VeovoimeYhikIndikaator,AtribuutikaLiikID,Kategooria) values (1,'Nimetus1',convert(DATETIME,'2012/05/03 00:00:00'),'Kommentaar1','A100G836628001',45435001,'XXXXXXXX',10000,'KG',1,'A');
insert into Atribuutika(ID,Nimetus,JargmineHooldusKP,Kommentaar,SeeriaNR_KereNR,Kood,RegistriKood,MaxVeovoime,VeovoimeYhikIndikaator,AtribuutikaLiikID,Kategooria) values (2,'Nimetus2',convert(DATETIME,'2012/05/03 00:00:00'),'Kommentaar2','A100G836628002',45435002,'YYYYYYYY',20000,'KG',2,'B');
insert into Atribuutika(ID,Nimetus,JargmineHooldusKP,Kommentaar,SeeriaNR_KereNR,Kood,RegistriKood,MaxVeovoime,VeovoimeYhikIndikaator,AtribuutikaLiikID,Kategooria) values (3,'Nimetus3',convert(DATETIME,'2012/05/03 00:00:00'),'Kommentaar3','A100G836628003',45435003,'ZZZZZZZZ',30000,'KG',3,'C');

insert into atribuutikaosakonnas(ID,OsakondID,AtribuutikaID,AlgusKP,LoppKP,Kommentaar) values (1,1,1,convert(DATETIME,'1991/05/03 00:00:00'),NULL,'Kommentaar1');
insert into atribuutikaosakonnas(ID,OsakondID,AtribuutikaID,AlgusKP,LoppKP,Kommentaar) values (2,2,2,convert(DATETIME,'1992/05/03 00:00:00'),NULL,'Kommentaar2');
insert into atribuutikaosakonnas(ID,OsakondID,AtribuutikaID,AlgusKP,LoppKP,Kommentaar) values (3,3,3,convert(DATETIME,'1993/05/03 00:00:00'),NULL,'Kommentaar3');

insert into TooAtribuutika(ID,AtribuutikaID,IsikID,AlgusKP,LoppKP) values (1,1,1,convert(DATETIME,'2001/05/03 00:00:00'),NULL);
insert into TooAtribuutika(ID,AtribuutikaID,IsikID,AlgusKP,LoppKP) values (2,2,2,convert(DATETIME,'2002/05/03 00:00:00'),NULL);
insert into TooAtribuutika(ID,AtribuutikaID,IsikID,AlgusKP,LoppKP) values (3,3,3,convert(DATETIME,'2003/05/03 00:00:00'),NULL);

insert into puudumineLiik(ID,Nimetus,Kommentaar,Tasuline,TooAasta_PuhkusePaevad_Suhe) values (1,'Nimetus1','Kommentaar1',1,24);
insert into puudumineLiik(ID,Nimetus,Kommentaar,Tasuline,TooAasta_PuhkusePaevad_Suhe) values (2,'Nimetus2','Kommentaar2',1,24);
insert into puudumineLiik(ID,Nimetus,Kommentaar,Tasuline,TooAasta_PuhkusePaevad_Suhe) values (3,'Nimetus3','Kommentaar3 (tööluus)',0,0);

insert into puudumine(ID,AlgusKP,LoppKP,IsikID,PuudumineLiikID,Kommentaar,Pohjendus) values (1,convert(DATETIME,'2009/05/03 00:00:00'),convert(DATETIME,'2009/05/04 00:00:00'),1,1,'kommentaar1','Põhjendus1');
insert into puudumine(ID,AlgusKP,LoppKP,IsikID,PuudumineLiikID,Kommentaar,Pohjendus) values (2,convert(DATETIME,'2009/05/03 00:00:00'),convert(DATETIME,'2009/05/04 00:00:00'),2,2,'kommentaar2','Põhjendus2');
insert into puudumine(ID,AlgusKP,LoppKP,IsikID,PuudumineLiikID,Kommentaar,Pohjendus) values (3,convert(DATETIME,'2009/05/03 00:00:00'),convert(DATETIME,'2009/05/04 00:00:00'),3,3,'kommentaar3','Põhjendus3');

insert into liik(ID,Nimetus,Kirjeldus,Kommentaar) values (1,'Nimetus1','Kirjeldus1','Kommentaar1');
insert into liik(ID,Nimetus,Kirjeldus,Kommentaar) values (2,'Nimetus2','Kirjeldus2','Kommentaar2');
insert into liik(ID,Nimetus,Kirjeldus,Kommentaar) values (3,'Nimetus3','Kirjeldus3','Kommentaar3');

insert into asutus(ID,Nimetus,LiikID,KontaktIsik,EMail,KontaktTelefon,Faks,Kommentaar) values (1,'Nimetus1',1,'KontaktIsik1','Email1',5435354001,532145001,'Kommentaar1');
insert into asutus(ID,Nimetus,LiikID,KontaktIsik,EMail,KontaktTelefon,Faks,Kommentaar) values (2,'Nimetus2',2,'KontaktIsik2','Email2',5435354002,532145002,'Kommentaar2');
insert into asutus(ID,Nimetus,LiikID,KontaktIsik,EMail,KontaktTelefon,Faks,Kommentaar) values (3,'Nimetus3',3,'KontaktIsik3','Email3',5435354003,532145003,'Kommentaar3');

insert into hooldus(ID,IsikID,AtribuutikaID,AsutusID,HoolduseTeostamiseKP,HooldusePohjendus,AlgusKP,LoppKP,HoolduseTulemus,Kommentaar) values(1,null,1,1,convert(DATETIME,'2010/05/03 00:00:00'),'HooldusePõhjendus1',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'),'HoolduseTulemus1','Kommentaar1');
insert into hooldus(ID,IsikID,AtribuutikaID,AsutusID,HoolduseTeostamiseKP,HooldusePohjendus,AlgusKP,LoppKP,HoolduseTulemus,Kommentaar) values(2,null,2,1,convert(DATETIME,'2010/05/03 00:00:00'),'HooldusePõhjendus2',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'),'HoolduseTulemus2','Kommentaar2');
insert into hooldus(ID,IsikID,AtribuutikaID,AsutusID,HoolduseTeostamiseKP,HooldusePohjendus,AlgusKP,LoppKP,HoolduseTulemus,Kommentaar) values(3,null,3,1,convert(DATETIME,'2010/05/03 00:00:00'),'HooldusePõhjendus3',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'),'HoolduseTulemus3','Kommentaar3');

insert into tehtudtood(ID,tooKirjeldus,TooMaksumus,TooMaksumusValuuta,TooalustamiseKP,TooloppKP,Kommentaar,HooldusID) values(1,'Kirjeldus1',1000,'EUR',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/04 00:00:00'),'Kommentaar1',1);
insert into tehtudtood(ID,tooKirjeldus,TooMaksumus,TooMaksumusValuuta,TooalustamiseKP,TooloppKP,Kommentaar,HooldusID) values(2,'Kirjeldus2',1000,'EUR',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/04 00:00:00'),'Kommentaar2',2);
insert into tehtudtood(ID,tooKirjeldus,TooMaksumus,TooMaksumusValuuta,TooalustamiseKP,TooloppKP,Kommentaar,HooldusID) values(3,'Kirjeldus3',1000,'EUR',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/04 00:00:00'),'Kommentaar3',3);

insert into kutusekaart(ID,AsutusID,KutusekaardiNR,KuuLimiit) values (1,2,235825001,10000);
insert into kutusekaart(ID,AsutusID,KutusekaardiNR,KuuLimiit) values (2,2,235825002,20000);
insert into kutusekaart(ID,AsutusID,KutusekaardiNR,KuuLimiit) values (3,2,235825003,30000);

insert into kutusekaardiValdaja(ID,IsikID,KutusekaartID,AlgusKP,LoppKP,Kommentaar) values (1,1,1,convert(DATETIME,'2010/05/03 00:00:00'),null,'Kommentaar1');
insert into kutusekaardiValdaja(ID,IsikID,KutusekaartID,AlgusKP,LoppKP,Kommentaar) values (2,2,2,convert(DATETIME,'2010/05/03 00:00:00'),null,'Kommentaar2');
insert into kutusekaardiValdaja(ID,IsikID,KutusekaartID,AlgusKP,LoppKP,Kommentaar) values (3,3,3,convert(DATETIME,'2010/05/03 00:00:00'),null,'Kommentaar3');

insert into kutuseOst(ID,KutusekaartID,OstuTeostamiseKP,OstuMaksumus,OstuMaksumusValuuta,Esindus) values (1,1,convert(DATETIME,'2010/05/03 00:00:00'),100,'EUR','Esindus1');
insert into kutuseOst(ID,KutusekaartID,OstuTeostamiseKP,OstuMaksumus,OstuMaksumusValuuta,Esindus) values (2,2,convert(DATETIME,'2010/05/03 00:00:00'),200,'EUR','Esindus2');
insert into kutuseOst(ID,KutusekaartID,OstuTeostamiseKP,OstuMaksumus,OstuMaksumusValuuta,Esindus) values (3,3,convert(DATETIME,'2010/05/03 00:00:00'),300,'EUR','Esindus3');

insert into tooteLiik(ID,Nimetus,Kirjeldus,ProduktMassMahtIndikaator,HoiustamisTempC) values(1,'Nimetus1','Kirjeldus1','KG',1);
insert into tooteLiik(ID,Nimetus,Kirjeldus,ProduktMassMahtIndikaator,HoiustamisTempC) values(2,'Nimetus2','Kirjeldus2','KG',2);
insert into tooteLiik(ID,Nimetus,Kirjeldus,ProduktMassMahtIndikaator,HoiustamisTempC) values(3,'Nimetus3','Kirjeldus3','KG',3);

insert into ValjundKogusMahtKL(ID,Nimetus,Tahis,Kirjeldus,Kommentaar) values (1,'Nimetus1','T1','Kirjeldus1','Kommentaar1');
insert into ValjundKogusMahtKL(ID,Nimetus,Tahis,Kirjeldus,Kommentaar) values (2,'Nimetus2','T2','Kirjeldus2','Kommentaar2');
insert into ValjundKogusMahtKL(ID,Nimetus,Tahis,Kirjeldus,Kommentaar) values (3,'Nimetus3','T3','Kirjeldus3','Kommentaar3');

insert into osakondvaljund(ID,TooteLiikID,OsakondID,LisamiseKP,MassKogus,ValjundKogusMahtKLID) values (1,1,1,convert(DATETIME,'2010/05/03 00:00:00'),1,1);
insert into osakondvaljund(ID,TooteLiikID,OsakondID,LisamiseKP,MassKogus,ValjundKogusMahtKLID) values (2,2,2,convert(DATETIME,'2010/05/03 00:00:00'),2,2);
insert into osakondvaljund(ID,TooteLiikID,OsakondID,LisamiseKP,MassKogus,ValjundKogusMahtKLID) values (3,3,3,convert(DATETIME,'2010/05/03 00:00:00'),3,3);

insert into tooteLiikumine(ID,OsakondValjundID,OsakondID,LiikumiseKP,Kommentaar,LiikuvMassKogus) values (1,1,4,convert(DATETIME,'2010/05/03 00:00:00'),'Kommentaar1',1);
insert into tooteLiikumine(ID,OsakondValjundID,OsakondID,LiikumiseKP,Kommentaar,LiikuvMassKogus) values (2,2,4,convert(DATETIME,'2010/05/03 00:00:00'),'Kommentaar2',2);
insert into tooteLiikumine(ID,OsakondValjundID,OsakondID,LiikumiseKP,Kommentaar,LiikuvMassKogus) values (3,3,4,convert(DATETIME,'2010/05/03 00:00:00'),'Kommentaar3',3);

insert into IsikGraafik(ID,AtribuutikaID,OsakondID,AlgusKP,LoppKP,Kommentaar,IsikID) values (1,1,1,convert(DATETIME,'1996/05/03 00:00:00'),NULL,'Kommentaar1',1);
insert into IsikGraafik(ID,AtribuutikaID,OsakondID,AlgusKP,LoppKP,Kommentaar,IsikID) values (2,2,2,convert(DATETIME,'1996/05/03 00:00:00'),NULL,'Kommentaar2',2);
insert into IsikGraafik(ID,AtribuutikaID,OsakondID,AlgusKP,LoppKP,Kommentaar,IsikID) values (3,3,3,convert(DATETIME,'1996/05/03 00:00:00'),NULL,'Kommentaar3',3);

insert into amet(ID,OsakondID,AmetiNimetus,TegevuseKirjeldus,Kommentaar) values (1,1,'AmetiNimetus1','TegevuseKIrjeldus1','Kommentaar1');
insert into amet(ID,OsakondID,AmetiNimetus,TegevuseKirjeldus,Kommentaar) values (2,2,'AmetiNimetus2','TegevuseKIrjeldus2','Kommentaar2');
insert into amet(ID,OsakondID,AmetiNimetus,TegevuseKirjeldus,Kommentaar) values (3,3,'AmetiNimetus3','TegevuseKIrjeldus3','Kommentaar3');

insert into tootamine(ID,AmetID,IsikID,AlgusKP,LoppKP,TooAeg,Kommentaar,TootajaPalk,TootajaPalkValuuta) values (1,1,1,convert(DATETIME,'1998/05/03 00:00:00'),NULL,1,'Komentaar1',1000,'EUR');
insert into tootamine(ID,AmetID,IsikID,AlgusKP,LoppKP,TooAeg,Kommentaar,TootajaPalk,TootajaPalkValuuta) values (2,2,2,convert(DATETIME,'1998/05/03 00:00:00'),NULL,1,'Komentaar2',1000,'EUR');
insert into tootamine(ID,AmetID,IsikID,AlgusKP,LoppKP,TooAeg,Kommentaar,TootajaPalk,TootajaPalkValuuta) values (3,3,3,convert(DATETIME,'1998/05/03 00:00:00'),NULL,1,'Komentaar3',1000,'EUR');

insert into kliendigrupp(ID,Nimetus,Kirjeldus,Kommentaar) values(1,'Nimetus1','Kirjeldus1','Kommentaar1');
insert into kliendigrupp(ID,Nimetus,Kirjeldus,Kommentaar) values(2,'Nimetus2','Kirjeldus2','Kommentaar2');
insert into kliendigrupp(ID,Nimetus,Kirjeldus,Kommentaar) values(3,'Nimetus3','Kirjeldus3','Kommentaar3');

insert into klient(ID,Nimetus,KliendiGruppID,Kommentaar) values(1,'Nimetus1',1,'Kommentaar1');
insert into klient(ID,Nimetus,KliendiGruppID,Kommentaar) values(2,'Nimetus2',2,'Kommentaar2');
insert into klient(ID,Nimetus,KliendiGruppID,Kommentaar) values(3,'Nimetus3',3,'Kommentaar3');

insert into kontakt(ID,LinnID,Aadress,KontaktTelefon,Kommentaar) values(1,1,'Aadress1',53252001,'Kommentaar1');
insert into kontakt(ID,LinnID,Aadress,KontaktTelefon,Kommentaar) values(2,2,'Aadress2',53252002,'Kommentaar2');
insert into kontakt(ID,LinnID,Aadress,KontaktTelefon,Kommentaar) values(3,3,'Aadress3',53252003,'Kommentaar3');

insert into klientKontakt(ID,KontaktID,KlientID,Kommentaar) values(1,1,1,'Kommentaar1');
insert into klientKontakt(ID,KontaktID,KlientID,Kommentaar) values(2,2,2,'Kommentaar2');
insert into klientKontakt(ID,KontaktID,KlientID,Kommentaar) values(3,3,3,'Kommentaar3');

insert into tooteHind(ID,TooteLiikID,KlientID,KliendiGruppID,Hind,KogusMass,KogusMassIndikaator,HindValuuta) values (1,1,null,1,100,1,'KG','EUR');
insert into tooteHind(ID,TooteLiikID,KlientID,KliendiGruppID,Hind,KogusMass,KogusMassIndikaator,HindValuuta) values (2,2,null,2,200,1,'KG','EUR');
insert into tooteHind(ID,TooteLiikID,KlientID,KliendiGruppID,Hind,KogusMass,KogusMassIndikaator,HindValuuta) values (3,3,null,3,300,1,'KG','EUR');

insert into arve(ID,ArveNR,ArveKoostamiseKP,Arvekoostaja,Kommentaar,KlientID) values(1,'A000001',convert(DATETIME,'2010/05/03 00:00:00'),'Koostaja1','Kommentaar1',1);
insert into arve(ID,ArveNR,ArveKoostamiseKP,Arvekoostaja,Kommentaar,KlientID) values(2,'A000002',convert(DATETIME,'2010/05/03 00:00:00'),'Koostaja2','Kommentaar2',2);
insert into arve(ID,ArveNR,ArveKoostamiseKP,Arvekoostaja,Kommentaar,KlientID) values(3,'A000003',convert(DATETIME,'2010/05/03 00:00:00'),'Koostaja3','Kommentaar3',3);

insert into veoTellimus(ID,KontaktID,HooneID,AlgusKP,Lisaja,Kommentaar,VeoteostamiseTahtaeg) values (1,1,1,convert(DATETIME,'2010/05/03 00:00:00'),'Lisaja1','Kommentaar1',convert(DATETIME,'2010/05/03 00:00:00'));
insert into veoTellimus(ID,KontaktID,HooneID,AlgusKP,Lisaja,Kommentaar,VeoteostamiseTahtaeg) values (2,2,2,convert(DATETIME,'2010/05/03 00:00:00'),'Lisaja2','Kommentaar2',convert(DATETIME,'2010/05/03 00:00:00'));
insert into veoTellimus(ID,KontaktID,HooneID,AlgusKP,Lisaja,Kommentaar,VeoteostamiseTahtaeg) values (3,3,3,convert(DATETIME,'2010/05/03 00:00:00'),'Lisaja3','Kommentaar3',convert(DATETIME,'2010/05/03 00:00:00'));

insert into arverida(ID,TooteLiikID,MassMaht,ArveID,Kommentaar,VeoTellimusID,MassMahtIndikaator,ArvestatudHind) values(1,1,1,1,'Kommentaar1',1,'KG',100);
insert into arverida(ID,TooteLiikID,MassMaht,ArveID,Kommentaar,VeoTellimusID,MassMahtIndikaator,ArvestatudHind) values(2,2,1,2,'Kommentaar1',1,'KG',200);
insert into arverida(ID,TooteLiikID,MassMaht,ArveID,Kommentaar,VeoTellimusID,MassMahtIndikaator,ArvestatudHind) values(3,3,1,3,'Kommentaar1',1,'KG',300);

insert into kaubavedu(ID,HooneID,KontaktID,Kommentaar,AlgusKP,LoppKP) values (1,1,1,'Kommentaar1',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));
insert into kaubavedu(ID,HooneID,KontaktID,Kommentaar,AlgusKP,LoppKP) values (2,2,2,'Kommentaar2',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));
insert into kaubavedu(ID,HooneID,KontaktID,Kommentaar,AlgusKP,LoppKP) values (3,3,3,'Kommentaar3',convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));

insert into vedavadautod(ID,KaubaVeduID,IsikGraafikID,Kommentaar) values (1,1,1,'Kommentaar1');
insert into vedavadautod(ID,KaubaVeduID,IsikGraafikID,Kommentaar) values (2,2,2,'Kommentaar2');
insert into vedavadautod(ID,KaubaVeduID,IsikGraafikID,Kommentaar) values (3,3,3,'Kommentaar3');

insert into tellimusautos(ID,VedavadAutodID,kommentaar,VeoTellimusID,TellimusVastuVoetud,TellimusKohaleToimetatud) values (1,1,'Kommentaar1',1,convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));
insert into tellimusautos(ID,VedavadAutodID,kommentaar,VeoTellimusID,TellimusVastuVoetud,TellimusKohaleToimetatud) values (2,2,'Kommentaar2',2,convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));
insert into tellimusautos(ID,VedavadAutodID,kommentaar,VeoTellimusID,TellimusVastuVoetud,TellimusKohaleToimetatud) values (3,3,'Kommentaar3',3,convert(DATETIME,'2010/05/03 00:00:00'),convert(DATETIME,'2010/05/03 00:00:00'));