-- ----------------------------
-- Table structure for characteractions
-- ----------------------------
CREATE TABLE [dbo].[characteractions] (
[CharacterGuid] bigint NOT NULL ,
[Action] tinyint NOT NULL DEFAULT ((0)) ,
[Slot] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of characteractions
-- ----------------------------

-- ----------------------------
-- Table structure for charactercreationactions
-- ----------------------------
CREATE TABLE [dbo].[charactercreationactions] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[Action] int NOT NULL DEFAULT ((0)) ,
[Slot] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of charactercreationactions
-- ----------------------------

-- ----------------------------
-- Table structure for charactercreationdata
-- ----------------------------
CREATE TABLE [dbo].[charactercreationdata] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[Zone] bigint NOT NULL DEFAULT ((0)) ,
[Map] bigint NOT NULL DEFAULT ((0)) ,
[X] real NOT NULL ,
[Y] real NOT NULL ,
[Z] real NOT NULL ,
[O] real NOT NULL 
)


GO

-- ----------------------------
-- Records of charactercreationdata
-- ----------------------------

-- ----------------------------
-- Table structure for charactercreationskills
-- ----------------------------
CREATE TABLE [dbo].[charactercreationskills] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[SkillId] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of charactercreationskills
-- ----------------------------

-- ----------------------------
-- Table structure for charactercreationspells
-- ----------------------------
CREATE TABLE [dbo].[charactercreationspells] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[SpellId] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of charactercreationspells
-- ----------------------------

-- ----------------------------
-- Table structure for characteritems
-- ----------------------------
CREATE TABLE [dbo].[characteritems] (
[CharacterGuid] bigint NOT NULL ,
[ItemId] bigint NOT NULL DEFAULT ((0)) ,
[Bag] tinyint NOT NULL DEFAULT ((255)) ,
[Slot] tinyint NOT NULL ,
[Equipped] smallint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of characteritems
-- ----------------------------

-- ----------------------------
-- Table structure for characters
-- ----------------------------
CREATE TABLE [dbo].[characters] (
[Guid] bigint NOT NULL IDENTITY(1,1) ,
[GameAccountId] bigint NOT NULL DEFAULT ((0)) ,
[RealmId] bigint NOT NULL DEFAULT ((0)) ,
[Name] nvarchar(192) NOT NULL DEFAULT (N'') ,
[ListPosition] tinyint NOT NULL DEFAULT ((0)) ,
[Race] tinyint NOT NULL DEFAULT ((0)) ,
[Class] tinyint NOT NULL DEFAULT ((0)) ,
[Sex] tinyint NOT NULL DEFAULT ((0)) ,
[Skin] tinyint NOT NULL DEFAULT ((0)) ,
[Face] tinyint NOT NULL DEFAULT ((0)) ,
[HairStyle] tinyint NOT NULL DEFAULT ((0)) ,
[HairColor] tinyint NOT NULL DEFAULT ((0)) ,
[FacialHairStyle] tinyint NOT NULL DEFAULT ((0)) ,
[Level] tinyint NOT NULL DEFAULT ((0)) ,
[Zone] bigint NOT NULL DEFAULT ((0)) ,
[Map] bigint NOT NULL DEFAULT ((0)) ,
[X] real NOT NULL DEFAULT ((0)) ,
[Y] real NOT NULL DEFAULT ((0)) ,
[Z] real NOT NULL DEFAULT ((0)) ,
[O] real NOT NULL DEFAULT ((0)) ,
[GuildGuid] bigint NOT NULL DEFAULT ((0)) ,
[CharacterFlags] bigint NOT NULL DEFAULT ((0)) ,
[CustomizeFlags] bigint NOT NULL DEFAULT ((0)) ,
[Flags3] bigint NOT NULL DEFAULT ((0)) ,
[FirstLogin] binary(1) NOT NULL DEFAULT (0x01) ,
[PetCreatureDisplayId] bigint NOT NULL DEFAULT ((0)) ,
[PetLevel] bigint NOT NULL DEFAULT ((0)) ,
[PetCreatureFamily] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of characters
-- ----------------------------
SET IDENTITY_INSERT [dbo].[characters] ON
GO
SET IDENTITY_INSERT [dbo].[characters] OFF
GO

-- ----------------------------
-- Table structure for characterskills
-- ----------------------------
CREATE TABLE [dbo].[characterskills] (
[CharacterGuid] bigint NOT NULL ,
[SkillId] bigint NOT NULL DEFAULT ((0)) ,
[SkillLevel] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of characterskills
-- ----------------------------

-- ----------------------------
-- Table structure for characterspells
-- ----------------------------
CREATE TABLE [dbo].[characterspells] (
[CharacterGuid] bigint NOT NULL ,
[SpellId] bigint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of characterspells
-- ----------------------------

-- ----------------------------
-- Table structure for charactertemplateactions
-- ----------------------------
CREATE TABLE [dbo].[charactertemplateactions] (
[ClassId] bigint NOT NULL ,
[Action] bigint NOT NULL ,
[Slot] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of charactertemplateactions
-- ----------------------------

-- ----------------------------
-- Table structure for charactertemplateclasses
-- ----------------------------
CREATE TABLE [dbo].[charactertemplateclasses] (
[ClassId] bigint NOT NULL IDENTITY(1,1) ,
[SetId] bigint NOT NULL ,
[FactionGroup] smallint NOT NULL 
)


GO

-- ----------------------------
-- Records of charactertemplateclasses
-- ----------------------------
SET IDENTITY_INSERT [dbo].[charactertemplateclasses] ON
GO
SET IDENTITY_INSERT [dbo].[charactertemplateclasses] OFF
GO

-- ----------------------------
-- Table structure for charactertemplatedata
-- ----------------------------
CREATE TABLE [dbo].[charactertemplatedata] (
[ClassId] bigint NOT NULL ,
[Map] smallint NOT NULL ,
[Zone] smallint NOT NULL ,
[X] real NOT NULL ,
[Y] real NOT NULL ,
[Z] real NOT NULL ,
[O] real NOT NULL 
)


GO

-- ----------------------------
-- Records of charactertemplatedata
-- ----------------------------

-- ----------------------------
-- Table structure for charactertemplateitems
-- ----------------------------
CREATE TABLE [dbo].[charactertemplateitems] (
[ItemId] bigint NOT NULL DEFAULT ((0)) ,
[ClassId] bigint NOT NULL ,
[IsEquipped] smallint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of charactertemplateitems
-- ----------------------------

-- ----------------------------
-- Table structure for charactertemplatesets
-- ----------------------------
CREATE TABLE [dbo].[charactertemplatesets] (
[Id] bigint NOT NULL IDENTITY(1,1) ,
[Name] nvarchar(255) NULL DEFAULT (N'') ,
[Description] nvarchar(255) NULL DEFAULT (N'') 
)


GO

-- ----------------------------
-- Records of charactertemplatesets
-- ----------------------------
SET IDENTITY_INSERT [dbo].[charactertemplatesets] ON
GO
SET IDENTITY_INSERT [dbo].[charactertemplatesets] OFF
GO

-- ----------------------------
-- Table structure for charactertemplateskills
-- ----------------------------
CREATE TABLE [dbo].[charactertemplateskills] (
[SkillId] bigint NOT NULL ,
[ClassId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of charactertemplateskills
-- ----------------------------

-- ----------------------------
-- Table structure for charactertemplatespells
-- ----------------------------
CREATE TABLE [dbo].[charactertemplatespells] (
[SpellId] bigint NOT NULL ,
[ClassId] bigint NOT NULL 
)


GO

-- ----------------------------
-- Records of charactertemplatespells
-- ----------------------------

-- ----------------------------
-- Indexes structure for table characteractions
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characteractions
-- ----------------------------
ALTER TABLE [dbo].[characteractions] ADD PRIMARY KEY ([CharacterGuid])
GO

-- ----------------------------
-- Indexes structure for table charactercreationactions
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactercreationactions
-- ----------------------------
ALTER TABLE [dbo].[charactercreationactions] ADD PRIMARY KEY ([Race], [Class])
GO

-- ----------------------------
-- Indexes structure for table charactercreationdata
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactercreationdata
-- ----------------------------
ALTER TABLE [dbo].[charactercreationdata] ADD PRIMARY KEY ([Race], [Class])
GO

-- ----------------------------
-- Indexes structure for table charactercreationskills
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactercreationskills
-- ----------------------------
ALTER TABLE [dbo].[charactercreationskills] ADD PRIMARY KEY ([Race], [Class])
GO

-- ----------------------------
-- Indexes structure for table charactercreationspells
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactercreationspells
-- ----------------------------
ALTER TABLE [dbo].[charactercreationspells] ADD PRIMARY KEY ([Race], [Class])
GO

-- ----------------------------
-- Indexes structure for table characteritems
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characteritems
-- ----------------------------
ALTER TABLE [dbo].[characteritems] ADD PRIMARY KEY ([CharacterGuid])
GO

-- ----------------------------
-- Indexes structure for table characters
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characters
-- ----------------------------
ALTER TABLE [dbo].[characters] ADD PRIMARY KEY ([Guid])
GO

-- ----------------------------
-- Indexes structure for table characterskills
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characterskills
-- ----------------------------
ALTER TABLE [dbo].[characterskills] ADD PRIMARY KEY ([CharacterGuid], [SkillId])
GO

-- ----------------------------
-- Indexes structure for table characterspells
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table characterspells
-- ----------------------------
ALTER TABLE [dbo].[characterspells] ADD PRIMARY KEY ([CharacterGuid], [SpellId])
GO

-- ----------------------------
-- Indexes structure for table charactertemplateactions
-- ----------------------------
CREATE INDEX [Class] ON [dbo].[charactertemplateactions]
([ClassId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table charactertemplateactions
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateactions] ADD PRIMARY KEY ([Action], [Slot])
GO

-- ----------------------------
-- Indexes structure for table charactertemplateclasses
-- ----------------------------
CREATE INDEX [Id] ON [dbo].[charactertemplateclasses]
([ClassId] ASC) 
GO
CREATE INDEX [SetId] ON [dbo].[charactertemplateclasses]
([SetId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table charactertemplateclasses
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateclasses] ADD PRIMARY KEY ([ClassId])
GO

-- ----------------------------
-- Indexes structure for table charactertemplatedata
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactertemplatedata
-- ----------------------------
ALTER TABLE [dbo].[charactertemplatedata] ADD PRIMARY KEY ([ClassId])
GO

-- ----------------------------
-- Indexes structure for table charactertemplateitems
-- ----------------------------
CREATE INDEX [ClassId] ON [dbo].[charactertemplateitems]
([ClassId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table charactertemplateitems
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateitems] ADD PRIMARY KEY ([ItemId], [ClassId], [IsEquipped])
GO

-- ----------------------------
-- Indexes structure for table charactertemplatesets
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table charactertemplatesets
-- ----------------------------
ALTER TABLE [dbo].[charactertemplatesets] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table charactertemplateskills
-- ----------------------------
CREATE INDEX [ClassId] ON [dbo].[charactertemplateskills]
([ClassId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table charactertemplateskills
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateskills] ADD PRIMARY KEY ([SkillId], [ClassId])
GO

-- ----------------------------
-- Indexes structure for table charactertemplatespells
-- ----------------------------
CREATE INDEX [ClassId] ON [dbo].[charactertemplatespells]
([ClassId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table charactertemplatespells
-- ----------------------------
ALTER TABLE [dbo].[charactertemplatespells] ADD PRIMARY KEY ([SpellId], [ClassId])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[characteractions]
-- ----------------------------
ALTER TABLE [dbo].[characteractions] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[characters] ([Guid]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[characteritems]
-- ----------------------------
ALTER TABLE [dbo].[characteritems] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[characters] ([Guid]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[characterskills]
-- ----------------------------
ALTER TABLE [dbo].[characterskills] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[characters] ([Guid]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[characterspells]
-- ----------------------------
ALTER TABLE [dbo].[characterspells] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[characters] ([Guid]) ON DELETE CASCADE ON UPDATE CASCADE
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplateactions]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateactions] ADD FOREIGN KEY ([ClassId]) REFERENCES [dbo].[charactertemplateclasses] ([ClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplateclasses]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateclasses] ADD FOREIGN KEY ([SetId]) REFERENCES [dbo].[charactertemplatesets] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplatedata]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplatedata] ADD FOREIGN KEY ([ClassId]) REFERENCES [dbo].[charactertemplateclasses] ([ClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplateitems]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateitems] ADD FOREIGN KEY ([ClassId]) REFERENCES [dbo].[charactertemplateclasses] ([ClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplateskills]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplateskills] ADD FOREIGN KEY ([ClassId]) REFERENCES [dbo].[charactertemplateclasses] ([ClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[charactertemplatespells]
-- ----------------------------
ALTER TABLE [dbo].[charactertemplatespells] ADD FOREIGN KEY ([ClassId]) REFERENCES [dbo].[charactertemplateclasses] ([ClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
