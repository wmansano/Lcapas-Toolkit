use [lcapasdb];

/****** Script for SelectTopNRows command from SSMS  ******/
INSERT INTO [dbo].[ReceivedMessages]
           ([UUID]
           ,[ASN]
           ,[MessageType]
           ,[MessageXML]
           ,[SenderId]
           ,[SentDate]
           ,[ReceiverId]
           ,[ReceivedDate])
  SELECT NEWID()
         ,[AlbertaStudentNumber]
	     ,0
         ,[DecryptedMessage]
	     ,'Archive'
	     ,[DateSubmitted]
	     ,'48027000'
	  ,[DateSubmitted]
  FROM [lcapasdb_dev].[dbo].[APAS_ApplicationMessages]
GO
