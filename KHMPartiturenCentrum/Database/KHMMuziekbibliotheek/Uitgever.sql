/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `Uitgever` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Naam` varchar(250) DEFAULT NULL,
  `Adres1` varchar(250) DEFAULT NULL,
  `Adres2` varchar(250) DEFAULT NULL,
  `Postcode` varchar(10) DEFAULT NULL,
  `Plaats` varchar(150) DEFAULT NULL,
  `Telefoon` varchar(15) DEFAULT NULL,
  `Website` varchar(450) DEFAULT NULL,
  `Klantnummer` varchar(50) DEFAULT NULL,
  `Notities` mediumtext DEFAULT NULL,
  `Created` datetime DEFAULT current_timestamp(),
  `Modifief` datetime DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

DELETE FROM `Uitgever`;
INSERT INTO `Uitgever` (`Id`, `Naam`, `Adres1`, `Adres2`, `Postcode`, `Plaats`, `Telefoon`, `Website`, `Klantnummer`, `Notities`, `Created`, `Modifief`) VALUES
	(1, '<Niet geselecteerd>', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-01-29 14:37:15', '2023-02-03 14:51:25');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
