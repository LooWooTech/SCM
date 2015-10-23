-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.6.26 - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出  表 scm.addresslist 结构
DROP TABLE IF EXISTS `addresslist`;
CREATE TABLE IF NOT EXISTS `addresslist` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `way` int(10) NOT NULL,
  `Value` varchar(1023) DEFAULT NULL,
  `CID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.components 结构
DROP TABLE IF EXISTS `components`;
CREATE TABLE IF NOT EXISTS `components` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Brand` varchar(255) DEFAULT NULL,
  `Specification` varchar(255) DEFAULT NULL,
  `Number` varchar(255) DEFAULT NULL,
  `Memo` varchar(255) DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.contacts 结构
DROP TABLE IF EXISTS `contacts`;
CREATE TABLE IF NOT EXISTS `contacts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Gender` bit(1) NOT NULL,
  `Mobile` varchar(45) DEFAULT NULL,
  `QQ` varchar(45) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `EnterpriseID` int(11) NOT NULL,
  `Email` varchar(128) DEFAULT NULL,
  `Note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_EID` (`EnterpriseID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.contracts 结构
DROP TABLE IF EXISTS `contracts`;
CREATE TABLE IF NOT EXISTS `contracts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL DEFAULT '0',
  `File` varchar(128) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.enterprises 结构
DROP TABLE IF EXISTS `enterprises`;
CREATE TABLE IF NOT EXISTS `enterprises` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Contact` varchar(45) DEFAULT NULL,
  `Tel` varchar(125) DEFAULT NULL,
  `Province` varchar(45) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `Deleted` bit(1) NOT NULL,
  `Business` bit(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.express 结构
DROP TABLE IF EXISTS `express`;
CREATE TABLE IF NOT EXISTS `express` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `ApiInfo` varchar(1024) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.inventorys 结构
DROP TABLE IF EXISTS `inventorys`;
CREATE TABLE IF NOT EXISTS `inventorys` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `CID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.messages 结构
DROP TABLE IF EXISTS `messages`;
CREATE TABLE IF NOT EXISTS `messages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL DEFAULT '0',
  `Note` varchar(255) DEFAULT NULL,
  `ContactTime` datetime NOT NULL,
  `EnterpriseId` int(11) NOT NULL,
  `ContactId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.orders 结构
DROP TABLE IF EXISTS `orders`;
CREATE TABLE IF NOT EXISTS `orders` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime DEFAULT NULL,
  `ExpressNo` varchar(255) DEFAULT NULL,
  `Express` int(11) NOT NULL,
  `Indenture` bit(1) NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `EnterpriseId` int(11) NOT NULL,
  `Type` int(11) NOT NULL,
  `State` int(10) NOT NULL,
  `Payment` bit(1) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `EnterpriseId` (`EnterpriseId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.order_components 结构
DROP TABLE IF EXISTS `order_components`;
CREATE TABLE IF NOT EXISTS `order_components` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL,
  `ComponentId` int(11) NOT NULL,
  `Price` float(10,4) DEFAULT NULL,
  `Number` int(11) DEFAULT NULL,
  `DealNumber` int(11) DEFAULT '0',
  `DealPrice` float DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `OrderId` (`OrderId`),
  KEY `ComponentId` (`ComponentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.order_products 结构
DROP TABLE IF EXISTS `order_products`;
CREATE TABLE IF NOT EXISTS `order_products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderID` int(11) NOT NULL DEFAULT '0',
  `ProductID` int(11) NOT NULL DEFAULT '0',
  `Price` decimal(10,0) NOT NULL DEFAULT '0',
  `Number` int(11) NOT NULL DEFAULT '0',
  `Status` int(11) NOT NULL DEFAULT '0',
  `DealPrice` decimal(10,0) DEFAULT '0',
  `DealNumber` int(11) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `OrderID` (`OrderID`),
  KEY `ProductID` (`ProductID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.products 结构
DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` varchar(255) NOT NULL,
  `Price` float(10,4) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.product_components 结构
DROP TABLE IF EXISTS `product_components`;
CREATE TABLE IF NOT EXISTS `product_components` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) DEFAULT NULL,
  `ProductId` int(11) NOT NULL,
  `ComponentId` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `IX_ProductId` (`ProductId`),
  KEY `ComponentId` (`ComponentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.product_price_logs 结构
DROP TABLE IF EXISTS `product_price_logs`;
CREATE TABLE IF NOT EXISTS `product_price_logs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime NOT NULL,
  `Price` float(10,4) NOT NULL,
  `ProductId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.remittances 结构
DROP TABLE IF EXISTS `remittances`;
CREATE TABLE IF NOT EXISTS `remittances` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Money` float(10,4) DEFAULT NULL,
  `Time` datetime NOT NULL,
  `Account` varchar(255) DEFAULT NULL,
  `Bank` varchar(255) DEFAULT NULL,
  `Pay` bit(1) NOT NULL,
  `Note` varchar(1023) DEFAULT NULL,
  `OrderId` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 scm.users 结构
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL DEFAULT '0',
  `Password` varchar(32) NOT NULL DEFAULT '0',
  `EnterpriseId` int(11) NOT NULL DEFAULT '0',
  `Role` int(11) NOT NULL DEFAULT '0',
  `Deleted` bit(1) NOT NULL DEFAULT b'0',
  `LastLoginTime` datetime DEFAULT '0000-00-00 00:00:00',
  `LastLoginIp` varchar(50) DEFAULT '0',
  PRIMARY KEY (`ID`),
  KEY `Username` (`Username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
