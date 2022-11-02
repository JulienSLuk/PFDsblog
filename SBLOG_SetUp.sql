--Create Database SBLOG
--GO

Use SBLOG
GO


/* Table: dbo.Response */
if exists (select * from sysobjects 
  where id = object_id('dbo.Response') and sysstat & 0xf = 3)
  drop table dbo.Response
GO

/* Table: dbo.Feedback */
if exists (select * from sysobjects 
  where id = object_id('dbo.Feedback') and sysstat & 0xf = 3)
  drop table dbo.Feedback
GO


/* Table: dbo.Entries */
if exists (select * from sysobjects 
  where id = object_id('dbo.Entries') and sysstat & 0xf = 3)
  drop table dbo.Entries
GO


/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.Entries*/
CREATE TABLE dbo.Entries
(
  EntryID 		int IDENTITY (1,1),
  EntryTitle  	varchar(255) 	NOT NULL,
  EntryImage		varchar(255) 	NULL,
  CONSTRAINT PK_Entries PRIMARY KEY CLUSTERED (EntryID)
)
GO

/* Table: dbo.Feedback */
CREATE TABLE dbo.Feedback
(
  FeedbackID		int IDENTITY (1,1),
  Email 			char(9)			NOT NULL, 
  DateTimePosted	datetime		NOT NULL 	DEFAULT (getdate()),
  Title				varchar (255)	NOT NULL,	  
  [Text] 			text 			NULL,
  ImageFileName		varchar (255)	NULL,
  CONSTRAINT PK_Feedback PRIMARY KEY CLUSTERED (FeedbackID),
)
GO

