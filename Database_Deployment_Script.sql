-- FEEDS TABLE AND INDEX
CREATE TABLE Feeds (
    ID  varchar(255) NOT NULL,
    Link varchar(255),
    Title varchar(255),
    Description varchar(255),
    CommentCount int,
	PictureUrl varchar(255),
	PublishDate DateTime,
	Category varchar(255),
	PRIMARY KEY (ID)
);

CREATE INDEX Feeds_ID
ON Feeds (ID);

CREATE INDEX Feeds_Category
ON Feeds (Category);

-- FEEDSUPDATETIME TABLE, DATA INSERTS AND INDEX
CREATE TABLE FeedsUpdateTime (
    Category  varchar(255) NOT NULL,
	UpdateDate DateTime
	PRIMARY KEY (Category)
);

CREATE INDEX FeedsUpdateTime_Category
ON FeedsUpdateTime (Category);

INSERT INTO FeedsUpdateTime
(
Category,
UpdateDate
)
Values
(
'Aculiecinieks', null
),
(
'Auto', null
)

-- UsersTable
CREATE TABLE Users
(
UserID nvarchar(255) NOT NULL,
FullName nvarchar(255),
Email nvarchar(255),
PRIMARY KEY (USERID)
);

CREATE INDEX Users_UserID
ON Users(UserID);

-- UserImages
CREATE TABLE UsersImages
(
UserID nvarchar(255) NOT NULL,
Picture varbinary(max),
PRIMARY KEY (UserID)
);

CREATE INDEX UsersImages_UserID
ON UsersImages(UserID);


-- Feed settings
CREATE TABLE FeedsSettings (
    UserID  varchar(255) NOT NULL,
	Category varchar(255),
	FeedCount int
	PRIMARY KEY (UserID)
);

CREATE INDEX FeedsSettings_UserID
ON FeedsSettings (UserID);





-- STORED PROCEDURES
Create PROCEDURE Sp_ChangeFeedCategoryUpdateTime
@Category nvarchar(255),
@UpdateDate DateTime

AS
Update FeedsUpdateTime 
SET 
UpdateDate = @UpdateDate
WHERE Category = @Category
GO




Create PROCEDURE Sp_InsertOrUpdateFeedByID
@ID nvarchar(255),
@Link varchar(255),
@Title varchar(255),
@Description varchar(255),
@CommentCount int,
@PictureUrl varchar(255),
@PublishDate DateTime,
@Category varchar(255)

AS
IF EXISTS (SELECT 1 FROM Feeds WHERE ID = @ID) 
BEGIN
   Update Feeds
   SET 
      ID = @ID,
      Link = @Link,
      Title = @Title,
      Description = @Description,
      CommentCount = @CommentCount,
      PictureUrl = @PictureUrl,
      PublishDate = @PublishDate,
      Category = @Category
	WHERE ID = @ID
	SELECT 0
END
ELSE
BEGIN
	INSERT INTO Feeds
	(
		ID,
		Link,
		Title,
		Description, 
		CommentCount,
		PictureUrl,
		PublishDate,
		Category
	)
	VALUES
	(
		@ID,
		@Link,
		@Title,
		@Description, 
		@CommentCount,
		@PictureUrl,
		@PublishDate,
		@Category
	)
	SELECT 1
END
GO

Create PROCEDURE Sp_GetUserByID
@UserID nvarchar(255)

AS
SELECT TOP 1
UserID,
FullName,
Email
FROM Users
WHERE UserID = @UserID
GO

Create PROCEDURE Sp_CreateOrUpdateUser
@UserID nvarchar(255),
@FullName nvarchar(255),
@Email nvarchar(255),
@Picture varbinary(max)
AS
IF EXISTS (SELECT 1 FROM Users WHERE UserID = @UserID) 
BEGIN
   Update Users
   SET 
      FullName = @FullName,
      Email = @Email
	WHERE UserID = @UserID
END
ELSE
BEGIN
	INSERT INTO Users
	(
		UserID,
		Email,
		FullName
	)
	VALUES
	(
		@UserID,
		@Email,
		@FullName
	)
END

IF EXISTS (SELECT 1 FROM UsersImages WHERE UserID = @UserID) 
BEGIN
   Update UsersImages
   SET 
      Picture = @Picture
	WHERE UserID = @UserID
END
ELSE
BEGIN
	INSERT INTO UsersImages
	(
		UserID,
		Picture
	)
	VALUES
	(
		@UserID,
		@Picture
	)
END
GO


CREATE PROCEDURE Sp_GetFeedsByCategory
@Category nvarchar(255),
@Count int

AS
SELECT TOP (@Count)
 ID,
 Link,
 Title,
 Description,
 CommentCount,
 PictureUrl,
 PublishDate
FROM Feeds
WHERE Category = @Category 
ORDER BY PublishDate DESC
GO

CREATE PROCEDURE Sp_GetFeedSettingsByUserID
@UserID nvarchar(255)
AS
SELECT TOP 1
 UserID,
 Category,
 FeedCount
FROM FeedsSettings
WHERE UserID = @UserID
GO

Create PROCEDURE Sp_GetUserProfileData
@UserID nvarchar(255)
AS
SELECT
u.Email,
u.FullName,
ui.Picture,
fs.Category,
fs.FeedCount
FROM Users as u
LEFT JOIN UsersImages as ui
ON ui.UserID = u.UserID
LEFT JOIN FeedsSettings as fs
ON ui.UserID = fs.UserID
WHERE u.UserID = @UserID
GO

Create PROCEDURE Sp_UpdateUserAndProfileData
@UserID nvarchar(255),
@FullName nvarchar(255),
@Email nvarchar(255),
@Category nvarchar(255),
@FeedCount int
AS
IF EXISTS (SELECT 1 FROM Users WHERE UserID = @UserID) 
BEGIN
   Update Users
   SET 
      FullName = @FullName,
      Email = @Email
	WHERE UserID = @UserID
END
ELSE
BEGIN
	RETURN
END
IF EXISTS (SELECT 1 FROM FeedsSettings WHERE UserID = @UserID) 
BEGIN
   Update FeedsSettings
   SET 
      Category = @Category,
	  FeedCount = @FeedCount
	WHERE UserID = @UserID
END
ELSE
BEGIN
	INSERT INTO FeedsSettings
	(
		UserID,
		Category,
		FeedCount
	)
	VALUES
	(
		@UserID,
		@Category,
		@FeedCount
	)
END
GO