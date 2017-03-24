


DELETE FROM [dbo].[OrderEquipments]
DELETE FROM [dbo].[Orders]
DELETE FROM [dbo].[CustomerSiteLabourClassifications]
DELETE FROM [dbo].[CustomerSiteNotes]
DELETE FROM [dbo].[CustomerSites]
DELETE FROM [dbo].[Customers]

----------------------------------------------------------------------------
--Drop all functions in the database
--DECLARE @TempFunctions Table (RowId Integer Identity(1,1), name varchar(120))
--INSERT INTO @TempFunctions(name)
--SELECT 'DROP FUNCTION ' + '[dbo].[' + name + ']' 
--FROM sys.objects 
--WHERE type in (N'FN', N'IF', N'TF', N'FS', N'FT') and name not like 'fn_dia%'

--DECLARE @functions nvarchar(4000)
--SELECT @functions = Coalesce(@functions + '; ', '') + name
--FROM @TempFunctions

--EXEC sp_executesql @functions

----Drop all T-SQL stored procedures
--DECLARE @TempProcs Table (RowId Integer Identity(1,1), name varchar(120))
--INSERT INTO @TempProcs(name)
--SELECT 'DROP PROCEDURE ' + '[dbo].[' + name + ']' 
--FROM sys.objects
--WHERE type in (N'P') and name not like 'sp_%'

--DECLARE @procs nvarchar(max)
--SELECT @procs = Coalesce(@procs + '; ', '') + name
--FROM @TempProcs

--EXEC sp_executesql @procs

----Drop all Views
--DECLARE @TempViews Table (RowId Integer Identity(1,1), name varchar(120))
--INSERT INTO @TempViews(name)
--SELECT 'DROP VIEW ' + '[dbo].[' + name + ']' 
--FROM sys.objects
--WHERE type in (N'V')

--DECLARE @views nvarchar(max)
--SELECT @views = Coalesce(@views + '; ', '') + name
--FROM @TempViews

--EXEC sp_executesql @views



----Drop Stored Procs that use SQL CLR
--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Product_MortgageDocument_Pages]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[usp_Product_MortgageDocument_Pages]

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Product_TitleSearch]') AND type in (N'P', N'PC'))
--DROP PROCEDURE [dbo].[usp_Product_TitleSearch]

---- Drop Triggers that use SQL CLR
--if object_id('Audit_ACCOUNT','TA') is not null drop trigger Audit_ACCOUNT
--if object_id('Audit_ACCOUNT_USER','TA') is not null drop trigger Audit_ACCOUNT_USER
--if object_id('Audit_ACCOUNT_ORG','TA') is not null drop trigger Audit_ACCOUNT_ORG
--if object_id('Audit_SCD','TA') is not null drop trigger Audit_SCD
--if object_id('Audit_SCT','TA') is not null drop trigger Audit_SCT
