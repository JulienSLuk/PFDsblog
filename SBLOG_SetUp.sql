--Create Database SBLOG
--GO

Use SBLOG
GO



/* Table: dbo.Feedback */
if exists (select * from sysobjects 
  where id = object_id('dbo.Feedback') and sysstat & 0xf = 3)
  drop table dbo.Feedback
GO


/* Table: dbo.Blog */
if exists (select * from sysobjects 
  where id = object_id('dbo.Blog') and sysstat & 0xf = 3)
  drop table dbo.Blog
GO


/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/

/* Table: dbo.Blog*/
CREATE TABLE dbo.Blog
(
  BlogID 		int IDENTITY (1,1),
  BlogTitle  	varchar(255) 	NOT NULL,
  BlogImage		varchar(255) 	NULL,
  CONSTRAINT PK_Entries PRIMARY KEY CLUSTERED (BlogID)
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

SELECT * FROM Blog

DELETE FROM Feedback 

/*****  Create records in Product Table *****/
SET IDENTITY_INSERT [dbo].[Blog] ON 
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (1, 'EMBROIDERED DRESS', '1381043712_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (2, 'FLORAL PRINT PENCIL SKIRT', '2705273400_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (3, 'BLUE TIGER SWEATSHIRT', '5644031413_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (4, 'BLAZER WITH ROLL-UP CUFFS ', '2070239550_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (5, 'PARROTS T-SHIRT ', '0722437052_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (6, 'COLORED STRIPED COTTON SWEATER ', '0367420800_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (7, 'NEON JACKET', '9815401617_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (8, 'PRINTED CAMOUFLAGE BERMUDAS', '6917485615_1_1_3.jpg')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage]) VALUES (9, 'TOP WITH ASYMMETRIC HEM ', '2669795710_1_1_3.jpg')
SET IDENTITY_INSERT [dbo].[Blog] OFF


