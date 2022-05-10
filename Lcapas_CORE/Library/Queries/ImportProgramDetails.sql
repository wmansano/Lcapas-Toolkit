/****** Need to lookup Programs  ******/
/****** Make sure Campuses exist  ******/
/****** Make sure Terms exist  ******/

USE [lcapasdb]
GO

INSERT INTO [dbo].[ProgramDetails]
           ([StartDate]
           ,[MonthlyBased]
           ,[ProgramTerm_ProgramTermId]
           ,[ProgramCampus_ProgramCampusId]
           ,[ApplicationProgram_ApplicationProgramId]
           ,[ProgramDetailOrder])
SELECT 'Startdate'
		, 'FALSE'
		, (Select ap.ApplicationProgramId from [dbo].ApplicationPrograms ap where ap.ProgramCode = [Program])
		, (Select pc.ProgramCampusId from [dbo].ProgramCampus pc where pc.CampusCode = [Location])
		, (Select pt.ProgramTermId from [dbo].ProgramTerms pt where pt.TermCode = [entryTerm])
		, NULL
  FROM [dbo].[apl_applications_admissions]
GO

