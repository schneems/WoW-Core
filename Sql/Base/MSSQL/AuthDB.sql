-- ----------------------------
-- Table structure for Accounts
-- ----------------------------
DROP TABLE [dbo].[Accounts]
GO
CREATE TABLE [dbo].[Accounts] (
[Id] int NOT NULL IDENTITY(1,1) ,
[GivenName] varchar(100) NULL DEFAULT '' ,
[Surname] varchar(100) NULL DEFAULT '' ,
[Email] varchar(100) NOT NULL ,
[Tag] varchar(30) NULL DEFAULT '' ,
[Region] tinyint NOT NULL ,
[Language] varchar(4) NULL DEFAULT NULL ,
[Flags] bigint NOT NULL ,
[PasswordVerifier] varchar(256) NULL DEFAULT NULL ,
[Salt] varchar(64) NULL DEFAULT NULL ,
[IP] varchar(15) NULL DEFAULT NULL ,
[LoginFailures] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of Accounts
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Accounts] ON
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO

-- ----------------------------
-- Table structure for CharacterRedirects
-- ----------------------------
DROP TABLE [dbo].[CharacterRedirects]
GO
CREATE TABLE [dbo].[CharacterRedirects] (
[Key] bigint NOT NULL ,
[CharacterGuid] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterRedirects
-- ----------------------------

-- ----------------------------
-- Table structure for Components
-- ----------------------------
DROP TABLE [dbo].[Components]
GO
CREATE TABLE [dbo].[Components] (
[Program] varchar(4) NOT NULL ,
[Platform] varchar(4) NOT NULL ,
[Build] int NOT NULL 
)


GO

-- ----------------------------
-- Records of Components
-- ----------------------------
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'Bnet', N'Win', N'37165')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'Bnet', N'Wn64', N'37165')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'base', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'deDE', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'enAU', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'enGB', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'enUS', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'esES', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'esMX', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'frFR', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'itIT', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'koKR', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Mc64', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'plPL', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'ptBR', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'ruRU', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Win', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Wn64', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'zhCN', N'20490')
GO
GO
INSERT INTO [dbo].[Components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'zhTW', N'20490')
GO
GO

-- ----------------------------
-- Table structure for GameAccountCharacterTemplates
-- ----------------------------
DROP TABLE [dbo].[GameAccountCharacterTemplates]
GO
CREATE TABLE [dbo].[GameAccountCharacterTemplates] (
[GameAccountId] int NOT NULL ,
[SetId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of GameAccountCharacterTemplates
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountClasses
-- ----------------------------
DROP TABLE [dbo].[GameAccountClasses]
GO
CREATE TABLE [dbo].[GameAccountClasses] (
[GameAccountId] int NOT NULL ,
[Class] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of GameAccountClasses
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountRaces
-- ----------------------------
DROP TABLE [dbo].[GameAccountRaces]
GO
CREATE TABLE [dbo].[GameAccountRaces] (
[GameAccountId] int NOT NULL ,
[Race] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of GameAccountRaces
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccountRedirects
-- ----------------------------
DROP TABLE [dbo].[GameAccountRedirects]
GO
CREATE TABLE [dbo].[GameAccountRedirects] (
[Key] bigint NOT NULL ,
[GameAccountId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of GameAccountRedirects
-- ----------------------------

-- ----------------------------
-- Table structure for GameAccounts
-- ----------------------------
DROP TABLE [dbo].[GameAccounts]
GO
CREATE TABLE [dbo].[GameAccounts] (
[Id] int NOT NULL IDENTITY(1,1) ,
[AccountId] int NOT NULL ,
[Game] varchar(10) NULL DEFAULT NULL ,
[Index] tinyint NOT NULL DEFAULT ((0)) ,
[Region] tinyint NOT NULL ,
[Flags] bigint NOT NULL ,
[BoxLevel] tinyint NOT NULL ,
[OS] varchar(4) NULL DEFAULT NULL ,
[SessionKey] varchar(80) NULL DEFAULT NULL ,
[IsOnline] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of GameAccounts
-- ----------------------------
SET IDENTITY_INSERT [dbo].[GameAccounts] ON
GO
SET IDENTITY_INSERT [dbo].[GameAccounts] OFF
GO

-- ----------------------------
-- Table structure for Modules
-- ----------------------------
DROP TABLE [dbo].[Modules]
GO
CREATE TABLE [dbo].[Modules] (
[Hash] varchar(64) NOT NULL ,
[Type] varchar(8) NOT NULL ,
[Name] varchar(255) NOT NULL ,
[System] varchar(8) NOT NULL ,
[Size] int NOT NULL ,
[Data] varchar(MAX) NULL DEFAULT NULL 
)


GO

-- ----------------------------
-- Records of Modules
-- ----------------------------
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'19c91b68752b7826df498bf73aca1103c86962a9a55a0a7033e5ad895f4d927c', N'auth', N'Password', N'Mc64', N'321', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'1af5418a448f8ad05451e3f7dbb2d9af9cb13458eea2368ebfc539476b954f1c', N'auth', N'RiskFingerprint', N'Mc64', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'2e6d53adab37a41542b38e01f62cd365eab8805ed0de73c307cc6d9d1dfe478c', N'auth', N'Password', N'Win', N'321', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'36b27cd911b33c61730a8b82c8b2495fd16e8024fc3b2dde08861c77a852941c', N'auth', N'Thumbprint', N'Win', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'52e2978db6468dfade7c61da89513f443c9225692b5085fbe956749870993703', N'auth', N'SelectGameAccount', N'Mc64', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'5e298e530698af905e1247e51ef0b109b352ac310ce7802a1f63613db980ed17', N'auth', N'RiskFingerprint', N'Win', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'851c1d2ef926e9b9a345a460874e65517195129b9e3bdec7cc77710fa0b1fad6', N'auth', N'Password', N'Wn64', N'321', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'894d25d3219d97d085ea5a8b98e66df5bd9f460ec6f104455246a12b8921409d', N'auth', N'SelectGameAccount', N'Wn64', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'8c43bda10be33a32abbc09fb2279126c7f5953336391276cff588565332fcd40', N'auth', N'RiskFingerprint', N'Wn64', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'abc6bb719a73ec1055296001910e26afa561f701ad9995b1ecd7f55f9d3ca37c', N'auth', N'SelectGameAccount', N'Win', N'0', null)
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'b37136b39add83cfdbafa81857de3dd8f15b34e0135ec6cd9c3131d3a578d8c2', N'auth', N'Thumbprint', N'Mc64', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO
INSERT INTO [dbo].[Modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'c3a1ac0694979e709c3b5486927e558af1e2be02ca96e5615c5a65aacc829226', N'auth', N'Thumbprint', N'Wn64', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO

-- ----------------------------
-- Table structure for RealmCharacterTemplates
-- ----------------------------
DROP TABLE [dbo].[RealmCharacterTemplates]
GO
CREATE TABLE [dbo].[RealmCharacterTemplates] (
[RealmId] int NOT NULL ,
[SetId] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of RealmCharacterTemplates
-- ----------------------------

-- ----------------------------
-- Table structure for RealmClasses
-- ----------------------------
DROP TABLE [dbo].[RealmClasses]
GO
CREATE TABLE [dbo].[RealmClasses] (
[RealmId] int NOT NULL ,
[Class] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of RealmClasses
-- ----------------------------
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'1', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'2', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'3', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'4', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'5', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'6', N'2')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'7', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'8', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'9', N'0')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'10', N'4')
GO
GO
INSERT INTO [dbo].[RealmClasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'11', N'0')
GO
GO

-- ----------------------------
-- Table structure for RealmRaces
-- ----------------------------
DROP TABLE [dbo].[RealmRaces]
GO
CREATE TABLE [dbo].[RealmRaces] (
[RealmId] int NOT NULL ,
[Race] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of RealmRaces
-- ----------------------------
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'1', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'2', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'3', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'4', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'5', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'6', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'7', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'8', N'0')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'9', N'3')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'10', N'1')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'11', N'1')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'22', N'3')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'24', N'4')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'25', N'4')
GO
GO
INSERT INTO [dbo].[RealmRaces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'26', N'4')
GO
GO

-- ----------------------------
-- Table structure for Realms
-- ----------------------------
DROP TABLE [dbo].[Realms]
GO
CREATE TABLE [dbo].[Realms] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Name] varchar(255) NULL DEFAULT NULL ,
[IP] varchar(15) NULL DEFAULT NULL ,
[Port] smallint NOT NULL DEFAULT ((8100)) ,
[Category] int NOT NULL DEFAULT ((1)) ,
[Type] tinyint NOT NULL ,
[State] tinyint NOT NULL ,
[Flags] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of Realms
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Realms] ON
GO
INSERT INTO [dbo].[Realms] ([Id], [Name], [IP], [Port], [Category], [Type], [State], [Flags]) VALUES (N'1', N'Project WoW', N'127.0.0.1', N'3724', N'1', N'1', N'0', N'0')
GO
GO
SET IDENTITY_INSERT [dbo].[Realms] OFF
GO

-- ----------------------------
-- Table structure for WorldNodes
-- ----------------------------
DROP TABLE [dbo].[WorldNodes]
GO
CREATE TABLE [dbo].[WorldNodes] (
[MapId] int NOT NULL DEFAULT ('-1') ,
[Address] varchar(15) NULL DEFAULT '' ,
[Port] smallint NOT NULL DEFAULT ((9100)) 
)


GO

-- ----------------------------
-- Records of WorldNodes
-- ----------------------------
INSERT INTO [dbo].[WorldNodes] ([MapId], [Address], [Port]) VALUES (N'-1', N'127.0.0.1', N'9100')
GO
GO

-- ----------------------------
-- Table structure for WorldServers
-- ----------------------------
DROP TABLE [dbo].[WorldServers]
GO
CREATE TABLE [dbo].[WorldServers] (
[MapId] int NOT NULL DEFAULT ('-1') ,
[Address] varchar(15) NULL DEFAULT '' ,
[Port] smallint NOT NULL DEFAULT ((8100)) 
)


GO

-- ----------------------------
-- Records of WorldServers
-- ----------------------------
INSERT INTO [dbo].[WorldServers] ([MapId], [Address], [Port]) VALUES (N'-1', N'127.0.0.1', N'8100')
GO
GO

-- ----------------------------
-- Indexes structure for table Accounts
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Accounts
-- ----------------------------
ALTER TABLE [dbo].[Accounts] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table CharacterRedirects
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table CharacterRedirects
-- ----------------------------
ALTER TABLE [dbo].[CharacterRedirects] ADD PRIMARY KEY ([Key])
GO

-- ----------------------------
-- Indexes structure for table Components
-- ----------------------------
CREATE UNIQUE INDEX [Program] ON [dbo].[Components]
([Program] ASC, [Platform] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table GameAccountCharacterTemplates
-- ----------------------------
CREATE UNIQUE INDEX [GameAccountId] ON [dbo].[GameAccountCharacterTemplates]
([GameAccountId] ASC, [SetId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table GameAccountClasses
-- ----------------------------
CREATE UNIQUE INDEX [GameAccountId] ON [dbo].[GameAccountClasses]
([GameAccountId] ASC, [Class] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table GameAccountRaces
-- ----------------------------
CREATE UNIQUE INDEX [GameAccountId] ON [dbo].[GameAccountRaces]
([GameAccountId] ASC, [Race] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table GameAccountRedirects
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table GameAccountRedirects
-- ----------------------------
ALTER TABLE [dbo].[GameAccountRedirects] ADD PRIMARY KEY ([Key])
GO

-- ----------------------------
-- Indexes structure for table GameAccounts
-- ----------------------------
CREATE INDEX [Account] ON [dbo].[GameAccounts]
([AccountId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table GameAccounts
-- ----------------------------
ALTER TABLE [dbo].[GameAccounts] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Modules
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Modules
-- ----------------------------
ALTER TABLE [dbo].[Modules] ADD PRIMARY KEY ([Hash])
GO

-- ----------------------------
-- Indexes structure for table RealmCharacterTemplates
-- ----------------------------
CREATE UNIQUE INDEX [RealmId] ON [dbo].[RealmCharacterTemplates]
([RealmId] ASC, [SetId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table RealmClasses
-- ----------------------------
CREATE UNIQUE INDEX [RealmId] ON [dbo].[RealmClasses]
([RealmId] ASC, [Class] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table RealmRaces
-- ----------------------------
CREATE UNIQUE INDEX [RealmId] ON [dbo].[RealmRaces]
([RealmId] ASC, [Race] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table Realms
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Realms
-- ----------------------------
ALTER TABLE [dbo].[Realms] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table WorldNodes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table WorldNodes
-- ----------------------------
ALTER TABLE [dbo].[WorldNodes] ADD PRIMARY KEY ([MapId])
GO

-- ----------------------------
-- Indexes structure for table WorldServers
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table WorldServers
-- ----------------------------
ALTER TABLE [dbo].[WorldServers] ADD PRIMARY KEY ([MapId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[GameAccountCharacterTemplates]
-- ----------------------------
ALTER TABLE [dbo].[GameAccountCharacterTemplates] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[GameAccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[GameAccountClasses]
-- ----------------------------
ALTER TABLE [dbo].[GameAccountClasses] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[GameAccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[GameAccountRaces]
-- ----------------------------
ALTER TABLE [dbo].[GameAccountRaces] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[GameAccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[GameAccounts]
-- ----------------------------
ALTER TABLE [dbo].[GameAccounts] ADD FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Accounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[RealmCharacterTemplates]
-- ----------------------------
ALTER TABLE [dbo].[RealmCharacterTemplates] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[Realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[RealmClasses]
-- ----------------------------
ALTER TABLE [dbo].[RealmClasses] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[Realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[RealmRaces]
-- ----------------------------
ALTER TABLE [dbo].[RealmRaces] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[Realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
