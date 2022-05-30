--
-- Скрипт сгенерирован Devart dbForge Studio 2019 for SQL Server, Версия 5.8.107.0
-- Домашняя страница продукта: http://www.devart.com/ru/dbforge/sql/studio
-- Дата скрипта: 31.05.2022 0:25:26
-- Версия сервера: 13.00.5026
--



USE apteki
GO

IF DB_NAME() <> N'apteki' SET NOEXEC ON
GO

--
-- Создать таблицу [dbo].[sysdiagrams]
--
PRINT (N'Создать таблицу [dbo].[sysdiagrams]')
GO
CREATE TABLE dbo.sysdiagrams (
  name sysname NOT NULL,
  principal_id int NOT NULL,
  diagram_id int IDENTITY,
  version int NULL,
  definition varbinary(max) NULL,
  PRIMARY KEY CLUSTERED (diagram_id),
  CONSTRAINT UK_principal_name UNIQUE (principal_id, name)
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[products]
--
PRINT (N'Создать таблицу [dbo].[products]')
GO
CREATE TABLE dbo.products (
  id_product bigint IDENTITY,
  product_name nvarchar(1000) NOT NULL,
  CONSTRAINT PK_products PRIMARY KEY CLUSTERED (id_product)
)
ON [PRIMARY]
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[products].[id_product] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[products].[id_product] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'id товара', 'SCHEMA', N'dbo', 'TABLE', N'products', 'COLUMN', N'id_product'
GO

--
-- Добавить расширенное свойство [MS_Description] для [dbo].[products].[product_name] (столбец)
--
PRINT (N'Добавить расширенное свойство [MS_Description] для [dbo].[products].[product_name] (столбец)')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'Наименование товара', 'SCHEMA', N'dbo', 'TABLE', N'products', 'COLUMN', N'product_name'
GO

--
-- Создать таблицу [dbo].[apteks]
--
PRINT (N'Создать таблицу [dbo].[apteks]')
GO
CREATE TABLE dbo.apteks (
  id_apteka int IDENTITY,
  apteka_name nvarchar(255) NOT NULL,
  apteka_address nvarchar(255) NOT NULL,
  apteka_telephone nvarchar(20) NOT NULL,
  CONSTRAINT PK_apteks PRIMARY KEY CLUSTERED (id_apteka)
)
ON [PRIMARY]
GO

--
-- Создать таблицу [dbo].[stores]
--
PRINT (N'Создать таблицу [dbo].[stores]')
GO
CREATE TABLE dbo.stores (
  id_store int IDENTITY,
  id_apteka int NOT NULL,
  store_name nvarchar(255) NOT NULL,
  CONSTRAINT PK_stores PRIMARY KEY CLUSTERED (id_store)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_stores_apteks] для объекта типа таблица [dbo].[stores]
--
PRINT (N'Создать внешний ключ [FK_stores_apteks] для объекта типа таблица [dbo].[stores]')
GO
ALTER TABLE dbo.stores
  ADD CONSTRAINT FK_stores_apteks FOREIGN KEY (id_apteka) REFERENCES dbo.apteks (id_apteka) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Создать таблицу [dbo].[consignments]
--
PRINT (N'Создать таблицу [dbo].[consignments]')
GO
CREATE TABLE dbo.consignments (
  id_consignment int IDENTITY,
  id_product bigint NOT NULL,
  id_store int NOT NULL,
  product_count int NOT NULL,
  CONSTRAINT PK_consignments PRIMARY KEY CLUSTERED (id_consignment)
)
ON [PRIMARY]
GO

--
-- Создать внешний ключ [FK_products_by_consignments_products] для объекта типа таблица [dbo].[consignments]
--
PRINT (N'Создать внешний ключ [FK_products_by_consignments_products] для объекта типа таблица [dbo].[consignments]')
GO
ALTER TABLE dbo.consignments
  ADD CONSTRAINT FK_products_by_consignments_products FOREIGN KEY (id_product) REFERENCES dbo.products (id_product) ON DELETE CASCADE ON UPDATE CASCADE
GO

--
-- Создать внешний ключ [FK_products_by_consignments_stores] для объекта типа таблица [dbo].[consignments]
--
PRINT (N'Создать внешний ключ [FK_products_by_consignments_stores] для объекта типа таблица [dbo].[consignments]')
GO
ALTER TABLE dbo.consignments
  ADD CONSTRAINT FK_products_by_consignments_stores FOREIGN KEY (id_store) REFERENCES dbo.stores (id_store) ON DELETE CASCADE ON UPDATE CASCADE
GO
SET NOEXEC OFF
GO