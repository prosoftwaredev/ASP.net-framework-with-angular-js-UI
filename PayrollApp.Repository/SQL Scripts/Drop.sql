USE [DB_A1803F_rsvp]
GO
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserID]
GO
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleID]
GO
ALTER TABLE [dbo].[States] DROP CONSTRAINT [FK_dbo.States_dbo.Countries_CountryID]
GO
ALTER TABLE [dbo].[ReportRequests] DROP CONSTRAINT [FK_dbo.ReportRequests_dbo.Reports_ReportId]
GO
ALTER TABLE [dbo].[ReportRequestParameters] DROP CONSTRAINT [FK_dbo.ReportRequestParameters_dbo.ReportRequests_ReportRequestId]
GO
ALTER TABLE [dbo].[ReportParameters] DROP CONSTRAINT [FK_dbo.ReportParameters_dbo.Reports_ReportId]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Orders_OrderID]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [FK_dbo.OrderTimeslips_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [FK_dbo.OrderTimeslips_dbo.CustomerSiteJobLocations_CustomerSiteJobLocationID]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_dbo.Orders_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[OrderEquipments] DROP CONSTRAINT [FK_dbo.OrderEquipments_dbo.Orders_OrderID]
GO
ALTER TABLE [dbo].[OrderEquipments] DROP CONSTRAINT [FK_dbo.OrderEquipments_dbo.Equipments_EquipmentID]
GO
ALTER TABLE [dbo].[LastLogins] DROP CONSTRAINT [FK_dbo.LastLogins_dbo.Users_UserID]
GO
ALTER TABLE [dbo].[EmployeeSkills] DROP CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Skills_SkillID]
GO
ALTER TABLE [dbo].[EmployeeSkills] DROP CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_dbo.Employees_dbo.PayFrequencies_PayFrequencyID]
GO
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_dbo.Employees_dbo.Cities_CityID]
GO
ALTER TABLE [dbo].[EmployeeNotes] DROP CONSTRAINT [FK_dbo.EmployeeNotes_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications] DROP CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications] DROP CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeCertifications] DROP CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeCertifications] DROP CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Certifications_CertificationID]
GO
ALTER TABLE [dbo].[EmployeeBlacklists] DROP CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeBlacklists] DROP CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[CustomerSites] DROP CONSTRAINT [FK_dbo.CustomerSites_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[CustomerSites] DROP CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_PrCityID]
GO
ALTER TABLE [dbo].[CustomerSites] DROP CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_InCityID]
GO
ALTER TABLE [dbo].[CustomerSiteNotes] DROP CONSTRAINT [FK_dbo.CustomerSiteNotes_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications] DROP CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications] DROP CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[CustomerSiteJobLocations] DROP CONSTRAINT [FK_dbo.CustomerSiteJobLocations_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_dbo.Customers_dbo.SalesReps_SalesRepID]
GO
ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_dbo.Customers_dbo.PaymentTerms_PaymentTermID]
GO
ALTER TABLE [dbo].[Cities] DROP CONSTRAINT [FK_dbo.Cities_dbo.States_StateID]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [DF__OrderTime__BillS__73852659]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [DF__OrderTime__IsOne__6AEFE058]
GO
ALTER TABLE [dbo].[OrderTimeslips] DROP CONSTRAINT [DF__OrderTime__RollO__65370702]
GO
/****** Object:  Index [IX_UserID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_UserID] ON [dbo].[UserRoles]
GO
/****** Object:  Index [IX_RoleID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_RoleID] ON [dbo].[UserRoles]
GO
/****** Object:  Index [IX_CountryID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CountryID] ON [dbo].[States]
GO
/****** Object:  Index [IX_ReportId]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_ReportId] ON [dbo].[ReportRequests]
GO
/****** Object:  Index [IX_ReportRequestId]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_ReportRequestId] ON [dbo].[ReportRequestParameters]
GO
/****** Object:  Index [IX_ReportId]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_ReportId] ON [dbo].[ReportParameters]
GO
/****** Object:  Index [IX_OrderID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_OrderID] ON [dbo].[OrderTimeslips]
GO
/****** Object:  Index [IX_LabourClassificationID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_LabourClassificationID] ON [dbo].[OrderTimeslips]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[OrderTimeslips]
GO
/****** Object:  Index [IX_CustomerSiteJobLocationID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerSiteJobLocationID] ON [dbo].[OrderTimeslips]
GO
/****** Object:  Index [IX_Customer_CustomerID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_Customer_CustomerID] ON [dbo].[OrderTimeslips]
GO
/****** Object:  Index [IX_CustomerSiteID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerSiteID] ON [dbo].[Orders]
GO
/****** Object:  Index [IX_CustomerID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerID] ON [dbo].[Orders]
GO
/****** Object:  Index [IX_OrderID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_OrderID] ON [dbo].[OrderEquipments]
GO
/****** Object:  Index [IX_EquipmentID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EquipmentID] ON [dbo].[OrderEquipments]
GO
/****** Object:  Index [IX_UserID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_UserID] ON [dbo].[LastLogins]
GO
/****** Object:  Index [IX_SkillID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_SkillID] ON [dbo].[EmployeeSkills]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[EmployeeSkills]
GO
/****** Object:  Index [IX_PayFrequencyID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_PayFrequencyID] ON [dbo].[Employees]
GO
/****** Object:  Index [IX_CityID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CityID] ON [dbo].[Employees]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[EmployeeNotes]
GO
/****** Object:  Index [IX_LabourClassificationID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_LabourClassificationID] ON [dbo].[EmployeeLabourClassifications]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[EmployeeLabourClassifications]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[EmployeeCertifications]
GO
/****** Object:  Index [IX_CertificationID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CertificationID] ON [dbo].[EmployeeCertifications]
GO
/****** Object:  Index [IX_EmployeeID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_EmployeeID] ON [dbo].[EmployeeBlacklists]
GO
/****** Object:  Index [IX_CustomerID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerID] ON [dbo].[EmployeeBlacklists]
GO
/****** Object:  Index [IX_PrCityID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_PrCityID] ON [dbo].[CustomerSites]
GO
/****** Object:  Index [IX_InCityID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_InCityID] ON [dbo].[CustomerSites]
GO
/****** Object:  Index [IX_CustomerID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerID] ON [dbo].[CustomerSites]
GO
/****** Object:  Index [IX_CustomerSiteID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerSiteID] ON [dbo].[CustomerSiteNotes]
GO
/****** Object:  Index [IX_LabourClassificationID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_LabourClassificationID] ON [dbo].[CustomerSiteLabourClassifications]
GO
/****** Object:  Index [IX_CustomerSiteID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerSiteID] ON [dbo].[CustomerSiteLabourClassifications]
GO
/****** Object:  Index [IX_CustomerSiteID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_CustomerSiteID] ON [dbo].[CustomerSiteJobLocations]
GO
/****** Object:  Index [IX_SalesRepID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_SalesRepID] ON [dbo].[Customers]
GO
/****** Object:  Index [IX_PaymentTermID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_PaymentTermID] ON [dbo].[Customers]
GO
/****** Object:  Index [IX_StateID]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP INDEX [IX_StateID] ON [dbo].[Cities]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[UserRoles]
GO
/****** Object:  Table [dbo].[Titles]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Titles]
GO
/****** Object:  Table [dbo].[States]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[States]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Skills]
GO
/****** Object:  Table [dbo].[SalesReps]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[SalesReps]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Reports]
GO
/****** Object:  Table [dbo].[ReportRequests]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[ReportRequests]
GO
/****** Object:  Table [dbo].[ReportRequestParameters]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[ReportRequestParameters]
GO
/****** Object:  Table [dbo].[ReportParameters]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[ReportParameters]
GO
/****** Object:  Table [dbo].[PaymentTerms]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[PaymentTerms]
GO
/****** Object:  Table [dbo].[PayFrequencies]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[PayFrequencies]
GO
/****** Object:  Table [dbo].[OrderTimeslips]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[OrderTimeslips]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Orders]
GO
/****** Object:  Table [dbo].[OrderEquipments]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[OrderEquipments]
GO
/****** Object:  Table [dbo].[LastLogins]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[LastLogins]
GO
/****** Object:  Table [dbo].[LabourClassifications]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[LabourClassifications]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Images]
GO
/****** Object:  Table [dbo].[ExcLoggers]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[ExcLoggers]
GO
/****** Object:  Table [dbo].[Equipments]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Equipments]
GO
/****** Object:  Table [dbo].[EmployeeTypes]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeTypes]
GO
/****** Object:  Table [dbo].[EmployeeSkills]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeSkills]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Employees]
GO
/****** Object:  Table [dbo].[EmployeeNotes]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeNotes]
GO
/****** Object:  Table [dbo].[EmployeeLabourClassifications]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeLabourClassifications]
GO
/****** Object:  Table [dbo].[EmployeeCertifications]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeCertifications]
GO
/****** Object:  Table [dbo].[EmployeeBlacklists]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[EmployeeBlacklists]
GO
/****** Object:  Table [dbo].[CustomerSites]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[CustomerSites]
GO
/****** Object:  Table [dbo].[CustomerSiteNotes]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[CustomerSiteNotes]
GO
/****** Object:  Table [dbo].[CustomerSiteLabourClassifications]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[CustomerSiteLabourClassifications]
GO
/****** Object:  Table [dbo].[CustomerSiteJobLocations]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[CustomerSiteJobLocations]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Customers]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Countries]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Cities]
GO
/****** Object:  Table [dbo].[Certifications]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[Certifications]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
USE [master]
GO
/****** Object:  Database [DB_A1803F_rsvp]    Script Date: 3/16/2017 7:14:16 PM ******/
DROP DATABASE [DB_A1803F_rsvp]
GO