/*======================================================*/
/*  Created in 2022                                     */
/*  PFD 2022 October Semester					        */
/*  Diploma in IT                                       */
/*                                                      */
/*  Database Script for setting up the database         */
/*  required for PFD Assignment.                        */
/*======================================================*/

--Create Database SbloG
--GO

Use SbloG
GO

/***************************************************************/
/***           Delete tables before creating                 ***/
/***************************************************************/

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



/* Table: dbo.Product */
if exists (select * from sysobjects 
  where id = object_id('dbo.Product') and sysstat & 0xf = 3)
  drop table dbo.Product
GO

/* Table: dbo.Customer */
if exists (select * from sysobjects 
  where id = object_id('dbo.Customer') and sysstat & 0xf = 3)
  drop table dbo.Customer
GO

if exists (select * from sysobjects
	where id = object_id('dbo.TempCustomer') and sysstat & 0xf = 3)
	drop table dbo.TempCustomer
GO

/* Table: dbo.Staff */
if exists (select * from sysobjects 
  where id = object_id('dbo.Staff') and sysstat & 0xf = 3)
  drop table dbo.Staff
GO


/***************************************************************/
/***                     Creating tables                     ***/
/***************************************************************/



/* Table: dbo.Staff */
CREATE TABLE dbo.Staff
(
  StaffID 	    varchar(20),
  StoreID   	varchar(25) 	NULL,
  SName			varchar(50) 	NOT NULL,
  SGender		char(1) 		NOT NULL	CHECK (SGender IN ('M','F')),
  SAppt			varchar(50) 	NOT NULL,	
  STelNo		varchar(20)		NOT NULL,
  SEmailAddr	varchar(50) 	NOT NULL,	
  SPassword		varchar(20)		NOT NULL,
  CONSTRAINT PK_Staff PRIMARY KEY CLUSTERED (StaffID),
)
GO

/* Table: dbo.Customer */
CREATE TABLE dbo.Customer
(
  MemberID 			char(9),
  MName				varchar(50) 	NOT NULL,
  MGender			char(1) 		NOT NULL	CHECK (MGender IN ('M','F')),
  MBirthDate		datetime		NOT NULL,
  MAddress			varchar(250)	NULL,
  MCountry			varchar(50)		NOT NULL,
  MTelNo			varchar(20)		NULL,
  MEmailAddr		varchar(50)		NULL,
  MPassword			varchar(20)		NOT NULL	DEFAULT ('AbC@123#'),
  CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (MemberID)
)
GO

/* Table: dbo.TempCustomer */
CREATE TABLE dbo.TempCustomer
(
  TempID 			int IDENTITY (1,1),
  TName				varchar(50) 	NOT NULL,
  TGender			char(1) 		NOT NULL	CHECK (TGender IN ('M','F')),
  TBirthDate		datetime		NOT NULL,
  TAddress			varchar(250)	NULL,
  TCountry			varchar(50)		NOT NULL,
  TTelNo			varchar(20)		NULL,
  TEmailAddr		varchar(50)		NULL,
  TPassword			varchar(20)		NOT NULL	DEFAULT ('AbC@123#'),
)
GO

/* Table: dbo.Product*/
CREATE TABLE dbo.Product
(
  ProductID 		int IDENTITY (1,1),
  ProductTitle  	varchar(255) 	NOT NULL,
  ProductImage		varchar(255) 	NULL,
  ProductDesc       text            NULL,
  ProductCat		varchar(255) 	NOT NULL,
  Obsolete			char(1)			NOT NULL	DEFAULT ('1')	
					CHECK (Obsolete IN ('0','1')),
  CONSTRAINT PK_Blog PRIMARY KEY CLUSTERED (ProductID)
)
GO




/* Table: dbo.Feedback */
CREATE TABLE dbo.Feedback
(
  FeedbackID		int IDENTITY (1,1),
  MemberID 			char(9)			NOT NULL, 
  DateTimePosted	datetime		NOT NULL 	DEFAULT (getdate()),
  Title				varchar (255)	NOT NULL,	  
  [Text] 			text 			NULL,
  ImageFileName		varchar (255)	NULL,
  CONSTRAINT PK_Feedback PRIMARY KEY CLUSTERED (FeedbackID),
  CONSTRAINT FK_Feedback_MemberID FOREIGN KEY (MemberID) 
  REFERENCES dbo.Customer(MemberID)
)
GO

/* Table: dbo.Response*/
CREATE TABLE dbo.Response
(
  ResponseID		int IDENTITY (1,1),
  FeedbackID 		int			NOT NULL, 
  MemberID 			char(9)		NULL,
  StaffID 			varchar(20)	NULL,
  DateTimePosted	datetime	NOT NULL	DEFAULT (getdate()),
  [Text] 			text 		NOT NULL,
  CONSTRAINT PK_Response PRIMARY KEY CLUSTERED (ResponseID),
  CONSTRAINT FK_Response_FeedbackID FOREIGN KEY (FeedbackID) 
  REFERENCES dbo.Feedback(FeedbackID),
  CONSTRAINT FK_Response_MemberID FOREIGN KEY (MemberID) 
  REFERENCES dbo.Customer(MemberID),
  CONSTRAINT FK_Response_StaffID FOREIGN KEY (StaffID) 
  REFERENCES dbo.Staff(StaffID)
)
GO


/***************************************************************/
/***              Load the tables with sample data           ***/
/***************************************************************/



/*****  Create records in Staff Table *****/
INSERT [dbo].[Staff] ([StaffID], [StoreID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('SG-Orchard', 'SG-Orchard', 'Samatha Tan', 'F', 'Sales Personnel', '64561234', 'st@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [StoreID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('SG-Jurong', 'SG-Jurong', 'Pinky Pander', 'F', 'Sales Personnel', '64561235', 'pp@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [StoreID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('SG-Bishan', 'SG-Bishan', 'Edward Lee', 'M', 'Sales Personnel', '64561236', 'el@zzf.com.sg', 'passSales')
INSERT [dbo].[Staff] ([StaffID], [StoreID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('ProductManager', NULL, 'Jenifer Greenspan', 'F', 'Product Manager', '64561237', 'jg@zzf.com.sg', 'passProduct')
INSERT [dbo].[Staff] ([StaffID], [StoreID], [SName], [SGender], [SAppt], [STelNo], [SEmailAddr], [SPassword]) VALUES ('Marketing', NULL, 'Ali Imran', 'M', 'Marketing Personnel', '64561238', 'ai@zzf.com.sg', 'passMarketing')

/*****  Create records in Customer Table *****/
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000001', 'Benjamin Bean', 'M', '05-May-1970', NULL, 'United Kingdom', '94609901', NULL, 'pass1234')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000002', 'Fatimah Bte Ahmad', 'F', '21-Jun-1992', '100, Bukit Timah Road', 'Singapore', '91234567', 'fa92@yahoo.com', 'AbC@123#')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000003', 'Peter Ghim', 'M', '31-Aug-1991', '203, Jalan Wong Ah Fok, Johor Bahru', 'Malaysia', '98765432', 'pg91@hotmail.com', 'pgPass')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000004', 'Xu Yazhi', 'F', '25-Dec-1980', NULL, 'China', NULL, 'xyz@np.edu.sg', 'xyz')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000005', 'Eliza Wong', 'F', '24-Jul-1993', 'Blk 123, #10-321, Hougang Ave 2', 'Singapore', NULL, NULL, 'pass1234')
INSERT [dbo].[Customer] ([MemberID], [MName], [MGender], [MBirthDate], [MAddress], [MCountry], [MTelNo], [MEmailAddr], [MPassword]) VALUES ('M00000006', 'K Kannan', 'M', '12-Sep-1990', NULL, 'India', NULL, '20100134@np.edu.sg', 'pass1234')

/*****  Create records in TempCustomer Table *****/
SET IDENTITY_INSERT [dbo].[TempCustomer] ON
INSERT [dbo].[TempCustomer]([TempID], [TName], [TGender], [TBirthDate], [TAddress], [TCountry], [TTelNo], [TEmailAddr], [TPassword]) VALUES (1, 'Test', 'M', '05-May-1970', NULL, 'United Kingdom', NULL , NULL, 'pass1234')
SET IDENTITY_INSERT [dbo].[TempCustomer] OFF

/*****  Create records in Product Table *****/
SET IDENTITY_INSERT [dbo].[Product] ON 
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (1, 'MOE Financial Assistance Schemes', 'MOE_FAS.png', 'Financial assistance from MOE on school fees and other expenses, such as meals, textbooks, school attire and transportation. ', 'Financial Scheme', '1')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (2, 'SGBono (Laptop loan and tech support)', 'sgBonoPage.png', 'SGBono is a registered society with Registry Of Societies, Singapore since 21st Nov 2018. Made up of a group of volunteers in Singapore from various backgrounds, they provide free services for low-income families to solve their IT issues. ', 'Tech', '1')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (3, 'Tech Composition Singaporean school guide', 'techcomposition.png', 'This website provides useful information for students that might require a laptop for school.', 'Tech', '1')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (4, 'Wishing Well', 'wishingwell.png', 'The WishingWell is an organisation focused on supporting the current and future needs of disadvantaged children. ', 'Financial Scheme', '1')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (5, 'IMDA Financial Assistance Schemes ', 'IMDA_FAS.png', 'Government financial assistance scheme for students who are from low SES families and those whose households have disabled persons.', 'Financial Scheme', '0')
INSERT [dbo].[Product] ([ProductID], [ProductTitle], [ProductImage], [ProductDesc], [ProductCat], [Obsolete]) VALUES (6, 'Seedly blog', 'SeedlyNeverGoHungry.png', 'Seedly is an online blog, where people can come together can discuss about anything they want. ', 'Food', '1')
SET IDENTITY_INSERT [dbo].[Product] OFF




/*****  Create records in Feedback Table *****/
SET IDENTITY_INSERT [dbo].[Feedback] ON
INSERT [dbo].[Feedback] ([FeedbackID], [MemberID], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (1, 'M00000005', '25-Apr-2022', 'Good Customer Service', 'Sales Personnel Mr Edward Lee at Bishan Bramch was excellent in providing customer service.  He had good knowledge in explaining the design ideas of various fashions, as well as maintaining patient and always wears a smiling face even though I had been fussy in choosing the right fashion for me.', NULL)
INSERT [dbo].[Feedback] ([FeedbackID], [MemberID], [DateTimePosted], [Title], [Text], [ImageFileName]) 
VALUES (2, 'M00000002', '15-May-2022', 'Flaw in Product', 'One out of the three Blue Tiger Sweatshirts I bought today seemed to have the colour of the design faded off after washing.', NULL)
SET IDENTITY_INSERT [dbo].[Feedback] OFF

/*****  Create records in Response Table *****/
SET IDENTITY_INSERT [dbo].[Response] ON
INSERT [dbo].[Response] ([ResponseID], [FeedbackID], [MemberID], [StaffID], [DateTimePosted], [Text]) 
VALUES (1, 1, NULL, 'Marketing', '27-Apr-2022', 'Thanks for your compliment, I have already share your feedback to Mr Edward Lee and all of our sales staff. Your valuable feedback will help to motivate our staff members in providing excellent customer service.')
INSERT [dbo].[Response] ([ResponseID], [FeedbackID], [MemberID], [StaffID], [DateTimePosted], [Text]) 
VALUES (2, 2, NULL, 'Marketing', '15-May-2022', 'We are sorry that you had purchased a flawed product that had went through our quality control system.  We will investigate this matter and hope to prevent this type of cases in future.  You can bring the flawed product (with receipt) to any of our branches to get a refund.')
INSERT [dbo].[Response] ([ResponseID], [FeedbackID], [MemberID], [StaffID], [DateTimePosted], [Text]) 
VALUES (3, 2, 'M00000002', NULL, '16-May-2022', 'I had returned the flawed product to ZZ Fashion Jurong Branch and got full refund. I am impressed with the good customer service provided by your staff.')
SET IDENTITY_INSERT [dbo].[Response] OFF



SELECT * FROM Product
SELECT * FROM TempCustomer