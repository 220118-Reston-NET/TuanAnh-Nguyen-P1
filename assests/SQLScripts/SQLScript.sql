-- P1 Revature Project --
-- Tuan Anh Nguyen --

-- Customers -------
CREATE TABLE Customers(
	cusID UNIQUEIDENTIFIER,
	cusFName varchar(50),
	cusLName varchar(50),
	cusAddress varchar(50),
	cusEmail varchar(50),
	cusPhoneNo varchar(10),
  dateOfBirth smalldatetime,
	PRIMARY KEY (cusID)
)

-- StoreFronts -------
CREATE TABLE StoreFronts(
	storeID UNIQUEIDENTIFIER,
	storeName varchar(50),
	storeAddress varchar(50),
	PRIMARY KEY (storeID)
)

-- Products -------
CREATE TABLE Products(
	productID UNIQUEIDENTIFIER,
	productName varchar(50),
	productPrice smallmoney,
	productDesc varchar(100),
  minimumAge int,
	createdAt smalldatetime,
	PRIMARY KEY (productID)
)

-- Orders -------
CREATE TABLE Orders(
	orderID UNIQUEIDENTIFIER,
	cusID UNIQUEIDENTIFIER,
	storeID UNIQUEIDENTIFIER,
	totalPrice smallmoney,
  orderStatus varchar(15),
	createdAt smalldatetime,
	PRIMARY KEY (orderID),
	FOREIGN KEY (cusID) REFERENCES Customers(cusID),
	FOREIGN KEY (storeID) REFERENCES StoreFronts(storeID)
)

-- LineItems -------
CREATE TABLE LineItems(
	productID UNIQUEIDENTIFIER,
	orderID UNIQUEIDENTIFIER,
	quantity int,
  priceAtCheckedOut smallmoney,
	FOREIGN KEY (productID) REFERENCES Products(productID),
	FOREIGN KEY (orderID) REFERENCES Orders(orderID)
)

-- Inventory -------
CREATE TABLE Inventory(
	storeID UNIQUEIDENTIFIER,
	productID UNIQUEIDENTIFIER,
	quantity int,
	FOREIGN KEY (storeID) REFERENCES StoreFronts(storeID),
	FOREIGN KEY (productID) REFERENCES Products(productID)
)

-- Shipment -------
CREATE TABLE Tracking(
	trackingID UNIQUEIDENTIFIER,
	orderID UNIQUEIDENTIFIER,
	trackingNumber varchar(50),
	PRIMARY KEY (trackingID),
	FOREIGN KEY (orderID) REFERENCES Orders(orderID)
)

-- User -------
CREATE TABLE Users(
    userID UNIQUEIDENTIFIER,
    userName varchar(50),
    passwordHash varchar(MAX),
    passwordSalt varchar(MAX),
    userRole int,
    isActivated bit,
    createdAt smalldatetime
)