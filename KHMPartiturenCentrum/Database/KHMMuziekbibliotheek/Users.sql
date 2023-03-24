/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `Users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `EMail` varchar(250) COLLATE armscii8_bin NOT NULL DEFAULT '',
  `UserName` varchar(50) COLLATE armscii8_bin NOT NULL DEFAULT '',
  `Password` varchar(250) COLLATE armscii8_bin NOT NULL DEFAULT '',
  `RoleId` int(11) NOT NULL DEFAULT 1,
  `Fullname` varchar(250) COLLATE armscii8_bin DEFAULT '',
  `CoverSheetFolder` varchar(500) COLLATE armscii8_bin DEFAULT '',
  `Created` datetime NOT NULL DEFAULT current_timestamp(),
  `Modified` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  UNIQUE KEY `E-Mail` (`EMail`) USING BTREE,
  UNIQUE KEY `UserName` (`UserName`),
  KEY `FKRole` (`RoleId`) USING BTREE,
  CONSTRAINT `FKRoles` FOREIGN KEY (`RoleId`) REFERENCES `UserRoles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

DELETE FROM `Users`;
INSERT INTO `Users` (`Id`, `EMail`, `UserName`, `Password`, `RoleId`, `Fullname`, `CoverSheetFolder`, `Created`, `Modified`) VALUES
	(1, 'herbert.nijkamp@gmail.com', 'herbertnijkamp', 'r3to8FwSrYFkRkqoDf8Ho3U9lxuxsQfmZvEhS9YsM38onB67wRFixLj295YcqETRGvYsMCura2WM0UYvJTX5/g==', 6, 'Herbert (Beheerder)', 'C:\\Data', '2023-03-01 16:34:50', '2023-03-21 15:18:07'),
	(2, 'hnijkamp68@hotmail.com', 'herbie', 'iz6WTSKZtN3FsZ7rl0uAvSTjs4QH6cQPtpBdY509vdB4k+LSsQWDDF/GgBgRqtOu1KhQlBG4E9XBgCOmApSDCg==', 5, 'Herbert Nijkamp (Gebruiker)', '', '2023-03-01 16:35:26', '2023-03-09 14:09:37');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
