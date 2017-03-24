﻿SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSiteLabourClassifications](
	[CustomerSiteLabourClassificationID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerSiteID] [bigint] NOT NULL,
	[LabourClassificationID] [bigint] NOT NULL,
	[IsExpire] [bit] NOT NULL,
	[PayRate] [decimal](18, 2) NOT NULL,
	[InvoiceRate] [decimal](18, 2) NOT NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.CustomerSiteLabourClassifications] PRIMARY KEY CLUSTERED 
(
	[CustomerSiteLabourClassificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO