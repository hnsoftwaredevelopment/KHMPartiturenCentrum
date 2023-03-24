/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `History` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LogDate` datetime NOT NULL DEFAULT current_timestamp(),
  `UserId` int(11) NOT NULL DEFAULT 1,
  `Action` varchar(150) NOT NULL DEFAULT '',
  `Description` varchar(500) NOT NULL DEFAULT '',
  `Created` datetime NOT NULL DEFAULT current_timestamp(),
  `Modifief` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`Id`) USING BTREE,
  KEY `FKUser` (`UserId`),
  CONSTRAINT `FKUser` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4;

DELETE FROM `History`;
INSERT INTO `History` (`Id`, `LogDate`, `UserId`, `Action`, `Description`, `Created`, `Modifief`) VALUES
	(1, '2023-03-23 14:39:21', 1, 'Gebruiker ingelogt', 'Herbert (Beheerder) is ingelogt', '2023-03-23 14:39:21', '2023-03-23 14:39:21'),
	(2, '2023-03-23 14:47:34', 1, 'Gebruiker uitgelogt', 'Herbert (Beheerder) heeft de applicatie afgesloten', '2023-03-23 14:47:34', '2023-03-23 14:47:34'),
	(3, '2023-03-24 10:43:33', 1, 'Gebruiker ingelogt', 'Herbert (Beheerder) is ingelogt', '2023-03-24 10:43:33', '2023-03-24 10:43:33'),
	(4, '2023-03-24 10:51:52', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 481 naar 900.', '2023-03-24 10:51:52', '2023-03-24 10:51:52'),
	(5, '2023-03-24 10:52:33', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 494 naar 901.', '2023-03-24 10:52:33', '2023-03-24 10:52:33'),
	(6, '2023-03-24 10:53:04', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 495 naar 902.', '2023-03-24 10:53:04', '2023-03-24 10:53:04'),
	(7, '2023-03-24 10:53:39', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 496 naar 903.', '2023-03-24 10:53:39', '2023-03-24 10:53:39'),
	(8, '2023-03-24 10:54:32', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 497 naar 904.', '2023-03-24 10:54:32', '2023-03-24 10:54:32'),
	(9, '2023-03-24 10:55:45', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 515 naar 905.', '2023-03-24 10:55:45', '2023-03-24 10:55:45'),
	(10, '2023-03-24 10:56:35', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 516 naar 906.', '2023-03-24 10:56:35', '2023-03-24 10:56:35'),
	(11, '2023-03-24 10:57:08', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 517 naar 907.', '2023-03-24 10:57:08', '2023-03-24 10:57:08'),
	(12, '2023-03-24 10:57:45', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 518 naar 908.', '2023-03-24 10:57:45', '2023-03-24 10:57:45'),
	(13, '2023-03-24 10:58:15', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 519 naar 909.', '2023-03-24 10:58:15', '2023-03-24 10:58:15'),
	(14, '2023-03-24 10:58:52', 1, 'Partituur omgenummerd', 'Partituur omgenummerd van 520 naar 910.', '2023-03-24 10:58:52', '2023-03-24 10:58:52'),
	(15, '2023-03-24 10:59:24', 1, 'Partituur gewijzigd', 'Partituur: 900', '2023-03-24 10:59:24', '2023-03-24 10:59:24'),
	(16, '2023-03-24 10:59:38', 1, 'Partituur gewijzigd', 'Partituur: 901', '2023-03-24 10:59:38', '2023-03-24 10:59:38'),
	(17, '2023-03-24 10:59:49', 1, 'Partituur gewijzigd', 'Partituur: 902', '2023-03-24 10:59:49', '2023-03-24 10:59:49'),
	(18, '2023-03-24 10:59:55', 1, 'Partituur gewijzigd', 'Partituur: 903', '2023-03-24 10:59:55', '2023-03-24 10:59:55'),
	(19, '2023-03-24 11:00:03', 1, 'Partituur gewijzigd', 'Partituur: 904', '2023-03-24 11:00:03', '2023-03-24 11:00:03'),
	(20, '2023-03-24 11:02:41', 1, 'Partituur gewijzigd', 'Partituur: 514', '2023-03-24 11:02:41', '2023-03-24 11:02:41'),
	(21, '2023-03-24 13:52:43', 1, 'Partituur gewijzigd', 'Partituur: 911', '2023-03-24 13:52:43', '2023-03-24 13:52:43'),
	(22, '2023-03-24 13:52:59', 1, 'Partituur gewijzigd', 'Partituur: 384', '2023-03-24 13:52:59', '2023-03-24 13:52:59'),
	(23, '2023-03-24 13:53:50', 1, 'Partituur gewijzigd', 'Partituur: 474', '2023-03-24 13:53:50', '2023-03-24 13:53:50'),
	(24, '2023-03-24 13:54:04', 1, 'Partituur gewijzigd', 'Partituur: 912', '2023-03-24 13:54:04', '2023-03-24 13:54:04'),
	(25, '2023-03-24 13:54:52', 1, 'Partituur gewijzigd', 'Partituur: 009', '2023-03-24 13:54:52', '2023-03-24 13:54:52'),
	(26, '2023-03-24 13:55:55', 1, 'Partituur gewijzigd', 'Partituur: 011', '2023-03-24 13:55:55', '2023-03-24 13:55:55'),
	(27, '2023-03-24 13:56:28', 1, 'Partituur gewijzigd', 'Partituur: 013', '2023-03-24 13:56:28', '2023-03-24 13:56:28'),
	(28, '2023-03-24 13:57:10', 1, 'Partituur gewijzigd', 'Partituur: 018', '2023-03-24 13:57:10', '2023-03-24 13:57:10'),
	(29, '2023-03-24 13:59:44', 1, 'Gebruiker uitgelogt', 'Herbert (Beheerder) heeft de applicatie afgesloten', '2023-03-24 13:59:44', '2023-03-24 13:59:44');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
