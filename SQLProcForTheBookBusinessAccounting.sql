USE TheBookOfBusinessAccounting;

--GO
--CREATE PROC AddCategory
--@Title NVARCHAR(40)
--AS BEGIN
--INSERT INTO Categories(Title)
--VALUES(@Title)
--END

--GO
--CREATE PROC DelCategory
--@Id INT
--AS BEGIN
--DELETE Categories
--WHERE Id = @Id
--END

--GO
--CREATE PROC UpdateCategory
--@Id INT,
--@Title NVARCHAR(40)
--AS BEGIN
--UPDATE Categories
--SET Title = @Title
--WHERE Id = @Id
--END

--GO
--CREATE PROC AddStatus
--@Title NVARCHAR(20)
--AS BEGIN
--INSERT INTO Statuses(Title)
--VALUES(@Title)
--END

--GO
--CREATE PROC DelStatus
--@Id INT
--AS BEGIN 
--DELETE Statuses
--WHERE Id = @Id
--END

--GO
--CREATE PROC UpdateStatus
--@Id INT,
--@Title NVARCHAR(20)
--AS BEGIN
--UPDATE Statuses
--SET Title = @Title
--WHERE Id = @Id
--END

--GO 
--CREATE PROC AddItem
--@Title NVARCHAR(100),
--@InventoryNumber NVARCHAR(50),
--@LocationOfItem NVARCHAR(200),
--@About NVARCHAR(500),
--@CategoryId INT,
--@StatusId INT,
--@Id INT OUTPUT
--AS BEGIN
--INSERT INTO Items(Title, InventoryNumber, LocationOfItem, About, CategoryId, StatusId)
--VALUES (@Title, @InventoryNumber, @LocationOfItem, @About, @CategoryId, @StatusId)
--SET @Id = @@IDENTITY
--END

--GO
--CREATE PROC DelItem
--@Id INT
--AS BEGIN
--DELETE Items
--WHERE Id = @Id
--END

--GO
--CREATE PROC UpdateItem
--@Id INT,
--@Title NVARCHAR(100),
--@InventoryNumber NVARCHAR(50),
--@LocationOfItem NVARCHAR(200),
--@About NVARCHAR(500),
--@CategoryId INT,
--@StatusId INT
--AS BEGIN 
--UPDATE Items 
--SET
--Title = @Title,
--InventoryNumber =@InventoryNumber,
--LocationOfItem = @LocationOfItem,
--About = @About,
--CategoryId = @CategoryId,
--StatusId = @StatusId
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetCategory
--@Id INT
--AS BEGIN
--SELECT * FROM Categories
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetStatus
--@Id INT
--AS BEGIN
--SELECT * FROM Statuses
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetItem
--@Id INT
--AS BEGIN
--SELECT Items.Id, Items.Title, Items.InventoryNumber,Items.LocationOfItem,Items.About,
--       Items.CategoryId,Items.StatusId,Categories.Title, Statuses.Title 
--FROM Items 
--INNER JOIN Categories ON Categories.Id = Items.CategoryId
--INNER JOIN Statuses ON Statuses.Id = Items.StatusId
--WHERE Items.Id = @Id
--END

--GO
--alter PROC GetListImagesOfItem
--@Id INT
--AS BEGIN
--SELECT Id, Screen, ScreenFormat, ItemId FROM Images
--WHERE ItemId = @Id
--END

--GO
--CREATE PROC GetAllItems
--AS BEGIN
--SELECT Items.Id, Items.Title, Items.InventoryNumber,Items.LocationOfItem,Items.About,
--       Items.CategoryId,Items.StatusId ,Categories.Title, Statuses.Title
--FROM Items
--INNER JOIN Categories ON Categories.Id = Items.CategoryId
--INNER JOIN Statuses ON Statuses.Id = Items.StatusId

--END

--GO 
--CREATE PROC GetAllStatuses
--AS BEGIN
--SELECT * FROM Statuses
--END

--GO
--CREATE PROC GetAllCategories
--AS BEGIN
--SELECT * FROM Categories
--END

--GO
--CREATE PROC GetImages
--@Id INT
--AS BEGIN
--SELECT * FROM Images
--WHERE Id = @Id
--END

--GO
--CREATE PROC AddImage
--@Screen IMAGE,
--@ScreenFormat VARCHAR(5),
--@ItemId INT,
--@Id INT OUTPUT
--AS BEGIN
--INSERT INTO Images(Screen, ScreenFormat, ItemId)
--VALUES (@Screen, @ScreenFormat, @ItemId)
--SET @Id = @@IDENTITY
--END

--GO
--CREATE PROC DelImage
--@Id INT
--AS BEGIN
--DELETE Images
--WHERE Id = @Id
--END

--GO
--CREATE PROC UpdateImage
--@Id INT,
--@Screen IMAGE,
--@ScreenFormat VARCHAR(5),
--@ItemId INT
--AS BEGIN
--UPDATE Images
--SET
--Screen = @Screen,
--ScreenFormat = @ScreenFormat,
--ItemId = @ItemId
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetAllImages
--AS BEGIN
--SELECT * FROM Images
--END

--GO
--CREATE PROC FindItems
--@CategoryName NVARCHAR(40),
--@StatusId INT,
--@Name NVARCHAR(100),
--@PageSize INT,
--@Skip INT
--AS BEGIN
--	SELECT * FROM Items
--    INNER JOIN Categories ON Categories.Id = Items.CategoryId
--	WHERE (@StatusId = 0 OR StatusId = @StatusId)  AND
--	      (@CategoryName = '' OR Categories.Title = @CategoryName) AND 
--		  Items.Title LIKE @Name+'%'
--	ORDER BY Items.Title
--	OFFSET @PageSize * @Skip ROWS
--    FETCH NEXT @PageSize ROWS ONLY
--END

--GO
--CREATE PROC FindAllItems
--@CategoryName NVARCHAR(40),
--@StatusId INT,
--@Name NVARCHAR(100)
--AS BEGIN
--	SELECT * FROM Items
--    INNER JOIN Categories ON Categories.Id = Items.CategoryId
--	WHERE (@StatusId = 0 OR StatusId = @StatusId)  AND
--	      (@CategoryName = '' OR Categories.Title = @CategoryName) AND 
--		  Items.Title LIKE @Name+'%'	
--END

--GO
--CREATE PROC GetUsersForPage
--@UserLogin NVARCHAR(50),
--@PageSize INT,
--@Skip INT
--AS BEGIN
--	SELECT * FROM Users
--	WHERE UserLogin LIKE @UserLogin+'%'
--	ORDER BY UserLogin
--	OFFSET @PageSize * @Skip ROWS
--    FETCH NEXT @PageSize ROWS ONLY
--END

--GO
--CREATE PROC GetUsers
--@UserLogin NVARCHAR(50)
--AS BEGIN
--	SELECT * FROM Users
--	WHERE UserLogin LIKE @UserLogin+'%'	
--END

--GO
--CREATE PROC AddUser
--@UserLogin NVARCHAR(50),
--@UserName  NVARCHAR(50),
--@UserPassword NVARCHAR(50),
--@Email NVARCHAR(50),
--@Id INT OUTPUT
--AS BEGIN
--INSERT INTO Users(UserLogin, UserPassword, Email, UserName)
--VALUES (@UserLogin, @UserPassword, @Email, @UserName)
--SET @Id = @@IDENTITY
--END

--GO
--CREATE PROC DelUser
--@Id INT
--AS BEGIN
--DELETE Users
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetUser
--@Id INT
--AS BEGIN
--   SELECT * FROM Users
--   WHERE Id = @Id
--END

--GO 
--CREATE PROC GetAllUsers
--AS BEGIN
--SELECT * FROM Users
--END

--GO 
--CREATE PROC AddRoleForUser
--@Id INT,
--@RoleId INT
--AS BEGIN
--    WHILE @RoleId > 0
--	BEGIN
--	IF NOT EXISTS(SELECT * FROM UsersRoles WHERE UserId = @Id AND RoleId = @RoleId) 
--	   BEGIN
--	     INSERT INTO UsersRoles(UserId, RoleId)
--	     VALUES (@Id, @RoleId)	   
--	   END
--	   SET @RoleId = @RoleId - 1
--	END
--END

--GO 
--CREATE PROC DelRoleFromUser
--@Id INT,
--@RoleId INT
--AS BEGIN
--	IF EXISTS(SELECT * FROM UsersRoles WHERE UserId = @Id AND RoleId = @RoleId) 
--	   BEGIN
--	     DELETE UsersRoles
--	     WHERE RoleId = @RoleId AND UserId = @Id	   
--	   END	
--END

--GO 
--CREATE PROC UpdateUser
--@Id INT,
--@UserName NVARCHAR(50),
--@UserPassword NVARCHAR(50)
--AS BEGIN 
--UPDATE Users
--SET
--UserPassword = @UserPassword,
--UserName = @UserName
--WHERE Id = @Id
--END

--GO
--CREATE PROC GetListRolesOfUser
--@Id INT
--AS BEGIN
--   SELECT RoleId FROM UsersRoles
--   WHERE UserId = @Id
--END

--GO 
--CREATE PROC GetRole
--@Id INT
--AS BEGIN
--   SELECT * FROM Roles
--   WHERE Id = @Id
--END

--GO
--CREATE PROC GetListRoles
--AS BEGIN
--   SELECT * FROM Roles
--END

--GO 
--CREATE PROC FindUserIsLogin
--@UserLogin NVARCHAR(50),
--@UserPassword NVARCHAR(50)
--AS BEGIN
--   SELECT * FROM Users
--   WHERE UserLogin = @UserLogin AND UserPassword = @UserPassword
--END

--GO 
--CREATE PROC FindUser
--@UserLogin NVARCHAR(50)
--AS BEGIN
--   SELECT * FROM Users
--   WHERE UserLogin = @UserLogin 
--END

--GO
--CREATE PROC GetRoles
--@UserLogin NVARCHAR(50)
--AS BEGIN
--   SELECT Roles.Id, RoleName FROM Roles
--   INNER JOIN UsersRoles ON UsersRoles.RoleId = Roles.Id
--   INNER JOIN Users ON Users.Id = UsersRoles.UserId
--   WHERE Users.UserLogin = @UserLogin
--END

--DECLARE @userId int, @roleId int
--set @userId = 1
--set @roleId = 2
--exec AddRoleForUser @userId, @roleId
