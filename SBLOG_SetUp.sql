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

/* Table: dbo.Response */
if exists (select * from sysobjects 
  where id = object_id('dbo.Response') and sysstat & 0xf = 3)
  drop table dbo.Response
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
  BlogDesc      text            NULL,
  --BlogCat		varchar(255) 	NOT NULL,
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


/* Table: dbo.Response*/
CREATE TABLE dbo.Response
(
  ResponseID		int IDENTITY (1,1),
  FeedbackID 		int			NOT NULL, 
  DateTimePosted	datetime	NOT NULL	DEFAULT (getdate()),
  [Text] 			text 		NOT NULL,
  CONSTRAINT PK_Response PRIMARY KEY CLUSTERED (ResponseID),
  CONSTRAINT FK_Response_FeedbackID FOREIGN KEY (FeedbackID) 
  REFERENCES dbo.Feedback(FeedbackID),
  --CONSTRAINT FK_Response_MemberID FOREIGN KEY (MemberID) 
  --REFERENCES dbo.Customer(MemberID),
  --CONSTRAINT FK_Response_StaffID FOREIGN KEY (StaffID) 
  --REFERENCES dbo.Staff(StaffID)
)
GO


/*****  Create records in Feedback Table *****/
SET IDENTITY_INSERT [dbo].[Feedback] ON
INSERT [dbo].[Feedback] ([FeedbackID], [Email], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (1, 'M00000005@gmail.com', '25-Apr-2022', 'Good Customer Service', 'Sales Personnel Mr Edward Lee at Bishan Bramch was excellent in providing customer service.  He had good knowledge in explaining the design ideas of various fashions, as well as maintaining patient and always wears a smiling face even though I had been fussy in choosing the right fashion for me.', NULL)
INSERT [dbo].[Feedback] ([FeedbackID], [Email], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (2, 'M00000002@gmail.com', '15-May-2022', 'Flaw in Product', 'One out of the three Blue Tiger Sweatshirts I bought today seemed to have the colour of the design faded off after washing.', NULL)
SET IDENTITY_INSERT [dbo].[Feedback] OFF



--DELETE FROM Feedback 

/*****  Create records in Blog Table *****/
SET IDENTITY_INSERT [dbo].[Blog] ON 
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (1, 'MOE Financial Assistance Schemes', 'MOE_FAS.png', 'Financial assistance from MOE on school fees and other expenses, such as meals, textbooks, school attire and transportation. ')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (2, 'SGBono (Laptop loan and tech support)', 'sgBonoPage.png', 'SGBono is a registered society with Registry Of Societies, Singapore since 21st Nov 2018. Made up of a group of volunteers in Singapore from various backgrounds, they provide free services for low-income families to solve their IT issues. ')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (3, 'Tech Composition Singaporean school guide', 'techcomposition.png', 'This website provides useful information for students that might require a laptop for school.')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (4, 'Wishing Well', 'wishingwell.png', 'The WishingWell is an organisation focused on supporting the current and future needs of disadvantaged children. ')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (5, 'IMDA Financial Assistance Schemes ', 'IMDA_FAS.png', 'Government financial assistance scheme for students who are from low SES families and those whose households have disabled persons.')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (6, 'Seedly blog', 'SeedlyNeverGoHungry.png', 'Seedly is an online blog, where people can come together can discuss about anything they want. ')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (7, 'NEON JACKET', '9815401617_1_1_3.jpg', 'lorem ipsum')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (8, 'PRINTED CAMOUFLAGE BERMUDAS', '6917485615_1_1_3.jpg', 'lorem ipsum')
INSERT [dbo].[Blog] ([BlogID], [BlogTitle], [BlogImage], [BlogDesc]) VALUES (9, 'TOP WITH ASYMMETRIC HEM ', '2669795710_1_1_3.jpg', 'lorem ipsum')
SET IDENTITY_INSERT [dbo].[Blog] OFF


SELECT * FROM Feedback

SELECT * FROM Blog

SELECT * FROM Response