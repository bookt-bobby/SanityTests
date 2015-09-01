
/****  This files contains the four necessary tables necessary for the Sanity Tests schems.  The are the company 
		1) Company
		2) Suite
		3) Test
		4) SuiteTest (a composite table so that a test could be part of multiple suites)
	
***/


/*****************************************/
/****** Object:  Table [dbo].[Company]    Script Date: 8/29/2015 6:35:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Company](
	[Id] [uniqueidentifier] NOT NULL,
	[Company] [varchar](100) NULL,
	[PrimaryEmail] [varchar](100) NULL,
	[Status] [int] NULL,
	[BaseUrl] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/**********************************************/
/****** Object:  Table [dbo].[Suite]    Script Date: 8/29/2015 6:31:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Suite](
	[Id] [uniqueidentifier] NOT NULL,
	[TestSuite] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[AddedOn] [datetime] NULL,
	[Description] [nvarchar](400) NULL,
	[CompanyId] [uniqueidentifier] NULL,
	[SortOrder] [int] IDENTITY(1,1) NOT NULL,
	[SuiteId] [uniqueidentifier] NULL,
	[TestInOrder] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


/***************************************/
/****** Object:  Table [dbo].[Test]    Script Date: 8/29/2015 6:33:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Test](
	[Id] [uniqueidentifier] NOT NULL,
	[Test] [varchar](100) NULL,
	[ContentType] [varchar](50) NULL,
	[PostData] [text] NULL,
	[Property] [varchar](400) NULL,
	[Value] [text] NULL,
	[CreatedBy] [varchar](50) NULL,
	[IsGlobal] [bit] NULL,
	[Method] [varchar](50) NULL,
	[CompanyID] [uniqueidentifier] NULL,
	[Url] [varchar](400) NULL,
	[Description] [varchar](5000) NULL,
	[ActualValue] [text] NULL,
	[ErrorMessagePath] [varchar](400) NULL,
	[ReturnParamName] [varchar](100) NULL,
	[ReturnParamPath] [varchar](300) NULL,
	[PauseFirst] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/**************************************************/
/****** Object:  Table [dbo].[SuitTest]    Script Date: 8/29/2015 6:35:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SuitTest](
	[Id] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NULL,
	[SuiteId] [uniqueidentifier] NULL,
	[Order] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

