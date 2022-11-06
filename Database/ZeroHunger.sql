-- Switch to the master database to create a new database
USE master;

-- Drop ZeroHunger database if exists
DROP DATABASE IF EXISTS ZeroHunger
GO

-- Create a new database named ZeroHunger
CREATE DATABASE ZeroHunger;
GO

-- Switch to the newly created database
USE ZeroHunger;
GO

CREATE TABLE Employees (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name nvarchar(50) NOT NULL,
    Email nvarchar(50) NOT NULL,
    Age int NOT NULL,
    Sex nvarchar(50) NOT NULL
)

CREATE TABLE History (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Order_Id int NOT NULL,
    Employee_Id int NOT NULL,
    Restaurant_Id int NOT NULL,
    Collection_Time datetime2 NOT NULL
)

CREATE TABLE Orders (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Request_Id int NOT NULL,
    Employee_id int NOT NULL,
    Order_Date datetime2 NOT NULL,
    Status nvarchar(20) NOT NULL
)

CREATE TABLE Requests (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Restaurants_Id int NOT NULL,
    Description text NOT NULL,
    Expiry_Date datetime2 NOT NULL,
    Status nvarchar(20) NOT NULL
)

CREATE TABLE Restaurants (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name nvarchar(50) NOT NULL,
    Location nvarchar(50) NOT NULL
)

INSERT INTO Employees (Name, Email, Age, Sex)
VALUES ('Miles Richards', 'miles@email.com', 25, 'male'),
       ('Denis Owen', 'denis@email.com', 32, 'male'),
       ('Edwin Rodrick', 'edwin@email.com', 27, 'male'),
       ('Ana Smith', 'ana@email.com', 29, 'female'),
       ('Rosa Dixon', 'rosa@email.com', 36, 'female')

INSERT INTO History (Order_Id, Employee_Id, Restaurant_Id, Collection_Time)
VALUES (1, 2, 1, '2022-11-02 00:00:00'),
       (2, 3, 4, '2022-11-02 00:00:00'),
       (3, 5, 5, '2022-11-06 09:54:04'),
       (4, 1, 3, '2022-11-06 09:54:06'),
       (5, 1, 3, '2022-11-06 09:54:24'),
       (6, 3, 4, '2022-11-06 09:54:25'),
       (7, 1, 1, '2023-03-20 00:06:02')

INSERT INTO Orders (Request_Id, Employee_id, Order_Date, Status)
VALUES (1, 2, '2022-11-01 00:00:00', 'Collected'),
       (2, 3, '2022-11-01 00:00:00', 'Collected'),
       (5, 5, '2022-11-01 00:00:00', 'Collected'),
       (11, 1, '2022-11-06 09:50:24', 'Collected'),
       (12, 1, '2022-11-06 09:50:28', 'Collected'),
       (14, 3, '2022-11-06 09:50:40', 'Collected'),
       (15, 4, '2022-11-06 09:50:46', 'Pending')

INSERT INTO Requests (Restaurants_Id, Description, Expiry_Date, Status)
VALUES (1, '20kg rice, 6kg chicken, etc.', '2022-12-31 00:00:00', 'Collected'),
       (4, '50kg rice, 10kg vegetable, etc.', '2022-12-25 00:00:00', 'Collected'),
       (5, '68kg kacchi biriyani', '2022-12-27 00:00:00', 'Waiting'),
       (5, '100kg rice', '2022-12-20 00:00:00', 'Collected'),
       (1, '6kg vegetables', '2022-11-29 09:45:00', 'Waiting'),
       (1, '20kg rice, 3kg beef etc', '2022-11-06 09:46:00', 'Waiting'),
       (2, 'Snacks items', '2022-11-26 09:46:00', 'Waiting'),
       (1, 'Random foods', '2022-11-30 09:47:00', 'Waiting'),
       (3, '100kg rice', '2022-11-22 09:48:00', 'Waiting'),
       (3, '20kg chicken', '2022-11-30 09:48:00', 'Collected'),
       (3, '10kg mutton', '2022-11-25 09:48:00', 'Collected'),
       (4, 'random foods', '2022-11-22 09:48:00', 'Waiting'),
       (4, 'snacks and drinks ', '2022-11-23 09:49:00', 'Collected'),
       (5, '30kg biryani ', '2022-11-15 09:49:00', 'Pending'),
       (2, '100kg rice', '2022-11-06 09:53:00', 'Waiting'),
       (2, '30kg kacchi', '2022-11-09 09:53:00', 'Waiting'),
       (2, '20kg chicken', '2022-11-24 09:53:00', 'Waiting'),
       (4, '30kg chicken', '2022-11-24 09:54:00', 'Waiting'),
       (3, '60kg rice', '2022-11-30 09:54:00', 'Waiting'),
       (4, 'random vegetables ', '2022-11-30 09:54:00', 'Waiting'),
       (5, '30kg mutton', '2022-11-24 09:55:00', 'Waiting'),
       (1, '20kg roast', '2023-03-20 00:04:00', 'Waiting'),
       (1, '69kg kacchi', '2023-03-21 00:04:00', 'Collected')

INSERT INTO Restaurants (Name, Location)
VALUES ('BearBurger', 'Bashundhara'),
       ('Pagla Baburchi', 'Banani'),
       ('Kapara Kafe', 'Gulshan'),
       ('La Jawaab', 'Mirpur'),
       ('Banglar Kacchi', 'Kuril')

ALTER TABLE History ADD CONSTRAINT FK_Collections_Employees FOREIGN KEY(Employee_Id)
REFERENCES Employees (Id) ON DELETE CASCADE

ALTER TABLE History ADD CONSTRAINT FK_Collections_Orders FOREIGN KEY(Order_Id)
REFERENCES Orders (Id)

ALTER TABLE History ADD CONSTRAINT FK_History_Restaurants FOREIGN KEY(Restaurant_Id)
REFERENCES Restaurants (Id)

ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Employees FOREIGN KEY(Employee_id)
REFERENCES Employees (Id) ON DELETE CASCADE

ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Requests FOREIGN KEY(Request_Id)
REFERENCES Requests (Id)

ALTER TABLE Requests ADD CONSTRAINT FK_Requests_Restaurants FOREIGN KEY(Restaurants_Id)
REFERENCES Restaurants (Id)