SET IDENTITY_INSERT [dbo].[companyTypes] ON
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (1, N'Գերփոքր ընկերություններում (մինչև 10 աշխատող)', 1)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (2, N'Փոքր ընկերություններում (մինչև 50 աշխատող)', 1)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (3, N'Միջին և խոշոր ընկերություններում ( 50-ից ավել աշխատող)', 1)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (4, N'Ուսումնական և կրթական հաստատություններ', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (5, N'Մշակույթային հաստատություններ', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (6, N'Կոմունալ ծառայություններ', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (7, N'Թաղապետարանների / քաղապետարանի աշխատակազմ', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (8, N'Նախարարությունների և կից գերատեսչությունների աշխատակազմ', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (9, N'Ուժային կառույցներ /ՆԳՆ, ՊՆ և  ՊՊԾ/', 2)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (10, N'Անհատ ձեռնարկատեր` մինչև 3 տարի գործունեությամբ', 3)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (11, N'Անհատ ձեռնարկատեր`  3 տարուց ավել գործունեությամբ', 3)
INSERT INTO [dbo].[companyTypes] ([companyTypeID], [companyTypeName], [FK_empType]) VALUES (12, N'Գյուղատնտես', 3)
SET IDENTITY_INSERT [dbo].[companyTypes] OFF
