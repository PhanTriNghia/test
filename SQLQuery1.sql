--drop database Ontap
Create database Ontap
go

Use Ontap
go

-- drop table Laptop
Create table Factory
(
	FactoryID int identity(1,1) primary key,
	FactoryName nvarchar(100),
)

Create table Laptop
(
	ID int identity(1,1) primary key,
	LaptopName nvarchar(100),
	ImagePath nvarchar(250),
	Price decimal(18,3),
	FactoryID int,
)	

ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD CONSTRAINT [FK_FactoryID] FOREIGN KEY(FactoryID) REFERENCES [dbo].[Factory] ([FactoryID])