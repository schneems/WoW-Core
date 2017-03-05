SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Accounts
-- ----------------------------
DROP TABLE IF EXISTS `Accounts`;
CREATE TABLE `Accounts` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Email` varchar(320) NOT NULL,
  `PasswordVerifier` varchar(256) NOT NULL,
  `Salt` varchar(64) NOT NULL,
  `Region` tinyint(4) unsigned NOT NULL,
  `Locale` varchar(4) DEFAULT NULL,
  `IPv4` varchar(15) DEFAULT NULL,
  `IPv6` varchar(39) DEFAULT NULL,
  `LoginFailures` int(11) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Accounts
-- ----------------------------

-- ----------------------------
-- Table structure for Applications
-- ----------------------------
DROP TABLE IF EXISTS `Applications`;
CREATE TABLE `Applications` (
  `Program` varchar(4) NOT NULL,
  `Platform` varchar(4) NOT NULL,
  `Locale` varchar(4) NOT NULL,
  `Version` int(11) NOT NULL,
  UNIQUE KEY `Program` (`Program`,`Platform`,`Locale`,`Version`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Applications
-- ----------------------------
INSERT INTO `Applications` VALUES ('WoW', 'xx', 'xx', '23194');

-- ----------------------------
-- Table structure for ServiceVersions
-- ----------------------------
DROP TABLE IF EXISTS `ServiceVersions`;
CREATE TABLE `ServiceVersions` (
  `Version` varchar(255) NOT NULL,
  PRIMARY KEY (`Version`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of ServiceVersions
-- ----------------------------
INSERT INTO `ServiceVersions` VALUES ('Battle.net Game Service SDK v1.6.4 \\\"5cf152fa90\\\"/92 (Dec  6 2016 22:33:44)');

-- ----------------------------
-- Table structure for GameAccounts
-- ----------------------------
DROP TABLE IF EXISTS `GameAccounts`;
CREATE TABLE `GameAccounts` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) unsigned NOT NULL,
  `Game` varchar(4) DEFAULT NULL,
  `Index` tinyint(4) unsigned NOT NULL,
  `Region` tinyint(4) unsigned NOT NULL,
  `ExpansionLevel` tinyint(4) NOT NULL DEFAULT '6',
  `Online` tinyint(1) unsigned DEFAULT '0',
  `JoinTicket` varchar(64) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `AccountId` (`AccountId`),
  CONSTRAINT `FK_GameAccounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccounts
-- ----------------------------

-- ----------------------------
-- Table structure for Realms
-- ----------------------------
DROP TABLE IF EXISTS `Realms`;
CREATE TABLE `Realms` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Flags` int(11) unsigned NOT NULL,
  `Type` int(11) NOT NULL,
  `Category` int(11) NOT NULL,
  `Timezone` int(11) NOT NULL,
  `Language` int(11) NOT NULL,
  `Population` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Realms
-- ----------------------------
INSERT INTO `Realms` VALUES ('1', 'Arctium Emulation', '4', '1', '1', '1', '1', '0');

-- ----------------------------
-- Table structure for RealmVersions
-- ----------------------------
DROP TABLE IF EXISTS `RealmVersions`;
CREATE TABLE `RealmVersions` (
  `RealmId` int(11) unsigned NOT NULL,
  `Major` int(11) unsigned NOT NULL,
  `Minor` int(11) unsigned NOT NULL,
  `Revision` int(11) unsigned NOT NULL,
  `Build` int(11) unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `RealmId` (`RealmId`,`Build`),
  CONSTRAINT `FK_RealmVersions_RealmId` FOREIGN KEY (`RealmId`) REFERENCES `Realms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of RealmVersions
-- ----------------------------
SET FOREIGN_KEY_CHECKS=1;
