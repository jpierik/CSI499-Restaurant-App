-- phpMyAdmin SQL Dump
-- version 4.0.10deb1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Sep 27, 2017 at 11:46 PM
-- Server version: 5.5.57-0ubuntu0.14.04.1
-- PHP Version: 5.5.9-1ubuntu4.22

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `capstone`
--

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `username` varchar(200) NOT NULL,
  `alevel` tinyint(1) NOT NULL,
  `password` varchar(200) NOT NULL,
  `email` text NOT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=40 ;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`username`, `alevel`, `password`, `email`, `id`) VALUES
('hiclass', 1, 'triangle', 'john@gmail.com', 1),
('hi99', 0, 'circle', 'rrr@mail.com', 2),
('helloworld99', 0, '1', '3333@mail.com', 3),
('fun99', 0, '2', '22@gmail.com', 4),
('hi88', 0, '4', '44@gmail.com', 5),
('mom3', 0, 'triangle', 'mom2@gmail.com', 6),
('rthartma', 0, 'password', 'rthartma@oakland.edu', 7),
('dd', 0, 'dd', 'ddd', 8),
('dad1', 0, 'triangle', 'dad@gmail.com', 9),
('hello2', 0, 'hi', 'hi@gmail.com', 10),
('cidricc', 0, 'memes', 'tcwatling@oakland.edu', 11),
('usernamâ‰¤â‰¥Ã·', 0, 'password', 'f@ke.lol', 12),
('usernÃ¥me', 0, 'password', 'f@ke.lol', 13),
('skahmed', 0, 'password', 'skahmed@oakland.edu', 14),
('21', 0, 'triangle', 'rrrww@mail.com', 22),
('skahmed', 0, 'lardforfun1', 'skahmed@oakland.edu', 26),
('abc', 0, 'password', 'a@b.c', 30),
('john89', 0, 'triangle1', 'pop1@gmail.com', 31),
('john9', 0, 'triangle', 'dad@dod.com', 32),
('cba', 0, 'password', 'c@b.a', 33),
('nickj', 0, 'triangle', 'nick@mail.com', 34),
('fff', 0, 'ffffffff', 'ggg@ff.m', 35),
('helloo', 0, '12345678', 'nn2@nn.com', 36),
('aergjkaerjerg', 0, 'nickj9999', 'bbcc@dcc.com', 37),
('niii', 0, 'nininini', 'Hii@dd.com', 38),
('jp123', 0, 'temp12345', 'jpierik@whatever.com', 39);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
