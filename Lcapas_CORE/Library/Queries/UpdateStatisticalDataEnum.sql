USE [lcapasdb_dev]
GO

UPDATE [dbo].[ApasMessages]
   SET [MessageType] = 10
 WHERE [MessageType] = 6
GO
