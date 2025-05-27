USE [NamiCustomers]
GO

INSERT INTO [dbo].[Dealers]
           ([DealerNo]
           ,[DealerName]
           ,[DealerAddress]
           ,[DealerPhone]
           ,[DealerprePhone]
           ,[DealerType]
           ,[CityId]
           ,[Email]
           ,[CityName]
           ,[DealerMobile]
           ,[Sort]
           ,[CreateAt]
           ,[IsRemoved]
           ,[LastModifiedAt]
           ,[RemovedAt])
     select 
          [DealerNo]
           ,[DealerName]
           ,[DealerAddress]
           ,[DealerPhone]
           ,[DealerprePhone]
           ,[DealerType]
           ,1
           ,[Email]
           ,[CityName]
           ,[DealerMobile]
           ,[Sort]
           ,getdate()
           ,0
           ,NULL
           ,NULL from NamiSale.dbo.Dealers
GO


