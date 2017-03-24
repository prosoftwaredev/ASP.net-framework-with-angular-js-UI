SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTimeslips](
	[OrderTimeslipID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[Customer_CustomerID] [bigint] NULL,
	[EmployeeID] [bigint] NULL,
	[PayRate] [decimal](18, 2) NOT NULL,
	[InvoiceRate] [decimal](18, 2) NOT NULL,
	[HrsReg] [int] NOT NULL,
	[HrsOT] [int] NOT NULL,
	[HrsTotal] [int] NOT NULL,
	[GrossPay] [decimal](18, 2) NOT NULL,
	[WithHolding] [decimal](18, 2) NOT NULL,
	[NetPay] [decimal](18, 2) NOT NULL,
	[ItemsDue] [decimal](18, 2) NOT NULL,
	[Advances] [decimal](18, 2) NOT NULL,
	[CreditBalance] [decimal](18, 2) NOT NULL,
	[BalanceFwd] [decimal](18, 2) NOT NULL,
	[NetPay1] [decimal](18, 2) NOT NULL,
	[ReturnItems] [decimal](18, 2) NOT NULL,
	[AvailableBalance] [decimal](18, 2) NOT NULL,
	[PayNow] [decimal](18, 2) NOT NULL,
	[ClosingBalance] [decimal](18, 2) NOT NULL,
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
	[WeekStart] [datetime] NULL,
	[WeekEnd] [datetime] NULL,
	[LabourClassificationID] [bigint] NULL,
	[CustomerSiteJobLocationID] [bigint] NULL,
	[Phone] [nvarchar](max) NULL,
	[Reporting] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[DispatchNote] [nvarchar](max) NULL,
	[RollOver] [bit] NOT NULL,
	[RollOverStart] [datetime] NULL,
	[RollOverTime] [time](7) NULL,
	[Stat] [nvarchar](max) NULL,
	[Roll] [nvarchar](max) NULL,
	[Week] [nvarchar](max) NULL,
	[IsOneDay] [bit] NOT NULL,
	[DayOfWeek] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[BillState] [int] NOT NULL,
	[XmlNote] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.OrderTimeslips] PRIMARY KEY CLUSTERED 
(
	[OrderTimeslipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO