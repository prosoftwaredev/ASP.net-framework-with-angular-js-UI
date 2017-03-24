ALTER TABLE [dbo].[OrderTimeslips] ADD  DEFAULT ((0)) FOR [RollOver]
GO
ALTER TABLE [dbo].[OrderTimeslips] ADD  DEFAULT ((0)) FOR [IsOneDay]
GO
ALTER TABLE [dbo].[OrderTimeslips] ADD  DEFAULT ((0)) FOR [BillState]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cities_dbo.States_StateID] FOREIGN KEY([StateID])
REFERENCES [dbo].[States] ([StateID])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_dbo.Cities_dbo.States_StateID]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Customers_dbo.PaymentTerms_PaymentTermID] FOREIGN KEY([PaymentTermID])
REFERENCES [dbo].[PaymentTerms] ([PaymentTermID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_dbo.Customers_dbo.PaymentTerms_PaymentTermID]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Customers_dbo.SalesReps_SalesRepID] FOREIGN KEY([SalesRepID])
REFERENCES [dbo].[SalesReps] ([SalesRepID])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_dbo.Customers_dbo.SalesReps_SalesRepID]
GO
ALTER TABLE [dbo].[CustomerSiteJobLocations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSiteJobLocations_dbo.CustomerSites_CustomerSiteID] FOREIGN KEY([CustomerSiteID])
REFERENCES [dbo].[CustomerSites] ([CustomerSiteID])
GO
ALTER TABLE [dbo].[CustomerSiteJobLocations] CHECK CONSTRAINT [FK_dbo.CustomerSiteJobLocations_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.CustomerSites_CustomerSiteID] FOREIGN KEY([CustomerSiteID])
REFERENCES [dbo].[CustomerSites] ([CustomerSiteID])
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications] CHECK CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.LabourClassifications_LabourClassificationID] FOREIGN KEY([LabourClassificationID])
REFERENCES [dbo].[LabourClassifications] ([LabourClassificationID])
GO
ALTER TABLE [dbo].[CustomerSiteLabourClassifications] CHECK CONSTRAINT [FK_dbo.CustomerSiteLabourClassifications_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[CustomerSiteNotes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSiteNotes_dbo.CustomerSites_CustomerSiteID] FOREIGN KEY([CustomerSiteID])
REFERENCES [dbo].[CustomerSites] ([CustomerSiteID])
GO
ALTER TABLE [dbo].[CustomerSiteNotes] CHECK CONSTRAINT [FK_dbo.CustomerSiteNotes_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[CustomerSites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_InCityID] FOREIGN KEY([InCityID])
REFERENCES [dbo].[Cities] ([CityID])
GO
ALTER TABLE [dbo].[CustomerSites] CHECK CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_InCityID]
GO
ALTER TABLE [dbo].[CustomerSites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_PrCityID] FOREIGN KEY([PrCityID])
REFERENCES [dbo].[Cities] ([CityID])
GO
ALTER TABLE [dbo].[CustomerSites] CHECK CONSTRAINT [FK_dbo.CustomerSites_dbo.Cities_PrCityID]
GO
ALTER TABLE [dbo].[CustomerSites]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CustomerSites_dbo.Customers_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[CustomerSites] CHECK CONSTRAINT [FK_dbo.CustomerSites_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[EmployeeBlacklists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Customers_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[EmployeeBlacklists] CHECK CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[EmployeeBlacklists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeBlacklists] CHECK CONSTRAINT [FK_dbo.EmployeeBlacklists_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeCertifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Certifications_CertificationID] FOREIGN KEY([CertificationID])
REFERENCES [dbo].[Certifications] ([CertificationID])
GO
ALTER TABLE [dbo].[EmployeeCertifications] CHECK CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Certifications_CertificationID]
GO
ALTER TABLE [dbo].[EmployeeCertifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeCertifications] CHECK CONSTRAINT [FK_dbo.EmployeeCertifications_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications] CHECK CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.LabourClassifications_LabourClassificationID] FOREIGN KEY([LabourClassificationID])
REFERENCES [dbo].[LabourClassifications] ([LabourClassificationID])
GO
ALTER TABLE [dbo].[EmployeeLabourClassifications] CHECK CONSTRAINT [FK_dbo.EmployeeLabourClassifications_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[EmployeeNotes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeNotes_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeNotes] CHECK CONSTRAINT [FK_dbo.EmployeeNotes_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.Cities_CityID] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([CityID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.Cities_CityID]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Employees_dbo.PayFrequencies_PayFrequencyID] FOREIGN KEY([PayFrequencyID])
REFERENCES [dbo].[PayFrequencies] ([PayFrequencyID])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_dbo.Employees_dbo.PayFrequencies_PayFrequencyID]
GO
ALTER TABLE [dbo].[EmployeeSkills]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[EmployeeSkills] CHECK CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[EmployeeSkills]  WITH CHECK ADD  CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Skills_SkillID] FOREIGN KEY([SkillID])
REFERENCES [dbo].[Skills] ([SkillID])
GO
ALTER TABLE [dbo].[EmployeeSkills] CHECK CONSTRAINT [FK_dbo.EmployeeSkills_dbo.Skills_SkillID]
GO
ALTER TABLE [dbo].[LastLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LastLogins_dbo.Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[LastLogins] CHECK CONSTRAINT [FK_dbo.LastLogins_dbo.Users_UserID]
GO
ALTER TABLE [dbo].[OrderEquipments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderEquipments_dbo.Equipments_EquipmentID] FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipments] ([EquipmentID])
GO
ALTER TABLE [dbo].[OrderEquipments] CHECK CONSTRAINT [FK_dbo.OrderEquipments_dbo.Equipments_EquipmentID]
GO
ALTER TABLE [dbo].[OrderEquipments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderEquipments_dbo.Orders_OrderID] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderEquipments] CHECK CONSTRAINT [FK_dbo.OrderEquipments_dbo.Orders_OrderID]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerID] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orders_dbo.CustomerSites_CustomerSiteID] FOREIGN KEY([CustomerSiteID])
REFERENCES [dbo].[CustomerSites] ([CustomerSiteID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_dbo.Orders_dbo.CustomerSites_CustomerSiteID]
GO
ALTER TABLE [dbo].[OrderTimeslips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Customers_CustomerID] FOREIGN KEY([Customer_CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[OrderTimeslips] CHECK CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Customers_CustomerID]
GO
ALTER TABLE [dbo].[OrderTimeslips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderTimeslips_dbo.CustomerSiteJobLocations_CustomerSiteJobLocationID] FOREIGN KEY([CustomerSiteJobLocationID])
REFERENCES [dbo].[CustomerSiteJobLocations] ([CustomerSiteJobLocationID])
GO
ALTER TABLE [dbo].[OrderTimeslips] CHECK CONSTRAINT [FK_dbo.OrderTimeslips_dbo.CustomerSiteJobLocations_CustomerSiteJobLocationID]
GO
ALTER TABLE [dbo].[OrderTimeslips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Employees_EmployeeID] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[OrderTimeslips] CHECK CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Employees_EmployeeID]
GO
ALTER TABLE [dbo].[OrderTimeslips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderTimeslips_dbo.LabourClassifications_LabourClassificationID] FOREIGN KEY([LabourClassificationID])
REFERENCES [dbo].[LabourClassifications] ([LabourClassificationID])
GO
ALTER TABLE [dbo].[OrderTimeslips] CHECK CONSTRAINT [FK_dbo.OrderTimeslips_dbo.LabourClassifications_LabourClassificationID]
GO
ALTER TABLE [dbo].[OrderTimeslips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Orders_OrderID] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([OrderID])
GO
ALTER TABLE [dbo].[OrderTimeslips] CHECK CONSTRAINT [FK_dbo.OrderTimeslips_dbo.Orders_OrderID]
GO
ALTER TABLE [dbo].[ReportParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReportParameters_dbo.Reports_ReportId] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Reports] ([ReportId])
GO
ALTER TABLE [dbo].[ReportParameters] CHECK CONSTRAINT [FK_dbo.ReportParameters_dbo.Reports_ReportId]
GO
ALTER TABLE [dbo].[ReportRequestParameters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReportRequestParameters_dbo.ReportRequests_ReportRequestId] FOREIGN KEY([ReportRequestId])
REFERENCES [dbo].[ReportRequests] ([ReportRequestId])
GO
ALTER TABLE [dbo].[ReportRequestParameters] CHECK CONSTRAINT [FK_dbo.ReportRequestParameters_dbo.ReportRequests_ReportRequestId]
GO
ALTER TABLE [dbo].[ReportRequests]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReportRequests_dbo.Reports_ReportId] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Reports] ([ReportId])
GO
ALTER TABLE [dbo].[ReportRequests] CHECK CONSTRAINT [FK_dbo.ReportRequests_dbo.Reports_ReportId]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_dbo.States_dbo.Countries_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_dbo.States_dbo.Countries_CountryID]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Roles_RoleID]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_dbo.UserRoles_dbo.Users_UserID]
GO
USE [master]
GO
ALTER DATABASE [DB_A1803F_rsvp] SET  READ_WRITE 
GO