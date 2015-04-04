-- ----------------------------
-- Table structure for CharacterActions
-- ----------------------------
DROP TABLE [dbo].[CharacterActions]
GO
CREATE TABLE [dbo].[CharacterActions] (
[CharacterGuid] bigint NOT NULL ,
[Action] tinyint NOT NULL DEFAULT ((0)) ,
[Slot] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationActions
-- ----------------------------
DROP TABLE [dbo].[CharacterCreationActions]
GO
CREATE TABLE [dbo].[CharacterCreationActions] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[Action] int NOT NULL DEFAULT ((0)) ,
[Slot] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterCreationActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationData
-- ----------------------------
DROP TABLE [dbo].[CharacterCreationData]
GO
CREATE TABLE [dbo].[CharacterCreationData] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[Zone] int NOT NULL DEFAULT ((0)) ,
[Map] int NOT NULL DEFAULT ((0)) ,
[X] real NOT NULL ,
[Y] real NOT NULL ,
[Z] real NOT NULL ,
[O] real NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterCreationData
-- ----------------------------
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'1', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'2', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'3', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'4', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'5', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'8', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'9', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'1', N'10', N'6170', N'0', N'-8914.57', N'-133.909', N'80.5378', N'5.10444')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'1', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'3', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'4', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'7', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'8', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'9', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'2', N'10', N'6451', N'1', N'-618.518', N'-4251.67', N'38.718', N'0')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'1', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'2', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'3', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'4', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'5', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'7', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'8', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'9', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'3', N'10', N'6176', N'0', N'-6230.42', N'330.232', N'383.105', N'0.501087')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'1', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'3', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'4', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'5', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'8', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'10', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'4', N'11', N'6450', N'1', N'10311.3', N'831.463', N'1326.57', N'5.48033')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'1', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'3', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'4', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'5', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'8', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'9', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'5', N'10', N'6454', N'0', N'1699.85', N'1706.56', N'135.928', N'4.88839')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'1', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'2', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'3', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'5', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'7', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'10', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'6', N'11', N'6452', N'1', N'-2915.55', N'-257.347', N'59.2693', N'0.302378')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'1', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'4', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'5', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'8', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'9', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'7', N'10', N'1', N'0', N'-4983.42', N'877.7', N'274.31', N'3.06393')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'1', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'3', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'4', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'5', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'7', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'8', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'9', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'10', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'8', N'11', N'6453', N'1', N'-1171.45', N'-5263.65', N'0.847728', N'5.78945')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'1', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'3', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'4', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'5', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'7', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'8', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'9', N'9', N'4737', N'648', N'-8423.81', N'1361.3', N'104.671', N'1.55428')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'1', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'2', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'3', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'4', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'5', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'8', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'9', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'10', N'10', N'6455', N'530', N'10349.6', N'-6357.29', N'33.4026', N'5.31605')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'1', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'2', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'3', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'5', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'7', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'8', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'11', N'10', N'6456', N'530', N'-3961.64', N'-13931.2', N'100.615', N'2.08364')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'1', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'3', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'4', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'5', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'6', N'4298', N'609', N'2355.84', N'-5664.77', N'426.028', N'3.65997')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'8', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'9', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'22', N'11', N'4755', N'654', N'-1451.53', N'1403.35', N'35.5561', N'0.333847')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'1', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'3', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'4', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'5', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'7', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'8', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO
INSERT INTO [dbo].[CharacterCreationData] ([Race], [Class], [Zone], [Map], [X], [Y], [Z], [O]) VALUES (N'24', N'10', N'5736', N'860', N'1466.09', N'3465.98', N'181.86', N'2.87962')
GO
GO

-- ----------------------------
-- Table structure for CharacterCreationSkills
-- ----------------------------
DROP TABLE [dbo].[CharacterCreationSkills]
GO
CREATE TABLE [dbo].[CharacterCreationSkills] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[SkillId] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterCreationSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterCreationSpells
-- ----------------------------
DROP TABLE [dbo].[CharacterCreationSpells]
GO
CREATE TABLE [dbo].[CharacterCreationSpells] (
[Race] tinyint NOT NULL ,
[Class] tinyint NOT NULL ,
[SpellId] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterCreationSpells
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterItems
-- ----------------------------
DROP TABLE [dbo].[CharacterItems]
GO
CREATE TABLE [dbo].[CharacterItems] (
[CharacterGuid] bigint NOT NULL ,
[ItemId] int NOT NULL DEFAULT ((0)) ,
[Bag] tinyint NOT NULL DEFAULT ((255)) ,
[Slot] tinyint NOT NULL ,
[Mode] tinyint NOT NULL DEFAULT ((0)) ,
[Equipped] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterItems
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterSkills
-- ----------------------------
DROP TABLE [dbo].[CharacterSkills]
GO
CREATE TABLE [dbo].[CharacterSkills] (
[CharacterGuid] bigint NOT NULL ,
[SkillId] int NOT NULL DEFAULT ((0)) ,
[SkillLevel] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterSpells
-- ----------------------------
DROP TABLE [dbo].[CharacterSpells]
GO
CREATE TABLE [dbo].[CharacterSpells] (
[CharacterGuid] bigint NOT NULL ,
[SpellId] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterSpells
-- ----------------------------

-- ----------------------------
-- Table structure for Characters
-- ----------------------------
DROP TABLE [dbo].[Characters]
GO
CREATE TABLE [dbo].[Characters] (
[Guid] bigint NOT NULL IDENTITY(1,1) ,
[GameAccountId] int NOT NULL DEFAULT ((0)) ,
[RealmId] int NOT NULL DEFAULT ((0)) ,
[Name] varchar(192) NOT NULL DEFAULT '' ,
[ListPosition] tinyint NOT NULL DEFAULT ((0)) ,
[Race] tinyint NOT NULL DEFAULT ((0)) ,
[Class] tinyint NOT NULL DEFAULT ((0)) ,
[Sex] tinyint NOT NULL DEFAULT ((0)) ,
[Skin] tinyint NOT NULL DEFAULT ((0)) ,
[Face] tinyint NOT NULL DEFAULT ((0)) ,
[HairStyle] tinyint NOT NULL DEFAULT ((0)) ,
[HairColor] tinyint NOT NULL DEFAULT ((0)) ,
[FacialHairStyle] tinyint NOT NULL DEFAULT ((0)) ,
[Experience] int NOT NULL DEFAULT ((0)) ,
[Zone] int NOT NULL DEFAULT ((0)) ,
[Map] int NOT NULL DEFAULT ((0)) ,
[X] real NOT NULL DEFAULT ((0)) ,
[Y] real NOT NULL DEFAULT ((0)) ,
[Z] real NOT NULL DEFAULT ((0)) ,
[O] real NOT NULL DEFAULT ((0)) ,
[GuildGuid] bigint NOT NULL DEFAULT ((0)) ,
[CharacterFlags] int NOT NULL DEFAULT ((0)) ,
[CustomizeFlags] int NOT NULL DEFAULT ((0)) ,
[Flags3] int NOT NULL DEFAULT ((0)) ,
[FirstLogin] tinyint NOT NULL DEFAULT ((1)) ,
[PetCreatureDisplayId] int NOT NULL DEFAULT ((0)) ,
[PetLevel] int NOT NULL DEFAULT ((0)) ,
[PetCreatureFamily] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of Characters
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Characters] ON
GO
SET IDENTITY_INSERT [dbo].[Characters] OFF
GO

-- ----------------------------
-- Table structure for CharacterTemplateActions
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateActions]
GO
CREATE TABLE [dbo].[CharacterTemplateActions] (
[ClassId] int NOT NULL ,
[Action] int NOT NULL ,
[Slot] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterTemplateActions
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateClasses
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateClasses]
GO
CREATE TABLE [dbo].[CharacterTemplateClasses] (
[ClassId] int NOT NULL IDENTITY(1,1) ,
[SetId] int NOT NULL ,
[FactionGroup] tinyint NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterTemplateClasses
-- ----------------------------
SET IDENTITY_INSERT [dbo].[CharacterTemplateClasses] ON
GO
SET IDENTITY_INSERT [dbo].[CharacterTemplateClasses] OFF
GO

-- ----------------------------
-- Table structure for CharacterTemplateData
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateData]
GO
CREATE TABLE [dbo].[CharacterTemplateData] (
[ClassId] int NOT NULL ,
[Map] smallint NOT NULL ,
[Zone] smallint NOT NULL ,
[X] real NOT NULL ,
[Y] real NOT NULL ,
[Z] real NOT NULL ,
[O] real NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterTemplateData
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateItems
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateItems]
GO
CREATE TABLE [dbo].[CharacterTemplateItems] (
[ItemId] int NOT NULL DEFAULT ((0)) ,
[ClassId] int NOT NULL ,
[IsEquipped] tinyint NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of CharacterTemplateItems
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateSets
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateSets]
GO
CREATE TABLE [dbo].[CharacterTemplateSets] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Name] varchar(255) NULL DEFAULT '' ,
[Description] varchar(255) NULL DEFAULT '' 
)


GO

-- ----------------------------
-- Records of CharacterTemplateSets
-- ----------------------------
SET IDENTITY_INSERT [dbo].[CharacterTemplateSets] ON
GO
SET IDENTITY_INSERT [dbo].[CharacterTemplateSets] OFF
GO

-- ----------------------------
-- Table structure for CharacterTemplateSkills
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateSkills]
GO
CREATE TABLE [dbo].[CharacterTemplateSkills] (
[SkillId] int NOT NULL ,
[ClassId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterTemplateSkills
-- ----------------------------

-- ----------------------------
-- Table structure for CharacterTemplateSpells
-- ----------------------------
DROP TABLE [dbo].[CharacterTemplateSpells]
GO
CREATE TABLE [dbo].[CharacterTemplateSpells] (
[SpellId] int NOT NULL ,
[ClassId] int NOT NULL 
)


GO

-- ----------------------------
-- Records of CharacterTemplateSpells
-- ----------------------------

-- ----------------------------
-- Indexes structure for table CharacterActions
-- ----------------------------
CREATE UNIQUE INDEX [CharacterGuid] ON [dbo].[CharacterActions]
([CharacterGuid] ASC, [Action] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterCreationActions
-- ----------------------------
CREATE UNIQUE INDEX [Race] ON [dbo].[CharacterCreationActions]
([Race] ASC, [Class] ASC, [Action] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterCreationData
-- ----------------------------
CREATE UNIQUE INDEX [Race] ON [dbo].[CharacterCreationData]
([Race] ASC, [Class] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterCreationSkills
-- ----------------------------
CREATE UNIQUE INDEX [Race] ON [dbo].[CharacterCreationSkills]
([Race] ASC, [Class] ASC, [SkillId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterCreationSpells
-- ----------------------------
CREATE UNIQUE INDEX [Race] ON [dbo].[CharacterCreationSpells]
([Race] ASC, [Class] ASC, [SpellId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterItems
-- ----------------------------
CREATE UNIQUE INDEX [CharacterGuid] ON [dbo].[CharacterItems]
([CharacterGuid] ASC, [ItemId] ASC, [Slot] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table Characters
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Characters
-- ----------------------------
ALTER TABLE [dbo].[Characters] ADD PRIMARY KEY ([Guid])
GO

-- ----------------------------
-- Indexes structure for table CharacterSkills
-- ----------------------------
CREATE UNIQUE INDEX [CharacterGuid] ON [dbo].[CharacterSkills]
([CharacterGuid] ASC, [SkillId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterSpells
-- ----------------------------
CREATE UNIQUE INDEX [CharacterGuid] ON [dbo].[CharacterSpells]
([CharacterGuid] ASC, [SpellId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateActions
-- ----------------------------
CREATE UNIQUE INDEX [ClassId] ON [dbo].[CharacterTemplateActions]
([ClassId] ASC, [Action] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [Class] ON [dbo].[CharacterTemplateActions]
([ClassId] ASC) 
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateClasses
-- ----------------------------
CREATE UNIQUE INDEX [ClassId] ON [dbo].[CharacterTemplateClasses]
([ClassId] ASC, [SetId] ASC, [FactionGroup] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [Id] ON [dbo].[CharacterTemplateClasses]
([ClassId] ASC) 
GO
CREATE INDEX [SetId] ON [dbo].[CharacterTemplateClasses]
([SetId] ASC) 
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateData
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table CharacterTemplateData
-- ----------------------------
ALTER TABLE [dbo].[CharacterTemplateData] ADD PRIMARY KEY ([ClassId])
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateItems
-- ----------------------------
CREATE UNIQUE INDEX [ItemId] ON [dbo].[CharacterTemplateItems]
([ItemId] ASC, [ClassId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [ClassId] ON [dbo].[CharacterTemplateItems]
([ClassId] ASC) 
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateSets
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table CharacterTemplateSets
-- ----------------------------
ALTER TABLE [dbo].[CharacterTemplateSets] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateSkills
-- ----------------------------
CREATE UNIQUE INDEX [SkillId] ON [dbo].[CharacterTemplateSkills]
([SkillId] ASC, [ClassId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [ClassId] ON [dbo].[CharacterTemplateSkills]
([ClassId] ASC) 
GO

-- ----------------------------
-- Indexes structure for table CharacterTemplateSpells
-- ----------------------------
CREATE UNIQUE INDEX [SpellId] ON [dbo].[CharacterTemplateSpells]
([SpellId] ASC, [ClassId] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [ClassId] ON [dbo].[CharacterTemplateSpells]
([ClassId] ASC) 
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[CharacterActions]
-- ----------------------------
ALTER TABLE [dbo].[CharacterActions] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[Characters] ([Guid]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[CharacterItems]
-- ----------------------------
ALTER TABLE [dbo].[CharacterItems] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[Characters] ([Guid]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[CharacterSkills]
-- ----------------------------
ALTER TABLE [dbo].[CharacterSkills] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[Characters] ([Guid]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[CharacterSpells]
-- ----------------------------
ALTER TABLE [dbo].[CharacterSpells] ADD FOREIGN KEY ([CharacterGuid]) REFERENCES [dbo].[Characters] ([Guid]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
