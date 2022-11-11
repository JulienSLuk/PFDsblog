--Create Database SBLOG
--GO

Use SBLOG
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
  Email 			text			NOT NULL, 
  DateTimePosted	datetime		NOT NULL 	DEFAULT (getdate()),
  Title				varchar (255)	NOT NULL,	  
  [Text] 			text 			NULL,
  ImageFileName		varchar (255)	NULL,
  CONSTRAINT PK_Feedback PRIMARY KEY CLUSTERED (FeedbackID),
)
GO


/*****  Create records in Feedback Table *****/
SET IDENTITY_INSERT [dbo].[Feedback] ON
INSERT [dbo].[Feedback] ([FeedbackID], [Email], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (1, 'M00000005@gmail.com', '25-Apr-2022', 'Good Customer Service', 'Sales Personnel Mr Edward Lee at Bishan Bramch was excellent in providing customer service.  He had good knowledge in explaining the design ideas of various fashions, as well as maintaining patient and always wears a smiling face even though I had been fussy in choosing the right fashion for me.', NULL)
INSERT [dbo].[Feedback] ([FeedbackID], [Email], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (2, 'M00000002@gmail.com', '15-May-2022', 'Flaw in Product', 'One out of the three Blue Tiger Sweatshirts I bought today seemed to have the colour of the design faded off after washing.', NULL)
SET IDENTITY_INSERT [dbo].[Feedback] OFF

SELECT * FROM Feedback

DELETE FROM Feedback 