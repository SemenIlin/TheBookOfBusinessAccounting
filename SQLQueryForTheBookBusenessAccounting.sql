CREATE DATABASE TheBookOfBusinessAccounting
GO 
USE TheBookOfBusinessAccounting;

CREATE TABLE Users
(
   Id INT PRIMARY KEY IDENTITY(1,1),
   UserLogin NVARCHAR(50) UNIQUE,
   UserPassword NVARCHAR(50),
   Email NVARCHAR(100) UNIQUE,
);

INSERT INTO Users
VALUES
('user', 'user', 'user@mail.ru'),
('editor', 'editor', 'editor@mail.ru'),
('admin', 'admin', 'admin@mail.ru');

CREATE TABLE Roles
(
  Id INT PRIMARY KEY IDENTITY(1,1),
  RoleName NVARCHAR(20)
);

INSERT INTO Roles
VALUES
('User'),
('Editor'),
('Administrator');

CREATE TABLE UsersRoles
(
   Id INT PRIMARY KEY IDENTITY(1,1),
   UserId INT,
   RoleId INT,
   FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE SET DEFAULT,
   FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE SET DEFAULT
);

INSERT INTO UsersRoles
VALUES
(1, 1),
(2, 1),
(2, 2),
(3, 1),
(3, 2),
(3, 3);

CREATE TABLE Categories
( 
   Id INT PRIMARY KEY IDENTITY(1,1),
   Title NVARCHAR(40)
);

CREATE TABLE Statuses
(
   Id INT PRIMARY KEY IDENTITY(1, 1),
   Title NVARCHAR(20)
);

INSERT INTO Statuses
VALUES
('будет приобретён'),
('в работе'),
('списан');

CREATE TABLE Items
(  
   Id INT PRIMARY KEY IDENTITY(1,1),
   Tite NVARCHAR(100),
   InventoryNumber NVARCHAR(50),
   LocationOfItem NVARCHAR(200),
   About NVARCHAR(500),
   CategoryId INT,
   StatusId INT,
   FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE,
   FOREIGN KEY (StatusId) REFERENCES Statuses(Id) ON DELETE CASCADE,
);

CREATE TABLE Images (
	Id BIGINT NOT NULL PRIMARY KEY IDENTITY,
	Screen IMAGE DEFAULT (0x),
	ScreenFormat VARCHAR(5) DEFAULT NULL,
	ItemId INT, 
	FOREIGN KEY (ItemId) REFERENCES Items(Id) ON DELETE CASCADE
);

