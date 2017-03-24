SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerSites](
	[CustomerSiteID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[AccountNo] [nvarchar](max) NULL,
	[SiteName] [nvarchar](max) NULL,
	[SiteDescription] [nvarchar](max) NULL,
	[PrContactName] [nvarchar](max) NULL,
	[PrAddress1] [nvarchar](max) NULL,
	[PrAddress2] [nvarchar](max) NULL,
	[PrCityID] [bigint] NULL,
	[PrPostalCode] [nvarchar](max) NULL,
	[PrMobile] [nvarchar](max) NULL,
	[PrPhone] [nvarchar](max) NULL,
	[PrFax] [nvarchar](max) NULL,
	[PrEmail] [nvarchar](max) NULL,
	[InContactName] [nvarchar](max) NULL,
	[InAddress1] [nvarchar](max) NULL,
	[InAddress2] [nvarchar](max) NULL,
	[InCityID] [bigint] NULL,
	[InPostalCode] [nvarchar](max) NULL,
	[InMobile] [nvarchar](max) NULL,
	[InPhone] [nvarchar](max) NULL,
	[InFax] [nvarchar](max) NULL,
	[InEmail] [nvarchar](max) NULL,
	[InvoiceViaMail] [bit] NOT NULL,
	[InvoiceViaFax] [bit] NOT NULL,
	[InvoiceViaEmail] [bit] NOT NULL,
	[InvoiceAutomatically] [bit] NOT NULL,
	[InvoiceCombine] [bit] NOT NULL,
	[CertificateNo] [nvarchar](max) NULL,
	[OTPerDay] [nvarchar](max) NULL,
	[OTPerWeek] [nvarchar](max) NULL,
	[TimeslipMsg] [nvarchar](max) NULL,
	[Reminder] [nvarchar](max) NULL,
	[IsPrimary] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.CustomerSites] PRIMARY KEY CLUSTERED 
(
	[CustomerSiteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO