-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 17, 2021 at 04:52 PM
-- Server version: 10.4.17-MariaDB
-- PHP Version: 8.0.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `smart_car`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `getCar` (IN `idn` INT)  BEGIN
select vehicles.id,users.name, users.surname,users.email, cities.city_name, vehicles.plate, brands.brand_name, models.model_name, colors.color_name, engine_model.type from vehicles join users on vehicles.user_id=users.id join cities on vehicles.city_code=cities.plate_code join brands on vehicles.brand=brands.id join models on vehicles.model=models.id join engine_model on engine_model.id = vehicles.engine_model  join colors on vehicles.color=colors.id where vehicles.id=idn;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `grage` (IN `idn` INT)  BEGIN
 Select vehicles.id,users.name, users.surname, users.email, cities.city_name, brands.brand_name, vehicles.plate from vehicles join users on vehicles.user_id=users.id  join brands on vehicles.brand = brands.id join cities on vehicles.city_code=cities.plate_code  where users.id= idn;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `brands`
--

CREATE TABLE `brands` (
  `id` int(11) NOT NULL,
  `brand_name` varchar(75) COLLATE utf8_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `brands`
--

INSERT INTO `brands` (`id`, `brand_name`) VALUES
(3, 'BMW'),
(4, 'AUDİ'),
(5, 'MERCEDES');

-- --------------------------------------------------------

--
-- Table structure for table `cities`
--

CREATE TABLE `cities` (
  `plate_code` int(11) NOT NULL,
  `city_name` varchar(50) COLLATE utf8_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `cities`
--

INSERT INTO `cities` (`plate_code`, `city_name`) VALUES
(1, 'Adana'),
(2, 'Adıyaman'),
(3, 'Afyon'),
(4, 'Ağrı'),
(5, 'Amasya'),
(6, 'Ankara'),
(7, 'Antalya'),
(8, 'Artvin'),
(9, 'Aydın'),
(10, 'Balıkesir'),
(11, 'Bilecik'),
(12, 'Bingöl'),
(13, 'Bitlis'),
(14, 'Bolu'),
(15, 'Burdur'),
(16, 'Bursa'),
(17, 'Çanakkale'),
(18, 'Çankırı'),
(19, 'Çorum'),
(20, 'Denizli'),
(21, 'Diyarbakır'),
(22, 'Edirne'),
(23, 'Elazığ'),
(24, 'Erzincan'),
(25, 'Erzurum'),
(26, 'Eskişehir'),
(27, 'Gaziantep'),
(28, 'Giresun'),
(29, 'Gümüşhane'),
(30, 'Hakkari'),
(31, 'Hatay'),
(32, 'Isparta'),
(33, 'Mersin'),
(34, 'İstanbul'),
(35, 'İzmir'),
(36, 'Kars'),
(37, 'Kastamonu'),
(38, 'Kayseri'),
(39, 'Kırklareli'),
(40, 'Kırşehir'),
(41, 'Kocaeli'),
(42, 'Konya'),
(43, 'Kütahya'),
(44, 'Malatya'),
(45, 'Manisa'),
(46, 'K.Maraş'),
(47, 'Mardin'),
(48, 'Muğla'),
(49, 'Muş'),
(50, 'Nevşehir'),
(51, 'Niğde'),
(52, 'Ordu'),
(53, 'Rize'),
(54, 'Sakarya'),
(55, 'Samsun'),
(56, 'Siirt'),
(57, 'Sinop'),
(58, 'Sivas'),
(59, 'Tekirdağ'),
(60, 'Tokat'),
(61, 'Trabzon'),
(62, 'Tunceli'),
(63, 'Şanlıurfa'),
(64, 'Uşak'),
(65, 'Van'),
(66, 'Yozgat'),
(67, 'Zonguldak'),
(68, 'Aksaray'),
(69, 'Bayburt'),
(70, 'Karaman'),
(71, 'Kırıkkale'),
(72, 'Batman'),
(73, 'Şırnak'),
(74, 'Bartın'),
(75, 'Ardahan'),
(76, 'Iğdır'),
(77, 'Yalova'),
(78, 'Karabük'),
(79, 'Kilis'),
(80, 'Osmaniye'),
(81, 'Düzce');

-- --------------------------------------------------------

--
-- Table structure for table `colors`
--

CREATE TABLE `colors` (
  `id` int(11) NOT NULL,
  `color_name` varchar(50) COLLATE utf8mb4_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_turkish_ci;

--
-- Dumping data for table `colors`
--

INSERT INTO `colors` (`id`, `color_name`) VALUES
(1, 'Kırmızı'),
(2, 'Sarı'),
(3, 'Beyaz'),
(4, 'Siyah'),
(5, 'Turkuaz'),
(6, 'Turuncu'),
(7, 'Mavi'),
(8, 'Yeşil');

-- --------------------------------------------------------

--
-- Table structure for table `engine_model`
--

CREATE TABLE `engine_model` (
  `id` int(11) NOT NULL,
  `type` varchar(20) COLLATE utf8_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `engine_model`
--

INSERT INTO `engine_model` (`id`, `type`) VALUES
(1, 'Benzinli'),
(2, 'Dizel'),
(3, 'Elektrikli');

-- --------------------------------------------------------

--
-- Table structure for table `models`
--

CREATE TABLE `models` (
  `id` int(11) NOT NULL,
  `brand_id` int(11) NOT NULL,
  `model_name` varchar(70) COLLATE utf8_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `models`
--

INSERT INTO `models` (`id`, `brand_id`, `model_name`) VALUES
(1, 3, 'X3'),
(2, 3, 'X4'),
(3, 3, 'X5'),
(4, 4, 'A3'),
(5, 4, 'A4'),
(6, 4, 'A5'),
(7, 4, 'A6'),
(8, 4, 'A8'),
(9, 5, 'c180'),
(10, 5, 'a-180');

-- --------------------------------------------------------

--
-- Table structure for table `newvehicleform`
--

CREATE TABLE `newvehicleform` (
  `id` int(11) NOT NULL,
  `plate` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `color` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `fuel` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `userid` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `newvehicleform`
--

INSERT INTO `newvehicleform` (`id`, `plate`, `color`, `fuel`, `userid`, `date`) VALUES
(4, '25 GB 111', 'Krmz', 'Benzin', 48, '2021-01-17 01:15:00');

-- --------------------------------------------------------

--
-- Table structure for table `requestform`
--

CREATE TABLE `requestform` (
  `id` int(11) NOT NULL,
  `subject` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `email` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `message` text COLLATE utf8_turkish_ci NOT NULL,
  `userid` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

-- --------------------------------------------------------

--
-- Stand-in structure for view `userlist`
-- (See below for the actual view)
--
CREATE TABLE `userlist` (
`id` int(11)
,`tc` varchar(50)
,`code` varchar(50)
,`email` varchar(200)
,`name` varchar(75)
,`surname` varchar(75)
);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `tc` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `code` varchar(50) COLLATE utf8_turkish_ci NOT NULL,
  `email` varchar(200) COLLATE utf8_turkish_ci NOT NULL,
  `name` varchar(75) COLLATE utf8_turkish_ci NOT NULL,
  `surname` varchar(75) COLLATE utf8_turkish_ci NOT NULL,
  `role` int(11) NOT NULL,
  `phone` varchar(30) COLLATE utf8_turkish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `tc`, `code`, `email`, `name`, `surname`, `role`, `phone`) VALUES
(48, '12345678910', '123456789', 'redvizor@gmail.com', 'Ömer', 'Elmalı', 0, '+90 551 162 81 84'),
(53, '10987654321', '321321', 'bet@gmail.com', 'İhsan', 'Güç', 99, '+90 553 498 21 27'),
(54, '25814736988', '159951456654', 'metinsenturk@gmail.com', 'Metin', 'Şentürk', 0, ''),
(55, '25836914721', '15963258741', 'denemuser@gmail.com', 'Deneme', 'User', 0, '');

-- --------------------------------------------------------

--
-- Table structure for table `vehicles`
--

CREATE TABLE `vehicles` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `plate` varchar(15) COLLATE utf8_turkish_ci NOT NULL,
  `city_code` int(11) NOT NULL,
  `color` int(11) NOT NULL,
  `brand` int(11) NOT NULL,
  `model` int(11) NOT NULL,
  `engine_model` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `vehicles`
--

INSERT INTO `vehicles` (`id`, `user_id`, `plate`, `city_code`, `color`, `brand`, `model`, `engine_model`) VALUES
(1, 48, '34 HC 4195', 1, 1, 3, 3, 1),
(18, 53, '45 TESTARACI 45', 1, 1, 3, 2, 1),
(20, 53, '81 DZC 123', 1, 2, 4, 5, 1),
(21, 53, '34 TRF 22', 34, 1, 5, 9, 1),
(22, 54, '34 AA 45', 10, 2, 3, 2, 2),
(23, 55, '52 BB 422', 34, 3, 4, 7, 2),
(24, 48, '34 GH 322', 2, 2, 4, 6, 1);

--
-- Triggers `vehicles`
--
DELIMITER $$
CREATE TRIGGER `Insert_Location` AFTER INSERT ON `vehicles` FOR EACH ROW INSERT into vehicle_locations(enlem,boylam,speed,vehicle_id,date) VALUES('38.4918203','27.7041138','0',NEW.id,CURRENT_DATE())
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `vehicleupdateforms`
--

CREATE TABLE `vehicleupdateforms` (
  `id` int(11) NOT NULL,
  `plate` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `newplate` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `fuel` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `color` varchar(60) COLLATE utf8_turkish_ci NOT NULL,
  `userid` int(11) NOT NULL,
  `date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `vehicle_locations`
--

CREATE TABLE `vehicle_locations` (
  `id` int(11) NOT NULL,
  `vehicle_id` int(11) NOT NULL,
  `date` date NOT NULL,
  `enlem` text COLLATE utf8_turkish_ci NOT NULL,
  `boylam` text COLLATE utf8_turkish_ci NOT NULL,
  `speed` int(11) NOT NULL,
  `vehicle_state` int(11) NOT NULL,
  `stop_state` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_turkish_ci;

--
-- Dumping data for table `vehicle_locations`
--

INSERT INTO `vehicle_locations` (`id`, `vehicle_id`, `date`, `enlem`, `boylam`, `speed`, `vehicle_state`, `stop_state`) VALUES
(1, 0, '2021-01-16', '38.4918203', '27.7041138', 0, 0, 0),
(5, 18, '2019-12-30', '38.4918203', '27.7041138', 123, 0, 0),
(6, 19, '2021-01-16', '38.4918203', '27.7041138', 0, 0, 0),
(7, 20, '2021-01-16', '38.4918203', '27.7041138', 0, 0, 0),
(8, 21, '2021-01-17', '38.4918203', '27.7041138', 0, 0, 1),
(9, 22, '2021-01-17', '38.4918203', '27.7041138', 0, 0, 0),
(10, 23, '2021-01-17', '38.4918203', '27.7041138', 0, 0, 0),
(11, 24, '2021-01-17', '38.4918203', '27.7041138', 150, 0, 0);

-- --------------------------------------------------------

--
-- Structure for view `userlist`
--
DROP TABLE IF EXISTS `userlist`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `userlist`  AS SELECT `users`.`id` AS `id`, `users`.`tc` AS `tc`, `users`.`code` AS `code`, `users`.`email` AS `email`, `users`.`name` AS `name`, `users`.`surname` AS `surname` FROM `users` ORDER BY `users`.`id` ASC ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `brands`
--
ALTER TABLE `brands`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cities`
--
ALTER TABLE `cities`
  ADD PRIMARY KEY (`plate_code`);

--
-- Indexes for table `colors`
--
ALTER TABLE `colors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `engine_model`
--
ALTER TABLE `engine_model`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`id`),
  ADD KEY `brand_id` (`brand_id`);

--
-- Indexes for table `newvehicleform`
--
ALTER TABLE `newvehicleform`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `requestform`
--
ALTER TABLE `requestform`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `code` (`code`),
  ADD UNIQUE KEY `tc` (`tc`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Indexes for table `vehicles`
--
ALTER TABLE `vehicles`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `plate` (`plate`),
  ADD KEY `brandCar` (`brand`),
  ADD KEY `modelCar` (`model`),
  ADD KEY `vehicle-own` (`user_id`),
  ADD KEY `city_code` (`city_code`),
  ADD KEY `engine_model` (`engine_model`),
  ADD KEY `color` (`color`);

--
-- Indexes for table `vehicleupdateforms`
--
ALTER TABLE `vehicleupdateforms`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `vehicle_locations`
--
ALTER TABLE `vehicle_locations`
  ADD PRIMARY KEY (`id`),
  ADD KEY `vehicle-id` (`vehicle_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `brands`
--
ALTER TABLE `brands`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `cities`
--
ALTER TABLE `cities`
  MODIFY `plate_code` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=82;

--
-- AUTO_INCREMENT for table `colors`
--
ALTER TABLE `colors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `engine_model`
--
ALTER TABLE `engine_model`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `models`
--
ALTER TABLE `models`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `newvehicleform`
--
ALTER TABLE `newvehicleform`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `requestform`
--
ALTER TABLE `requestform`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- AUTO_INCREMENT for table `vehicles`
--
ALTER TABLE `vehicles`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `vehicleupdateforms`
--
ALTER TABLE `vehicleupdateforms`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `vehicle_locations`
--
ALTER TABLE `vehicle_locations`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
