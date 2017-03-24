
CREATE TABLE [dbo].[Images](
	[ImageID] [bigint]  NOT NULL,
	[ImageName] [nvarchar](255) NULL,
	[ImageSize] bigint NULL,
	[ImageType] [varchar](50) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[ImageData] [varbinary](max) NULL,
	[SequenceNumber] [int] NOT NULL,
	[ActivityNumber] [bigint] NULL,
	[SortOrder] [int] NULL,
	[IsEnable] [bit] NULL,
	[Created] [datetime] NULL,
	[LastUpdated] [datetime] NULL,
	[Remark] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
	[CreatedBy] [bigint] NULL,
	[LastUpdatedBy] [bigint] NULL,

  CONSTRAINT [PK_Images] PRIMARY KEY NONCLUSTERED 
(
	[ImageID] ASC,
	[SequenceNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



--UPDATE [dbo].[Images]
--   SET 
--       [SortOrder] = '1'
--      ,[IsEnable] = '1'
--      ,[Created] = getdate()
--      ,[LastUpdated] = getdate()
--      ,[Remark] = '--'
--      ,[IsDelete] = '0'
--      ,[CreatedBy] = '1'
--      ,[LastUpdatedBy] = '1'
--GO