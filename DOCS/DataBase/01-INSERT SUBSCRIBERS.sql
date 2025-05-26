USE [NamiCustomers]
GO

INSERT INTO [dbo].[Subscribers]
           ([CityId]
     
           ,[Name]
           ,[Family]
           ,[NatinalCode]
           ,[IdNumber]
           ,[FathersName]
           ,[BrithDatePersian]
           ,[BrithDate]
           ,[Mobile]
           ,[Phone]
           ,[Address]
           ,[PostalCode]
           ,[NationalCode]
           ,[Sex]

           ,[CreateAt]
           ,[IsRemoved]
           ,[LastModifiedAt]
           ,[RemovedAt])
    SELECT 
           1

           ,Name 
           ,Family 
           ,NatinalCode 
           ,IdNumber 
           ,FathersName 
           ,Dateofbirth 
           ,BrithDate 
           ,Mobile 
           ,Phone 
           ,Address 
           ,PostalCode 
           ,NatinalCode 
           ,Sex 

           ,getdate()
           ,IsRemoved
           ,ModifiedOn 
           ,RemovedTime 

		   from  NamiSale.dbo.Subscriber

GO


