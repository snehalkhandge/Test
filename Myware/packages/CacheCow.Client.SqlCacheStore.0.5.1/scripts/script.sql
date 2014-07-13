
/****** Object:  Table [dbo].[CacheState]    Script Date: 07/25/2012 13:38:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cache]') AND type in (N'U'))
DROP TABLE [dbo].[Cache]
GO


/****** Object:  Table [dbo].[CacheState]    Script Date: 07/25/2012 13:38:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Cache](
	[CacheKeyHash] [binary](20) NOT NULL,
	[Domain] [varchar](256) NOT NULL,
	[Size] [int] NOT NULL,
	[CacheBlob] [varbinary](max) NOT NULL,
	[LastAccessed] [datetime] NOT NULL,
 CONSTRAINT [PK_Cache] PRIMARY KEY CLUSTERED 
(
	[CacheKeyHash] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_Cache_Domain_LastAccessed] ON [dbo].[Cache] 
(
	[Domain] ASC,
	[LastAccessed] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Cache_Domain_Size] ON [dbo].[Cache] 
(
	[Domain] ASC,
	[Size] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

SET ANSI_PADDING OFF
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUpdateCache]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].AddUpdateCache
GO

-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-12-12
-- Description:	Adds or updates cache entry
-- =============================================
CREATE PROCEDURE AddUpdateCache
	@cacheKeyHash	BINARY(20),
	@Domain			NVARCHAR(256),
	@Size			INT,
	@CacheBlob		VARBINARY(MAX)	
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	BEGIN TRAN
	IF EXISTS (SELECT 1 FROM dbo.Cache 
			WITH (UPDLOCK,SERIALIZABLE) WHERE CacheKeyHash = @cacheKeyHash)
		BEGIN
			UPDATE dbo.Cache SET 
					Domain = @Domain,
					CacheBlob = @CacheBlob,
					Size = @Size,
					LastAccessed = GETDATE()
				WHERE CacheKeyHash = @cacheKeyHash
		END
	ELSE
	
		BEGIN
			INSERT INTO dbo.Cache
				(
				cacheKeyHash,
				Domain,
				Size,
				CacheBlob,
				LastAccessed)			
			values 
				(@cacheKeyHash, @Domain, @Size, @CacheBlob, GETDATE())
		END
	COMMIT TRAN

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteCacheById]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteCacheById]
GO

-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-12-12
-- Description:	Deletes a CacheKey record by its id
-- =============================================
CREATE PROCEDURE DeleteCacheById
	@CacheKeyHash BINARY(20) 
AS
BEGIN
	SET NOCOUNT OFF
	DELETE FROM dbo.Cache WHERE CacheKeyHash = @CacheKeyHash
	SELECT @@ROWCOUNT AS Deleted
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCache]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].GetCache
GO

-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-12-12
-- Description:	returns cache entry by its Id
-- =============================================
CREATE PROCEDURE GetCache
	@cacheKeyHash		BINARY(20)
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		CacheBlob
	FROM
		dbo.Cache
	WHERE
		CacheKeyHash = @cacheKeyHash

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDomainSizes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].GetDomainSizes
GO

-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-12-12
-- Description:	returns each domain with its total size
-- =============================================
CREATE PROCEDURE GetDomainSizes
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT 
		Domain,
		SUM(Size) As TotalSize
	FROM
		dbo.Cache
	GROUP BY
		Domain

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetEarliestAccessedItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].GetEarliestAccessedItem
GO

-- =============================================
-- Author:		Ali Kheyrollahi
-- Create date: 2012-12-12
-- Description:	returns earliest accessed item optionally for a domain
-- =============================================
CREATE PROCEDURE GetEarliestAccessedItem
	 @Domain	NVARCHAR(256)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @Domain = ''
		SELECT TOP 1 
			CacheKeyHash,
			LastAccessed,
			Size,
			Domain
		FROM
			dbo.Cache
		ORDER BY
			LastAccessed
	ELSE
		SELECT TOP 1 
			CacheKeyHash,
			LastAccessed,
			Size,
			Domain
		FROM
			dbo.Cache
		WHERE
			Domain = @Domain
		ORDER BY
			LastAccessed
			
		


END
GO
