/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

DROP TABLE IF EXISTS `Bibliotheek_View`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `Bibliotheek_View` AS select `b`.`Id` AS `Id`,case when `b`.`SubNummer` is null or `b`.`SubNummer` = '' then concat(`b`.`Partituur`) else concat(`b`.`Partituur`,'-',`b`.`SubNummer`) end AS `PartituurNummer`,`b`.`Partituur` AS `Partituur`,`b`.`SubNummer` AS `SubNummer`,`b`.`Titel` AS `Titel`,`b`.`Ondertitel` AS `Ondertitel`,`b`.`Componist` AS `Componist`,`b`.`Tekstschrijver` AS `Tekstschrijver`,`b`.`Arrangement` AS `Arrangement`,`b`.`ArchiefId` AS `ArchiefId`,`a`.`Archief` AS `AchiefNaam`,`b`.`RepertoireId` AS `RepertoireId`,`r`.`Repertoire` AS `RepertoireNaam`,`b`.`TaalId` AS `TaalId`,`t`.`Taal` AS `TaalNaam`,`b`.`GenreId` AS `GenreId`,`g`.`Genre` AS `GenreNaam`,`b`.`Lyrics` AS `Lyrics`,`b`.`Gecontroleerd` AS `Gecontroleerd`,`b`.`Gedigitaliseerd` AS `Gedigitaliseerd`,`b`.`Revisie` AS `Revisie`,`b`.`BegeleidingId` AS `BegeleidingId`,`be`.`Begeleiding` AS `BegeleidingNaam`,`b`.`PDFORP` AS `PDFORP`,`b`.`PDFORK` AS `PDFORK`,`b`.`PDFTOP` AS `PDFTOP`,`b`.`PDFTOK` AS `PDFTOK`,`b`.`MSCORP` AS `MSCORP`,`b`.`MSCORK` AS `MSCORK`,`b`.`MSCTOP` AS `MSCTOP`,`b`.`MSCTOK` AS `MSCTOK`,`b`.`MP3TOT` AS `MP3TOT`,`b`.`MP3T1` AS `MP3T1`,`b`.`MP3T2` AS `MP3T2`,`b`.`MP3B1` AS `MP3B1`,`b`.`MP3B2` AS `MP3B2`,`b`.`MP3SOL` AS `MP3SOL`,`b`.`MP3PIA` AS `MP3PIA`,`b`.`Online` AS `Online`,`b`.`UHH` AS `UHH`,`b`.`Muziekstuk` AS `Muziekstuk`,`b`.`Opmerkingen` AS `Opmerkingen`,`b`.`AantalUitgever1` AS `AantalUitgever1`,`b`.`AantalUitgever2` AS `AantalUitgever2`,`b`.`AantalUitgever3` AS `AantalUitgever3`,`b`.`AantalUitgever4` AS `AantalUitgever4`,`b`.`Uitgever1Id` AS `Uitgever1Id`,`u1`.`Naam` AS `Uitgever1Naam`,`b`.`Uitgever2Id` AS `Uitgever2Id`,`u2`.`Naam` AS `Uitgever2Naam`,`b`.`Uitgever3Id` AS `Uitgever3Id`,`u3`.`Naam` AS `Uitgever3Naam`,`b`.`Uitgever4Id` AS `Uitgever4Id`,`u4`.`Naam` AS `Uitgever4Naam`,`b`.`Duur` AS `Duur` from (((((((((`Bibliotheek` `b` join `Archief` `a` on(`b`.`ArchiefId` = `a`.`Id`)) join `Repertoire` `r` on(`b`.`RepertoireId` = `r`.`Id`)) join `Taal` `t` on(`b`.`TaalId` = `t`.`Id`)) join `Genre` `g` on(`b`.`GenreId` = `g`.`Id`)) join `Begeleiding` `be` on(`b`.`BegeleidingId` = `be`.`Id`)) join `Uitgever` `u1` on(`b`.`Uitgever1Id` = `u1`.`Id`)) join `Uitgever` `u2` on(`b`.`Uitgever2Id` = `u2`.`Id`)) join `Uitgever` `u3` on(`b`.`Uitgever3Id` = `u3`.`Id`)) join `Uitgever` `u4` on(`b`.`Uitgever4Id` = `u4`.`Id`)) where `b`.`Titel` is not null order by `b`.`Partituur`,`b`.`SubNummer`;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
