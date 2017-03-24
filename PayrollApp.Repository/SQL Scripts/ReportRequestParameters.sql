SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportRequestParameters](
	[ReportRequestParameterId] [int] IDENTITY(1,1) NOT NULL,
	[ReportRequestId] [int] NOT NULL,
	[ParameterValue] [nvarchar](max) NULL,
	[ReportParameterName] [nvarchar](max) NULL,
	[ParameterViewName] [nvarchar](max) NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_dbo.ReportRequestParameters] PRIMARY KEY CLUSTERED 
(
	[ReportRequestParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO