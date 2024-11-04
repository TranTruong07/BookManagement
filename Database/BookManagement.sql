-- Tạo database
CREATE DATABASE BookManagement;
GO

USE BookManagement;

CREATE TABLE [Admin] (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL  -- Lưu mật khẩu dưới dạng mã hóa
);
-- Tạo bảng Customer (khách hàng)
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(15),
    Address NVARCHAR(255),
    RegistrationDate DATETIME DEFAULT GETDATE()
);

-- Tạo bảng Category (thể loại sách)
CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL
);

-- Tạo bảng Book (sách)
CREATE TABLE Book (
    BookID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255),
    PublishedYear INT,
    CategoryID INT,
    IsAvailable BIT DEFAULT 1,  -- Xác định sách có sẵn hay không
    CONSTRAINT FK_Book_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Tạo bảng Order (lịch sử mượn sách)
CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    BookID INT,
    BorrowDate DATETIME DEFAULT GETDATE(),
    ReturnDate DATETIME,
    CONSTRAINT FK_Order_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    CONSTRAINT FK_Order_Book FOREIGN KEY (BookID) REFERENCES Book(BookID)
);
Go
-- Thêm dữ liệu vào bảng Category
INSERT INTO Category (CategoryName) VALUES 
('Science Fiction'), 
('Fantasy'), 
('Mystery'), 
('Biography');

-- Thêm dữ liệu vào bảng Book
INSERT INTO Book (Title, Author, PublishedYear, CategoryID) VALUES 
('Dune', 'Frank Herbert', 1965, 1), 
('The Hobbit', 'J.R.R. Tolkien', 1937, 2), 
('The Hound of the Baskervilles', 'Arthur Conan Doyle', 1902, 3),
('Steve Jobs', 'Walter Isaacson', 2011, 4);

-- Thêm dữ liệu vào bảng Customer
INSERT INTO Customer (FullName, Email, Phone, Address) VALUES 
('John Doe', 'john.doe@example.com', '123456789', '123 Main St'),
('Jane Smith', 'jane.smith@example.com', '987654321', '456 Oak Ave');

-- Thêm dữ liệu vào bảng Order (lịch sử mượn sách)
INSERT INTO [Order] (CustomerID, BookID, BorrowDate, ReturnDate) VALUES 
(1, 1, '2024-10-01', '2024-10-15'),
(2, 3, '2024-09-20', '2024-09-25');  -- NULL nghĩa là chưa trả sách

INSERT INTO Admin (Username, Password) VALUES 
('admin', '123');