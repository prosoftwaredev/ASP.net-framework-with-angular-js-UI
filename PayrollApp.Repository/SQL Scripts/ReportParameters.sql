SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportParameters](
	[ReportParameterId] [int] IDENTITY(1,1) NOT NULL,
	[ReportId] [int] NOT NULL,
	[ReportParameterName] [nvarchar](max) NULL,
	[ParameterName] [nvarchar](max) NULL,
	[ParameterViewName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ReportParameters] PRIMARY KEY CLUSTERED 
(
	[ReportParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO