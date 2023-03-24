/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `UserRoles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleOrder` int(2) DEFAULT NULL,
  `RoleName` varchar(50) COLLATE armscii8_bin NOT NULL,
  `Description` varchar(100) COLLATE armscii8_bin DEFAULT NULL,
  `Created` datetime NOT NULL DEFAULT current_timestamp(),
  `Modified` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=armscii8 COLLATE=armscii8_bin;

DELETE FROM `UserRoles`;
INSERT INTO `UserRoles` (`Id`, `RoleOrder`, `RoleName`, `Description`, `Created`, `Modified`) VALUES
	(1, 1, 'UserAPP', 'Gebruiker Oefenbestanden', '2023-01-14 14:08:02', '2023-03-04 13:33:40'),
	(2, 3, 'AdminAPP', 'Beheerder Oefenbestanden', '2023-01-14 14:08:09', '2023-03-04 13:33:40'),
	(3, 4, 'UserLibrary', 'Gebruiker Partiturencentrum', '2023-02-02 09:39:35', '2023-03-04 13:33:40'),
	(4, 6, 'AdminLibrary', 'Beheerder Partiturencentrum', '2023-02-02 09:39:35', '2023-03-04 13:33:40'),
	(5, 7, 'SuperUser', 'Gebruiker Oefenbestanden en Partiturencentrum', '2023-02-02 09:39:35', '2023-03-04 13:33:41'),
	(6, 9, 'SuperAdmin', 'Beheerder Oefenbestanden en Partiturencentrum', '2023-02-02 09:39:35', '2023-03-04 13:33:41'),
	(7, 11, 'AdminAPPUserLibrary', 'Beheerder Oefenbestanden en Gebruiker Partiturencentrum', '2023-03-01 16:32:07', '2023-03-04 13:33:41'),
	(8, 14, 'AdminLibraryUserApp', 'Beheerder Partiturencentrum en Gebruiker Oefenbestanden', '2023-03-01 16:33:27', '2023-03-04 13:35:00'),
	(9, 2, 'EditorApp', 'Bewerker Oefenbestanden', '2023-03-04 13:25:37', '2023-03-04 13:33:41'),
	(10, 5, 'EditorLibrary', 'Bewerker Partiturencentrum', '2023-03-04 13:25:37', '2023-03-04 13:33:41'),
	(11, 8, 'SuperEditor', 'Bewerker Oefenbestanden en Partiturencentrum', '2023-03-04 13:25:37', '2023-03-04 13:33:41'),
	(12, 10, 'EditorAppUserLibrary', 'Bewerker Oefenbestanden en Gebruiker Partiturencentrum', '2023-03-04 13:25:37', '2023-03-04 13:33:41'),
	(13, 13, 'EditorLibraryUserApp', 'Bewerker Partiturencentrum en Gebruiker Oefenbestanden', '2023-03-04 13:25:38', '2023-03-04 13:35:00'),
	(14, 12, 'AdminAppEditorLibrary', 'Beheerder Oefenbestanden en Bewerker Partiturencentrum', '2023-03-04 13:25:38', '2023-03-04 13:34:17'),
	(15, 15, 'AdminLibraryEditorApp', 'Beheerder Partiturencentrum en Bewerker Oefenbestanden', '2023-03-04 13:25:38', '2023-03-04 13:43:58');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
