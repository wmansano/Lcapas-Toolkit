
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/02/2021 16:44:47
-- Generated from EDMX file: C:\Projects\Lcapas-Toolkit\Lcapas_CORE\Models\Lcappsdb\Lcapasdb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [lcapasdb_dev];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AcademicRecordAcademicAward]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicAwards] DROP CONSTRAINT [FK_AcademicRecordAcademicAward];
GO
IF OBJECT_ID(N'[dbo].[FK_AcademicRecordAcademicSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicSessions] DROP CONSTRAINT [FK_AcademicRecordAcademicSession];
GO
IF OBJECT_ID(N'[dbo].[FK_AcademicRecordCourses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_AcademicRecordCourses];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessGroupPermissionRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionRecords] DROP CONSTRAINT [FK_AccessGroupPermissionRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessGroupUser_AccessGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessGroupUser] DROP CONSTRAINT [FK_AccessGroupUser_AccessGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessGroupUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccessGroupUser] DROP CONSTRAINT [FK_AccessGroupUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionType_ActionId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_ActionType_ActionId];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionTypePermissionRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionRecords] DROP CONSTRAINT [FK_ActionTypePermissionRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_ApasCodeValCodeLookup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValCodeLookups] DROP CONSTRAINT [FK_ApasCodeValCodeLookup];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationFeeStudentApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentApplications] DROP CONSTRAINT [FK_ApplicationFeeStudentApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationMessageAcknowledgement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acknowledgements] DROP CONSTRAINT [FK_ApplicationMessageAcknowledgement];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationMessagePayPalResponse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayPalResponses] DROP CONSTRAINT [FK_ApplicationMessagePayPalResponse];
GO
IF OBJECT_ID(N'[dbo].[FK_ApplicationProgramProgramDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramDetails] DROP CONSTRAINT [FK_ApplicationProgramProgramDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_CodeTypeValCodeLookup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValCodeLookups] DROP CONSTRAINT [FK_CodeTypeValCodeLookup];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactPersonContactEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactEmails] DROP CONSTRAINT [FK_ContactPersonContactEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_ContactPersonContactPhone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactPhones] DROP CONSTRAINT [FK_ContactPersonContactPhone];
GO
IF OBJECT_ID(N'[dbo].[FK_ControllerTypeActionType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionTypes] DROP CONSTRAINT [FK_ControllerTypeActionType];
GO
IF OBJECT_ID(N'[dbo].[FK_CourseOverrideSchool]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Courses] DROP CONSTRAINT [FK_CourseOverrideSchool];
GO
IF OBJECT_ID(N'[dbo].[FK_DestinationInstitutionTransmissionData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransmissionDatas] DROP CONSTRAINT [FK_DestinationInstitutionTransmissionData];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailSentEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SentEmails] DROP CONSTRAINT [FK_EmailSentEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_HoldTypes_Recipients]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TranscriptHolds] DROP CONSTRAINT [FK_HoldTypes_Recipients];
GO
IF OBJECT_ID(N'[dbo].[FK_Institution_InstitutionId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BulkSendTranscripts] DROP CONSTRAINT [FK_Institution_InstitutionId];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionAcademicRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicRecords] DROP CONSTRAINT [FK_InstitutionAcademicRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_InstitutionAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_InstitutionEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionPhone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Phones] DROP CONSTRAINT [FK_InstitutionPhone];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionReceivedApplications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplicationMessages] DROP CONSTRAINT [FK_InstitutionReceivedApplications];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_InstitutionRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_InstitutionSentApplications]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplicationMessages] DROP CONSTRAINT [FK_InstitutionSentApplications];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_PersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonCitizenship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Citizenships] DROP CONSTRAINT [FK_PersonCitizenship];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonDisability]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Disabilities] DROP CONSTRAINT [FK_PersonDisability];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonEmail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Emails] DROP CONSTRAINT [FK_PersonEmail];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonGender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Genders] DROP CONSTRAINT [FK_PersonGender];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonImmigration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Immigrations] DROP CONSTRAINT [FK_PersonImmigration];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonName]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PersonNames] DROP CONSTRAINT [FK_PersonName];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonPhone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Phones] DROP CONSTRAINT [FK_PersonPhone];
GO
IF OBJECT_ID(N'[dbo].[FK_PersonResidency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Residencies] DROP CONSTRAINT [FK_PersonResidency];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramCampusProgramDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramDetails] DROP CONSTRAINT [FK_ProgramCampusProgramDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramDetailsStudentApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentApplications] DROP CONSTRAINT [FK_ProgramDetailsStudentApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_ProgramTermProgramDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProgramDetails] DROP CONSTRAINT [FK_ProgramTermProgramDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_RecipientResponseStatuses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResponseStatuses] DROP CONSTRAINT [FK_RecipientResponseStatuses];
GO
IF OBJECT_ID(N'[dbo].[FK_ReferenceTypeStudentApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentApplications] DROP CONSTRAINT [FK_ReferenceTypeStudentApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestMatchedStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_RequestMatchedStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_RequestRecipient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Recipients] DROP CONSTRAINT [FK_RequestRecipient];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseAcademicRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicRecords] DROP CONSTRAINT [FK_ResponseAcademicRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseMatchedStudent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_ResponseMatchedStudent];
GO
IF OBJECT_ID(N'[dbo].[FK_ResponseResponseHold]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ResponseHolds] DROP CONSTRAINT [FK_ResponseResponseHold];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingBooleanValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BooleanValues] DROP CONSTRAINT [FK_SettingBooleanValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingCategorySetting]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_SettingCategorySetting];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingDateTimeValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DateTimeValues] DROP CONSTRAINT [FK_SettingDateTimeValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingDoubleValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DoubleValues] DROP CONSTRAINT [FK_SettingDoubleValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingIntegerValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[IntegerValues] DROP CONSTRAINT [FK_SettingIntegerValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SettingShortStringValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShortStringValues] DROP CONSTRAINT [FK_SettingShortStringValue];
GO
IF OBJECT_ID(N'[dbo].[FK_SourceInstitutionTransmissionData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TransmissionDatas] DROP CONSTRAINT [FK_SourceInstitutionTransmissionData];
GO
IF OBJECT_ID(N'[dbo].[FK_Student_StudentId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BulkSendTranscripts] DROP CONSTRAINT [FK_Student_StudentId];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentAcademicRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AcademicRecords] DROP CONSTRAINT [FK_StudentAcademicRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentApplicationApplicationMessage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ApplicationMessages] DROP CONSTRAINT [FK_StudentApplicationApplicationMessage];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentASN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ASNs] DROP CONSTRAINT [FK_StudentASN];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentContactPersons]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ContactPersons] DROP CONSTRAINT [FK_StudentContactPersons];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentPerson]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Students] DROP CONSTRAINT [FK_StudentPerson];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_StudentRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentResponse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_StudentResponse];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentsNumber]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[sNumbers] DROP CONSTRAINT [FK_StudentsNumber];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentStudentAlias]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentAlias] DROP CONSTRAINT [FK_StudentStudentAlias];
GO
IF OBJECT_ID(N'[dbo].[FK_StudentStudentApplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StudentApplications] DROP CONSTRAINT [FK_StudentStudentApplication];
GO
IF OBJECT_ID(N'[dbo].[FK_TranscriptHoldCourseTitle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CourseTitles] DROP CONSTRAINT [FK_TranscriptHoldCourseTitle];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NoteMessages] DROP CONSTRAINT [FK_TransmissionData];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionData_TransmissionDataId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Acknowledgements] DROP CONSTRAINT [FK_TransmissionData_TransmissionDataId];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionDataRequest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Requests] DROP CONSTRAINT [FK_TransmissionDataRequest];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionDataResponse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_TransmissionDataResponse];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionDataStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Statuses] DROP CONSTRAINT [FK_TransmissionDataStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_TransmissionDataTranscript]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Transcripts] DROP CONSTRAINT [FK_TransmissionDataTranscript];
GO
IF OBJECT_ID(N'[dbo].[FK_User_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Histories] DROP CONSTRAINT [FK_User_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_ValCodeValCodeLookup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ValCodeLookups] DROP CONSTRAINT [FK_ValCodeValCodeLookup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AcademicAwards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicAwards];
GO
IF OBJECT_ID(N'[dbo].[AcademicRecords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicRecords];
GO
IF OBJECT_ID(N'[dbo].[AcademicSessions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AcademicSessions];
GO
IF OBJECT_ID(N'[dbo].[AccessGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessGroups];
GO
IF OBJECT_ID(N'[dbo].[AccessGroupUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccessGroupUser];
GO
IF OBJECT_ID(N'[dbo].[Acknowledgements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Acknowledgements];
GO
IF OBJECT_ID(N'[dbo].[ActionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionTypes];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[ApasCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApasCodes];
GO
IF OBJECT_ID(N'[dbo].[ApasMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApasMessages];
GO
IF OBJECT_ID(N'[dbo].[ApplicationFees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationFees];
GO
IF OBJECT_ID(N'[dbo].[ApplicationMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationMessages];
GO
IF OBJECT_ID(N'[dbo].[ApplicationPrograms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationPrograms];
GO
IF OBJECT_ID(N'[dbo].[ASNs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ASNs];
GO
IF OBJECT_ID(N'[dbo].[BooleanValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BooleanValues];
GO
IF OBJECT_ID(N'[dbo].[BulkSendTranscripts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BulkSendTranscripts];
GO
IF OBJECT_ID(N'[dbo].[Citizenships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Citizenships];
GO
IF OBJECT_ID(N'[dbo].[CodeTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CodeTypes];
GO
IF OBJECT_ID(N'[dbo].[ContactEmails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactEmails];
GO
IF OBJECT_ID(N'[dbo].[ContactPersons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactPersons];
GO
IF OBJECT_ID(N'[dbo].[ContactPhones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContactPhones];
GO
IF OBJECT_ID(N'[dbo].[ControllerTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControllerTypes];
GO
IF OBJECT_ID(N'[dbo].[Courses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Courses];
GO
IF OBJECT_ID(N'[dbo].[CourseTitles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CourseTitles];
GO
IF OBJECT_ID(N'[dbo].[DateTimeValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DateTimeValues];
GO
IF OBJECT_ID(N'[dbo].[Disabilities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Disabilities];
GO
IF OBJECT_ID(N'[dbo].[DoubleValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DoubleValues];
GO
IF OBJECT_ID(N'[dbo].[Emails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Emails];
GO
IF OBJECT_ID(N'[dbo].[ExceptionRecords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceptionRecords];
GO
IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[Histories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Histories];
GO
IF OBJECT_ID(N'[dbo].[Immigrations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Immigrations];
GO
IF OBJECT_ID(N'[dbo].[InstitutionNames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstitutionNames];
GO
IF OBJECT_ID(N'[dbo].[Institutions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Institutions];
GO
IF OBJECT_ID(N'[dbo].[IntegerValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IntegerValues];
GO
IF OBJECT_ID(N'[dbo].[KeyValueTempCaches]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KeyValueTempCaches];
GO
IF OBJECT_ID(N'[dbo].[MyCredsMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MyCredsMessages];
GO
IF OBJECT_ID(N'[dbo].[NoteMessages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NoteMessages];
GO
IF OBJECT_ID(N'[dbo].[PayPalResponses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayPalResponses];
GO
IF OBJECT_ID(N'[dbo].[PermissionRecords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionRecords];
GO
IF OBJECT_ID(N'[dbo].[PersonNames]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PersonNames];
GO
IF OBJECT_ID(N'[dbo].[Persons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Persons];
GO
IF OBJECT_ID(N'[dbo].[Phones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Phones];
GO
IF OBJECT_ID(N'[dbo].[ProgramCampuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramCampuses];
GO
IF OBJECT_ID(N'[dbo].[ProgramDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramDetails];
GO
IF OBJECT_ID(N'[dbo].[ProgramTerms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProgramTerms];
GO
IF OBJECT_ID(N'[dbo].[ReceivedErrors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReceivedErrors];
GO
IF OBJECT_ID(N'[dbo].[Recipients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Recipients];
GO
IF OBJECT_ID(N'[dbo].[ReferenceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReferenceTypes];
GO
IF OBJECT_ID(N'[dbo].[ReportQueries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportQueries];
GO
IF OBJECT_ID(N'[dbo].[Requests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Requests];
GO
IF OBJECT_ID(N'[dbo].[Residencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Residencies];
GO
IF OBJECT_ID(N'[dbo].[ResponseHolds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResponseHolds];
GO
IF OBJECT_ID(N'[dbo].[Responses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Responses];
GO
IF OBJECT_ID(N'[dbo].[ResponseStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ResponseStatuses];
GO
IF OBJECT_ID(N'[dbo].[SentEmails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SentEmails];
GO
IF OBJECT_ID(N'[dbo].[SettingCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingCategories];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[ShortStringValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShortStringValues];
GO
IF OBJECT_ID(N'[dbo].[sNumbers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sNumbers];
GO
IF OBJECT_ID(N'[dbo].[Statuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Statuses];
GO
IF OBJECT_ID(N'[dbo].[StudentAlias]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentAlias];
GO
IF OBJECT_ID(N'[dbo].[StudentApplications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StudentApplications];
GO
IF OBJECT_ID(N'[dbo].[Students]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Students];
GO
IF OBJECT_ID(N'[dbo].[TransactionCourses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionCourses];
GO
IF OBJECT_ID(N'[dbo].[TransactionRequestLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionRequestLogs];
GO
IF OBJECT_ID(N'[dbo].[TransactionTranscripts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionTranscripts];
GO
IF OBJECT_ID(N'[dbo].[TranscriptHolds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TranscriptHolds];
GO
IF OBJECT_ID(N'[dbo].[TranscriptJobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TranscriptJobs];
GO
IF OBJECT_ID(N'[dbo].[TranscriptRestrictions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TranscriptRestrictions];
GO
IF OBJECT_ID(N'[dbo].[Transcripts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transcripts];
GO
IF OBJECT_ID(N'[dbo].[TranscriptTermDescs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TranscriptTermDescs];
GO
IF OBJECT_ID(N'[dbo].[TransmissionDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransmissionDatas];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ValCodeLookups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValCodeLookups];
GO
IF OBJECT_ID(N'[dbo].[ValCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ValCodes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AcademicAwards'
CREATE TABLE [dbo].[AcademicAwards] (
    [AcademicAwardId] int IDENTITY(1,1) NOT NULL,
    [AcademicAwardTitle] nvarchar(400)  NULL,
    [AcademicAwardLevel] int  NULL,
    [AcademicAwardDate] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [AcademicRecord_AcademicRecordId] int  NOT NULL
);
GO

-- Creating table 'AcademicRecords'
CREATE TABLE [dbo].[AcademicRecords] (
    [AcademicRecordId] int IDENTITY(1,1) NOT NULL,
    [StudentLevel] int  NULL,
    [FirstDateAttended] datetime  NULL,
    [LastDateAttended] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ApplicationId] nvarchar(20)  NULL,
    [School_InstitutionId] int  NULL,
    [Student_StudentId] int  NULL,
    [Response_ResponseId] int  NULL
);
GO

-- Creating table 'AcademicSessions'
CREATE TABLE [dbo].[AcademicSessions] (
    [AcademicSessionId] int IDENTITY(1,1) NOT NULL,
    [Term] nvarchar(60)  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Designator] nvarchar(10)  NULL,
    [Type] int  NULL,
    [SchoolYear] nvarchar(10)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [AcademicRecord_AcademicRecordId] int  NOT NULL
);
GO

-- Creating table 'AccessGroups'
CREATE TABLE [dbo].[AccessGroups] (
    [AccessGroupId] int IDENTITY(1,1) NOT NULL,
    [AccessGroupType] int  NOT NULL,
    [AccessGroupDesc] nvarchar(200)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Acknowledgements'
CREATE TABLE [dbo].[Acknowledgements] (
    [AcknowledgementId] int IDENTITY(1,1) NOT NULL,
    [Originator] nvarchar(max)  NOT NULL,
    [OriginatorAPASCode] nvarchar(max)  NOT NULL,
    [IsSuccessful] bit  NOT NULL,
    [AcknowledgementValue] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [TransmissionData_TransmissionDataId] int  NULL,
    [ApplicationMessage_ApplicationMessageId] int  NULL
);
GO

-- Creating table 'ActionTypes'
CREATE TABLE [dbo].[ActionTypes] (
    [ActionId] int IDENTITY(1,1) NOT NULL,
    [ActionTypeType] int  NOT NULL,
    [ActionDesc] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ControllerType_ControllerId] int  NOT NULL,
    [ExternalAction] bit  NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [AddressId] int IDENTITY(1,1) NOT NULL,
    [AddressLine1] nvarchar(80)  NULL,
    [AddressLine2] nvarchar(80)  NULL,
    [AddressType] nvarchar(30)  NULL,
    [PostalCode] varchar(17)  NULL,
    [City] varchar(84)  NULL,
    [Province] varchar(30)  NULL,
    [Country] varchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [InstitutionId] int  NULL,
    [PersonId] int  NULL
);
GO

-- Creating table 'ApasCodes'
CREATE TABLE [dbo].[ApasCodes] (
    [ApasCodeId] int IDENTITY(1,1) NOT NULL,
    [ApasCodeCode] nvarchar(50)  NULL,
    [ApasDesc] nvarchar(100)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ApasMessages'
CREATE TABLE [dbo].[ApasMessages] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [ASN] nvarchar(30)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [SenderId] nvarchar(max)  NULL,
    [ReceiverId] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ApplicationFees'
CREATE TABLE [dbo].[ApplicationFees] (
    [ApplicationFeeId] int IDENTITY(1,1) NOT NULL,
    [ApplicationFeeAmt] float  NOT NULL,
    [StartDateTime] datetime  NOT NULL,
    [Message] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ApplicationMessages'
CREATE TABLE [dbo].[ApplicationMessages] (
    [ApplicationMessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] varchar(36)  NULL,
    [DocumentID] varchar(36)  NULL,
    [ApplicationID] varchar(20)  NULL,
    [ApplicationBody] nvarchar(max)  NOT NULL,
    [SessionId] varchar(36)  NULL,
    [SecurityToken] varchar(36)  NULL,
    [Valid] bit  NOT NULL,
    [Production] bit  NOT NULL,
    [Debugging] bit  NOT NULL,
    [Administrator] bit  NOT NULL,
    [Username] varchar(30)  NULL,
    [ReceivedDateTime] datetime  NULL,
    [DatashareDateTime] datetime  NULL,
    [ParsedDateTime] datetime  NULL,
    [SubmittedDateTime] datetime  NULL,
    [PaidDateTime] datetime  NULL,
    [ReturnedDateTime] datetime  NULL,
    [CancelledDateTime] datetime  NULL,
    [ExportedDateTime] datetime  NULL,
    [DontExportDateTime] datetime  NULL,
    [CreatedBy] varchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] varchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [SourceInstitutionId] int  NOT NULL,
    [DestinationInstitution_InstitutionId] int  NOT NULL,
    [StudentApplication_StudentApplicationId] int  NOT NULL
);
GO

-- Creating table 'ApplicationPrograms'
CREATE TABLE [dbo].[ApplicationPrograms] (
    [ApplicationProgramId] int IDENTITY(1,1) NOT NULL,
    [ProgramCode] nvarchar(15)  NOT NULL,
    [ProgramDesc] nvarchar(max)  NULL,
    [ProgramOrder] int  NULL,
    [Active] bit  NOT NULL,
    [RequiresTranscript] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ASNs'
CREATE TABLE [dbo].[ASNs] (
    [ASNId] int IDENTITY(1,1) NOT NULL,
    [AgencyAssignedID] nvarchar(30)  NOT NULL,
    [AgencyCode] int  NULL,
    [AgencyName] nvarchar(60)  NULL,
    [CountryCode] int  NULL,
    [StateProvinceCode] int  NULL,
    [CollegeData] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NOT NULL
);
GO

-- Creating table 'BooleanValues'
CREATE TABLE [dbo].[BooleanValues] (
    [BooleanValueId] int IDENTITY(1,1) NOT NULL,
    [Value] bit  NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Description] nvarchar(200)  NULL,
    [Show] bit  NOT NULL,
    [GroupName] nvarchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Setting_SettingsId] int  NOT NULL
);
GO

-- Creating table 'CodeTypes'
CREATE TABLE [dbo].[CodeTypes] (
    [CodeTypeId] int IDENTITY(1,1) NOT NULL,
    [TypeCode] nvarchar(50)  NOT NULL,
    [TypeDesc] nvarchar(100)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ContactEmails'
CREATE TABLE [dbo].[ContactEmails] (
    [ContactEmailId] int IDENTITY(1,1) NOT NULL,
    [ContactEmailAddress] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ContactPerson_ContactPersonId] int  NOT NULL
);
GO

-- Creating table 'ContactPersons'
CREATE TABLE [dbo].[ContactPersons] (
    [ContactPersonId] int IDENTITY(1,1) NOT NULL,
    [ContactFirstName] nvarchar(30)  NOT NULL,
    [ContactLastName] nvarchar(50)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NULL
);
GO

-- Creating table 'ContactPhones'
CREATE TABLE [dbo].[ContactPhones] (
    [ContactPhoneId] int IDENTITY(1,1) NOT NULL,
    [ContactPhoneExt] nvarchar(max)  NULL,
    [ContactPhoneNumber] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ContactPerson_ContactPersonId] int  NOT NULL
);
GO

-- Creating table 'ControllerTypes'
CREATE TABLE [dbo].[ControllerTypes] (
    [ControllerId] int IDENTITY(1,1) NOT NULL,
    [ControllerTypeType] int  NOT NULL,
    [ControllerDesc] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseId] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NULL,
    [CourseNumber] nvarchar(30)  NULL,
    [OriginalCourseID] nvarchar(50)  NULL,
    [AgencyCourseID] nvarchar(50)  NULL,
    [SectionNumber] nvarchar(20)  NULL,
    [Level] int  NULL,
    [CreditBasis] int  NULL,
    [CreditUnits] int  NULL,
    [CreditLevel] int  NULL,
    [CreditValue] nvarchar(20)  NULL,
    [CreditEarned] nvarchar(20)  NULL,
    [AcademicGradeScaleCode] nvarchar(5)  NULL,
    [AcademicGrade] nvarchar(20)  NULL,
    [AcademicGradeStatusCode] int  NULL,
    [NarrativeExplanationGrade] nvarchar(200)  NULL,
    [RepeatCode] int  NULL,
    [SubjectAbbreviation] nvarchar(20)  NULL,
    [OverrideSchoolCourseNumber] nvarchar(40)  NULL,
    [AddDate] datetime  NULL,
    [DropDate] datetime  NULL,
    [BeginDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Applicability] int  NULL,
    [InstructionSite] int  NULL,
    [InstructionSiteName] nvarchar(30)  NULL,
    [QualityPointsEarned] nvarchar(20)  NULL,
    [GPAApplicabilityCode] int  NULL,
    [NoteMessage] nvarchar(2000)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [AcademicRecord_AcademicRecordId] int  NOT NULL,
    [CourseOverrideSchool_InstitutionId] int  NULL
);
GO

-- Creating table 'CourseTitles'
CREATE TABLE [dbo].[CourseTitles] (
    [CourseTitleId] int IDENTITY(1,1) NOT NULL,
    [CourseTitleValue] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [TranscriptHold_TranscriptHoldID] int  NULL
);
GO

-- Creating table 'DateTimeValues'
CREATE TABLE [dbo].[DateTimeValues] (
    [DateTimeValueId] int IDENTITY(1,1) NOT NULL,
    [Value] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Setting_SettingsId] int  NOT NULL
);
GO

-- Creating table 'DoubleValues'
CREATE TABLE [dbo].[DoubleValues] (
    [DoubleValueId] int IDENTITY(1,1) NOT NULL,
    [Value] float  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Setting_SettingsId] int  NOT NULL
);
GO

-- Creating table 'Emails'
CREATE TABLE [dbo].[Emails] (
    [EmailId] int IDENTITY(1,1) NOT NULL,
    [EmailAddress] nvarchar(max)  NOT NULL,
    [EmailType] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [InstitutionId] int  NULL,
    [PersonId] int  NULL
);
GO

-- Creating table 'ExceptionRecords'
CREATE TABLE [dbo].[ExceptionRecords] (
    [StatusTrackingId] int IDENTITY(1,1) NOT NULL,
    [Project] nvarchar(20)  NOT NULL,
    [Page] nvarchar(50)  NOT NULL,
    [Function] nvarchar(50)  NOT NULL,
    [Variable] nvarchar(250)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'InstitutionNames'
CREATE TABLE [dbo].[InstitutionNames] (
    [InstitutionNameId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(80)  NOT NULL,
    [DefaultName] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Institution_InstitutionId] int  NULL
);
GO

-- Creating table 'Institutions'
CREATE TABLE [dbo].[Institutions] (
    [InstitutionId] int IDENTITY(1,1) NOT NULL,
    [LocalOrganizationID] nvarchar(36)  NULL,
    [ESIS] nvarchar(36)  NULL,
    [APAS] nvarchar(36)  NULL,
    [ApasInstitution] bit  NULL,
    [ParentInstitutionId] nvarchar(max)  NULL,
    [DefaultInstitution] bit  NOT NULL,
    [PreferredInstitution] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'IntegerValues'
CREATE TABLE [dbo].[IntegerValues] (
    [IntegerValueId] int IDENTITY(1,1) NOT NULL,
    [Value] int  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Setting_SettingsId] int  NOT NULL
);
GO

-- Creating table 'KeyValueTempCaches'
CREATE TABLE [dbo].[KeyValueTempCaches] (
    [KeyValueTempCacheId] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(50)  NULL,
    [Value] nvarchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'PayPalResponses'
CREATE TABLE [dbo].[PayPalResponses] (
    [PayPalResponseId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NOT NULL,
    [SECURETOKEN] nvarchar(36)  NULL,
    [AUTHCODE] nvarchar(10)  NULL,
    [PNREF] nvarchar(20)  NULL,
    [RESPMSG] nvarchar(1200)  NULL,
    [RESULT] nvarchar(3)  NULL,
    [BILLTOLASTNAME] nvarchar(60)  NULL,
    [BILLTOFIRSTNAME] nvarchar(60)  NULL,
    [BILLTOEMAIL] nvarchar(100)  NULL,
    [BILLTOSTREET] nvarchar(250)  NULL,
    [BILLTOCOUNTRY] nvarchar(40)  NULL,
    [BILLTOZIP] nvarchar(12)  NULL,
    [BILLTOPHONE] nvarchar(20)  NULL,
    [CARDTYPE] int  NOT NULL,
    [METHOD] int  NOT NULL,
    [ACCT] nvarchar(10)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ApplicationMessage_ApplicationMessageId] int  NOT NULL
);
GO

-- Creating table 'PermissionRecords'
CREATE TABLE [dbo].[PermissionRecords] (
    [PermissionRecordId] int IDENTITY(1,1) NOT NULL,
    [PermissionRecordNote] nvarchar(150)  NULL,
    [Enabled] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ActionType_ActionId] int  NOT NULL,
    [AccessGroup_AccessGroupId] int  NOT NULL
);
GO

-- Creating table 'PersonNames'
CREATE TABLE [dbo].[PersonNames] (
    [NameId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [MiddleNames] nvarchar(50)  NULL,
    [NameType] nvarchar(50)  NULL,
    [CollegeData] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NULL
);
GO

-- Creating table 'Persons'
CREATE TABLE [dbo].[Persons] (
    [PersonId] int IDENTITY(1,1) NOT NULL,
    [BirthDate] datetime  NULL,
    [CanadianEthnicityRace] int  NULL,
    [LanguageCode] nvarchar(5)  NULL,
    [LanguageUsage] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Phones'
CREATE TABLE [dbo].[Phones] (
    [PhoneId] int IDENTITY(1,1) NOT NULL,
    [CountryCode] nvarchar(10)  NULL,
    [AreaCode] nvarchar(10)  NULL,
    [PhoneNumber] nvarchar(30)  NULL,
    [PhoneNumberExtension] nvarchar(10)  NULL,
    [PhoneType] int  NULL,
    [InstitutionId] int  NULL,
    [PersonId] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ProgramCampuses'
CREATE TABLE [dbo].[ProgramCampuses] (
    [ProgramCampusId] int IDENTITY(1,1) NOT NULL,
    [CampusCode] nvarchar(15)  NOT NULL,
    [CampusDesc] nvarchar(50)  NOT NULL,
    [CampusOrder] int  NULL,
    [Active] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ProgramDetails'
CREATE TABLE [dbo].[ProgramDetails] (
    [ProgramDetailsId] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NULL,
    [MonthlyBased] bit  NOT NULL,
    [ProgramDetailOrder] int  NULL,
    [Active] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [ProgramTerm_ProgramTermId] int  NULL,
    [ProgramCampus_ProgramCampusId] int  NULL,
    [ApplicationProgram_ApplicationProgramId] int  NULL
);
GO

-- Creating table 'ProgramTerms'
CREATE TABLE [dbo].[ProgramTerms] (
    [ProgramTermId] int IDENTITY(1,1) NOT NULL,
    [TermCode] nvarchar(15)  NOT NULL,
    [TermDesc] nvarchar(50)  NULL,
    [TermOrder] int  NULL,
    [Active] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ReceivedErrors'
CREATE TABLE [dbo].[ReceivedErrors] (
    [ReceivedErrorId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(max)  NOT NULL,
    [RequestTrackingID] nvarchar(35)  NULL,
    [OriginalMessageType] nvarchar(max)  NOT NULL,
    [OriginalMessageBody] nvarchar(max)  NOT NULL,
    [ErrorMessage] nvarchar(max)  NOT NULL,
    [ReceivedDateTime] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Recipients'
CREATE TABLE [dbo].[Recipients] (
    [RecipientId] int IDENTITY(1,1) NOT NULL,
    [RequestTrackingID] nvarchar(35)  NULL,
    [TranscriptPurposeType] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Request_RequestId] int  NOT NULL
);
GO

-- Creating table 'ReferenceTypes'
CREATE TABLE [dbo].[ReferenceTypes] (
    [ReferenceTypeId] int IDENTITY(1,1) NOT NULL,
    [ReferenceTypeName] nvarchar(10)  NOT NULL,
    [ReferenceTypeDesc] nvarchar(50)  NOT NULL,
    [ReferenceTypeOrder] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [RequestId] int IDENTITY(1,1) NOT NULL,
    [SentDateTime] datetime  NULL,
    [ReceivedDateTime] datetime  NULL,
    [ViewedDateTime] datetime  NULL,
    [SentToColleagueTRRQ] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [TransmissionData_TransmissionDataId] int  NOT NULL,
    [Student_StudentId] int  NOT NULL,
    [RequestorInstitution_InstitutionId] int  NULL,
    [Matched_StudentId] int  NULL
);
GO

-- Creating table 'ResponseStatuses'
CREATE TABLE [dbo].[ResponseStatuses] (
    [ResponseStatusId] int IDENTITY(1,1) NOT NULL,
    [ResponseStatusType] int  NULL,
    [ResponseStatusData] varchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Recipient_RecipientId] int  NOT NULL
);
GO

-- Creating table 'ResponseHolds'
CREATE TABLE [dbo].[ResponseHolds] (
    [ResponseHoldTypeId] int IDENTITY(1,1) NOT NULL,
    [HoldReason] int  NULL,
    [PlannedReleaseDate] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Response_ResponseId] int  NOT NULL
);
GO

-- Creating table 'Responses'
CREATE TABLE [dbo].[Responses] (
    [ResponseId] int IDENTITY(1,1) NOT NULL,
    [RequestTrackingID] nchar(35)  NULL,
    [ResponseStatusType] int  NULL,
    [SentDateTime] datetime  NULL,
    [ReceivedDateTime] datetime  NULL,
    [ViewedDateTime] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NULL,
    [TransmissionData_TransmissionDataId] int  NOT NULL,
    [Matched_StudentId] int  NULL
);
GO

-- Creating table 'SettingCategories'
CREATE TABLE [dbo].[SettingCategories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(30)  NULL,
    [CategoryOrder] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [SettingsId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [SettingOrder] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [SettingCategory_CategoryId] int  NOT NULL
);
GO

-- Creating table 'ShortStringValues'
CREATE TABLE [dbo].[ShortStringValues] (
    [ShortStringValueId] int IDENTITY(1,1) NOT NULL,
    [Value] varchar(max)  NOT NULL,
    [CheckServer] bit  NULL,
    [IsWindowsServer] bit  NULL,
    [Services] nvarchar(500)  NULL,
    [Environment] nvarchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Setting_SettingsId] int  NOT NULL
);
GO

-- Creating table 'Statuses'
CREATE TABLE [dbo].[Statuses] (
    [StatusId] int IDENTITY(1,1) NOT NULL,
    [StatusValue] nvarchar(max)  NOT NULL,
    [StatusDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [TransmissionData_TransmissionDataId] int  NOT NULL
);
GO

-- Creating table 'StudentApplications'
CREATE TABLE [dbo].[StudentApplications] (
    [StudentApplicationId] int IDENTITY(1,1) NOT NULL,
    [ApplicationID] varchar(20)  NOT NULL,
    [ApplicationSource] nvarchar(10)  NULL,
    [SessionDesignator] nvarchar(10)  NULL,
    [PreviouslyApplied] bit  NULL,
    [StartingYear] int  NULL,
    [StudyLoad] nvarchar(1)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NULL,
    [ApplicationFee_ApplicationFeeId] int  NULL,
    [ReferenceType_ReferenceTypeId] int  NULL,
    [ProgramDetail_ProgramDetailsId] int  NULL
);
GO

-- Creating table 'Students'
CREATE TABLE [dbo].[Students] (
    [StudentId] int IDENTITY(1,1) NOT NULL,
    [PrevSNumber] nvarchar(10)  NULL,
    [FamilyAttendedCollege] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'SvcJobs'
CREATE TABLE [dbo].[SvcJobs] (
    [SvcJobId] int IDENTITY(1,1) NOT NULL,
    [SvcName] nvarchar(50)  NOT NULL,
    [SvcActive] bit  NOT NULL,
    [SvcInt] int  NOT NULL,
    [SvcUseSched] bit  NOT NULL
);
GO

-- Creating table 'SvcLogs'
CREATE TABLE [dbo].[SvcLogs] (
    [SvcLogId] int IDENTITY(1,1) NOT NULL,
    [SvcStartDateTime] datetime  NOT NULL,
    [SvcEndDateTime] datetime  NOT NULL,
    [SvcFail] bit  NOT NULL,
    [SvcFailMsg] nvarchar(250)  NOT NULL,
    [SvcJob_SvcJobId] int  NOT NULL
);
GO

-- Creating table 'SvcScheds'
CREATE TABLE [dbo].[SvcScheds] (
    [SvcSchedId] int IDENTITY(1,1) NOT NULL,
    [SvcStartTime] time  NOT NULL,
    [SvcJobs_SvcJobId] int  NULL
);
GO

-- Creating table 'TranscriptHolds'
CREATE TABLE [dbo].[TranscriptHolds] (
    [TranscriptHoldID] int IDENTITY(1,1) NOT NULL,
    [HoldType] int  NULL,
    [HoldTypeData] nvarchar(250)  NULL,
    [SessionDesignator] nvarchar(50)  NULL,
    [SessionName] nvarchar(30)  NULL,
    [ReleaseDate] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Recipient_RecipientId] int  NULL
);
GO

-- Creating table 'Transcripts'
CREATE TABLE [dbo].[Transcripts] (
    [TranscriptId] int IDENTITY(1,1) NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [TransmissionData_TransmissionDataId] int  NOT NULL
);
GO

-- Creating table 'TransmissionDatas'
CREATE TABLE [dbo].[TransmissionDatas] (
    [TransmissionDataId] int IDENTITY(1,1) NOT NULL,
    [Uuid] nvarchar(36)  NULL,
    [DocumentID] nvarchar(35)  NULL,
    [Xml] nvarchar(max)  NULL,
    [DocumentTypeCode] int  NULL,
    [TransmissionTypeType] int  NULL,
    [DocumentProcessCodeType] int  NULL,
    [DocumentOfficialCodeType] int  NULL,
    [DocumentCompleteCodeType] int  NULL,
    [RequestTrackingID] nvarchar(35)  NULL,
    [ExportedDateTime] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [SourceInstitution_InstitutionId] int  NOT NULL,
    [DestinationInstitution_InstitutionId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [sNumber] nvarchar(12)  NULL,
    [FirstName] nvarchar(30)  NULL,
    [LastName] nvarchar(50)  NULL,
    [FullName] nvarchar(60)  NULL,
    [Active] bit  NULL,
    [UserOrder] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ValCodeLookups'
CREATE TABLE [dbo].[ValCodeLookups] (
    [ApasCodeId] int  NOT NULL,
    [ValCodeId] int  NOT NULL,
    [CodeTypeId] int  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'ValCodes'
CREATE TABLE [dbo].[ValCodes] (
    [ValCodeId] int IDENTITY(1,1) NOT NULL,
    [ValCodeCode] nvarchar(50)  NULL,
    [ValDesc] nvarchar(100)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Citizenships'
CREATE TABLE [dbo].[Citizenships] (
    [CitizenshipId] int IDENTITY(1,1) NOT NULL,
    [CountryCodeType] int  NOT NULL,
    [CitizenshipStatusCodeType] int  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'Residencies'
CREATE TABLE [dbo].[Residencies] (
    [ResidencyId] int IDENTITY(1,1) NOT NULL,
    [ResidencyStatusCodeType] int  NOT NULL,
    [ResidencyCountry] nvarchar(80)  NOT NULL,
    [CountryCodeType] int  NOT NULL,
    [ResidencyFirstEntryDate] datetime  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [GenderId] int IDENTITY(1,1) NOT NULL,
    [GenderCodeType] int  NULL,
    [CollegeData] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'Disabilities'
CREATE TABLE [dbo].[Disabilities] (
    [DisabilityId] int IDENTITY(1,1) NOT NULL,
    [Disabled] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'Immigrations'
CREATE TABLE [dbo].[Immigrations] (
    [ImmigrationId] int IDENTITY(1,1) NOT NULL,
    [FirstEntryIntoCountryDateTime] datetime  NOT NULL,
    [NonImmigrantVisaNumber] nvarchar(30)  NULL,
    [VisaExpirationDateTime] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'TranscriptJobs'
CREATE TABLE [dbo].[TranscriptJobs] (
    [JobId] int IDENTITY(1,1) NOT NULL,
    [JobKey] nvarchar(36)  NOT NULL,
    [JobType] int  NOT NULL,
    [CreatedDateTime] datetime  NULL,
    [StartedDateTime] datetime  NULL,
    [ModifiedDateTime] datetime  NULL,
    [CompletedDateTime] datetime  NULL,
    [JobKilledDateTime] datetime  NULL
);
GO

-- Creating table 'RequestorReceivers'
CREATE TABLE [dbo].[RequestorReceivers] (
    [RequestorReceiverId] int IDENTITY(1,1) NOT NULL,
    [ReceiverType] nvarchar(max)  NOT NULL,
    [Receiver] nvarchar(max)  NOT NULL,
    [Recipient_RecipientId] int  NOT NULL
);
GO

-- Creating table 'NoteMessages'
CREATE TABLE [dbo].[NoteMessages] (
    [NoteMessageId] int IDENTITY(1,1) NOT NULL,
    [NoteMessageContent] nvarchar(100)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [NoteMessage_TransmissionDataId] int  NULL
);
GO

-- Creating table 'TransactionTranscripts'
CREATE TABLE [dbo].[TransactionTranscripts] (
    [TransactionTranscriptId] int IDENTITY(1,1) NOT NULL,
    [TransactionTranscriptUuid] nvarchar(36)  NULL,
    [StudentId] nvarchar(20)  NULL,
    [TranscriptGrouping] nvarchar(10)  NULL,
    [TranscriptText] nvarchar(max)  NULL,
    [CompletedDateTime] datetime  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifiedDateTime] datetime  NULL
);
GO

-- Creating table 'TransactionRequestLogs'
CREATE TABLE [dbo].[TransactionRequestLogs] (
    [TransactionRequestLogId] int IDENTITY(1,1) NOT NULL,
    [TransactionRequestLogUuid] nvarchar(36)  NULL,
    [StudentId] nvarchar(20)  NULL,
    [RequestType] nvarchar(20)  NULL,
    [RecipientId] nvarchar(100)  NULL,
    [RecipientName] nvarchar(250)  NULL,
    [RecipientAddressLines] nvarchar(max)  NULL,
    [RecipientCity] nvarchar(250)  NULL,
    [RecipientState] nvarchar(10)  NULL,
    [RecipientPostalCode] nvarchar(10)  NULL,
    [RecipientCountryCode] nvarchar(10)  NULL,
    [NumberOfCopies] nvarchar(10)  NULL,
    [Comments] nvarchar(max)  NULL,
    [RequestHoldCode] nvarchar(20)  NULL,
    [TranscriptGrouping] nvarchar(10)  NULL,
    [ErrorOccured] nvarchar(max)  NULL,
    [ErrorMessage] nvarchar(max)  NULL,
    [StudentRequestLogsId] nvarchar(20)  NULL,
    [CompletedDateTime] datetime  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifiedDateTime] datetime  NULL
);
GO

-- Creating table 'TransactionCourses'
CREATE TABLE [dbo].[TransactionCourses] (
    [TransactionCourseId] int IDENTITY(1,1) NOT NULL,
    [AExternalTranscriptsId] nvarchar(max)  NULL,
    [AExtlPersonId] nvarchar(max)  NULL,
    [AExtlInstitution] nvarchar(max)  NULL,
    [AExtlCourse] nvarchar(max)  NULL,
    [AExtlTitle] nvarchar(max)  NULL,
    [AExtlCred] nvarchar(max)  NULL,
    [AExtlGrade] nvarchar(max)  NULL,
    [AExtlGradeScheme] nvarchar(max)  NULL,
    [AExtlStartDate] nvarchar(max)  NULL,
    [AExtlEndDate] nvarchar(max)  NULL,
    [AExtlOpr] nvarchar(max)  NULL,
    [AExtlPersInstIdx] nvarchar(max)  NULL,
    [Result] nvarchar(max)  NULL,
    [CompletedDateTime] datetime  NULL,
    [CreatedDateTime] datetime  NULL,
    [ModifiedDateTime] datetime  NULL
);
GO

-- Creating table 'sNumbers'
CREATE TABLE [dbo].[sNumbers] (
    [sNumId] int IDENTITY(1,1) NOT NULL,
    [sNumVal] nvarchar(10)  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NULL
);
GO

-- Creating table 'StudentAlias'
CREATE TABLE [dbo].[StudentAlias] (
    [StudentAliasId] int IDENTITY(1,1) NOT NULL,
    [AliasActive] bit  NOT NULL,
    [AliasStatusType] int  NULL,
    [AliasNumber] nchar(10)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Student_StudentId] int  NOT NULL
);
GO

-- Creating table 'TranscriptRestrictions'
CREATE TABLE [dbo].[TranscriptRestrictions] (
    [TranscriptRestrictionId] int IDENTITY(1,1) NOT NULL,
    [TranscriptRestrictionCode] nvarchar(max)  NOT NULL,
    [TranscriptRestrictionDesc] nvarchar(max)  NOT NULL,
    [IsRule] bit  NULL,
    [Active] bit  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Histories'
CREATE TABLE [dbo].[Histories] (
    [HistoryId] int IDENTITY(1,1) NOT NULL,
    [User_UserId] int  NOT NULL,
    [ActionType_ActionId] int  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'TranscriptTermDescs'
CREATE TABLE [dbo].[TranscriptTermDescs] (
    [TranscriptTermDescId] int IDENTITY(1,1) NOT NULL,
    [TermCode] nvarchar(15)  NOT NULL,
    [TermDesc] nvarchar(50)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'AdmissionOutstandingRequests'
CREATE TABLE [dbo].[AdmissionOutstandingRequests] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [ASN] nvarchar(30)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [SenderId] nvarchar(max)  NULL,
    [ReceiverId] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'RecordsOutstandingRequests'
CREATE TABLE [dbo].[RecordsOutstandingRequests] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [ASN] nvarchar(30)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [SenderId] nvarchar(max)  NULL,
    [ReceiverId] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'RecordsHoldResponses'
CREATE TABLE [dbo].[RecordsHoldResponses] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [ASN] nvarchar(30)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [SenderId] nvarchar(max)  NULL,
    [ReceiverId] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'AdmissionOfflineSearchResponses'
CREATE TABLE [dbo].[AdmissionOfflineSearchResponses] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [ASN] nvarchar(30)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [SenderId] nvarchar(max)  NULL,
    [ReceiverId] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL
);
GO

-- Creating table 'Requests_Alberta_Education'
CREATE TABLE [dbo].[Requests_Alberta_Education] (
    [RequestId] int IDENTITY(1,1) NOT NULL,
    [SentDateTime] datetime  NULL,
    [ReceivedDateTime] datetime  NULL,
    [ViewedDateTime] datetime  NULL,
    [SentToColleagueTRRQ] datetime  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [RequestorInstitution_InstitutionId] int  NULL,
    [TransmissionData_TransmissionDataId] int  NOT NULL,
    [Student_StudentId] int  NOT NULL
);
GO

-- Creating table 'BulkSendTranscripts'
CREATE TABLE [dbo].[BulkSendTranscripts] (
    [BulkSendTranscriptId] int IDENTITY(1,1) NOT NULL,
    [sNumber] nvarchar(10)  NOT NULL,
    [SentDateTime] datetime  NULL,
    [StudentRestriction] bit  NOT NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Institution_InstitutionId] int  NULL,
    [Student_StudentId] int  NULL
);
GO

-- Creating table 'SentEmails'
CREATE TABLE [dbo].[SentEmails] (
    [SentEmailId] int IDENTITY(1,1) NOT NULL,
    [EmailType] nvarchar(30)  NULL,
    [From] nvarchar(50)  NOT NULL,
    [To] nvarchar(50)  NOT NULL,
    [Subject] nvarchar(100)  NULL,
    [Body] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [Email_EmailId] int  NULL
);
GO

-- Creating table 'ReportQueries'
CREATE TABLE [dbo].[ReportQueries] (
    [ReportQueryId] int IDENTITY(1,1) NOT NULL,
    [ReportName] nvarchar(max)  NOT NULL,
    [QueryString] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MyCredsMessages'
CREATE TABLE [dbo].[MyCredsMessages] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [UUID] nvarchar(36)  NULL,
    [sNumber] nvarchar(10)  NULL,
    [MessageType] int  NOT NULL,
    [MessageXML] nvarchar(max)  NULL,
    [CreatedBy] nvarchar(10)  NOT NULL,
    [CreatedDateTime] datetime  NOT NULL,
    [ModifiedBy] nvarchar(10)  NOT NULL,
    [ModifiedDateTime] datetime  NOT NULL,
    [FullName] nvarchar(150)  NULL,
    [Email] nvarchar(150)  NULL,
    [BatchCode] nvarchar(50)  NULL
);
GO

-- Creating table 'AccessGroupUser'
CREATE TABLE [dbo].[AccessGroupUser] (
    [AccessGroups_AccessGroupId] int  NOT NULL,
    [Users_UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AcademicAwardId] in table 'AcademicAwards'
ALTER TABLE [dbo].[AcademicAwards]
ADD CONSTRAINT [PK_AcademicAwards]
    PRIMARY KEY CLUSTERED ([AcademicAwardId] ASC);
GO

-- Creating primary key on [AcademicRecordId] in table 'AcademicRecords'
ALTER TABLE [dbo].[AcademicRecords]
ADD CONSTRAINT [PK_AcademicRecords]
    PRIMARY KEY CLUSTERED ([AcademicRecordId] ASC);
GO

-- Creating primary key on [AcademicSessionId] in table 'AcademicSessions'
ALTER TABLE [dbo].[AcademicSessions]
ADD CONSTRAINT [PK_AcademicSessions]
    PRIMARY KEY CLUSTERED ([AcademicSessionId] ASC);
GO

-- Creating primary key on [AccessGroupId] in table 'AccessGroups'
ALTER TABLE [dbo].[AccessGroups]
ADD CONSTRAINT [PK_AccessGroups]
    PRIMARY KEY CLUSTERED ([AccessGroupId] ASC);
GO

-- Creating primary key on [AcknowledgementId] in table 'Acknowledgements'
ALTER TABLE [dbo].[Acknowledgements]
ADD CONSTRAINT [PK_Acknowledgements]
    PRIMARY KEY CLUSTERED ([AcknowledgementId] ASC);
GO

-- Creating primary key on [ActionId] in table 'ActionTypes'
ALTER TABLE [dbo].[ActionTypes]
ADD CONSTRAINT [PK_ActionTypes]
    PRIMARY KEY CLUSTERED ([ActionId] ASC);
GO

-- Creating primary key on [AddressId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([AddressId] ASC);
GO

-- Creating primary key on [ApasCodeId] in table 'ApasCodes'
ALTER TABLE [dbo].[ApasCodes]
ADD CONSTRAINT [PK_ApasCodes]
    PRIMARY KEY CLUSTERED ([ApasCodeId] ASC);
GO

-- Creating primary key on [MessageId] in table 'ApasMessages'
ALTER TABLE [dbo].[ApasMessages]
ADD CONSTRAINT [PK_ApasMessages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [ApplicationFeeId] in table 'ApplicationFees'
ALTER TABLE [dbo].[ApplicationFees]
ADD CONSTRAINT [PK_ApplicationFees]
    PRIMARY KEY CLUSTERED ([ApplicationFeeId] ASC);
GO

-- Creating primary key on [ApplicationMessageId] in table 'ApplicationMessages'
ALTER TABLE [dbo].[ApplicationMessages]
ADD CONSTRAINT [PK_ApplicationMessages]
    PRIMARY KEY CLUSTERED ([ApplicationMessageId] ASC);
GO

-- Creating primary key on [ApplicationProgramId] in table 'ApplicationPrograms'
ALTER TABLE [dbo].[ApplicationPrograms]
ADD CONSTRAINT [PK_ApplicationPrograms]
    PRIMARY KEY CLUSTERED ([ApplicationProgramId] ASC);
GO

-- Creating primary key on [ASNId] in table 'ASNs'
ALTER TABLE [dbo].[ASNs]
ADD CONSTRAINT [PK_ASNs]
    PRIMARY KEY CLUSTERED ([ASNId] ASC);
GO

-- Creating primary key on [BooleanValueId] in table 'BooleanValues'
ALTER TABLE [dbo].[BooleanValues]
ADD CONSTRAINT [PK_BooleanValues]
    PRIMARY KEY CLUSTERED ([BooleanValueId] ASC);
GO

-- Creating primary key on [CodeTypeId] in table 'CodeTypes'
ALTER TABLE [dbo].[CodeTypes]
ADD CONSTRAINT [PK_CodeTypes]
    PRIMARY KEY CLUSTERED ([CodeTypeId] ASC);
GO

-- Creating primary key on [ContactEmailId] in table 'ContactEmails'
ALTER TABLE [dbo].[ContactEmails]
ADD CONSTRAINT [PK_ContactEmails]
    PRIMARY KEY CLUSTERED ([ContactEmailId] ASC);
GO

-- Creating primary key on [ContactPersonId] in table 'ContactPersons'
ALTER TABLE [dbo].[ContactPersons]
ADD CONSTRAINT [PK_ContactPersons]
    PRIMARY KEY CLUSTERED ([ContactPersonId] ASC);
GO

-- Creating primary key on [ContactPhoneId] in table 'ContactPhones'
ALTER TABLE [dbo].[ContactPhones]
ADD CONSTRAINT [PK_ContactPhones]
    PRIMARY KEY CLUSTERED ([ContactPhoneId] ASC);
GO

-- Creating primary key on [ControllerId] in table 'ControllerTypes'
ALTER TABLE [dbo].[ControllerTypes]
ADD CONSTRAINT [PK_ControllerTypes]
    PRIMARY KEY CLUSTERED ([ControllerId] ASC);
GO

-- Creating primary key on [CourseId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseId] ASC);
GO

-- Creating primary key on [CourseTitleId] in table 'CourseTitles'
ALTER TABLE [dbo].[CourseTitles]
ADD CONSTRAINT [PK_CourseTitles]
    PRIMARY KEY CLUSTERED ([CourseTitleId] ASC);
GO

-- Creating primary key on [DateTimeValueId] in table 'DateTimeValues'
ALTER TABLE [dbo].[DateTimeValues]
ADD CONSTRAINT [PK_DateTimeValues]
    PRIMARY KEY CLUSTERED ([DateTimeValueId] ASC);
GO

-- Creating primary key on [DoubleValueId] in table 'DoubleValues'
ALTER TABLE [dbo].[DoubleValues]
ADD CONSTRAINT [PK_DoubleValues]
    PRIMARY KEY CLUSTERED ([DoubleValueId] ASC);
GO

-- Creating primary key on [EmailId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [PK_Emails]
    PRIMARY KEY CLUSTERED ([EmailId] ASC);
GO

-- Creating primary key on [StatusTrackingId] in table 'ExceptionRecords'
ALTER TABLE [dbo].[ExceptionRecords]
ADD CONSTRAINT [PK_ExceptionRecords]
    PRIMARY KEY CLUSTERED ([StatusTrackingId] ASC);
GO

-- Creating primary key on [InstitutionNameId] in table 'InstitutionNames'
ALTER TABLE [dbo].[InstitutionNames]
ADD CONSTRAINT [PK_InstitutionNames]
    PRIMARY KEY CLUSTERED ([InstitutionNameId] ASC);
GO

-- Creating primary key on [InstitutionId] in table 'Institutions'
ALTER TABLE [dbo].[Institutions]
ADD CONSTRAINT [PK_Institutions]
    PRIMARY KEY CLUSTERED ([InstitutionId] ASC);
GO

-- Creating primary key on [IntegerValueId] in table 'IntegerValues'
ALTER TABLE [dbo].[IntegerValues]
ADD CONSTRAINT [PK_IntegerValues]
    PRIMARY KEY CLUSTERED ([IntegerValueId] ASC);
GO

-- Creating primary key on [KeyValueTempCacheId] in table 'KeyValueTempCaches'
ALTER TABLE [dbo].[KeyValueTempCaches]
ADD CONSTRAINT [PK_KeyValueTempCaches]
    PRIMARY KEY CLUSTERED ([KeyValueTempCacheId] ASC);
GO

-- Creating primary key on [PayPalResponseId] in table 'PayPalResponses'
ALTER TABLE [dbo].[PayPalResponses]
ADD CONSTRAINT [PK_PayPalResponses]
    PRIMARY KEY CLUSTERED ([PayPalResponseId] ASC);
GO

-- Creating primary key on [PermissionRecordId] in table 'PermissionRecords'
ALTER TABLE [dbo].[PermissionRecords]
ADD CONSTRAINT [PK_PermissionRecords]
    PRIMARY KEY CLUSTERED ([PermissionRecordId] ASC);
GO

-- Creating primary key on [NameId] in table 'PersonNames'
ALTER TABLE [dbo].[PersonNames]
ADD CONSTRAINT [PK_PersonNames]
    PRIMARY KEY CLUSTERED ([NameId] ASC);
GO

-- Creating primary key on [PersonId] in table 'Persons'
ALTER TABLE [dbo].[Persons]
ADD CONSTRAINT [PK_Persons]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- Creating primary key on [PhoneId] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [PK_Phones]
    PRIMARY KEY CLUSTERED ([PhoneId] ASC);
GO

-- Creating primary key on [ProgramCampusId] in table 'ProgramCampuses'
ALTER TABLE [dbo].[ProgramCampuses]
ADD CONSTRAINT [PK_ProgramCampuses]
    PRIMARY KEY CLUSTERED ([ProgramCampusId] ASC);
GO

-- Creating primary key on [ProgramDetailsId] in table 'ProgramDetails'
ALTER TABLE [dbo].[ProgramDetails]
ADD CONSTRAINT [PK_ProgramDetails]
    PRIMARY KEY CLUSTERED ([ProgramDetailsId] ASC);
GO

-- Creating primary key on [ProgramTermId] in table 'ProgramTerms'
ALTER TABLE [dbo].[ProgramTerms]
ADD CONSTRAINT [PK_ProgramTerms]
    PRIMARY KEY CLUSTERED ([ProgramTermId] ASC);
GO

-- Creating primary key on [ReceivedErrorId] in table 'ReceivedErrors'
ALTER TABLE [dbo].[ReceivedErrors]
ADD CONSTRAINT [PK_ReceivedErrors]
    PRIMARY KEY CLUSTERED ([ReceivedErrorId] ASC);
GO

-- Creating primary key on [RecipientId] in table 'Recipients'
ALTER TABLE [dbo].[Recipients]
ADD CONSTRAINT [PK_Recipients]
    PRIMARY KEY CLUSTERED ([RecipientId] ASC);
GO

-- Creating primary key on [ReferenceTypeId] in table 'ReferenceTypes'
ALTER TABLE [dbo].[ReferenceTypes]
ADD CONSTRAINT [PK_ReferenceTypes]
    PRIMARY KEY CLUSTERED ([ReferenceTypeId] ASC);
GO

-- Creating primary key on [RequestId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([RequestId] ASC);
GO

-- Creating primary key on [ResponseStatusId] in table 'ResponseStatuses'
ALTER TABLE [dbo].[ResponseStatuses]
ADD CONSTRAINT [PK_ResponseStatuses]
    PRIMARY KEY CLUSTERED ([ResponseStatusId] ASC);
GO

-- Creating primary key on [ResponseHoldTypeId] in table 'ResponseHolds'
ALTER TABLE [dbo].[ResponseHolds]
ADD CONSTRAINT [PK_ResponseHolds]
    PRIMARY KEY CLUSTERED ([ResponseHoldTypeId] ASC);
GO

-- Creating primary key on [ResponseId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [PK_Responses]
    PRIMARY KEY CLUSTERED ([ResponseId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'SettingCategories'
ALTER TABLE [dbo].[SettingCategories]
ADD CONSTRAINT [PK_SettingCategories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [SettingsId] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([SettingsId] ASC);
GO

-- Creating primary key on [ShortStringValueId] in table 'ShortStringValues'
ALTER TABLE [dbo].[ShortStringValues]
ADD CONSTRAINT [PK_ShortStringValues]
    PRIMARY KEY CLUSTERED ([ShortStringValueId] ASC);
GO

-- Creating primary key on [StatusId] in table 'Statuses'
ALTER TABLE [dbo].[Statuses]
ADD CONSTRAINT [PK_Statuses]
    PRIMARY KEY CLUSTERED ([StatusId] ASC);
GO

-- Creating primary key on [StudentApplicationId] in table 'StudentApplications'
ALTER TABLE [dbo].[StudentApplications]
ADD CONSTRAINT [PK_StudentApplications]
    PRIMARY KEY CLUSTERED ([StudentApplicationId] ASC);
GO

-- Creating primary key on [StudentId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [PK_Students]
    PRIMARY KEY CLUSTERED ([StudentId] ASC);
GO

-- Creating primary key on [SvcJobId] in table 'SvcJobs'
ALTER TABLE [dbo].[SvcJobs]
ADD CONSTRAINT [PK_SvcJobs]
    PRIMARY KEY CLUSTERED ([SvcJobId] ASC);
GO

-- Creating primary key on [SvcLogId] in table 'SvcLogs'
ALTER TABLE [dbo].[SvcLogs]
ADD CONSTRAINT [PK_SvcLogs]
    PRIMARY KEY CLUSTERED ([SvcLogId] ASC);
GO

-- Creating primary key on [SvcSchedId] in table 'SvcScheds'
ALTER TABLE [dbo].[SvcScheds]
ADD CONSTRAINT [PK_SvcScheds]
    PRIMARY KEY CLUSTERED ([SvcSchedId] ASC);
GO

-- Creating primary key on [TranscriptHoldID] in table 'TranscriptHolds'
ALTER TABLE [dbo].[TranscriptHolds]
ADD CONSTRAINT [PK_TranscriptHolds]
    PRIMARY KEY CLUSTERED ([TranscriptHoldID] ASC);
GO

-- Creating primary key on [TranscriptId] in table 'Transcripts'
ALTER TABLE [dbo].[Transcripts]
ADD CONSTRAINT [PK_Transcripts]
    PRIMARY KEY CLUSTERED ([TranscriptId] ASC);
GO

-- Creating primary key on [TransmissionDataId] in table 'TransmissionDatas'
ALTER TABLE [dbo].[TransmissionDatas]
ADD CONSTRAINT [PK_TransmissionDatas]
    PRIMARY KEY CLUSTERED ([TransmissionDataId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ApasCodeId], [ValCodeId], [CodeTypeId] in table 'ValCodeLookups'
ALTER TABLE [dbo].[ValCodeLookups]
ADD CONSTRAINT [PK_ValCodeLookups]
    PRIMARY KEY CLUSTERED ([ApasCodeId], [ValCodeId], [CodeTypeId] ASC);
GO

-- Creating primary key on [ValCodeId] in table 'ValCodes'
ALTER TABLE [dbo].[ValCodes]
ADD CONSTRAINT [PK_ValCodes]
    PRIMARY KEY CLUSTERED ([ValCodeId] ASC);
GO

-- Creating primary key on [CitizenshipId] in table 'Citizenships'
ALTER TABLE [dbo].[Citizenships]
ADD CONSTRAINT [PK_Citizenships]
    PRIMARY KEY CLUSTERED ([CitizenshipId] ASC);
GO

-- Creating primary key on [ResidencyId] in table 'Residencies'
ALTER TABLE [dbo].[Residencies]
ADD CONSTRAINT [PK_Residencies]
    PRIMARY KEY CLUSTERED ([ResidencyId] ASC);
GO

-- Creating primary key on [GenderId] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([GenderId] ASC);
GO

-- Creating primary key on [DisabilityId] in table 'Disabilities'
ALTER TABLE [dbo].[Disabilities]
ADD CONSTRAINT [PK_Disabilities]
    PRIMARY KEY CLUSTERED ([DisabilityId] ASC);
GO

-- Creating primary key on [ImmigrationId] in table 'Immigrations'
ALTER TABLE [dbo].[Immigrations]
ADD CONSTRAINT [PK_Immigrations]
    PRIMARY KEY CLUSTERED ([ImmigrationId] ASC);
GO

-- Creating primary key on [JobId] in table 'TranscriptJobs'
ALTER TABLE [dbo].[TranscriptJobs]
ADD CONSTRAINT [PK_TranscriptJobs]
    PRIMARY KEY CLUSTERED ([JobId] ASC);
GO

-- Creating primary key on [RequestorReceiverId] in table 'RequestorReceivers'
ALTER TABLE [dbo].[RequestorReceivers]
ADD CONSTRAINT [PK_RequestorReceivers]
    PRIMARY KEY CLUSTERED ([RequestorReceiverId] ASC);
GO

-- Creating primary key on [NoteMessageId] in table 'NoteMessages'
ALTER TABLE [dbo].[NoteMessages]
ADD CONSTRAINT [PK_NoteMessages]
    PRIMARY KEY CLUSTERED ([NoteMessageId] ASC);
GO

-- Creating primary key on [TransactionTranscriptId] in table 'TransactionTranscripts'
ALTER TABLE [dbo].[TransactionTranscripts]
ADD CONSTRAINT [PK_TransactionTranscripts]
    PRIMARY KEY CLUSTERED ([TransactionTranscriptId] ASC);
GO

-- Creating primary key on [TransactionRequestLogId] in table 'TransactionRequestLogs'
ALTER TABLE [dbo].[TransactionRequestLogs]
ADD CONSTRAINT [PK_TransactionRequestLogs]
    PRIMARY KEY CLUSTERED ([TransactionRequestLogId] ASC);
GO

-- Creating primary key on [TransactionCourseId] in table 'TransactionCourses'
ALTER TABLE [dbo].[TransactionCourses]
ADD CONSTRAINT [PK_TransactionCourses]
    PRIMARY KEY CLUSTERED ([TransactionCourseId] ASC);
GO

-- Creating primary key on [sNumId] in table 'sNumbers'
ALTER TABLE [dbo].[sNumbers]
ADD CONSTRAINT [PK_sNumbers]
    PRIMARY KEY CLUSTERED ([sNumId] ASC);
GO

-- Creating primary key on [StudentAliasId] in table 'StudentAlias'
ALTER TABLE [dbo].[StudentAlias]
ADD CONSTRAINT [PK_StudentAlias]
    PRIMARY KEY CLUSTERED ([StudentAliasId] ASC);
GO

-- Creating primary key on [TranscriptRestrictionId] in table 'TranscriptRestrictions'
ALTER TABLE [dbo].[TranscriptRestrictions]
ADD CONSTRAINT [PK_TranscriptRestrictions]
    PRIMARY KEY CLUSTERED ([TranscriptRestrictionId] ASC);
GO

-- Creating primary key on [HistoryId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [PK_Histories]
    PRIMARY KEY CLUSTERED ([HistoryId] ASC);
GO

-- Creating primary key on [TranscriptTermDescId] in table 'TranscriptTermDescs'
ALTER TABLE [dbo].[TranscriptTermDescs]
ADD CONSTRAINT [PK_TranscriptTermDescs]
    PRIMARY KEY CLUSTERED ([TranscriptTermDescId] ASC);
GO

-- Creating primary key on [MessageId] in table 'AdmissionOutstandingRequests'
ALTER TABLE [dbo].[AdmissionOutstandingRequests]
ADD CONSTRAINT [PK_AdmissionOutstandingRequests]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [MessageId] in table 'RecordsOutstandingRequests'
ALTER TABLE [dbo].[RecordsOutstandingRequests]
ADD CONSTRAINT [PK_RecordsOutstandingRequests]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [MessageId] in table 'RecordsHoldResponses'
ALTER TABLE [dbo].[RecordsHoldResponses]
ADD CONSTRAINT [PK_RecordsHoldResponses]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [MessageId] in table 'AdmissionOfflineSearchResponses'
ALTER TABLE [dbo].[AdmissionOfflineSearchResponses]
ADD CONSTRAINT [PK_AdmissionOfflineSearchResponses]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [RequestId], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime], [TransmissionData_TransmissionDataId], [Student_StudentId] in table 'Requests_Alberta_Education'
ALTER TABLE [dbo].[Requests_Alberta_Education]
ADD CONSTRAINT [PK_Requests_Alberta_Education]
    PRIMARY KEY CLUSTERED ([RequestId], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime], [TransmissionData_TransmissionDataId], [Student_StudentId] ASC);
GO

-- Creating primary key on [BulkSendTranscriptId] in table 'BulkSendTranscripts'
ALTER TABLE [dbo].[BulkSendTranscripts]
ADD CONSTRAINT [PK_BulkSendTranscripts]
    PRIMARY KEY CLUSTERED ([BulkSendTranscriptId] ASC);
GO

-- Creating primary key on [SentEmailId] in table 'SentEmails'
ALTER TABLE [dbo].[SentEmails]
ADD CONSTRAINT [PK_SentEmails]
    PRIMARY KEY CLUSTERED ([SentEmailId] ASC);
GO

-- Creating primary key on [ReportQueryId] in table 'ReportQueries'
ALTER TABLE [dbo].[ReportQueries]
ADD CONSTRAINT [PK_ReportQueries]
    PRIMARY KEY CLUSTERED ([ReportQueryId] ASC);
GO

-- Creating primary key on [MessageId] in table 'MyCredsMessages'
ALTER TABLE [dbo].[MyCredsMessages]
ADD CONSTRAINT [PK_MyCredsMessages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [AccessGroups_AccessGroupId], [Users_UserId] in table 'AccessGroupUser'
ALTER TABLE [dbo].[AccessGroupUser]
ADD CONSTRAINT [PK_AccessGroupUser]
    PRIMARY KEY CLUSTERED ([AccessGroups_AccessGroupId], [Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AcademicRecord_AcademicRecordId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_AcademicRecordCourses]
    FOREIGN KEY ([AcademicRecord_AcademicRecordId])
    REFERENCES [dbo].[AcademicRecords]
        ([AcademicRecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicRecordCourses'
CREATE INDEX [IX_FK_AcademicRecordCourses]
ON [dbo].[Courses]
    ([AcademicRecord_AcademicRecordId]);
GO

-- Creating foreign key on [School_InstitutionId] in table 'AcademicRecords'
ALTER TABLE [dbo].[AcademicRecords]
ADD CONSTRAINT [FK_InstitutionAcademicRecord]
    FOREIGN KEY ([School_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionAcademicRecord'
CREATE INDEX [IX_FK_InstitutionAcademicRecord]
ON [dbo].[AcademicRecords]
    ([School_InstitutionId]);
GO

-- Creating foreign key on [Response_ResponseId] in table 'AcademicRecords'
ALTER TABLE [dbo].[AcademicRecords]
ADD CONSTRAINT [FK_ResponseAcademicRecord]
    FOREIGN KEY ([Response_ResponseId])
    REFERENCES [dbo].[Responses]
        ([ResponseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseAcademicRecord'
CREATE INDEX [IX_FK_ResponseAcademicRecord]
ON [dbo].[AcademicRecords]
    ([Response_ResponseId]);
GO

-- Creating foreign key on [AccessGroup_AccessGroupId] in table 'PermissionRecords'
ALTER TABLE [dbo].[PermissionRecords]
ADD CONSTRAINT [FK_AccessGroupPermissionRecord]
    FOREIGN KEY ([AccessGroup_AccessGroupId])
    REFERENCES [dbo].[AccessGroups]
        ([AccessGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessGroupPermissionRecord'
CREATE INDEX [IX_FK_AccessGroupPermissionRecord]
ON [dbo].[PermissionRecords]
    ([AccessGroup_AccessGroupId]);
GO

-- Creating foreign key on [ActionType_ActionId] in table 'PermissionRecords'
ALTER TABLE [dbo].[PermissionRecords]
ADD CONSTRAINT [FK_ActionTypePermissionRecord]
    FOREIGN KEY ([ActionType_ActionId])
    REFERENCES [dbo].[ActionTypes]
        ([ActionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionTypePermissionRecord'
CREATE INDEX [IX_FK_ActionTypePermissionRecord]
ON [dbo].[PermissionRecords]
    ([ActionType_ActionId]);
GO

-- Creating foreign key on [ControllerType_ControllerId] in table 'ActionTypes'
ALTER TABLE [dbo].[ActionTypes]
ADD CONSTRAINT [FK_ControllerTypeActionType]
    FOREIGN KEY ([ControllerType_ControllerId])
    REFERENCES [dbo].[ControllerTypes]
        ([ControllerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControllerTypeActionType'
CREATE INDEX [IX_FK_ControllerTypeActionType]
ON [dbo].[ActionTypes]
    ([ControllerType_ControllerId]);
GO

-- Creating foreign key on [InstitutionId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_InstitutionAddress]
    FOREIGN KEY ([InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionAddress'
CREATE INDEX [IX_FK_InstitutionAddress]
ON [dbo].[Addresses]
    ([InstitutionId]);
GO

-- Creating foreign key on [PersonId] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [FK_PersonAddress]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAddress'
CREATE INDEX [IX_FK_PersonAddress]
ON [dbo].[Addresses]
    ([PersonId]);
GO

-- Creating foreign key on [ApasCodeId] in table 'ValCodeLookups'
ALTER TABLE [dbo].[ValCodeLookups]
ADD CONSTRAINT [FK_ApasCodeValCodeLookup]
    FOREIGN KEY ([ApasCodeId])
    REFERENCES [dbo].[ApasCodes]
        ([ApasCodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ApplicationFee_ApplicationFeeId] in table 'StudentApplications'
ALTER TABLE [dbo].[StudentApplications]
ADD CONSTRAINT [FK_ApplicationFeeStudentApplication]
    FOREIGN KEY ([ApplicationFee_ApplicationFeeId])
    REFERENCES [dbo].[ApplicationFees]
        ([ApplicationFeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationFeeStudentApplication'
CREATE INDEX [IX_FK_ApplicationFeeStudentApplication]
ON [dbo].[StudentApplications]
    ([ApplicationFee_ApplicationFeeId]);
GO

-- Creating foreign key on [ApplicationMessage_ApplicationMessageId] in table 'PayPalResponses'
ALTER TABLE [dbo].[PayPalResponses]
ADD CONSTRAINT [FK_ApplicationMessagePayPalResponse]
    FOREIGN KEY ([ApplicationMessage_ApplicationMessageId])
    REFERENCES [dbo].[ApplicationMessages]
        ([ApplicationMessageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationMessagePayPalResponse'
CREATE INDEX [IX_FK_ApplicationMessagePayPalResponse]
ON [dbo].[PayPalResponses]
    ([ApplicationMessage_ApplicationMessageId]);
GO

-- Creating foreign key on [SourceInstitutionId] in table 'ApplicationMessages'
ALTER TABLE [dbo].[ApplicationMessages]
ADD CONSTRAINT [FK_InstitutionReceivedApplications]
    FOREIGN KEY ([SourceInstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionReceivedApplications'
CREATE INDEX [IX_FK_InstitutionReceivedApplications]
ON [dbo].[ApplicationMessages]
    ([SourceInstitutionId]);
GO

-- Creating foreign key on [DestinationInstitution_InstitutionId] in table 'ApplicationMessages'
ALTER TABLE [dbo].[ApplicationMessages]
ADD CONSTRAINT [FK_InstitutionSentApplications]
    FOREIGN KEY ([DestinationInstitution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionSentApplications'
CREATE INDEX [IX_FK_InstitutionSentApplications]
ON [dbo].[ApplicationMessages]
    ([DestinationInstitution_InstitutionId]);
GO

-- Creating foreign key on [StudentApplication_StudentApplicationId] in table 'ApplicationMessages'
ALTER TABLE [dbo].[ApplicationMessages]
ADD CONSTRAINT [FK_StudentApplicationApplicationMessage]
    FOREIGN KEY ([StudentApplication_StudentApplicationId])
    REFERENCES [dbo].[StudentApplications]
        ([StudentApplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentApplicationApplicationMessage'
CREATE INDEX [IX_FK_StudentApplicationApplicationMessage]
ON [dbo].[ApplicationMessages]
    ([StudentApplication_StudentApplicationId]);
GO

-- Creating foreign key on [ApplicationProgram_ApplicationProgramId] in table 'ProgramDetails'
ALTER TABLE [dbo].[ProgramDetails]
ADD CONSTRAINT [FK_ApplicationProgramProgramDetails]
    FOREIGN KEY ([ApplicationProgram_ApplicationProgramId])
    REFERENCES [dbo].[ApplicationPrograms]
        ([ApplicationProgramId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationProgramProgramDetails'
CREATE INDEX [IX_FK_ApplicationProgramProgramDetails]
ON [dbo].[ProgramDetails]
    ([ApplicationProgram_ApplicationProgramId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'ASNs'
ALTER TABLE [dbo].[ASNs]
ADD CONSTRAINT [FK_StudentASN]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentASN'
CREATE INDEX [IX_FK_StudentASN]
ON [dbo].[ASNs]
    ([Student_StudentId]);
GO

-- Creating foreign key on [Setting_SettingsId] in table 'BooleanValues'
ALTER TABLE [dbo].[BooleanValues]
ADD CONSTRAINT [FK_SettingBooleanValue]
    FOREIGN KEY ([Setting_SettingsId])
    REFERENCES [dbo].[Settings]
        ([SettingsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingBooleanValue'
CREATE INDEX [IX_FK_SettingBooleanValue]
ON [dbo].[BooleanValues]
    ([Setting_SettingsId]);
GO

-- Creating foreign key on [CodeTypeId] in table 'ValCodeLookups'
ALTER TABLE [dbo].[ValCodeLookups]
ADD CONSTRAINT [FK_CodeTypeValCodeLookup]
    FOREIGN KEY ([CodeTypeId])
    REFERENCES [dbo].[CodeTypes]
        ([CodeTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CodeTypeValCodeLookup'
CREATE INDEX [IX_FK_CodeTypeValCodeLookup]
ON [dbo].[ValCodeLookups]
    ([CodeTypeId]);
GO

-- Creating foreign key on [ContactPerson_ContactPersonId] in table 'ContactEmails'
ALTER TABLE [dbo].[ContactEmails]
ADD CONSTRAINT [FK_ContactPersonContactEmail]
    FOREIGN KEY ([ContactPerson_ContactPersonId])
    REFERENCES [dbo].[ContactPersons]
        ([ContactPersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactPersonContactEmail'
CREATE INDEX [IX_FK_ContactPersonContactEmail]
ON [dbo].[ContactEmails]
    ([ContactPerson_ContactPersonId]);
GO

-- Creating foreign key on [ContactPerson_ContactPersonId] in table 'ContactPhones'
ALTER TABLE [dbo].[ContactPhones]
ADD CONSTRAINT [FK_ContactPersonContactPhone]
    FOREIGN KEY ([ContactPerson_ContactPersonId])
    REFERENCES [dbo].[ContactPersons]
        ([ContactPersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactPersonContactPhone'
CREATE INDEX [IX_FK_ContactPersonContactPhone]
ON [dbo].[ContactPhones]
    ([ContactPerson_ContactPersonId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'ContactPersons'
ALTER TABLE [dbo].[ContactPersons]
ADD CONSTRAINT [FK_StudentContactPersons]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentContactPersons'
CREATE INDEX [IX_FK_StudentContactPersons]
ON [dbo].[ContactPersons]
    ([Student_StudentId]);
GO

-- Creating foreign key on [TranscriptHold_TranscriptHoldID] in table 'CourseTitles'
ALTER TABLE [dbo].[CourseTitles]
ADD CONSTRAINT [FK_TranscriptHoldCourseTitle]
    FOREIGN KEY ([TranscriptHold_TranscriptHoldID])
    REFERENCES [dbo].[TranscriptHolds]
        ([TranscriptHoldID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TranscriptHoldCourseTitle'
CREATE INDEX [IX_FK_TranscriptHoldCourseTitle]
ON [dbo].[CourseTitles]
    ([TranscriptHold_TranscriptHoldID]);
GO

-- Creating foreign key on [Setting_SettingsId] in table 'DateTimeValues'
ALTER TABLE [dbo].[DateTimeValues]
ADD CONSTRAINT [FK_SettingDateTimeValue]
    FOREIGN KEY ([Setting_SettingsId])
    REFERENCES [dbo].[Settings]
        ([SettingsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingDateTimeValue'
CREATE INDEX [IX_FK_SettingDateTimeValue]
ON [dbo].[DateTimeValues]
    ([Setting_SettingsId]);
GO

-- Creating foreign key on [Setting_SettingsId] in table 'DoubleValues'
ALTER TABLE [dbo].[DoubleValues]
ADD CONSTRAINT [FK_SettingDoubleValue]
    FOREIGN KEY ([Setting_SettingsId])
    REFERENCES [dbo].[Settings]
        ([SettingsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingDoubleValue'
CREATE INDEX [IX_FK_SettingDoubleValue]
ON [dbo].[DoubleValues]
    ([Setting_SettingsId]);
GO

-- Creating foreign key on [InstitutionId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_InstitutionEmail]
    FOREIGN KEY ([InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionEmail'
CREATE INDEX [IX_FK_InstitutionEmail]
ON [dbo].[Emails]
    ([InstitutionId]);
GO

-- Creating foreign key on [PersonId] in table 'Emails'
ALTER TABLE [dbo].[Emails]
ADD CONSTRAINT [FK_PersonEmail]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmail'
CREATE INDEX [IX_FK_PersonEmail]
ON [dbo].[Emails]
    ([PersonId]);
GO

-- Creating foreign key on [Institution_InstitutionId] in table 'InstitutionNames'
ALTER TABLE [dbo].[InstitutionNames]
ADD CONSTRAINT [FK_InstitutionInstitutionName]
    FOREIGN KEY ([Institution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionInstitutionName'
CREATE INDEX [IX_FK_InstitutionInstitutionName]
ON [dbo].[InstitutionNames]
    ([Institution_InstitutionId]);
GO

-- Creating foreign key on [DestinationInstitution_InstitutionId] in table 'TransmissionDatas'
ALTER TABLE [dbo].[TransmissionDatas]
ADD CONSTRAINT [FK_DestinationInstitutionTransmissionData]
    FOREIGN KEY ([DestinationInstitution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DestinationInstitutionTransmissionData'
CREATE INDEX [IX_FK_DestinationInstitutionTransmissionData]
ON [dbo].[TransmissionDatas]
    ([DestinationInstitution_InstitutionId]);
GO

-- Creating foreign key on [InstitutionId] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [FK_InstitutionPhone]
    FOREIGN KEY ([InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionPhone'
CREATE INDEX [IX_FK_InstitutionPhone]
ON [dbo].[Phones]
    ([InstitutionId]);
GO

-- Creating foreign key on [SourceInstitution_InstitutionId] in table 'TransmissionDatas'
ALTER TABLE [dbo].[TransmissionDatas]
ADD CONSTRAINT [FK_SourceInstitutionTransmissionData]
    FOREIGN KEY ([SourceInstitution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SourceInstitutionTransmissionData'
CREATE INDEX [IX_FK_SourceInstitutionTransmissionData]
ON [dbo].[TransmissionDatas]
    ([SourceInstitution_InstitutionId]);
GO

-- Creating foreign key on [Setting_SettingsId] in table 'IntegerValues'
ALTER TABLE [dbo].[IntegerValues]
ADD CONSTRAINT [FK_SettingIntegerValue]
    FOREIGN KEY ([Setting_SettingsId])
    REFERENCES [dbo].[Settings]
        ([SettingsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingIntegerValue'
CREATE INDEX [IX_FK_SettingIntegerValue]
ON [dbo].[IntegerValues]
    ([Setting_SettingsId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'PersonNames'
ALTER TABLE [dbo].[PersonNames]
ADD CONSTRAINT [FK_PersonName]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonName'
CREATE INDEX [IX_FK_PersonName]
ON [dbo].[PersonNames]
    ([Person_PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'Phones'
ALTER TABLE [dbo].[Phones]
ADD CONSTRAINT [FK_PersonPhone]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPhone'
CREATE INDEX [IX_FK_PersonPhone]
ON [dbo].[Phones]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'Students'
ALTER TABLE [dbo].[Students]
ADD CONSTRAINT [FK_StudentPerson]
    FOREIGN KEY ([PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentPerson'
CREATE INDEX [IX_FK_StudentPerson]
ON [dbo].[Students]
    ([PersonId]);
GO

-- Creating foreign key on [ProgramCampus_ProgramCampusId] in table 'ProgramDetails'
ALTER TABLE [dbo].[ProgramDetails]
ADD CONSTRAINT [FK_ProgramCampusProgramDetails]
    FOREIGN KEY ([ProgramCampus_ProgramCampusId])
    REFERENCES [dbo].[ProgramCampuses]
        ([ProgramCampusId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramCampusProgramDetails'
CREATE INDEX [IX_FK_ProgramCampusProgramDetails]
ON [dbo].[ProgramDetails]
    ([ProgramCampus_ProgramCampusId]);
GO

-- Creating foreign key on [ProgramDetail_ProgramDetailsId] in table 'StudentApplications'
ALTER TABLE [dbo].[StudentApplications]
ADD CONSTRAINT [FK_ProgramDetailsStudentApplication]
    FOREIGN KEY ([ProgramDetail_ProgramDetailsId])
    REFERENCES [dbo].[ProgramDetails]
        ([ProgramDetailsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramDetailsStudentApplication'
CREATE INDEX [IX_FK_ProgramDetailsStudentApplication]
ON [dbo].[StudentApplications]
    ([ProgramDetail_ProgramDetailsId]);
GO

-- Creating foreign key on [ProgramTerm_ProgramTermId] in table 'ProgramDetails'
ALTER TABLE [dbo].[ProgramDetails]
ADD CONSTRAINT [FK_ProgramTermProgramDetails]
    FOREIGN KEY ([ProgramTerm_ProgramTermId])
    REFERENCES [dbo].[ProgramTerms]
        ([ProgramTermId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProgramTermProgramDetails'
CREATE INDEX [IX_FK_ProgramTermProgramDetails]
ON [dbo].[ProgramDetails]
    ([ProgramTerm_ProgramTermId]);
GO

-- Creating foreign key on [Recipient_RecipientId] in table 'TranscriptHolds'
ALTER TABLE [dbo].[TranscriptHolds]
ADD CONSTRAINT [FK_HoldTypes_Recipients]
    FOREIGN KEY ([Recipient_RecipientId])
    REFERENCES [dbo].[Recipients]
        ([RecipientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HoldTypes_Recipients'
CREATE INDEX [IX_FK_HoldTypes_Recipients]
ON [dbo].[TranscriptHolds]
    ([Recipient_RecipientId]);
GO

-- Creating foreign key on [Recipient_RecipientId] in table 'ResponseStatuses'
ALTER TABLE [dbo].[ResponseStatuses]
ADD CONSTRAINT [FK_RecipientResponseStatuses]
    FOREIGN KEY ([Recipient_RecipientId])
    REFERENCES [dbo].[Recipients]
        ([RecipientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipientResponseStatuses'
CREATE INDEX [IX_FK_RecipientResponseStatuses]
ON [dbo].[ResponseStatuses]
    ([Recipient_RecipientId]);
GO

-- Creating foreign key on [Request_RequestId] in table 'Recipients'
ALTER TABLE [dbo].[Recipients]
ADD CONSTRAINT [FK_RequestRecipient]
    FOREIGN KEY ([Request_RequestId])
    REFERENCES [dbo].[Requests]
        ([RequestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestRecipient'
CREATE INDEX [IX_FK_RequestRecipient]
ON [dbo].[Recipients]
    ([Request_RequestId]);
GO

-- Creating foreign key on [ReferenceType_ReferenceTypeId] in table 'StudentApplications'
ALTER TABLE [dbo].[StudentApplications]
ADD CONSTRAINT [FK_ReferenceTypeStudentApplication]
    FOREIGN KEY ([ReferenceType_ReferenceTypeId])
    REFERENCES [dbo].[ReferenceTypes]
        ([ReferenceTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ReferenceTypeStudentApplication'
CREATE INDEX [IX_FK_ReferenceTypeStudentApplication]
ON [dbo].[StudentApplications]
    ([ReferenceType_ReferenceTypeId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_StudentRequest]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentRequest'
CREATE INDEX [IX_FK_StudentRequest]
ON [dbo].[Requests]
    ([Student_StudentId]);
GO

-- Creating foreign key on [TransmissionData_TransmissionDataId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_TransmissionDataRequest]
    FOREIGN KEY ([TransmissionData_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionDataRequest'
CREATE INDEX [IX_FK_TransmissionDataRequest]
ON [dbo].[Requests]
    ([TransmissionData_TransmissionDataId]);
GO

-- Creating foreign key on [Response_ResponseId] in table 'ResponseHolds'
ALTER TABLE [dbo].[ResponseHolds]
ADD CONSTRAINT [FK_ResponseResponseHold]
    FOREIGN KEY ([Response_ResponseId])
    REFERENCES [dbo].[Responses]
        ([ResponseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseResponseHold'
CREATE INDEX [IX_FK_ResponseResponseHold]
ON [dbo].[ResponseHolds]
    ([Response_ResponseId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_StudentResponse]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentResponse'
CREATE INDEX [IX_FK_StudentResponse]
ON [dbo].[Responses]
    ([Student_StudentId]);
GO

-- Creating foreign key on [TransmissionData_TransmissionDataId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_TransmissionDataResponse]
    FOREIGN KEY ([TransmissionData_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionDataResponse'
CREATE INDEX [IX_FK_TransmissionDataResponse]
ON [dbo].[Responses]
    ([TransmissionData_TransmissionDataId]);
GO

-- Creating foreign key on [SettingCategory_CategoryId] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [FK_SettingCategorySetting]
    FOREIGN KEY ([SettingCategory_CategoryId])
    REFERENCES [dbo].[SettingCategories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingCategorySetting'
CREATE INDEX [IX_FK_SettingCategorySetting]
ON [dbo].[Settings]
    ([SettingCategory_CategoryId]);
GO

-- Creating foreign key on [Setting_SettingsId] in table 'ShortStringValues'
ALTER TABLE [dbo].[ShortStringValues]
ADD CONSTRAINT [FK_SettingShortStringValue]
    FOREIGN KEY ([Setting_SettingsId])
    REFERENCES [dbo].[Settings]
        ([SettingsId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SettingShortStringValue'
CREATE INDEX [IX_FK_SettingShortStringValue]
ON [dbo].[ShortStringValues]
    ([Setting_SettingsId]);
GO

-- Creating foreign key on [TransmissionData_TransmissionDataId] in table 'Statuses'
ALTER TABLE [dbo].[Statuses]
ADD CONSTRAINT [FK_TransmissionDataStatus]
    FOREIGN KEY ([TransmissionData_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionDataStatus'
CREATE INDEX [IX_FK_TransmissionDataStatus]
ON [dbo].[Statuses]
    ([TransmissionData_TransmissionDataId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'StudentApplications'
ALTER TABLE [dbo].[StudentApplications]
ADD CONSTRAINT [FK_StudentStudentApplication]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentApplication'
CREATE INDEX [IX_FK_StudentStudentApplication]
ON [dbo].[StudentApplications]
    ([Student_StudentId]);
GO

-- Creating foreign key on [SvcJob_SvcJobId] in table 'SvcLogs'
ALTER TABLE [dbo].[SvcLogs]
ADD CONSTRAINT [FK_SvcJobSvcLog]
    FOREIGN KEY ([SvcJob_SvcJobId])
    REFERENCES [dbo].[SvcJobs]
        ([SvcJobId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SvcJobSvcLog'
CREATE INDEX [IX_FK_SvcJobSvcLog]
ON [dbo].[SvcLogs]
    ([SvcJob_SvcJobId]);
GO

-- Creating foreign key on [SvcJobs_SvcJobId] in table 'SvcScheds'
ALTER TABLE [dbo].[SvcScheds]
ADD CONSTRAINT [FK_SvcSchedSvcJob]
    FOREIGN KEY ([SvcJobs_SvcJobId])
    REFERENCES [dbo].[SvcJobs]
        ([SvcJobId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SvcSchedSvcJob'
CREATE INDEX [IX_FK_SvcSchedSvcJob]
ON [dbo].[SvcScheds]
    ([SvcJobs_SvcJobId]);
GO

-- Creating foreign key on [TransmissionData_TransmissionDataId] in table 'Transcripts'
ALTER TABLE [dbo].[Transcripts]
ADD CONSTRAINT [FK_TransmissionDataTranscript]
    FOREIGN KEY ([TransmissionData_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionDataTranscript'
CREATE INDEX [IX_FK_TransmissionDataTranscript]
ON [dbo].[Transcripts]
    ([TransmissionData_TransmissionDataId]);
GO

-- Creating foreign key on [ValCodeId] in table 'ValCodeLookups'
ALTER TABLE [dbo].[ValCodeLookups]
ADD CONSTRAINT [FK_ValCodeValCodeLookup]
    FOREIGN KEY ([ValCodeId])
    REFERENCES [dbo].[ValCodes]
        ([ValCodeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ValCodeValCodeLookup'
CREATE INDEX [IX_FK_ValCodeValCodeLookup]
ON [dbo].[ValCodeLookups]
    ([ValCodeId]);
GO

-- Creating foreign key on [AccessGroups_AccessGroupId] in table 'AccessGroupUser'
ALTER TABLE [dbo].[AccessGroupUser]
ADD CONSTRAINT [FK_AccessGroupUser_AccessGroups]
    FOREIGN KEY ([AccessGroups_AccessGroupId])
    REFERENCES [dbo].[AccessGroups]
        ([AccessGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'AccessGroupUser'
ALTER TABLE [dbo].[AccessGroupUser]
ADD CONSTRAINT [FK_AccessGroupUser_Users]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessGroupUser_Users'
CREATE INDEX [IX_FK_AccessGroupUser_Users]
ON [dbo].[AccessGroupUser]
    ([Users_UserId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Citizenships'
ALTER TABLE [dbo].[Citizenships]
ADD CONSTRAINT [FK_PersonCitizenship]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonCitizenship'
CREATE INDEX [IX_FK_PersonCitizenship]
ON [dbo].[Citizenships]
    ([Person_PersonId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Residencies'
ALTER TABLE [dbo].[Residencies]
ADD CONSTRAINT [FK_PersonResidency]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonResidency'
CREATE INDEX [IX_FK_PersonResidency]
ON [dbo].[Residencies]
    ([Person_PersonId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [FK_PersonGender]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonGender'
CREATE INDEX [IX_FK_PersonGender]
ON [dbo].[Genders]
    ([Person_PersonId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Disabilities'
ALTER TABLE [dbo].[Disabilities]
ADD CONSTRAINT [FK_PersonDisability]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonDisability'
CREATE INDEX [IX_FK_PersonDisability]
ON [dbo].[Disabilities]
    ([Person_PersonId]);
GO

-- Creating foreign key on [Person_PersonId] in table 'Immigrations'
ALTER TABLE [dbo].[Immigrations]
ADD CONSTRAINT [FK_PersonImmigration]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[Persons]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonImmigration'
CREATE INDEX [IX_FK_PersonImmigration]
ON [dbo].[Immigrations]
    ([Person_PersonId]);
GO

-- Creating foreign key on [AcademicRecord_AcademicRecordId] in table 'AcademicSessions'
ALTER TABLE [dbo].[AcademicSessions]
ADD CONSTRAINT [FK_AcademicRecordAcademicSession]
    FOREIGN KEY ([AcademicRecord_AcademicRecordId])
    REFERENCES [dbo].[AcademicRecords]
        ([AcademicRecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicRecordAcademicSession'
CREATE INDEX [IX_FK_AcademicRecordAcademicSession]
ON [dbo].[AcademicSessions]
    ([AcademicRecord_AcademicRecordId]);
GO

-- Creating foreign key on [AcademicRecord_AcademicRecordId] in table 'AcademicAwards'
ALTER TABLE [dbo].[AcademicAwards]
ADD CONSTRAINT [FK_AcademicRecordAcademicAward]
    FOREIGN KEY ([AcademicRecord_AcademicRecordId])
    REFERENCES [dbo].[AcademicRecords]
        ([AcademicRecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademicRecordAcademicAward'
CREATE INDEX [IX_FK_AcademicRecordAcademicAward]
ON [dbo].[AcademicAwards]
    ([AcademicRecord_AcademicRecordId]);
GO

-- Creating foreign key on [Recipient_RecipientId] in table 'RequestorReceivers'
ALTER TABLE [dbo].[RequestorReceivers]
ADD CONSTRAINT [FK_RecipientRequestorReceiver]
    FOREIGN KEY ([Recipient_RecipientId])
    REFERENCES [dbo].[Recipients]
        ([RecipientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RecipientRequestorReceiver'
CREATE INDEX [IX_FK_RecipientRequestorReceiver]
ON [dbo].[RequestorReceivers]
    ([Recipient_RecipientId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'AcademicRecords'
ALTER TABLE [dbo].[AcademicRecords]
ADD CONSTRAINT [FK_StudentAcademicRecord]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentAcademicRecord'
CREATE INDEX [IX_FK_StudentAcademicRecord]
ON [dbo].[AcademicRecords]
    ([Student_StudentId]);
GO

-- Creating foreign key on [CourseOverrideSchool_InstitutionId] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK_CourseOverrideSchool]
    FOREIGN KEY ([CourseOverrideSchool_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CourseOverrideSchool'
CREATE INDEX [IX_FK_CourseOverrideSchool]
ON [dbo].[Courses]
    ([CourseOverrideSchool_InstitutionId]);
GO

-- Creating foreign key on [NoteMessage_TransmissionDataId] in table 'NoteMessages'
ALTER TABLE [dbo].[NoteMessages]
ADD CONSTRAINT [FK_TransmissionData]
    FOREIGN KEY ([NoteMessage_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionData'
CREATE INDEX [IX_FK_TransmissionData]
ON [dbo].[NoteMessages]
    ([NoteMessage_TransmissionDataId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'sNumbers'
ALTER TABLE [dbo].[sNumbers]
ADD CONSTRAINT [FK_StudentsNumber]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentsNumber'
CREATE INDEX [IX_FK_StudentsNumber]
ON [dbo].[sNumbers]
    ([Student_StudentId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'StudentAlias'
ALTER TABLE [dbo].[StudentAlias]
ADD CONSTRAINT [FK_StudentStudentAlias]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentAlias'
CREATE INDEX [IX_FK_StudentStudentAlias]
ON [dbo].[StudentAlias]
    ([Student_StudentId]);
GO

-- Creating foreign key on [ApplicationMessage_ApplicationMessageId] in table 'Acknowledgements'
ALTER TABLE [dbo].[Acknowledgements]
ADD CONSTRAINT [FK_ApplicationMessageAcknowledgement]
    FOREIGN KEY ([ApplicationMessage_ApplicationMessageId])
    REFERENCES [dbo].[ApplicationMessages]
        ([ApplicationMessageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ApplicationMessageAcknowledgement'
CREATE INDEX [IX_FK_ApplicationMessageAcknowledgement]
ON [dbo].[Acknowledgements]
    ([ApplicationMessage_ApplicationMessageId]);
GO

-- Creating foreign key on [User_UserId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_User_UserId]
    FOREIGN KEY ([User_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_UserId'
CREATE INDEX [IX_FK_User_UserId]
ON [dbo].[Histories]
    ([User_UserId]);
GO

-- Creating foreign key on [ActionType_ActionId] in table 'Histories'
ALTER TABLE [dbo].[Histories]
ADD CONSTRAINT [FK_ActionType_ActionId]
    FOREIGN KEY ([ActionType_ActionId])
    REFERENCES [dbo].[ActionTypes]
        ([ActionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionType_ActionId'
CREATE INDEX [IX_FK_ActionType_ActionId]
ON [dbo].[Histories]
    ([ActionType_ActionId]);
GO

-- Creating foreign key on [TransmissionData_TransmissionDataId] in table 'Acknowledgements'
ALTER TABLE [dbo].[Acknowledgements]
ADD CONSTRAINT [FK_TransmissionData_TransmissionDataId]
    FOREIGN KEY ([TransmissionData_TransmissionDataId])
    REFERENCES [dbo].[TransmissionDatas]
        ([TransmissionDataId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TransmissionData_TransmissionDataId'
CREATE INDEX [IX_FK_TransmissionData_TransmissionDataId]
ON [dbo].[Acknowledgements]
    ([TransmissionData_TransmissionDataId]);
GO

-- Creating foreign key on [RequestorInstitution_InstitutionId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_InstitutionRequest]
    FOREIGN KEY ([RequestorInstitution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstitutionRequest'
CREATE INDEX [IX_FK_InstitutionRequest]
ON [dbo].[Requests]
    ([RequestorInstitution_InstitutionId]);
GO

-- Creating foreign key on [Institution_InstitutionId] in table 'BulkSendTranscripts'
ALTER TABLE [dbo].[BulkSendTranscripts]
ADD CONSTRAINT [FK_Institution_InstitutionId]
    FOREIGN KEY ([Institution_InstitutionId])
    REFERENCES [dbo].[Institutions]
        ([InstitutionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Institution_InstitutionId'
CREATE INDEX [IX_FK_Institution_InstitutionId]
ON [dbo].[BulkSendTranscripts]
    ([Institution_InstitutionId]);
GO

-- Creating foreign key on [Student_StudentId] in table 'BulkSendTranscripts'
ALTER TABLE [dbo].[BulkSendTranscripts]
ADD CONSTRAINT [FK_Student_StudentId]
    FOREIGN KEY ([Student_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Student_StudentId'
CREATE INDEX [IX_FK_Student_StudentId]
ON [dbo].[BulkSendTranscripts]
    ([Student_StudentId]);
GO

-- Creating foreign key on [Email_EmailId] in table 'SentEmails'
ALTER TABLE [dbo].[SentEmails]
ADD CONSTRAINT [FK_EmailSentEmail]
    FOREIGN KEY ([Email_EmailId])
    REFERENCES [dbo].[Emails]
        ([EmailId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailSentEmail'
CREATE INDEX [IX_FK_EmailSentEmail]
ON [dbo].[SentEmails]
    ([Email_EmailId]);
GO

-- Creating foreign key on [Matched_StudentId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_RequestMatchedStudent]
    FOREIGN KEY ([Matched_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestMatchedStudent'
CREATE INDEX [IX_FK_RequestMatchedStudent]
ON [dbo].[Requests]
    ([Matched_StudentId]);
GO

-- Creating foreign key on [Matched_StudentId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_ResponseMatchedStudent]
    FOREIGN KEY ([Matched_StudentId])
    REFERENCES [dbo].[Students]
        ([StudentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ResponseMatchedStudent'
CREATE INDEX [IX_FK_ResponseMatchedStudent]
ON [dbo].[Responses]
    ([Matched_StudentId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------