/*
Navicat MariaDB Data Transfer

Source Server         : local
Source Server Version : 100011
Source Host           : localhost:3306
Source Database       : characterdb

Target Server Type    : MariaDB
Target Server Version : 100011
File Encoding         : 65001

Date: 2014-06-17 01:45:54
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for CharacterTemplateActions
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateActions`;
CREATE TABLE `CharacterTemplateActions` (
  `ClassId` int(11) unsigned NOT NULL,
  `Action` int(10) unsigned NOT NULL,
  `Slot` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY (`Action`,`Slot`),
  KEY `Class` (`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES CharacterTemplateClasses(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateClasses
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateClasses`;
CREATE TABLE `CharacterTemplateClasses` (
  `ClassId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SetId` int(11) unsigned NOT NULL,
  `FactionGroup` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`,`SetId`),
  KEY `Id` (`Id`),
  FOREIGN KEY (`SetId`) REFERENCES CharacterTemplateSets(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateClasses
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateData
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateData`;
CREATE TABLE `CharacterTemplateData` (
  `ClassId` int(11) unsigned NOT NULL,
  `Map` smallint(6) NOT NULL,
  `Zone` smallint(6) NOT NULL,
  `X` float NOT NULL,
  `Y` float NOT NULL,
  `Z` float NOT NULL,
  `O` float NOT NULL,
  PRIMARY KEY (`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES CharacterTemplateClasses(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateData
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateItems
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateItems`;
CREATE TABLE `CharacterTemplateItems` (
  `ItemId` int(10) unsigned DEFAULT NULL,
  `ClassId` int(11) unsigned NOT NULL,
  `IsEquipped` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`,`ClassId`,`IsEquipped`),
  FOREIGN KEY (`ClassId`) REFERENCES CharacterTemplateClasses(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateItems
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateSets
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateSets`;
CREATE TABLE `CharacterTemplateSets` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT '',
  `Description` varchar(255) DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateSets
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateSkills
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateSkills`;
CREATE TABLE `CharacterTemplateSkills` (
  `SkillId` int(10) unsigned NOT NULL,
  `ClassId` int(11) unsigned NOT NULL,
  PRIMARY KEY (`Id`,`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES CharacterTemplateClasses(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateSpells
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateSpells`;
CREATE TABLE `CharacterTemplateSpells` (
  `SpellId` int(10) unsigned NOT NULL,
  `ClassId` int(11) unsigned NOT NULL,
  PRIMARY KEY (`Id`,`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES CharacterTemplateClasses(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateSpells
-- ----------------------------
