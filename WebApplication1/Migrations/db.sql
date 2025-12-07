-- Create database and use it
IF DB_ID('assessmentdb') IS NULL
    CREATE DATABASE assessmentdb;
GO

USE assessmentdb;
GO

------------------------------------------------------------
-- Table: ltcourierfee
------------------------------------------------------------
DROP TABLE IF EXISTS ltcourierfee;
GO

CREATE TABLE ltcourierfee (
    WeightID INT NOT NULL,
    CourierID INT NOT NULL,
    StartKg INT NOT NULL,
    EndKg INT NULL,
    Price DECIMAL(10,0) NULL
);
GO

INSERT INTO ltcourierfee (WeightID, CourierID, StartKg, EndKg, Price) VALUES
(1, 1, 1, 2, 8000),
(2, 1, 3, 4, 9500),
(3, 2, 1, 2, 7500),
(4, 2, 3, 4, 8500),
(5, 3, 1, 2, 10000),
(6, 3, 3, 4, 10000),
(7, 1, 5, 10, 10500),
(8, 2, 5, 10, 9500),
(9, 3, 5, 10, 12000);
GO

------------------------------------------------------------
-- Table: mscourier
------------------------------------------------------------
DROP TABLE IF EXISTS mscourier;
GO

CREATE TABLE mscourier (
    CourierID INT NOT NULL PRIMARY KEY,
    CourierName VARCHAR(50) NOT NULL
);
GO

INSERT INTO mscourier (CourierID, CourierName) VALUES
(1, 'JNE'),
(2, 'J&T'),
(3, 'Wahana');
GO

------------------------------------------------------------
-- Table: mspayment
------------------------------------------------------------
DROP TABLE IF EXISTS mspayment;
GO

CREATE TABLE mspayment (
    PaymentID INT NOT NULL PRIMARY KEY,
    PaymentName VARCHAR(50) NOT NULL
);
GO

INSERT INTO mspayment (PaymentID, PaymentName) VALUES
(1, 'Cash'),
(2, 'COD');
GO

------------------------------------------------------------
-- Table: msproduct
------------------------------------------------------------
DROP TABLE IF EXISTS msproduct;
GO

CREATE TABLE msproduct (
    ProductID INT NOT NULL PRIMARY KEY,
    ProductName VARCHAR(50) NOT NULL,
    Weight FLOAT NOT NULL,
    Price DECIMAL(10,0) NOT NULL
);
GO

INSERT INTO msproduct (ProductID, ProductName, Weight, Price) VALUES
(1, 'Tepung', 1.5, 10000),
(7, 'Bluband', 0.25, 8000),
(9, 'Beras', 1, 64000),
(10, 'Eskrim', 0.5, 20000),
(11, 'Kentang', 1, 15000);
GO

------------------------------------------------------------
-- Table: mssales
------------------------------------------------------------
DROP TABLE IF EXISTS mssales;
GO

CREATE TABLE mssales (
    SalesID INT NOT NULL PRIMARY KEY,
    SalesName VARCHAR(50) NOT NULL
);
GO

INSERT INTO mssales (SalesID, SalesName) VALUES
(1, 'Andy'),
(2, 'Jessica');
GO

------------------------------------------------------------
-- Table: trinvoice
------------------------------------------------------------
DROP TABLE IF EXISTS trinvoice;
GO

CREATE TABLE trinvoice (
    InvoiceNo VARCHAR(10) NOT NULL PRIMARY KEY,
    InvoiceDate DATETIME NOT NULL,
    InvoiceTo VARCHAR(500) NOT NULL,
    ShipTo VARCHAR(500) NOT NULL,
    SalesID INT NOT NULL,
    CourierID INT NOT NULL,
    PaymentType INT NOT NULL,
    CourierFee DECIMAL(10,0) NOT NULL
);
GO

INSERT INTO trinvoice (InvoiceNo, InvoiceDate, InvoiceTo, ShipTo, SalesID, CourierID, PaymentType, CourierFee) VALUES
('IN0001', '2015-06-23', 'Invoice Orang 1', 'Ship Orang 1', 1, 1, 1, 0),
('IN0002', '2019-02-27', 'Invoice Orang 2', 'Ship Orang 2', 2, 2, 2, 0);
GO

------------------------------------------------------------
-- Table: trinvoicedetail
------------------------------------------------------------
DROP TABLE IF EXISTS trinvoicedetail;
GO

CREATE TABLE trinvoicedetail (
    InvoiceNo VARCHAR(10) NOT NULL,
    ProductID INT NOT NULL,
    Weight FLOAT NOT NULL,
    Qty SMALLINT NOT NULL,
    Price DECIMAL(10,0) NOT NULL
);
GO

INSERT INTO trinvoicedetail (InvoiceNo, ProductID, Weight, Qty, Price) VALUES
('IN0001', 1, 1.5, 3, 10000),
('IN0001', 7, 0.25, 1, 8000),
('IN0001', 9, 2, 3, 64000),
('IN0002', 7, 0.25, 1, 8000),
('IN0002', 10, 0.5, 3, 20000),
('IN0002', 9, 2, 2, 64000);
GO
