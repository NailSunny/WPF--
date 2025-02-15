USE [Cactus_Project]
GO
/****** Object:  Table [dbo].[Cactus]    Script Date: 12.09.2024 21:55:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cactus](
	[Id_cactus] [int] IDENTITY(1,1) NOT NULL,
	[Name_cactus] [nvarchar](50) NULL,
	[Proishogdenie] [nvarchar](50) NULL,
	[Vozrast] [int] NULL,
	[Price] [int] NULL,
	[Instruction] [nvarchar](max) NULL,
	[Id_vid] [int] NULL,
 CONSTRAINT [PK_Cactus] PRIMARY KEY CLUSTERED 
(
	[Id_cactus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cactus_Vistavka]    Script Date: 12.09.2024 21:55:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cactus_Vistavka](
	[Id_CV] [int] IDENTITY(1,1) NOT NULL,
	[Id_cactus] [int] NULL,
	[Id_vistavka] [int] NULL,
	[Nagrada] [nvarchar](50) NULL,
 CONSTRAINT [PK_Cactus_Vistavka] PRIMARY KEY CLUSTERED 
(
	[Id_CV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vid]    Script Date: 12.09.2024 21:55:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vid](
	[Id_vid] [int] IDENTITY(1,1) NOT NULL,
	[Name_vid] [nvarchar](50) NULL,
 CONSTRAINT [PK_Vid] PRIMARY KEY CLUSTERED 
(
	[Id_vid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vistavka]    Script Date: 12.09.2024 21:55:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vistavka](
	[Id_vistavka] [int] IDENTITY(1,1) NOT NULL,
	[Mesto_provedeniya] [nvarchar](50) NULL,
	[Comment] [nvarchar](max) NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Vistavka] PRIMARY KEY CLUSTERED 
(
	[Id_vistavka] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cactus] ON 

INSERT [dbo].[Cactus] ([Id_cactus], [Name_cactus], [Proishogdenie], [Vozrast], [Price], [Instruction], [Id_vid]) VALUES (4, N'trgegerg', N'evfdfverg', 21, 12334, N'weefergerwgr', 1)
INSERT [dbo].[Cactus] ([Id_cactus], [Name_cactus], [Proishogdenie], [Vozrast], [Price], [Instruction], [Id_vid]) VALUES (5, N'wefwefrgerwrgw', N'wr3r32geer', 21, 34234, N'rrnyhrs oi;ddenhvtg yernufrehbgbreighvegeyrferu', 2)
SET IDENTITY_INSERT [dbo].[Cactus] OFF
GO
SET IDENTITY_INSERT [dbo].[Vid] ON 

INSERT [dbo].[Vid] ([Id_vid], [Name_vid]) VALUES (1, N'Кактусовые')
INSERT [dbo].[Vid] ([Id_vid], [Name_vid]) VALUES (2, N'Опунциевые')
INSERT [dbo].[Vid] ([Id_vid], [Name_vid]) VALUES (3, N'Маихуениевые')
INSERT [dbo].[Vid] ([Id_vid], [Name_vid]) VALUES (4, N'Перескиевые')
SET IDENTITY_INSERT [dbo].[Vid] OFF
GO
ALTER TABLE [dbo].[Cactus]  WITH CHECK ADD  CONSTRAINT [FK_Cactus_Vid] FOREIGN KEY([Id_vid])
REFERENCES [dbo].[Vid] ([Id_vid])
GO
ALTER TABLE [dbo].[Cactus] CHECK CONSTRAINT [FK_Cactus_Vid]
GO
ALTER TABLE [dbo].[Cactus_Vistavka]  WITH CHECK ADD  CONSTRAINT [FK_Cactus_Vistavka_Cactus] FOREIGN KEY([Id_cactus])
REFERENCES [dbo].[Cactus] ([Id_cactus])
GO
ALTER TABLE [dbo].[Cactus_Vistavka] CHECK CONSTRAINT [FK_Cactus_Vistavka_Cactus]
GO
ALTER TABLE [dbo].[Cactus_Vistavka]  WITH CHECK ADD  CONSTRAINT [FK_Cactus_Vistavka_Vistavka] FOREIGN KEY([Id_vistavka])
REFERENCES [dbo].[Vistavka] ([Id_vistavka])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cactus_Vistavka] CHECK CONSTRAINT [FK_Cactus_Vistavka_Vistavka]
GO
