USE [BetterWork]
GO
/****** Object:  StoredProcedure [dbo].[spa_BW_GetPermissionListByUserId]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***************************************************************************************************************************  
* PROCEDURE:  spa_BW_GetPermissionListByUserId  
* PURPOSE:    Get Permission of user
* NOTES:      N/A  
*  
* Modification History - Use spaces, do not use TABS.  Latest change goes on top  
*  
* DATE        VERSION   TICKET    AUTHOR              DESCRIPTION  
*---------------------------------------------------------------------------------------------------------------------------  
* 18/12/2015  1.00                LY PHAM			  CREATE NEW
***************************************************************************************************************************/  
  
CREATE PROCEDURE [dbo].[spa_BW_GetPermissionListByUserId]
	@UserId int  
AS
BEGIN
	SET NOCOUNT ON;
	SELECT M.ModuleId, M.ModuleName, M.ModuleCode
	, CASE WHEN PView = 1 THEN 'View,' ELSE '' END
	+ CASE WHEN PAdd = 1 THEN 'Add,' ELSE '' END 
	+ CASE WHEN PEdit = 1 THEN 'Edit,' ELSE '' END 
	+ CASE WHEN PDelete = 1 THEN 'Delete,' ELSE '' END AS PermissionCode
	FROM BW_User U WITH(NOLOCK)
	 INNER JOIN BW_UserInRole UiR WITH(NOLOCK) ON U.UserId = UiR.UserId
	 INNER JOIN BW_RoleInPermission RiP WITH(NOLOCK) ON RiP.RoleId = UiR.RoleId
	 INNER JOIN BW_Module M WITH(NOLOCK) ON M.ModuleId = RiP.ModuleId
	WHERE U.UserId = @UserId
END

/* Examples  
Exec spa_BW_GetPermissionListByUserId  4
*/

GO
/****** Object:  Table [dbo].[BW_Module]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BW_Module](
	[ModuleId] [int] NOT NULL,
	[ModuleName] [varchar](255) NOT NULL,
	[ModuleCode] [varchar](255) NULL,
	[ModuleDescription] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BW_Module] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BW_Role]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BW_Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](255) NOT NULL,
	[RoleDescription] [nvarchar](1000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BW_RoleInPermission]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BW_RoleInPermission](
	[RoleInPermissionId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[PAdd] [bit] NOT NULL,
	[PEdit] [bit] NOT NULL,
	[PDelete] [bit] NOT NULL,
	[PView] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_BW_RoleInPermission_1] PRIMARY KEY CLUSTERED 
(
	[RoleInPermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BW_User]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BW_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BW_UserInRole]    Script Date: 31/12/2015 1:48:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BW_UserInRole](
	[UserInRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_BW_UserInRole] PRIMARY KEY CLUSTERED 
(
	[UserInRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[BW_Module] ([ModuleId], [ModuleName], [ModuleCode], [ModuleDescription], [IsActive], [CreatedDate]) VALUES (1, N'Manage User', N'ManageUser', N'Manage users account', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_Module] ([ModuleId], [ModuleName], [ModuleCode], [ModuleDescription], [IsActive], [CreatedDate]) VALUES (2, N'Manage Role', N'ManageRole', N'manage role of system', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_Module] ([ModuleId], [ModuleName], [ModuleCode], [ModuleDescription], [IsActive], [CreatedDate]) VALUES (3, N'Manage RoleInPermission', N'ManageRoleInPermission', N'manage role in permmision', 1, CAST(N'2015-12-28 00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[BW_Role] ON 

INSERT [dbo].[BW_Role] ([RoleId], [RoleName], [RoleDescription], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (1, N'admin', N'admin of system', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[BW_Role] ([RoleId], [RoleName], [RoleDescription], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (2, N'moderator', N'moderator of system', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[BW_Role] ([RoleId], [RoleName], [RoleDescription], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (3, N'user', N'user in ystem', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[BW_Role] ([RoleId], [RoleName], [RoleDescription], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (4, N'IT', N'IT of system', 0, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-29 12:01:26.080' AS DateTime))
SET IDENTITY_INSERT [dbo].[BW_Role] OFF
SET IDENTITY_INSERT [dbo].[BW_RoleInPermission] ON 

INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (1, 1, 1, 1, 1, 1, 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:17:31.620' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (2, 1, 2, 0, 0, 0, 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:15:16.123' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (3, 1, 3, 0, 0, 0, 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 09:07:19.513' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (4, 1, 4, 0, 0, 0, 0, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:22:17.300' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (5, 2, 1, 0, 0, 0, 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:17:31.650' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (6, 2, 2, 0, 0, 0, 0, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:15:16.160' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (7, 2, 3, 0, 0, 0, 0, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 09:07:19.567' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (8, 2, 4, 0, 0, 0, 0, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:22:17.330' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (9, 3, 1, 1, 1, 0, 1, CAST(N'2015-12-28 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:17:31.683' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (13, 3, 2, 0, 0, 0, 0, CAST(N'2015-12-28 00:00:00.000' AS DateTime), CAST(N'2015-12-31 11:15:16.193' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (14, 3, 3, 0, 0, 0, 0, CAST(N'2015-12-28 00:00:00.000' AS DateTime), CAST(N'2015-12-31 09:07:19.617' AS DateTime))
INSERT [dbo].[BW_RoleInPermission] ([RoleInPermissionId], [ModuleId], [RoleId], [PAdd], [PEdit], [PDelete], [PView], [CreatedDate], [UpdatedDate]) VALUES (15, 3, 4, 0, 0, 0, 0, CAST(N'2015-12-28 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:22:17.360' AS DateTime))
SET IDENTITY_INSERT [dbo].[BW_RoleInPermission] OFF
SET IDENTITY_INSERT [dbo].[BW_User] ON 

INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (1, N'System ', N'Admin', N'admin@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:15:37.713' AS DateTime))
INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (2, N'System', N'Mod', N'mod@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:21:26.693' AS DateTime))
INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (3, N'1', N'user', N'user1@gmail.com', N'c4ca4238a0b923820dcc509a6f75849b', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-31 10:19:18.717' AS DateTime))
INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (4, N'2', N'user', N'user2@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', 1, CAST(N'2015-12-17 00:00:00.000' AS DateTime), CAST(N'2015-12-30 16:38:19.910' AS DateTime))
INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (6, N'Than Van', N'Tam', N'tamtv@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', 0, CAST(N'2015-12-28 14:15:08.300' AS DateTime), CAST(N'2015-12-31 10:15:14.427' AS DateTime))
INSERT [dbo].[BW_User] ([UserId], [FirstName], [LastName], [Email], [Password], [IsActive], [CreatedDate], [UpdatedDate]) VALUES (9, N'ly', N'pham', N'lypham22@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', 1, CAST(N'2015-12-31 11:08:07.557' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[BW_User] OFF
SET IDENTITY_INSERT [dbo].[BW_UserInRole] ON 

INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (50, 1, 1, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (51, 1, 2, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (52, 1, 3, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (53, 1, 4, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (57, 3, 3, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (58, 4, 4, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (62, 6, 1, CAST(N'2015-12-31 00:00:00.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (63, 2, 2, CAST(N'2015-12-31 10:21:26.000' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (66, 9, 1, CAST(N'2015-12-31 11:08:07.570' AS DateTime))
INSERT [dbo].[BW_UserInRole] ([UserInRoleId], [UserId], [RoleId], [CreatedDate]) VALUES (67, 9, 2, CAST(N'2015-12-31 11:08:07.580' AS DateTime))
SET IDENTITY_INSERT [dbo].[BW_UserInRole] OFF
ALTER TABLE [dbo].[BW_RoleInPermission]  WITH CHECK ADD  CONSTRAINT [FK_BW_RoleInPermission_BW_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[BW_Module] ([ModuleId])
GO
ALTER TABLE [dbo].[BW_RoleInPermission] CHECK CONSTRAINT [FK_BW_RoleInPermission_BW_Module]
GO
ALTER TABLE [dbo].[BW_RoleInPermission]  WITH CHECK ADD  CONSTRAINT [FK_BW_RoleInPermission_BW_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[BW_Role] ([RoleId])
GO
ALTER TABLE [dbo].[BW_RoleInPermission] CHECK CONSTRAINT [FK_BW_RoleInPermission_BW_Role]
GO
ALTER TABLE [dbo].[BW_UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_BW_UserInRole_BW_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[BW_Role] ([RoleId])
GO
ALTER TABLE [dbo].[BW_UserInRole] CHECK CONSTRAINT [FK_BW_UserInRole_BW_Role]
GO
ALTER TABLE [dbo].[BW_UserInRole]  WITH CHECK ADD  CONSTRAINT [FK_BW_UserInRole_BW_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[BW_User] ([UserId])
GO
ALTER TABLE [dbo].[BW_UserInRole] CHECK CONSTRAINT [FK_BW_UserInRole_BW_User]
GO
