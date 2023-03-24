/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `HistoryDetails` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LogId` int(11) NOT NULL DEFAULT 1,
  `ModifiedField` varchar(100) NOT NULL DEFAULT '',
  `OldValue` varchar(500) DEFAULT '',
  `NewValue` varchar(500) DEFAULT '',
  `Created` datetime NOT NULL DEFAULT current_timestamp(),
  `Modifief` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`),
  KEY `FKLog` (`LogId`),
  CONSTRAINT `FKLog` FOREIGN KEY (`LogId`) REFERENCES `History` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4;

DELETE FROM `HistoryDetails`;
INSERT INTO `HistoryDetails` (`Id`, `LogId`, `ModifiedField`, `OldValue`, `NewValue`, `Created`, `Modifief`) VALUES
	(1, 4, 'Partituur omgenummerd', '481', '900', '2023-03-24 10:51:52', '2023-03-24 10:51:52'),
	(2, 5, 'Partituur omgenummerd', '494', '901', '2023-03-24 10:52:33', '2023-03-24 10:52:33'),
	(3, 6, 'Partituur omgenummerd', '495', '902', '2023-03-24 10:53:04', '2023-03-24 10:53:04'),
	(4, 7, 'Partituur omgenummerd', '496', '903', '2023-03-24 10:53:39', '2023-03-24 10:53:39'),
	(5, 8, 'Partituur omgenummerd', '497', '904', '2023-03-24 10:54:32', '2023-03-24 10:54:32'),
	(6, 9, 'Partituur omgenummerd', '515', '905', '2023-03-24 10:55:45', '2023-03-24 10:55:45'),
	(7, 10, 'Partituur omgenummerd', '516', '906', '2023-03-24 10:56:35', '2023-03-24 10:56:35'),
	(8, 11, 'Partituur omgenummerd', '517', '907', '2023-03-24 10:57:08', '2023-03-24 10:57:08'),
	(9, 12, 'Partituur omgenummerd', '518', '908', '2023-03-24 10:57:45', '2023-03-24 10:57:45'),
	(10, 13, 'Partituur omgenummerd', '519', '909', '2023-03-24 10:58:16', '2023-03-24 10:58:16'),
	(11, 14, 'Partituur omgenummerd', '520', '910', '2023-03-24 10:58:53', '2023-03-24 10:58:53'),
	(12, 15, 'Archief', 'Huisarchief', 'Huisarchief', '2023-03-24 10:59:24', '2023-03-24 10:59:24'),
	(13, 16, 'Repertoire', 'Basis', 'Project', '2023-03-24 10:59:38', '2023-03-24 10:59:38'),
	(14, 17, 'Repertoire', 'Basis', 'Project', '2023-03-24 10:59:49', '2023-03-24 10:59:49'),
	(15, 18, 'Repertoire', 'Basis', 'Project', '2023-03-24 10:59:55', '2023-03-24 10:59:55'),
	(16, 19, 'Repertoire', 'Basis', 'Project', '2023-03-24 11:00:03', '2023-03-24 11:00:03'),
	(17, 20, 'Repertoire', 'Project', 'Standaard', '2023-03-24 11:02:41', '2023-03-24 11:02:41'),
	(18, 21, 'Liedtekst aangepast', '', '', '2023-03-24 13:52:43', '2023-03-24 13:52:43'),
	(19, 22, 'Liedtekst aangepast', '', '', '2023-03-24 13:52:59', '2023-03-24 13:52:59'),
	(20, 23, 'Liedtekst aangepast', '', '', '2023-03-24 13:53:50', '2023-03-24 13:53:50'),
	(21, 24, 'Liedtekst aangepast', '', '', '2023-03-24 13:54:04', '2023-03-24 13:54:04'),
	(22, 25, 'Liedtekst aangepast', '', '', '2023-03-24 13:54:52', '2023-03-24 13:54:52'),
	(23, 26, 'Liedtekst aangepast', '', '', '2023-03-24 13:55:55', '2023-03-24 13:55:55'),
	(24, 27, 'Liedtekst aangepast', '', '', '2023-03-24 13:56:28', '2023-03-24 13:56:28'),
	(25, 28, 'Liedtekst aangepast', '', '', '2023-03-24 13:57:10', '2023-03-24 13:57:10');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
