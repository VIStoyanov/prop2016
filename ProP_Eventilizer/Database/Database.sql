-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 28, 2016 at 01:22 AM
-- Server version: 10.1.13-MariaDB
-- PHP Version: 5.6.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mydb`
--
CREATE DATABASE IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `mydb`;

-- --------------------------------------------------------

--
-- Table structure for table `booking`
--

CREATE TABLE `booking` (
  `Ticket number` int(11) NOT NULL,
  `Client_Client_id` int(11) DEFAULT NULL,
  `Price` decimal(11,0) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `booking`
--

INSERT INTO `booking` (`Ticket number`, `Client_Client_id`, `Price`) VALUES
(8, 8, '55'),
(9, 9, '65'),
(10, 10, '55');

-- --------------------------------------------------------

--
-- Table structure for table `check`
--

CREATE TABLE `check` (
  `Client_Client_id` int(11) NOT NULL,
  `Timestamp` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Type` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `check`
--

INSERT INTO `check` (`Client_Client_id`, `Timestamp`, `Type`) VALUES
(8, '2016-06-27 17:44:30', 'TOARRIVE'),
(9, '2016-06-27 17:48:49', 'TOARRIVE'),
(10, '2016-06-27 17:54:10', 'TOARRIVE');

-- --------------------------------------------------------

--
-- Table structure for table `client`
--

CREATE TABLE `client` (
  `Client_id` int(11) NOT NULL,
  `First_name` varchar(45) DEFAULT NULL,
  `Last_name` varchar(45) DEFAULT NULL,
  `Balance` decimal(11,2) DEFAULT '0.00',
  `e-mail` varchar(1024) DEFAULT NULL,
  `Password` varchar(156) DEFAULT NULL,
  `Telephone` int(11) DEFAULT NULL,
  `RFID` varchar(20) DEFAULT NULL,
  `Client_Client_id` int(11) DEFAULT NULL,
  `activated` int(11) DEFAULT '0',
  `barcode` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `client`
--

INSERT INTO `client` (`Client_id`, `First_name`, `Last_name`, `Balance`, `e-mail`, `Password`, `Telephone`, `RFID`, `Client_Client_id`, `activated`, `barcode`) VALUES
(8, 'Mirela', 'Goranova', '440.00', 'goranova.mirela@gmail.com', '$2y$10$0zQhNi8.2kBZ4huZIeb1cOO6cvK2gRSbQauuoE5w.hIdT.fUvDhgq', 2147483647, '', NULL, 1, 1232426294),
(9, 'Hristo', 'Atanasov', '20.00', 'hristokgj@gmail.com', '$2y$10$ld3OXZDdTR.YQ.h1AuM2jenshhaqjHQGWlbqBNcIjTl.ThzohbxoS', 232354235, '', NULL, 1, 1022888477),
(10, 'Vasil', 'Stoyanov', '52.50', 'zaekvamp@abv.bg', '$2y$10$FHr.oofLROtDWtrF3HxmAuuEu769ZaE2q075uI2Po3pAm.HZrEb2u', 231242, '', 8, 1, 1316271455);

-- --------------------------------------------------------

--
-- Table structure for table `equipment`
--

CREATE TABLE `equipment` (
  `Equipment_id` int(11) NOT NULL,
  `Description` varchar(20) DEFAULT NULL,
  `Price` decimal(10,0) DEFAULT NULL,
  `InStock` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `equipment`
--

INSERT INTO `equipment` (`Equipment_id`, `Description`, `Price`, `InStock`) VALUES
(1, 'Camera', '3', 21),
(2, 'Tripod', '2', 38),
(3, 'Laptop Charger', '2', 21),
(4, 'Universal Charger', '1', 14);

-- --------------------------------------------------------

--
-- Table structure for table `products`
--

CREATE TABLE `products` (
  `Product_id` int(11) NOT NULL,
  `Product_desc` varchar(45) DEFAULT NULL,
  `Price` decimal(10,0) DEFAULT NULL,
  `InStock` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `products`
--

INSERT INTO `products` (`Product_id`, `Product_desc`, `Price`, `InStock`) VALUES
(1, 'Fries', '3', 96),
(2, 'Pizza', '3', 100),
(3, 'Burger', '4', 94),
(4, 'Ice cream', '2', 99),
(5, 'Cake', '4', 100),
(6, 'Doughnut', '2', 100),
(7, 'Hot dog', '3', 100),
(8, 'Sandwich', '4', 52),
(9, 'Popcorn', '2', 45),
(10, 'Coca Cola', '3', 196),
(11, 'Fanta', '3', 199),
(12, 'Pepsi', '3', 418),
(13, 'Heineken', '4', 249),
(14, 'Bacardi', '6', 60),
(15, 'Jaegermeister', '7', 60),
(16, 'Sprite', '3', 50),
(17, 'Perrier', '3', 50),
(18, 'Amstel', '4', 50);

-- --------------------------------------------------------

--
-- Table structure for table `products_purchase`
--

CREATE TABLE `products_purchase` (
  `Products_Product_id` int(11) NOT NULL,
  `Purchase_Purchase_id` int(11) NOT NULL,
  `Quantity` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `products_purchase`
--

INSERT INTO `products_purchase` (`Products_Product_id`, `Purchase_Purchase_id`, `Quantity`) VALUES
(1, 1, 1),
(1, 3, 2),
(3, 1, 2),
(3, 3, 1),
(10, 2, 1),
(10, 4, 1),
(11, 2, 1),
(13, 4, 1);

-- --------------------------------------------------------

--
-- Table structure for table `purchase`
--

CREATE TABLE `purchase` (
  `Purchase_id` int(11) NOT NULL,
  `Client_Client_id` int(11) NOT NULL,
  `Date` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Shop_Shop_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `purchase`
--

INSERT INTO `purchase` (`Purchase_id`, `Client_Client_id`, `Date`, `Shop_Shop_id`) VALUES
(1, 8, '2016-06-27 18:42:45', 2),
(2, 8, '2016-06-27 18:50:43', 1),
(3, 8, '2016-06-27 18:53:37', 2),
(4, 8, '2016-06-27 18:54:32', 1);

-- --------------------------------------------------------

--
-- Table structure for table `rent`
--

CREATE TABLE `rent` (
  `Rent_id` int(11) NOT NULL,
  `TimeOfRent` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `TimeOfReturn` timestamp NULL DEFAULT NULL,
  `Client_Client_id1` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `rent`
--

INSERT INTO `rent` (`Rent_id`, `TimeOfRent`, `TimeOfReturn`, `Client_Client_id1`) VALUES
(1, '2016-06-27 18:22:02', '2016-06-27 18:27:11', 8),
(2, '2016-06-27 19:09:49', '2016-06-27 19:12:10', 8);

-- --------------------------------------------------------

--
-- Table structure for table `rent_equipment`
--

CREATE TABLE `rent_equipment` (
  `Equipment_Equipment_id` int(11) NOT NULL,
  `Rent_Rent_id` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `rent_equipment`
--

INSERT INTO `rent_equipment` (`Equipment_Equipment_id`, `Rent_Rent_id`, `Amount`) VALUES
(1, 1, 1),
(1, 2, 1),
(2, 1, 1),
(3, 2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `security`
--

CREATE TABLE `security` (
  `token` varchar(156) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `security`
--

INSERT INTO `security` (`token`) VALUES
('$2y$10$C3AU.4ZpHY1HEyTf34gZy.fqnKVCXJ6PaQG5sQSp/gqWm7QWGpYzW'),
('$2y$10$gqSrbJwTC23wTyIYweoaU.YoVX3ikpGcAxB8D9lIVg.3jqmS1iFvm'),
('$2y$10$K0EFbaFfBUf5SjdWAQFGa.KfGARAPwDJgXjtxdqSHM1OkQ5iMN.Ki'),
('$2y$10$k7/zs3P4iwm6pHxt/tZSQO.Nj6RETKmvpFT1b9yAu7kpSQ5xAomm.'),
('$2y$10$l3ir5p5.mfk/SvhPyZjxXONchroRNFGyf8p6nTnO4Mui2KVSnGos.'),
('$2y$10$qHH.7n.PTsmky/GnomHv3Ob4yrsQMtdkPGBm2aAiaW1VZLZ4HQaQa'),
('$2y$10$qPMphGIRlDCnktzKTMgpqeVpExvMHIyv9dhrSLGau1YX/3PIR9yre'),
('$2y$10$T3OUJQ1Lte1PSnSFOwH03uvEGBVGAKV0AKs7tiSZBpXRItcW8XqY2');

-- --------------------------------------------------------

--
-- Table structure for table `shop`
--

CREATE TABLE `shop` (
  `Shop_id` int(11) NOT NULL,
  `Shop_name` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `shop`
--

INSERT INTO `shop` (`Shop_id`, `Shop_name`) VALUES
(1, 'West Coast'),
(2, 'Bob''s Shop');

-- --------------------------------------------------------

--
-- Table structure for table `shops_product`
--

CREATE TABLE `shops_product` (
  `Products_Product_id` int(11) NOT NULL,
  `Shop_Shop_id` int(11) NOT NULL,
  `Quantity` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `shops_product`
--

INSERT INTO `shops_product` (`Products_Product_id`, `Shop_Shop_id`, `Quantity`) VALUES
(1, 2, 15),
(2, 2, 19),
(3, 2, 14),
(4, 2, 15),
(5, 2, 16),
(10, 1, 16),
(11, 1, 19),
(12, 1, 18),
(13, 1, 19),
(14, 1, 20);

-- --------------------------------------------------------

--
-- Table structure for table `tent`
--

CREATE TABLE `tent` (
  `Tent_spot_id` int(11) NOT NULL,
  `Client_Client_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `tent`
--

INSERT INTO `tent` (`Tent_spot_id`, `Client_Client_id`) VALUES
(1, NULL),
(2, NULL),
(3, NULL),
(4, NULL),
(5, NULL),
(6, NULL),
(7, NULL),
(8, NULL),
(9, NULL),
(10, NULL),
(11, NULL),
(12, NULL),
(13, NULL),
(15, NULL),
(14, 8);

-- --------------------------------------------------------

--
-- Table structure for table `transaction`
--

CREATE TABLE `transaction` (
  `Transaction_id` varchar(20) NOT NULL,
  `Client_Client_id` int(11) NOT NULL,
  `Amount` decimal(11,2) DEFAULT NULL,
  `DateTime` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `Booking_Ticket number` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `transaction`
--

INSERT INTO `transaction` (`Transaction_id`, `Client_Client_id`, `Amount`, `DateTime`, `Booking_Ticket number`) VALUES
('66T7Z7O822ORJ063GWPX', 9, '65.00', '2016-06-27 17:48:49', 9),
('71JTVF6XDKS0RHM9MTLI', 10, '20.00', '2016-06-27 17:54:10', NULL),
('BB1GK4Z6JEKKOZI3NNOD', 10, '55.00', '2016-06-27 17:54:10', 10),
('IIRENQJN48DLVTLIDPCS', 8, '30.00', '2016-06-27 17:44:30', NULL),
('U0NK970DDF2VUY84L5B8', 8, '55.00', '2016-06-27 17:44:30', 8);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `booking`
--
ALTER TABLE `booking`
  ADD PRIMARY KEY (`Ticket number`),
  ADD KEY `fk_Booking_Client1_idx` (`Client_Client_id`);

--
-- Indexes for table `check`
--
ALTER TABLE `check`
  ADD PRIMARY KEY (`Client_Client_id`),
  ADD KEY `fk_Check_Client_idx` (`Client_Client_id`);

--
-- Indexes for table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`Client_id`),
  ADD KEY `fk_Client_Client1_idx` (`Client_Client_id`);

--
-- Indexes for table `equipment`
--
ALTER TABLE `equipment`
  ADD PRIMARY KEY (`Equipment_id`);

--
-- Indexes for table `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Product_id`);

--
-- Indexes for table `products_purchase`
--
ALTER TABLE `products_purchase`
  ADD PRIMARY KEY (`Products_Product_id`,`Purchase_Purchase_id`),
  ADD KEY `fk_Products_purchase_Purchase1_idx` (`Purchase_Purchase_id`);

--
-- Indexes for table `purchase`
--
ALTER TABLE `purchase`
  ADD PRIMARY KEY (`Purchase_id`),
  ADD KEY `fk_Purchase_Client1_idx` (`Client_Client_id`),
  ADD KEY `fk_Purchase_Shop1_idx` (`Shop_Shop_id`);

--
-- Indexes for table `rent`
--
ALTER TABLE `rent`
  ADD PRIMARY KEY (`Rent_id`),
  ADD KEY `fk_Rent_Client1_idx` (`Client_Client_id1`);

--
-- Indexes for table `rent_equipment`
--
ALTER TABLE `rent_equipment`
  ADD PRIMARY KEY (`Equipment_Equipment_id`,`Rent_Rent_id`),
  ADD KEY `fk_Rent_Equipment_Rent1_idx` (`Rent_Rent_id`);

--
-- Indexes for table `security`
--
ALTER TABLE `security`
  ADD PRIMARY KEY (`token`);

--
-- Indexes for table `shop`
--
ALTER TABLE `shop`
  ADD PRIMARY KEY (`Shop_id`);

--
-- Indexes for table `shops_product`
--
ALTER TABLE `shops_product`
  ADD PRIMARY KEY (`Products_Product_id`,`Shop_Shop_id`),
  ADD KEY `fk_Products_has_Shop_Shop1_idx` (`Shop_Shop_id`),
  ADD KEY `fk_Products_has_Shop_Products1_idx` (`Products_Product_id`);

--
-- Indexes for table `tent`
--
ALTER TABLE `tent`
  ADD PRIMARY KEY (`Tent_spot_id`),
  ADD KEY `fk_Tent_Client1_idx` (`Client_Client_id`);

--
-- Indexes for table `transaction`
--
ALTER TABLE `transaction`
  ADD PRIMARY KEY (`Transaction_id`,`Client_Client_id`),
  ADD KEY `fk_Transaction_Client1_idx` (`Client_Client_id`),
  ADD KEY `fk_Transaction_Booking1_idx` (`Booking_Ticket number`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `booking`
--
ALTER TABLE `booking`
  MODIFY `Ticket number` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `client`
--
ALTER TABLE `client`
  MODIFY `Client_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
--
-- AUTO_INCREMENT for table `equipment`
--
ALTER TABLE `equipment`
  MODIFY `Equipment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `products`
--
ALTER TABLE `products`
  MODIFY `Product_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT for table `rent`
--
ALTER TABLE `rent`
  MODIFY `Rent_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `shop`
--
ALTER TABLE `shop`
  MODIFY `Shop_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT for table `tent`
--
ALTER TABLE `tent`
  MODIFY `Tent_spot_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `booking`
--
ALTER TABLE `booking`
  ADD CONSTRAINT `fk_Booking_Client1` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `check`
--
ALTER TABLE `check`
  ADD CONSTRAINT `fk_Check_Client` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `client`
--
ALTER TABLE `client`
  ADD CONSTRAINT `fk_Client_Client1` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `products_purchase`
--
ALTER TABLE `products_purchase`
  ADD CONSTRAINT `fk_Products_purchase_Products1` FOREIGN KEY (`Products_Product_id`) REFERENCES `products` (`Product_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Products_purchase_Purchase1` FOREIGN KEY (`Purchase_Purchase_id`) REFERENCES `purchase` (`Purchase_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `purchase`
--
ALTER TABLE `purchase`
  ADD CONSTRAINT `fk_Purchase_Client1` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Purchase_Shop1` FOREIGN KEY (`Shop_Shop_id`) REFERENCES `shop` (`Shop_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `rent`
--
ALTER TABLE `rent`
  ADD CONSTRAINT `fk_Rent_Client1` FOREIGN KEY (`Client_Client_id1`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `rent_equipment`
--
ALTER TABLE `rent_equipment`
  ADD CONSTRAINT `fk_Rent_Equipment_Equipment1` FOREIGN KEY (`Equipment_Equipment_id`) REFERENCES `equipment` (`Equipment_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Rent_Equipment_Rent1` FOREIGN KEY (`Rent_Rent_id`) REFERENCES `rent` (`Rent_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `shops_product`
--
ALTER TABLE `shops_product`
  ADD CONSTRAINT `fk_Products_has_Shop_Products1` FOREIGN KEY (`Products_Product_id`) REFERENCES `products` (`Product_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Products_has_Shop_Shop1` FOREIGN KEY (`Shop_Shop_id`) REFERENCES `shop` (`Shop_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `tent`
--
ALTER TABLE `tent`
  ADD CONSTRAINT `fk_Tent_Client1` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `fk_Transaction_Booking1` FOREIGN KEY (`Booking_Ticket number`) REFERENCES `booking` (`Ticket number`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_Transaction_Client1` FOREIGN KEY (`Client_Client_id`) REFERENCES `client` (`Client_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
