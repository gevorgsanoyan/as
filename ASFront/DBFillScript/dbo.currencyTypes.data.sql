SET IDENTITY_INSERT [dbo].[currencyTypes] ON
INSERT INTO [dbo].[currencyTypes] ([currencyTypesId], [currencyArm], [currencyEng], [exchCode], [Sign]) VALUES (1, N'ՀՀ Դրամ', N'Armenian Dram', N'AMD', N'֏')
INSERT INTO [dbo].[currencyTypes] ([currencyTypesId], [currencyArm], [currencyEng], [exchCode], [Sign]) VALUES (2, N'ԱՄՆ Դոլար', N'US Dollar', N'USD', N'$')
INSERT INTO [dbo].[currencyTypes] ([currencyTypesId], [currencyArm], [currencyEng], [exchCode], [Sign]) VALUES (3, N'ՌԴ Ռուբլի', N'RF Rubli', N'RUR', N'₽')
INSERT INTO [dbo].[currencyTypes] ([currencyTypesId], [currencyArm], [currencyEng], [exchCode], [Sign]) VALUES (4, N'Եվրո', N'Euro', N'EUR', N'€')
SET IDENTITY_INSERT [dbo].[currencyTypes] OFF
