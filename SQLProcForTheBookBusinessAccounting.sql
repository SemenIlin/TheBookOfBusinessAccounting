USE TheBookOfBusinessAccounting;

GO
CREATE PROC AddCategory
@Title NVARCHAR(40)
AS BEGIN
INSERT INTO Categories(Title)
VALUES(@Title)
END

GO
CREATE PROC DelCategory
@Id INT
AS BEGIN
DELETE Categories
WHERE Id = @Id
END

GO
CREATE PROC UpdateCategory
@Id INT,
@Title NVARCHAR(40)
AS BEGIN
UPDATE Categories
SET Title = @Title
WHERE Id = @Id
END

GO
CREATE PROC AddStatus
@Title NVARCHAR(20)
AS BEGIN
INSERT INTO Statuses(Title)
VALUES(@Title)
END

GO
CREATE PROC DelStatus
@Id INT
AS BEGIN 
DELETE Statuses
WHERE Id = @Id
END

GO
CREATE PROC UpdateStatus
@Id INT,
@Title NVARCHAR(20)
AS BEGIN
UPDATE Statuses
SET Title = @Title
WHERE Id = @Id
END

GO 
CREATE PROC AddItem
@Title NVARCHAR(100),
@InventoryNumber NVARCHAR(50),
@LocationOfItem NVARCHAR(200),
@About NVARCHAR(500),
@CategoryId INT,
@StatusId INT
AS BEGIN
INSERT INTO Items(Tite, InventoryNumber, LocationOfItem, About, CategoryId, StatusId)
VALUES (@Title, @InventoryNumber, @LocationOfItem, @About, @CategoryId, @StatusId)
END

GO
CREATE PROC DelItem
@Id INT
AS BEGIN
DELETE Items
WHERE Id = @Id
END

GO
CREATE PROC UpdateItem
@Id INT,
@Title NVARCHAR(100),
@InventoryNumber NVARCHAR(50),
@LocationOfItem NVARCHAR(200),
@About NVARCHAR(500),
@CategoryId INT,
@StatusId INT
AS BEGIN 
UPDATE Items 
SET
Tite = @Title,
InventoryNumber =@InventoryNumber,
LocationOfItem = @LocationOfItem,
About = @About,
CategoryId = @CategoryId,
StatusId = @StatusId
WHERE Id = @Id
END

GO
CREATE PROC GetCategory
@Id INT
AS BEGIN
SELECT * FROM Categories
WHERE Id = @Id
END

GO
CREATE PROC GetStatus
@Id INT
AS BEGIN
SELECT * FROM Statuses
WHERE Id = @Id
END

GO
CREATE PROC GetItem
@Id INT
AS BEGIN
SELECT * FROM Items
WHERE Id = @Id
END

GO
CREATE PROC GetAllItems
AS BEGIN
SELECT * FROM Items
END

GO 
CREATE PROC GetAllStatuses
AS BEGIN
SELECT * FROM Statuses
END

GO
CREATE PROC GetAllCategories
AS BEGIN
SELECT * FROM Categories
END

GO
CREATE PROC GetImages
@Id INT
AS BEGIN
SELECT * FROM Images
WHERE Id = @Id
END

GO
CREATE PROC AddImage
@Screen IMAGE,
@ScreenFormat VARCHAR(5),
@ItemId INT
AS BEGIN
INSERT INTO Images(Screen, ScreenFormat, ItemId)
VALUES (@Screen, @ScreenFormat, @ItemId)
END

GO
CREATE PROC DelImage
@Id INT
AS BEGIN
DELETE Images
WHERE Id = @Id
END

GO
CREATE PROC UpdateImage
@Id INT,
@Screen IMAGE,
@ScreenFormat VARCHAR(5),
@ItemId INT
AS BEGIN
UPDATE Images
SET
Screen = @Screen,
ScreenFormat = @ScreenFormat,
ItemId = @ItemId
WHERE Id = @Id
END

GO
CREATE PROC GetAllImages
AS BEGIN
SELECT * FROM Images
END

