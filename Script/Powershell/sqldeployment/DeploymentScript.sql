-------------------------------------------------------------------------------
-- Create TABLE [dm].[zdm_CuryRateHelper]
-------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dm].[zdm_CuryRateHelper]') AND type in (N'U'))
BEGIN
CREATE TABLE [dm].[zdm_CuryRateHelper](
	[number] [int] NULL,
	[type] [varchar](1) NULL
) ON [PRIMARY]
END
GO
-------------------------------------------------------------------------------
-- Populate Data in TABLE [dm].[zdm_CuryRateHelper]
-------------------------------------------------------------------------------
IF NOT EXISTS (SELECT TOP 1 * FROM [dm].[zdm_CuryRateHelper])
BEGIN
	DECLARE @NumberOfRows INT
	SET @NumberOfRows = 0
	WHILE @NumberOfRows <= 10000
	BEGIN
		INSERT INTO [dm].[zdm_CuryRateHelper]([number],[type])VALUES(@NumberOfRows,'P')
		SET @NumberOfRows = @NumberOfRows + 1
	END
END
ELSE
BEGIN
	PRINT '[dm].[zdm_CuryRateHelper] records already Inserted'
END

-------------------------------------------------------------------------------
-- Create View [dbo].[XVR_CuryTbl]
-------------------------------------------------------------------------------
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[XVR_CuryTbl]'))
BEGIN
EXEC('CREATE VIEW [dbo].[XVR_CuryTbl]
 AS 
	 --okok
	( SELECT r.Rate,x.RateType,x.EffDate,r.MultDiv,r.FromCuryID,r.ToCuryID,r.Crtd_DateTime As Cury_Crtd_DateTime,
      r.LUpd_DateTime As Cury_LUpd_DateTime,r.GLBaseCuryID, r.CMRateType --r.*,  --x.RateType, x.EffDate, r.Rate    
      from    
      (
      --**ok
       select r.RateType, mindate + v.number as EffDate, max(r.effdate) as reffdate    
        from 
			(select MIN(c1.effdate) as mindate, Case When MAX(c1.effdate) > getDate() Then getDate() Else Max(c1.effDate) End As maxdate     
              from [dbo].CuryRate c1    
				inner join [dbo].GLSetup g on c1.ToCuryID = ''USD''  --AND c1.FromCuryID = g.BaseCuryID    
				inner join [dbo].CMSetup c on c1.RateType = c.APRtTpDflt     
			  Where c1.FromCuryID != c1.ToCuryID 
			  ) dt    
			inner join dm.zdm_CuryRateHelper v on v.type=''P'' and dt.mindate + v.number <= dt.maxdate    
            inner join (	select	c1.Rate,c1.RateType,c1.MultDiv,c1.FromCuryID,c1.ToCuryID,c1.EffDate,
							c1.Crtd_DateTime,c1.LUpd_DateTime    
							from [dbo].CuryRate c1    
								inner join [dbo].GLSetup g on c1.ToCuryID = ''USD''  --AND c1.FromCuryID = g.BaseCuryID    
								inner join [dbo].CMSetup c on c1.RateType = c.APRtTpDflt     
							Where c1.FromCuryID != c1.ToCuryID
						)  r 	on r.EffDate <= dt.mindate + v.number    
        group by r.ratetype, mindate + v.number 
        --**ok
        ) x    
        inner join (    
					select c1.Rate,c1.RateType,c1.MultDiv,c1.FromCuryID,c1.ToCuryID,c1.EffDate,
						c1.Crtd_DateTime,c1.LUpd_DateTime, g.BaseCuryID As GLBaseCuryID, c.APRtTpDflt As CMRateType    
					from [dbo].CuryRate c1    
						inner join [dbo].GLSetup g on c1.ToCuryID = ''USD'' --AND c1.FromCuryID = g.BaseCuryID    
						inner join [dbo].CMSetup c on c1.RateType = c.APRtTpDflt    
					Where c1.FromCuryID != c1.ToCuryID 
				   ) r on r.EffDate = x.reffdate and r.RateType = x.RateType
				   ) 
')
END
ELSE
BEGIN
	PRINT 'VIEW [dbo].[XVR_CuryTbl] Already exists'
END
GO

-------------------------------------------------------------------------------
-- Install Stored Proc XBS_RegionalExtractInventory
-------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XBS_RegionalExtractInventory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[XBS_RegionalExtractInventory]
GO

-------------------------------------------------------------------------------
-- Install Stored Proc XBS_RegionalExtractInvoices
-------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XBS_RegionalExtractInvoices]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[XBS_RegionalExtractInvoices]
GO

-------------------------------------------------------------------------------
-- Install Stored Proc XBS_RegionalExtractPurchaseOrders
-------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XBS_RegionalExtractPurchaseOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[XBS_RegionalExtractPurchaseOrders]
GO

-------------------------------------------------------------------------------
-- Install Stored Proc XBS_RegionalExtractSalesOrders
-------------------------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[XBS_RegionalExtractSalesOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[XBS_RegionalExtractSalesOrders]
GO