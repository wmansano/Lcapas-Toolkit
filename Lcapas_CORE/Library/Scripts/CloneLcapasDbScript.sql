USE [lcapasdb_test];
  
	/********  Clone LCAPAS Integration Toolkit: PROD --> TEST *******/
	/********  William Mansano & Patrick Dudley **********************/

		DECLARE @env_db_name nvarchar(14);
		DECLARE @env_backup_path nvarchar(40);
		DECLARE @prod_backup_path nvarchar(40); 

		SET @env_db_name = 'lcapasdb_test';
		SET @env_backup_path = 'F:\Backup\' + @env_db_name + '_' + REPLACE(convert(varchar, getdate(), 114),':','') + '.bak';
		SET @prod_backup_path = 'F:\Backup\' + @prod_backup_path + '_' + REPLACE(convert(varchar, getdate(), 114),':','') + '.bak';	
	
	/* Backup Production Database */
		BACKUP DATABASE lcapasdb_prod TO DISK = @prod_backup_path
		GO
	
	/* Backup Existing Test Database */
		BACKUP DATABASE @env_db_name TO DISK = @env_backup_path
		GO
	
	--/* Delete Database Backup and Restore History from MSDB System Database */

		EXEC msdb.dbo.sp_delete_database_backuphistory @env_db_name
		GO

	--/* Get Exclusive Access of SQL Server Database before Dropping the Database  */

		USE [master]
		GO
		ALTER DATABASE @env_db_name SET SINGLE_USER WITH ROLLBACK IMMEDIATE
		GO

	--/* Drop Database in SQL Server */

		DROP DATABASE @env_db_name
		GO

	--/* Restore PROD Backup to New Test Instance */

		RESTORE DATABASE @env_db_name FROM DISK = @prod_backup_path
		WITH MOVE @env_db_name TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.LCAPPSDB\MSSQL\DATA\' + @env_db_name + '.mdf',
		MOVE @env_db_name + '_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL10_50.LCAPPSDB\MSSQL\DATA\' + @env_db_name + '_log.mdf'
 
		ALTER DATABASE @env_db_name SET MULTI_USER
		GO
