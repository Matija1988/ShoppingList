use master;
go
drop database if exists "ShoppingList";
go
create database "ShoppingList";
go
alter database "ShoppingList" collate CROATIAN_CI_AS;
go
use "ShoppingList";

Create table shop_users
(
	usr_id uniqueidentifier primary key not null,
	usr_username varchar(50) not null,
	usr_password varchar(255) not null,
	usr_email varchar(255) not null,
	usr_dateCreated datetime not null,
	usr_dateUpdated datetime,
	usr_isActive bit not null default 1,
);

CREATE TABLE shop_products
(
	prod_id int primary key identity(1,1),
	prod_unitPrice decimal(10,2),
	prod_name varchar(50),
	prod_dateCreated datetime not null,
	prod_dateUpdated datetime,
	prod_isActive bit not null default 1,
);

CREATE TABLE shop_lists
(
	lst_id int primary key identity(1,1),
	lst_name varchar(50),
	lst_dateCreated datetime not null,
	lst_dateUpdated datetime,
	lst_isActive bit not null default 1,
	lst_userId uniqueidentifier,
	lst_totalValue decimal(18,2),

	CONSTRAINT FK_ShopList_UserId FOREIGN KEY (lst_userId) REFERENCES shop_users(usr_id)
);

CREATE TABLE shop_listProducts
(
	ulp_id int primary key identity(1,1),
	ulp_productId int not null,
	ulp_listId int not null,
	ulp_totalValue decimal(10,2),
	ulp_productQuantity int not null default 1,

	CONSTRAINT FK_ULP_ProductId FOREIGN KEY (ulp_productId) REFERENCES shop_products(prod_id),
	CONSTRAINT FK_ULP_listId FOREIGN KEY (ulp_listId) REFERENCES shop_lists(lst_id)
);
