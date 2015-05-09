SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Accounts
-- ----------------------------
DROP TABLE IF EXISTS `Accounts`;
CREATE TABLE `Accounts` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `GivenName` varchar(100) DEFAULT '',
  `Surname` varchar(100) DEFAULT '',
  `Email` varchar(100) NOT NULL,
  `Tag` varchar(30) DEFAULT '',
  `Region` tinyint(4) unsigned NOT NULL,
  `Language` varchar(4) DEFAULT NULL,
  `Flags` bigint(20) unsigned NOT NULL,
  `PasswordVerifier` varchar(256) DEFAULT NULL,
  `Salt` varchar(64) DEFAULT NULL,
  `IP` varchar(15) DEFAULT NULL,
  `LoginFailures` tinyint(4) unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Accounts
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterRedirects
-- ----------------------------
DROP TABLE IF EXISTS `CharacterRedirects`;
CREATE TABLE `CharacterRedirects` (
  `Key` bigint(20) unsigned NOT NULL,
  `CharacterGuid` bigint(20) unsigned NOT NULL,
  PRIMARY KEY (`Key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of CharacterRedirects
-- ----------------------------

-- ----------------------------
-- Table structure for Components
-- ----------------------------
DROP TABLE IF EXISTS `Components`;
CREATE TABLE `Components` (
  `Program` varchar(4) NOT NULL,
  `Platform` varchar(4) NOT NULL,
  `Build` int(11) unsigned NOT NULL,
  UNIQUE KEY `Program` (`Program`,`Platform`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Components
-- ----------------------------
INSERT INTO `Components` VALUES ('Bnet', 'Mc64', '37165');
INSERT INTO `Components` VALUES ('Bnet', 'Win', '37165');
INSERT INTO `Components` VALUES ('Bnet', 'Wn64', '37165');
INSERT INTO `Components` VALUES ('WoW', 'base', '19973');
INSERT INTO `Components` VALUES ('WoW', 'deDE', '19988');
INSERT INTO `Components` VALUES ('WoW', 'enAU', '19988');
INSERT INTO `Components` VALUES ('WoW', 'enGB', '19988');
INSERT INTO `Components` VALUES ('WoW', 'enUS', '19988');
INSERT INTO `Components` VALUES ('WoW', 'esES', '19988');
INSERT INTO `Components` VALUES ('WoW', 'esMX', '19988');
INSERT INTO `Components` VALUES ('WoW', 'frFR', '19988');
INSERT INTO `Components` VALUES ('WoW', 'itIT', '19988');
INSERT INTO `Components` VALUES ('WoW', 'koKR', '19988');
INSERT INTO `Components` VALUES ('WoW', 'Mc64', '19988');
INSERT INTO `Components` VALUES ('WoW', 'plPL', '19988');
INSERT INTO `Components` VALUES ('WoW', 'ptBR', '19988');
INSERT INTO `Components` VALUES ('WoW', 'ruRU', '19988');
INSERT INTO `Components` VALUES ('WoW', 'Win', '19988');
INSERT INTO `Components` VALUES ('WoW', 'Wn64', '19988');
INSERT INTO `Components` VALUES ('WoW', 'zhCN', '19988');
INSERT INTO `Components` VALUES ('WoW', 'zhTW', '19988');

-- ----------------------------
-- Table structure for GameAccountCharacterTemplates
-- ----------------------------
DROP TABLE IF EXISTS `GameAccountCharacterTemplates`;
CREATE TABLE `GameAccountCharacterTemplates` (
  `GameAccountId` int(10) unsigned NOT NULL,
  `SetId` int(10) unsigned NOT NULL,
  UNIQUE KEY `GameAccountId` (`GameAccountId`,`SetId`) USING BTREE,
  FOREIGN KEY (`GameAccountId`) REFERENCES `GameAccounts` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccountCharacterTemplates
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountClasses
-- ----------------------------
DROP TABLE IF EXISTS `GameAccountClasses`;
CREATE TABLE `GameAccountClasses` (
  `GameAccountId` int(11) unsigned NOT NULL,
  `Class` tinyint(4) unsigned NOT NULL COMMENT 'Class Id',
  `Expansion` tinyint(4) unsigned NOT NULL COMMENT 'Expansion for class activation',
  UNIQUE KEY `GameAccountId` (`GameAccountId`,`Class`) USING BTREE,
  FOREIGN KEY (`GameAccountId`) REFERENCES `GameAccounts` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccountClasses
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountRaces
-- ----------------------------
DROP TABLE IF EXISTS `GameAccountRaces`;
CREATE TABLE `GameAccountRaces` (
  `GameAccountId` int(11) unsigned NOT NULL,
  `Race` tinyint(4) unsigned NOT NULL COMMENT 'Race Id',
  `Expansion` tinyint(4) unsigned NOT NULL COMMENT 'Expansion for race activation',
  UNIQUE KEY `GameAccountId` (`GameAccountId`,`Race`) USING BTREE,
  FOREIGN KEY (`GameAccountId`) REFERENCES `GameAccounts` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccountRaces
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountRedirects
-- ----------------------------
DROP TABLE IF EXISTS `GameAccountRedirects`;
CREATE TABLE `GameAccountRedirects` (
  `Key` bigint(20) unsigned NOT NULL,
  `GameAccountId` int(10) unsigned NOT NULL,
  PRIMARY KEY (`Key`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccountRedirects
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccounts
-- ----------------------------
DROP TABLE IF EXISTS `GameAccounts`;
CREATE TABLE `GameAccounts` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) unsigned NOT NULL,
  `Game` varchar(10) DEFAULT NULL,
  `Index` tinyint(4) unsigned NOT NULL DEFAULT '0',
  `Region` tinyint(4) unsigned NOT NULL,
  `Flags` bigint(20) unsigned NOT NULL,
  `BoxLevel` tinyint(4) unsigned NOT NULL,
  `OS` varchar(4) DEFAULT NULL,
  `SessionKey` varchar(80) DEFAULT NULL,
  `IsOnline` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  KEY `Account` (`AccountId`),
  FOREIGN KEY (`AccountId`) REFERENCES `Accounts` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of GameAccounts
-- ----------------------------

-- ----------------------------
-- Table structure for Modules
-- ----------------------------
DROP TABLE IF EXISTS `Modules`;
CREATE TABLE `Modules` (
  `Hash` varchar(64) NOT NULL,
  `Type` varchar(8) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `System` varchar(8) NOT NULL,
  `Size` int(11) unsigned NOT NULL,
  `Data` text,
  PRIMARY KEY (`Hash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Modules
-- ----------------------------
INSERT INTO `Modules` VALUES ('19c91b68752b7826df498bf73aca1103c86962a9a55a0a7033e5ad895f4d927c', 'auth', 'Password', 'Mc64', '321', null);
INSERT INTO `Modules` VALUES ('1af5418a448f8ad05451e3f7dbb2d9af9cb13458eea2368ebfc539476b954f1c', 'auth', 'RiskFingerprint', 'Mc64', '0', null);
INSERT INTO `Modules` VALUES ('2e6d53adab37a41542b38e01f62cd365eab8805ed0de73c307cc6d9d1dfe478c', 'auth', 'Password', 'Win', '321', null);
INSERT INTO `Modules` VALUES ('36b27cd911b33c61730a8b82c8b2495fd16e8024fc3b2dde08861c77a852941c', 'auth', 'Thumbprint', 'Win', '512', 'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208');
INSERT INTO `Modules` VALUES ('52e2978db6468dfade7c61da89513f443c9225692b5085fbe956749870993703', 'auth', 'SelectGameAccount', 'Mc64', '0', null);
INSERT INTO `Modules` VALUES ('5e298e530698af905e1247e51ef0b109b352ac310ce7802a1f63613db980ed17', 'auth', 'RiskFingerprint', 'Win', '0', null);
INSERT INTO `Modules` VALUES ('851c1d2ef926e9b9a345a460874e65517195129b9e3bdec7cc77710fa0b1fad6', 'auth', 'Password', 'Wn64', '321', null);
INSERT INTO `Modules` VALUES ('894d25d3219d97d085ea5a8b98e66df5bd9f460ec6f104455246a12b8921409d', 'auth', 'SelectGameAccount', 'Wn64', '0', null);
INSERT INTO `Modules` VALUES ('8c43bda10be33a32abbc09fb2279126c7f5953336391276cff588565332fcd40', 'auth', 'RiskFingerprint', 'Wn64', '0', null);
INSERT INTO `Modules` VALUES ('abc6bb719a73ec1055296001910e26afa561f701ad9995b1ecd7f55f9d3ca37c', 'auth', 'SelectGameAccount', 'Win', '0', null);
INSERT INTO `Modules` VALUES ('b37136b39add83cfdbafa81857de3dd8f15b34e0135ec6cd9c3131d3a578d8c2', 'auth', 'Thumbprint', 'Mc64', '512', 'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208');
INSERT INTO `Modules` VALUES ('c3a1ac0694979e709c3b5486927e558af1e2be02ca96e5615c5a65aacc829226', 'auth', 'Thumbprint', 'Wn64', '512', 'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208');

-- ----------------------------
-- Table structure for RealmCharacterTemplates
-- ----------------------------
DROP TABLE IF EXISTS `RealmCharacterTemplates`;
CREATE TABLE `RealmCharacterTemplates` (
  `RealmId` int(10) unsigned NOT NULL,
  `SetId` int(10) unsigned NOT NULL DEFAULT '0',
  UNIQUE KEY `RealmId` (`RealmId`,`SetId`) USING BTREE,
  FOREIGN KEY (`RealmId`) REFERENCES `Realms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of RealmCharacterTemplates
-- ----------------------------

-- ----------------------------
-- Table structure for RealmClasses
-- ----------------------------
DROP TABLE IF EXISTS `RealmClasses`;
CREATE TABLE `RealmClasses` (
  `RealmId` int(11) unsigned NOT NULL,
  `Class` tinyint(4) unsigned NOT NULL COMMENT 'Class Id',
  `Expansion` tinyint(4) unsigned NOT NULL COMMENT 'Expansion for class activation',
  UNIQUE KEY `RealmId` (`RealmId`,`Class`) USING BTREE,
  FOREIGN KEY (`RealmId`) REFERENCES `Realms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of RealmClasses
-- ----------------------------
INSERT INTO `RealmClasses` VALUES ('1', '1', '0');
INSERT INTO `RealmClasses` VALUES ('1', '2', '0');
INSERT INTO `RealmClasses` VALUES ('1', '3', '0');
INSERT INTO `RealmClasses` VALUES ('1', '4', '0');
INSERT INTO `RealmClasses` VALUES ('1', '5', '0');
INSERT INTO `RealmClasses` VALUES ('1', '6', '2');
INSERT INTO `RealmClasses` VALUES ('1', '7', '0');
INSERT INTO `RealmClasses` VALUES ('1', '8', '0');
INSERT INTO `RealmClasses` VALUES ('1', '9', '0');
INSERT INTO `RealmClasses` VALUES ('1', '10', '4');
INSERT INTO `RealmClasses` VALUES ('1', '11', '0');

-- ----------------------------
-- Table structure for RealmRaces
-- ----------------------------
DROP TABLE IF EXISTS `RealmRaces`;
CREATE TABLE `RealmRaces` (
  `RealmId` int(11) unsigned NOT NULL,
  `Race` tinyint(4) unsigned NOT NULL COMMENT 'Race Id',
  `Expansion` tinyint(4) unsigned NOT NULL COMMENT 'Expansion for race activation',
  UNIQUE KEY `RealmId` (`RealmId`,`Race`) USING BTREE,
  FOREIGN KEY (`RealmId`) REFERENCES `Realms` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of RealmRaces
-- ----------------------------
INSERT INTO `RealmRaces` VALUES ('1', '1', '0');
INSERT INTO `RealmRaces` VALUES ('1', '2', '0');
INSERT INTO `RealmRaces` VALUES ('1', '3', '0');
INSERT INTO `RealmRaces` VALUES ('1', '4', '0');
INSERT INTO `RealmRaces` VALUES ('1', '5', '0');
INSERT INTO `RealmRaces` VALUES ('1', '6', '0');
INSERT INTO `RealmRaces` VALUES ('1', '7', '0');
INSERT INTO `RealmRaces` VALUES ('1', '8', '0');
INSERT INTO `RealmRaces` VALUES ('1', '9', '3');
INSERT INTO `RealmRaces` VALUES ('1', '10', '1');
INSERT INTO `RealmRaces` VALUES ('1', '11', '1');
INSERT INTO `RealmRaces` VALUES ('1', '22', '3');
INSERT INTO `RealmRaces` VALUES ('1', '24', '4');
INSERT INTO `RealmRaces` VALUES ('1', '25', '4');
INSERT INTO `RealmRaces` VALUES ('1', '26', '4');

-- ----------------------------
-- Table structure for Realms
-- ----------------------------
DROP TABLE IF EXISTS `Realms`;
CREATE TABLE `Realms` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `IP` varchar(15) DEFAULT NULL,
  `Port` smallint(5) unsigned NOT NULL DEFAULT '8100',
  `Category` int(11) unsigned NOT NULL DEFAULT '1',
  `Type` tinyint(4) unsigned NOT NULL,
  `State` tinyint(4) unsigned NOT NULL,
  `Flags` tinyint(4) unsigned NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of Realms
-- ----------------------------
INSERT INTO `Realms` VALUES ('1', 'Arctium WoW', '127.0.0.1', '3724', '1', '1', '0', '0');

-- ----------------------------
-- Table structure for WorldNodes
-- ----------------------------
DROP TABLE IF EXISTS `WorldNodes`;
CREATE TABLE `WorldNodes` (
  `MapId` int(10) NOT NULL DEFAULT '-1',
  `Address` varchar(15) DEFAULT '',
  `Port` smallint(5) unsigned NOT NULL DEFAULT '9100',
  PRIMARY KEY (`MapId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of WorldNodes
-- ----------------------------
INSERT INTO `WorldNodes` VALUES ('-1', '127.0.0.1', '9100');

-- ----------------------------
-- Table structure for WorldServers
-- ----------------------------
DROP TABLE IF EXISTS `WorldServers`;
CREATE TABLE `WorldServers` (
  `MapId` int(10) NOT NULL DEFAULT '-1',
  `Address` varchar(15) DEFAULT '',
  `Port` smallint(5) unsigned NOT NULL DEFAULT '8100',
  PRIMARY KEY (`MapId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of WorldServers
-- ----------------------------
INSERT INTO `WorldServers` VALUES ('-1', '127.0.0.1', '8100');
