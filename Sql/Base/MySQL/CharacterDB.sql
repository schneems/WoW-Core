/*
Date: 2014-08-31 02:40:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for CharacterActions
-- ----------------------------
DROP TABLE IF EXISTS `CharacterActions`;
CREATE TABLE `CharacterActions` (
  `CharacterGuid` bigint(20) unsigned NOT NULL,
  `Action` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Slot` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CharacterGuid`),
  FOREIGN KEY (`CharacterGuid`) REFERENCES `Characters` (`Guid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationActions
-- ----------------------------
DROP TABLE IF EXISTS `CharacterCreationActions`;
CREATE TABLE `CharacterCreationActions` (
  `Race` tinyint(3) unsigned NOT NULL,
  `Class` tinyint(3) unsigned NOT NULL,
  `Action` int(11) NOT NULL DEFAULT '0',
  `Slot` tinyint(3) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Race`,`Class`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterCreationActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationData
-- ----------------------------
DROP TABLE IF EXISTS `CharacterCreationData`;
CREATE TABLE `CharacterCreationData` (
  `Race` tinyint(3) unsigned NOT NULL,
  `Class` tinyint(3) unsigned NOT NULL,
  `Zone` int(10) unsigned NOT NULL DEFAULT '0',
  `Map` int(10) unsigned NOT NULL DEFAULT '0',
  `X` float NOT NULL,
  `Y` float NOT NULL,
  `Z` float NOT NULL,
  `O` float NOT NULL,
  PRIMARY KEY (`Race`,`Class`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterCreationData
-- ----------------------------
INSERT INTO `CharacterCreationData` VALUES ('0', '6', '4298', '609', '2355.84', '-5664.77', '426.028', '3.65997');
INSERT INTO `CharacterCreationData` VALUES ('1', '0', '6170', '0', '-8914.57', '-133.909', '80.5378', '5.10444');
INSERT INTO `CharacterCreationData` VALUES ('2', '0', '6451', '1', '-618.518', '-4251.67', '38.718', '0');
INSERT INTO `CharacterCreationData` VALUES ('3', '0', '6176', '0', '-6230.42', '330.232', '383.105', '0.501087');
INSERT INTO `CharacterCreationData` VALUES ('4', '0', '6450', '1', '10311.3', '831.463', '1326.57', '5.48033');
INSERT INTO `CharacterCreationData` VALUES ('5', '0', '6454', '0', '1699.85', '1706.56', '135.928', '4.88839');
INSERT INTO `CharacterCreationData` VALUES ('6', '0', '6452', '1', '-2915.55', '-257.347', '59.2693', '0.302378');
INSERT INTO `CharacterCreationData` VALUES ('7', '0', '1', '0', '-4983.42', '877.7', '274.31', '3.06393');
INSERT INTO `CharacterCreationData` VALUES ('8', '0', '6453', '1', '-1171.45', '-5263.65', '0.847728', '5.78945');
INSERT INTO `CharacterCreationData` VALUES ('9', '0', '4737', '648', '-8423.81', '1361.3', '104.671', '1.55428');
INSERT INTO `CharacterCreationData` VALUES ('10', '0', '6455', '530', '10349.6', '-6357.29', '33.4026', '5.31605');
INSERT INTO `CharacterCreationData` VALUES ('11', '0', '6456', '530', '-3961.64', '-13931.2', '100.615', '2.08364');
INSERT INTO `CharacterCreationData` VALUES ('22', '0', '4755', '654', '-1451.53', '1403.35', '35.5561', '0.333847');
INSERT INTO `CharacterCreationData` VALUES ('24', '0', '5736', '860', '1466.09', '3465.98', '181.86', '2.87962');

-- ----------------------------
-- Table structure for CharacterCreationSkills
-- ----------------------------
DROP TABLE IF EXISTS `CharacterCreationSkills`;
CREATE TABLE `CharacterCreationSkills` (
  `Race` tinyint(3) unsigned NOT NULL,
  `Class` tinyint(3) unsigned NOT NULL,
  `SkillId` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Race`,`Class`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterCreationSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationSpells
-- ----------------------------
DROP TABLE IF EXISTS `CharacterCreationSpells`;
CREATE TABLE `CharacterCreationSpells` (
  `Race` tinyint(3) unsigned NOT NULL,
  `Class` tinyint(3) unsigned NOT NULL,
  `SpellId` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Race`,`Class`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterCreationSpells
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterItems
-- ----------------------------
DROP TABLE IF EXISTS `CharacterItems`;
CREATE TABLE `CharacterItems` (
  `CharacterGuid` bigint(20) unsigned NOT NULL,
  `ItemId` int(10) unsigned NOT NULL DEFAULT '0',
  `Bag` tinyint(3) unsigned NOT NULL DEFAULT '255',
  `Slot` tinyint(3) unsigned NOT NULL,
  `Mode` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Equipped` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`CharacterGuid`,`ItemId`),
  FOREIGN KEY (`CharacterGuid`) REFERENCES `Characters` (`Guid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterItems
-- ----------------------------

-- ----------------------------
-- Table structure for Characters
-- ----------------------------
DROP TABLE IF EXISTS `Characters`;
CREATE TABLE `Characters` (
  `Guid` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `GameAccountId` int(10) unsigned NOT NULL DEFAULT '0',
  `RealmId` int(10) unsigned NOT NULL DEFAULT '0',
  `Name` varchar(192) NOT NULL DEFAULT '',
  `ListPosition` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Race` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Class` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Sex` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Skin` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Face` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `HairStyle` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `HairColor` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `FacialHairStyle` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Level` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `Zone` int(10) unsigned NOT NULL DEFAULT '0',
  `Map` int(10) unsigned NOT NULL DEFAULT '0',
  `X` float NOT NULL DEFAULT '0',
  `Y` float NOT NULL DEFAULT '0',
  `Z` float NOT NULL DEFAULT '0',
  `O` float NOT NULL DEFAULT '0',
  `GuildGuid` bigint(20) unsigned NOT NULL DEFAULT '0',
  `CharacterFlags` int(10) unsigned NOT NULL DEFAULT '0',
  `CustomizeFlags` int(10) unsigned NOT NULL DEFAULT '0',
  `Flags3` int(10) unsigned NOT NULL DEFAULT '0',
  `FirstLogin` tinyint(3) unsigned NOT NULL DEFAULT '1',
  `PetCreatureDisplayId` int(10) unsigned NOT NULL DEFAULT '0',
  `PetLevel` int(10) unsigned NOT NULL DEFAULT '0',
  `PetCreatureFamily` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Characters
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterSkills
-- ----------------------------
DROP TABLE IF EXISTS `CharacterSkills`;
CREATE TABLE `CharacterSkills` (
  `CharacterGuid` bigint(20) unsigned NOT NULL,
  `SkillId` int(10) unsigned NOT NULL DEFAULT '0',
  `SkillLevel` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CharacterGuid`,`SkillId`),
  FOREIGN KEY (`CharacterGuid`) REFERENCES `Characters` (`Guid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterSpells
-- ----------------------------
DROP TABLE IF EXISTS `CharacterSpells`;
CREATE TABLE `CharacterSpells` (
  `CharacterGuid` bigint(20) unsigned NOT NULL,
  `SpellId` int(10) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`CharacterGuid`,`SpellId`),
  FOREIGN KEY (`CharacterGuid`) REFERENCES `Characters` (`Guid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterSpells
-- ----------------------------

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
  FOREIGN KEY (`ClassId`) REFERENCES `CharacterTemplateclasses` (`ClassId`)
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
  PRIMARY KEY (`ClassId`,`SetId`),
  KEY `Id` (`ClassId`),
  KEY `SetId` (`SetId`),
  FOREIGN KEY (`SetId`) REFERENCES `CharacterTemplatesets` (`Id`)
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
  FOREIGN KEY (`ClassId`) REFERENCES `CharacterTemplateClasses` (`ClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateData
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateItems
-- ----------------------------
DROP TABLE IF EXISTS `CharacterTemplateItems`;
CREATE TABLE `CharacterTemplateItems` (
  `ItemId` int(10) unsigned NOT NULL DEFAULT '0',
  `ClassId` int(11) unsigned NOT NULL,
  `IsEquipped` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ItemId`,`ClassId`,`IsEquipped`),
  KEY `ClassId` (`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES `CharacterTemplateClasses` (`ClassId`)
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
  PRIMARY KEY (`SkillId`,`ClassId`),
  KEY `ClassId` (`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES `CharacterTemplateClasses` (`ClassId`)
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
  PRIMARY KEY (`SpellId`,`ClassId`),
  KEY `ClassId` (`ClassId`),
  FOREIGN KEY (`ClassId`) REFERENCES `CharacterTemplateClasses` (`ClassId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterTemplateSpells
-- ----------------------------
