/*
Navicat MySQL Data Transfer

Source Server         : 90
Source Server Version : 50173
Source Host           : 10.22.102.90:3306
Source Database       : supplychain

Target Server Type    : MYSQL
Target Server Version : 50173
File Encoding         : 65001

Date: 2015-09-22 14:16:41
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `addresslist`
-- ----------------------------
DROP TABLE IF EXISTS `addresslist`;
CREATE TABLE `addresslist` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `way` int(10) NOT NULL,
  `Value` varchar(1023) DEFAULT NULL,
  `CID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of addresslist
-- ----------------------------
INSERT INTO `addresslist` VALUES ('21', '0', '810844480', '5');
INSERT INTO `addresslist` VALUES ('22', '0', '123456789；18758326914', '1');
INSERT INTO `addresslist` VALUES ('23', '1', '810844480', '1');
INSERT INTO `addresslist` VALUES ('24', '3', '11712785', '5');
INSERT INTO `addresslist` VALUES ('25', '0', '737545324；22332', '6');
INSERT INTO `addresslist` VALUES ('26', '1', '0571-86994975', '6');
INSERT INTO `addresslist` VALUES ('27', '2', '73745324@qq.com', '6');
INSERT INTO `addresslist` VALUES ('28', '3', '88063975', '6');
INSERT INTO `addresslist` VALUES ('29', '4', '浙江省杭州市下城区中山北路632号越都商务大厦2101室', '6');
INSERT INTO `addresslist` VALUES ('33', '0', '二二', '8');
INSERT INTO `addresslist` VALUES ('34', '1', '返到', '8');
INSERT INTO `addresslist` VALUES ('35', '2', '对方答复', '8');
INSERT INTO `addresslist` VALUES ('36', '3', '地方', '8');
INSERT INTO `addresslist` VALUES ('37', '4', '地方', '8');

-- ----------------------------
-- Table structure for `components`
-- ----------------------------
DROP TABLE IF EXISTS `components`;
CREATE TABLE `components` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Brand` varchar(255) DEFAULT NULL,
  `Specification` varchar(255) DEFAULT NULL,
  `Number` varchar(255) DEFAULT NULL,
  `Memo` varchar(255) DEFAULT NULL,
  `Type` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of components
-- ----------------------------
INSERT INTO `components` VALUES ('1', '英特尔', 'i7', '4700', null, '0');
INSERT INTO `components` VALUES ('2', '英特尔', 'i3', '4160', '3.6GHZ', '0');
INSERT INTO `components` VALUES ('3', '华硕', 'B85M', 'GAMER', null, '4');
INSERT INTO `components` VALUES ('4', '三星', 'S34', '34英寸', null, '3');
INSERT INTO `components` VALUES ('5', '英特尔', '3GHz', '5960X', '适用台式机', '0');
INSERT INTO `components` VALUES ('6', '英特尔', '4GHz', '4790k', null, '0');
INSERT INTO `components` VALUES ('7', '华硕', 'A15', '152', '协议', '1');

-- ----------------------------
-- Table structure for `contacts`
-- ----------------------------
DROP TABLE IF EXISTS `contacts`;
CREATE TABLE `contacts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `sex` bit(1) DEFAULT NULL,
  `EID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of contacts
-- ----------------------------
INSERT INTO `contacts` VALUES ('1', '袁洋', '', '3');
INSERT INTO `contacts` VALUES ('3', '156', '', '3');
INSERT INTO `contacts` VALUES ('4', '58', '', '3');
INSERT INTO `contacts` VALUES ('5', '女', '', '3');
INSERT INTO `contacts` VALUES ('6', '孙夏阳', '', '5');
INSERT INTO `contacts` VALUES ('8', 'ty', '', '5');

-- ----------------------------
-- Table structure for `enterprises`
-- ----------------------------
DROP TABLE IF EXISTS `enterprises`;
CREATE TABLE `enterprises` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Business` bit(1) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of enterprises
-- ----------------------------
INSERT INTO `enterprises` VALUES ('1', '供应商1', '浙江省杭州市下城区中山北路', '');
INSERT INTO `enterprises` VALUES ('2', '供应商2', '浙江省杭州市中山北路', '');
INSERT INTO `enterprises` VALUES ('3', '智拓', '浙江省杭州市下城区中山北路632号越都商务大厦2101室', '');
INSERT INTO `enterprises` VALUES ('4', '瑞泽', '福州', '');
INSERT INTO `enterprises` VALUES ('5', '杭州智拓土地规划设计咨询有限公司', '浙江省杭州市下城区中山北路632号越都商务大厦2101室', '');
INSERT INTO `enterprises` VALUES ('6', '杭州智拓房地产土地评估咨询有限公司', '浙江省杭州市下城区中山北路632号越都商务大厦2101室', '');

-- ----------------------------
-- Table structure for `inventorys`
-- ----------------------------
DROP TABLE IF EXISTS `inventorys`;
CREATE TABLE `inventorys` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) NOT NULL,
  `CID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of inventorys
-- ----------------------------
INSERT INTO `inventorys` VALUES ('1', '10', '1');
INSERT INTO `inventorys` VALUES ('2', '12', '2');
INSERT INTO `inventorys` VALUES ('3', '15', '3');
INSERT INTO `inventorys` VALUES ('4', '10', '4');

-- ----------------------------
-- Table structure for `items`
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` int(11) DEFAULT NULL,
  `PID` int(11) NOT NULL,
  `CID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of items
-- ----------------------------
INSERT INTO `items` VALUES ('1', '1', '4', '1');
INSERT INTO `items` VALUES ('2', '1', '4', '3');
INSERT INTO `items` VALUES ('3', '1', '4', '4');
INSERT INTO `items` VALUES ('4', '2', '5', '1');
INSERT INTO `items` VALUES ('5', '3', '6', '1');

-- ----------------------------
-- Table structure for `messages`
-- ----------------------------
DROP TABLE IF EXISTS `messages`;
CREATE TABLE `messages` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Word` varchar(255) DEFAULT NULL,
  `Time` datetime NOT NULL,
  `EID` int(11) NOT NULL,
  `CID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of messages
-- ----------------------------
INSERT INTO `messages` VALUES ('1', '我是一只小小鸟\r\n怎么飞也飞不高\r\n\r\n\r\n\r\n一个温暖的拥抱', '2015-07-26 15:45:17', '3', '2');
INSERT INTO `messages` VALUES ('2', '     我是\r\n杭州智拓\r\n             土地规划设计信息咨询\r\n                                                有限公司', '2015-07-26 16:00:20', '3', '1');
INSERT INTO `messages` VALUES ('3', '       我是\r\n              杭州智拓\r\n                            土地规划设计信息咨询\r\n                                                               有限公司', '2015-07-26 16:01:33', '3', '1');
INSERT INTO `messages` VALUES ('4', '有时候 我觉得我是一只小小鸟', '2015-07-27 14:08:08', '3', '3');
INSERT INTO `messages` VALUES ('5', '反馈信息', '2015-09-10 11:25:57', '5', '8');
INSERT INTO `messages` VALUES ('6', '翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想翻了亏信息想', '2015-09-10 11:26:15', '5', '6');

-- ----------------------------
-- Table structure for `orders`
-- ----------------------------
DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Time` datetime DEFAULT NULL,
  `Express` varchar(255) DEFAULT NULL,
  `Indenture` varchar(255) DEFAULT NULL,
  `EID` int(11) NOT NULL,
  `Type` bit(1) NOT NULL,
  `State` int(10) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of orders
-- ----------------------------
INSERT INTO `orders` VALUES ('1', '2015-07-22 19:07:40', '330521425846222', 'Indentures/P50613-141638-635736052540610787.jpg', '5', '', '2');
INSERT INTO `orders` VALUES ('2', '2015-07-23 15:27:37', null, null, '6', '', '0');
INSERT INTO `orders` VALUES ('3', '2015-07-29 15:25:17', null, null, '1', '', '0');
INSERT INTO `orders` VALUES ('4', '2015-07-29 16:25:51', null, null, '1', '', '0');
INSERT INTO `orders` VALUES ('5', '2015-07-29 16:27:26', null, null, '1', '', '0');
INSERT INTO `orders` VALUES ('6', '2015-07-29 16:28:39', null, null, '1', '', '0');
INSERT INTO `orders` VALUES ('7', '2015-07-29 16:29:59', null, null, '1', '', '0');

-- ----------------------------
-- Table structure for `products`
-- ----------------------------
DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Number` varchar(255) NOT NULL,
  `Price` float(10,4) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of products
-- ----------------------------
INSERT INTO `products` VALUES ('1', '陆吾一号', '89.0000');
INSERT INTO `products` VALUES ('2', '陆吾二号', '1586.0000');
INSERT INTO `products` VALUES ('3', '陆吾三号', '200000.0000');
INSERT INTO `products` VALUES ('4', '陆吾四号', '400000.0000');
INSERT INTO `products` VALUES ('5', '陆吾五号', '10000.2354');
INSERT INTO `products` VALUES ('6', '陆吾六号', '78.2350');

-- ----------------------------
-- Table structure for `quotations`
-- ----------------------------
DROP TABLE IF EXISTS `quotations`;
CREATE TABLE `quotations` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Price` float(10,4) DEFAULT NULL,
  `Time` datetime DEFAULT NULL,
  `Number` int(11) DEFAULT NULL,
  `CID` int(11) NOT NULL,
  `OID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of quotations
-- ----------------------------
INSERT INTO `quotations` VALUES ('1', '5000.0000', null, '10', '1', '1');
INSERT INTO `quotations` VALUES ('2', '3000.0000', null, '12', '2', '1');
INSERT INTO `quotations` VALUES ('3', '688.0000', null, '15', '3', '1');
INSERT INTO `quotations` VALUES ('4', '1500.0000', null, '10', '4', '1');
INSERT INTO `quotations` VALUES ('5', '5000.0000', null, '10', '1', '2');
INSERT INTO `quotations` VALUES ('6', '588.0000', null, '10', '3', '2');
INSERT INTO `quotations` VALUES ('7', '1600.0000', null, '20', '4', '2');
INSERT INTO `quotations` VALUES ('8', '1990.0000', null, '20', '1', '7');

-- ----------------------------
-- Table structure for `rates`
-- ----------------------------
DROP TABLE IF EXISTS `rates`;
CREATE TABLE `rates` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Time` datetime DEFAULT NULL,
  `Price` float(10,4) DEFAULT NULL,
  `SID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rates
-- ----------------------------
INSERT INTO `rates` VALUES ('1', '2015-07-24 14:55:40', '0.0000', '1');
INSERT INTO `rates` VALUES ('2', '2015-07-24 14:56:43', '500.0000', '1');
INSERT INTO `rates` VALUES ('3', '2015-07-24 14:57:20', '800.0000', '1');
INSERT INTO `rates` VALUES ('4', '2015-07-24 14:57:33', '100.5600', '1');
INSERT INTO `rates` VALUES ('5', '2015-07-24 14:57:46', '900.6800', '1');
INSERT INTO `rates` VALUES ('6', '2015-07-24 15:26:53', '89.0000', '1');
INSERT INTO `rates` VALUES ('7', '2015-07-24 15:27:06', '12.5800', '1');
INSERT INTO `rates` VALUES ('8', '2015-07-24 15:27:14', '1586.0000', '2');
INSERT INTO `rates` VALUES ('9', '2015-07-24 15:27:46', '56.2530', '6');
INSERT INTO `rates` VALUES ('10', '2015-07-24 15:29:41', '896.2356', '6');
INSERT INTO `rates` VALUES ('11', '2015-07-24 15:29:56', '78.2350', '6');
INSERT INTO `rates` VALUES ('12', '2015-07-24 15:47:55', '5.4000', '1');
INSERT INTO `rates` VALUES ('13', '2015-07-24 15:48:06', '13.0000', '1');
INSERT INTO `rates` VALUES ('14', '2015-07-24 15:48:25', '900.0000', '1');
INSERT INTO `rates` VALUES ('15', '2015-07-24 15:48:39', '780.0000', '1');
INSERT INTO `rates` VALUES ('16', '2015-07-24 15:48:55', '456.0000', '1');
INSERT INTO `rates` VALUES ('17', '2015-07-24 15:49:02', '300.0000', '1');
INSERT INTO `rates` VALUES ('18', '2015-07-24 15:49:22', '600.0000', '1');
INSERT INTO `rates` VALUES ('19', '2015-07-24 15:49:36', '660.0000', '1');
INSERT INTO `rates` VALUES ('20', '2015-07-24 15:49:47', '670.0000', '1');
INSERT INTO `rates` VALUES ('21', '2015-07-24 15:50:16', '900.0000', '1');
INSERT INTO `rates` VALUES ('22', '2015-07-24 18:57:27', '58.0000', '1');
INSERT INTO `rates` VALUES ('23', '2015-07-24 19:13:28', '500.5556', '1');
INSERT INTO `rates` VALUES ('24', '2015-07-29 16:56:54', '89.0000', '1');

-- ----------------------------
-- Table structure for `remittances`
-- ----------------------------
DROP TABLE IF EXISTS `remittances`;
CREATE TABLE `remittances` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Money` float(10,4) DEFAULT NULL,
  `Time` datetime NOT NULL,
  `Account` varchar(255) DEFAULT NULL,
  `Bank` varchar(255) DEFAULT NULL,
  `Pay` bit(1) NOT NULL,
  `Memo` varchar(1023) DEFAULT NULL,
  `SID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of remittances
-- ----------------------------
