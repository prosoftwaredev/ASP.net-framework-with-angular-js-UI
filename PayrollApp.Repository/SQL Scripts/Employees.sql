SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [bigint] IDENTITY(1,1) NOT NULL,
	[CityID] [bigint] NULL,
	[PayFrequencyID] [bigint] NULL,
	[TitleID] [bigint] NOT NULL,
	[FirstName] [nvarchar](25) NULL,
	[MiddleName] [nvarchar](2) NULL,
	[LastName] [nvarchar](25) NULL,
	[PrintName] [nvarchar](99) NULL,
	[AccountNo] [nvarchar](99) NULL,
	[EmailMain] [nvarchar](99) NULL,
	[EmailCC] [nvarchar](99) NULL,
	[Website] [nvarchar](99) NULL,
	[Other] [nvarchar](255) NULL,
	[Phone] [nvarchar](21) NULL,
	[Mobile] [nvarchar](21) NULL,
	[Fax] [nvarchar](21) NULL,
	[Address1] [nvarchar](41) NULL,
	[Address2] [nvarchar](41) NULL,
	[PostalCode] [nvarchar](13) NULL,
	[NextOfKin] [nvarchar](99) NULL,
	[NextOfKinContact] [nvarchar](99) NULL,
	[SIN] [nvarchar](15) NULL,
	[DOB] [datetime] NULL,
	[Gender] [nvarchar](max) NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Withholding] [decimal](18, 2) NOT NULL,
	[Dormant] [decimal](18, 2) NOT NULL,
	[PayStubs] [datetime] NULL,
	[Garnishee] [bit] NOT NULL,
	[IsNeverUse] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO