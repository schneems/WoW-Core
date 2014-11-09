-- ----------------------------
-- Table structure for accounts
-- ----------------------------
DROP TABLE [dbo].[accounts]
GO
CREATE TABLE [dbo].[accounts] (
[Id] bigint NOT NULL IDENTITY(1,1) ,
[GivenName] nvarchar(100) NULL DEFAULT (N'') ,
[Surname] nvarchar(100) NULL DEFAULT (N'') ,
[Email] nvarchar(100) NOT NULL ,
[Tag] nvarchar(30) NULL DEFAULT (N'') ,
[Region] tinyint NOT NULL ,
[Language] nvarchar(4) NULL DEFAULT NULL ,
[Flags] bigint NOT NULL ,
[PasswordVerifier] nvarchar(256) NULL DEFAULT NULL ,
[Salt] nvarchar(64) NULL DEFAULT NULL ,
[IP] nvarchar(15) NULL DEFAULT NULL ,
[LoginFailures] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of accounts
-- ----------------------------
SET IDENTITY_INSERT [dbo].[accounts] ON
GO
SET IDENTITY_INSERT [dbo].[accounts] OFF
GO

-- ----------------------------
-- Table structure for characterredirects
-- ----------------------------
DROP TABLE [dbo].[characterredirects]
GO
CREATE TABLE [dbo].[characterredirects] (
[Key] bigint NOT NULL ,
[CharacterGuid] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of characterredirects
-- ----------------------------

-- ----------------------------
-- Table structure for components
-- ----------------------------
DROP TABLE [dbo].[components]
GO
CREATE TABLE [dbo].[components] (
[Program] nvarchar(4) NOT NULL ,
[Platform] nvarchar(4) NOT NULL ,
[Build] int NOT NULL 
)


GO

-- ----------------------------
-- Records of components
-- ----------------------------
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'Bnet', N'Win', N'37165')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'Bnet', N'Wn64', N'37165')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'base', N'19057')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'deDE', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'enGB', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'enUS', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'esES', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'esMX', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'frFR', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'itIT', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'koKR', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Mc64', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'plPL', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'ptBR', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'ruRU', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Win', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'Wn64', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'zhCN', N'19116')
GO
GO
INSERT INTO [dbo].[components] ([Program], [Platform], [Build]) VALUES (N'WoW', N'zhTW', N'19116')
GO
GO

-- ----------------------------
-- Table structure for gameaccountcharactertemplates
-- ----------------------------
DROP TABLE [dbo].[gameaccountcharactertemplates]
GO
CREATE TABLE [dbo].[gameaccountcharactertemplates] (
[GameAccountId] bigint NOT NULL ,
[SetId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of gameaccountcharactertemplates
-- ----------------------------

-- ----------------------------
-- Table structure for gameaccountclasses
-- ----------------------------
DROP TABLE [dbo].[gameaccountclasses]
GO
CREATE TABLE [dbo].[gameaccountclasses] (
[GameAccountId] bigint NOT NULL ,
[Class] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of gameaccountclasses
-- ----------------------------

-- ----------------------------
-- Table structure for gameaccountraces
-- ----------------------------
DROP TABLE [dbo].[gameaccountraces]
GO
CREATE TABLE [dbo].[gameaccountraces] (
[GameAccountId] bigint NOT NULL ,
[Race] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of gameaccountraces
-- ----------------------------

-- ----------------------------
-- Table structure for gameaccountredirects
-- ----------------------------
DROP TABLE [dbo].[gameaccountredirects]
GO
CREATE TABLE [dbo].[gameaccountredirects] (
[Key] bigint NOT NULL ,
[GameAccountId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of gameaccountredirects
-- ----------------------------

-- ----------------------------
-- Table structure for gameaccounts
-- ----------------------------
DROP TABLE [dbo].[gameaccounts]
GO
CREATE TABLE [dbo].[gameaccounts] (
[Id] bigint NOT NULL IDENTITY(1,1) ,
[AccountId] bigint NOT NULL ,
[Game] nvarchar(10) NULL DEFAULT NULL ,
[Index] tinyint NOT NULL DEFAULT ((0)) ,
[Region] tinyint NOT NULL ,
[Flags] bigint NOT NULL ,
[BoxLevel] tinyint NOT NULL ,
[OS] nvarchar(4) NULL DEFAULT NULL ,
[SessionKey] nvarchar(80) NULL DEFAULT NULL ,
[IsOnline] smallint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of gameaccounts
-- ----------------------------
SET IDENTITY_INSERT [dbo].[gameaccounts] ON
GO
SET IDENTITY_INSERT [dbo].[gameaccounts] OFF
GO

-- ----------------------------
-- Table structure for modules
-- ----------------------------
DROP TABLE [dbo].[modules]
GO
CREATE TABLE [dbo].[modules] (
[Hash] nvarchar(64) NOT NULL ,
[Type] nvarchar(8) NOT NULL ,
[Name] nvarchar(255) NOT NULL ,
[System] nvarchar(8) NOT NULL ,
[Size] int NOT NULL ,
[Data] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of modules
-- ----------------------------
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'19c91b68752b7826df498bf73aca1103c86962a9a55a0a7033e5ad895f4d927c', N'auth', N'Password', N'Mc64', N'321', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'1af5418a448f8ad05451e3f7dbb2d9af9cb13458eea2368ebfc539476b954f1c', N'auth', N'RiskFingerprint', N'Mc64', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'2e6d53adab37a41542b38e01f62cd365eab8805ed0de73c307cc6d9d1dfe478c', N'auth', N'Password', N'Win', N'321', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'36b27cd911b33c61730a8b82c8b2495fd16e8024fc3b2dde08861c77a852941c', N'auth', N'Thumbprint', N'Win', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'52e2978db6468dfade7c61da89513f443c9225692b5085fbe956749870993703', N'auth', N'SelectGameAccount', N'Mc64', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'5e298e530698af905e1247e51ef0b109b352ac310ce7802a1f63613db980ed17', N'auth', N'RiskFingerprint', N'Win', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'851c1d2ef926e9b9a345a460874e65517195129b9e3bdec7cc77710fa0b1fad6', N'auth', N'Password', N'Wn64', N'321', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'894d25d3219d97d085ea5a8b98e66df5bd9f460ec6f104455246a12b8921409d', N'auth', N'SelectGameAccount', N'Wn64', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'8c43bda10be33a32abbc09fb2279126c7f5953336391276cff588565332fcd40', N'auth', N'RiskFingerprint', N'Wn64', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'abc6bb719a73ec1055296001910e26afa561f701ad9995b1ecd7f55f9d3ca37c', N'auth', N'SelectGameAccount', N'Win', N'0', null)
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'b37136b39add83cfdbafa81857de3dd8f15b34e0135ec6cd9c3131d3a578d8c2', N'auth', N'Thumbprint', N'Mc64', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO
INSERT INTO [dbo].[modules] ([Hash], [Type], [Name], [System], [Size], [Data]) VALUES (N'c3a1ac0694979e709c3b5486927e558af1e2be02ca96e5615c5a65aacc829226', N'auth', N'Thumbprint', N'Wn64', N'512', N'E716F4F0A01EB9C032A6C1393356A4F766F067949D71023C0CFC0613718966EF814E65CC6EE70C432A7F8AFD8A062B52603A2697E851D231D72C0277614181D713369B1E8E4BEEAB72045A9AAD45F319DB918ECDDB83C8EF8B7510600D391D45E7FEC0BEEAE904A5F9FA620F1CCDAD699D84A4739CE669B5A551831E396214E13B4C88F573F5CDC784CD01530C086B674C03BEB66403A0F87ED17ABBB403DE54CF31BE828A20C566C22E4D4263AA77220B0644D99245345BCAC276EA06925EB984D664725C3CB757140AFE12E27CB996F17159B1057E9B58B78BBB5A139C9FF6215A0D250B75FC9DD435655DDEADCD6CFD84800792C146B3633188ECEB53D2038C185E0BD51A9E6C70FD38ADF530F8DF50FB62053C5E894897AB7DD65C7AC80665F18E7989BE6E30F15E939751123D6D8A44F033175301D15AAAD2AEA06FAC60BA4065846AE938F32B1CB15F16DC0E76792A7332346896048065D17C059899E1D2300E402BD0EA74265DA6A42B1C854E2470D7B21AE4A2DAE90E602A759B2CA0EE610B50D5389DB89335D5451FE76DD85B09FD5297D6F9EFB6C34CE885007F7DF20D6A524E0C3E772FA04B3DD2E014D3A337A790943DAD523CBB5453F4FDF8E74DFE361BD5F25AB31952B478148B570DF5762643F32B994FEC99A747E4A265A66EE84A53509EC285C84679606049314FC526C61B537AC8061C788F8B86F52208')
GO
GO

-- ----------------------------
-- Table structure for realmcharactertemplates
-- ----------------------------
DROP TABLE [dbo].[realmcharactertemplates]
GO
CREATE TABLE [dbo].[realmcharactertemplates] (
[RealmId] bigint NOT NULL ,
[SetId] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of realmcharactertemplates
-- ----------------------------

-- ----------------------------
-- Table structure for realmclasses
-- ----------------------------
DROP TABLE [dbo].[realmclasses]
GO
CREATE TABLE [dbo].[realmclasses] (
[RealmId] int NOT NULL ,
[Class] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of realmclasses
-- ----------------------------
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'1', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'2', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'3', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'4', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'5', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'6', N'2')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'7', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'8', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'9', N'0')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'10', N'4')
GO
GO
INSERT INTO [dbo].[realmclasses] ([RealmId], [Class], [Expansion]) VALUES (N'1', N'11', N'0')
GO
GO

-- ----------------------------
-- Table structure for realmraces
-- ----------------------------
DROP TABLE [dbo].[realmraces]
GO
CREATE TABLE [dbo].[realmraces] (
[RealmId] int NOT NULL ,
[Race] tinyint NOT NULL ,
[Expansion] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of realmraces
-- ----------------------------
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'1', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'2', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'3', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'4', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'5', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'6', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'7', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'8', N'0')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'9', N'3')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'10', N'1')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'11', N'1')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'22', N'3')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'24', N'4')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'25', N'4')
GO
GO
INSERT INTO [dbo].[realmraces] ([RealmId], [Race], [Expansion]) VALUES (N'1', N'26', N'4')
GO
GO

-- ----------------------------
-- Table structure for realms
-- ----------------------------
DROP TABLE [dbo].[realms]
GO
CREATE TABLE [dbo].[realms] (
[Id] int NOT NULL IDENTITY(2,1) ,
[Name] nvarchar(255) NULL DEFAULT NULL ,
[IP] nvarchar(15) NULL DEFAULT NULL ,
[Port] int NOT NULL DEFAULT ((8100)) ,
[Category] tinyint NOT NULL DEFAULT ((1)) ,
[Type] tinyint NOT NULL ,
[State] tinyint NOT NULL ,
[Flags] tinyint NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[realms]', RESEED, 1)
GO

-- ----------------------------
-- Records of realms
-- ----------------------------
SET IDENTITY_INSERT [dbo].[realms] ON
GO
INSERT INTO [dbo].[realms] ([Id], [Name], [IP], [Port], [Category], [Type], [State], [Flags]) VALUES (N'1', N'Arctium WoW', N'127.0.0.1', N'3724', N'1', N'1', N'0', N'0')
GO
GO
SET IDENTITY_INSERT [dbo].[realms] OFF
GO

-- ----------------------------
-- Table structure for worldservers
-- ----------------------------
DROP TABLE [dbo].[worldservers]
GO
CREATE TABLE [dbo].[worldservers] (
[MapId] int NOT NULL DEFAULT ((-1)) ,
[Address] nvarchar(15) NULL DEFAULT (N'') ,
[Port] int NOT NULL DEFAULT ((8100)) 
)


GO

-- ----------------------------
-- Records of worldservers
-- ----------------------------
INSERT INTO [dbo].[worldservers] ([MapId], [Address], [Port]) VALUES (N'-1', N'127.0.0.1', N'8100')
GO
GO

-- ----------------------------
-- Indexes structure for table accounts
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table accounts
-- ----------------------------
ALTER TABLE [dbo].[accounts] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table characterredirects
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characterredirects
-- ----------------------------
ALTER TABLE [dbo].[characterredirects] ADD PRIMARY KEY ([Key])
GO

-- ----------------------------
-- Indexes structure for table components
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table components
-- ----------------------------
ALTER TABLE [dbo].[components] ADD PRIMARY KEY ([Program], [Platform])
GO

-- ----------------------------
-- Indexes structure for table gameaccountcharactertemplates
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table gameaccountcharactertemplates
-- ----------------------------
ALTER TABLE [dbo].[gameaccountcharactertemplates] ADD PRIMARY KEY ([GameAccountId], [SetId])
GO

-- ----------------------------
-- Indexes structure for table gameaccountclasses
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table gameaccountclasses
-- ----------------------------
ALTER TABLE [dbo].[gameaccountclasses] ADD PRIMARY KEY ([GameAccountId], [Class])
GO

-- ----------------------------
-- Indexes structure for table gameaccountraces
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table gameaccountraces
-- ----------------------------
ALTER TABLE [dbo].[gameaccountraces] ADD PRIMARY KEY ([GameAccountId], [Race])
GO

-- ----------------------------
-- Indexes structure for table gameaccountredirects
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table gameaccountredirects
-- ----------------------------
ALTER TABLE [dbo].[gameaccountredirects] ADD PRIMARY KEY ([Key])
GO

-- ----------------------------
-- Indexes structure for table gameaccounts
-- ----------------------------
CREATE INDEX [Account] ON [dbo].[gameaccounts]
([AccountId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table gameaccounts
-- ----------------------------
ALTER TABLE [dbo].[gameaccounts] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table modules
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table modules
-- ----------------------------
ALTER TABLE [dbo].[modules] ADD PRIMARY KEY ([Hash])
GO

-- ----------------------------
-- Indexes structure for table realmcharactertemplates
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table realmcharactertemplates
-- ----------------------------
ALTER TABLE [dbo].[realmcharactertemplates] ADD PRIMARY KEY ([RealmId], [SetId])
GO

-- ----------------------------
-- Indexes structure for table realmclasses
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table realmclasses
-- ----------------------------
ALTER TABLE [dbo].[realmclasses] ADD PRIMARY KEY ([RealmId], [Class])
GO

-- ----------------------------
-- Indexes structure for table realmraces
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table realmraces
-- ----------------------------
ALTER TABLE [dbo].[realmraces] ADD PRIMARY KEY ([RealmId], [Race])
GO

-- ----------------------------
-- Indexes structure for table realms
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table realms
-- ----------------------------
ALTER TABLE [dbo].[realms] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table worldservers
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table worldservers
-- ----------------------------
ALTER TABLE [dbo].[worldservers] ADD PRIMARY KEY ([MapId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[gameaccountcharactertemplates]
-- ----------------------------
ALTER TABLE [dbo].[gameaccountcharactertemplates] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[gameaccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[gameaccountclasses]
-- ----------------------------
ALTER TABLE [dbo].[gameaccountclasses] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[gameaccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[gameaccountraces]
-- ----------------------------
ALTER TABLE [dbo].[gameaccountraces] ADD FOREIGN KEY ([GameAccountId]) REFERENCES [dbo].[gameaccounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[gameaccounts]
-- ----------------------------
ALTER TABLE [dbo].[gameaccounts] ADD FOREIGN KEY ([AccountId]) REFERENCES [dbo].[accounts] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[realmcharactertemplates]
-- ----------------------------
ALTER TABLE [dbo].[realmcharactertemplates] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[realmclasses]
-- ----------------------------
ALTER TABLE [dbo].[realmclasses] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[realmraces]
-- ----------------------------
ALTER TABLE [dbo].[realmraces] ADD FOREIGN KEY ([RealmId]) REFERENCES [dbo].[realms] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
GO
