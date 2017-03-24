SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[CustomerSiteID] [bigint] NOT NULL,
	[LabourClassificationID] [bigint] NOT NULL,
	[CustomerSiteJobLocationID] [bigint] NOT NULL,
	[PONumber] [nvarchar](max) NULL,
	[ContactName] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[People] [int] NOT NULL,
	[Reporting] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[DispatchNote] [nvarchar](max) NULL,
	[JobDuration] [int] NOT NULL,
	[OTPerDay] [nvarchar](max) NULL,
	[OTPerWeek] [nvarchar](max) NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
	[WorkStartRsv] [datetime] NULL,
	[StartTimeRsv] [time](7) NULL,
	[WorkEndRsv] [datetime] NULL,
	[EndTimeRsv] [time](7) NULL,
	[WorkStartCust] [datetime] NULL,
	[StartTimeCust] [time](7) NULL,
	[WorkEndCust] [datetime] NULL,
	[EndTimeCust] [time](7) NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO