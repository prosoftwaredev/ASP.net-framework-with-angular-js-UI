SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](max) NULL,
	[CompanyName] [nvarchar](max) NULL,
	[AccountNo] [nvarchar](max) NULL,
	[SalesRepID] [bigint] NULL,
	[PaymentTermID] [bigint] NULL,
	[RequirePO] [bit] NOT NULL,
	[UniquePO] [bit] NOT NULL,
	[PayByCC] [bit] NOT NULL,
	[Delinquent] [bit] NOT NULL,
	[InvoiceDiscountMessage] [bit] NOT NULL,
	[HideCustomerName] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO