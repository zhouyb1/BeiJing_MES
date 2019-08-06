/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2019-01-04 10:56:26                          */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('Mes_InventoryLS')
            and   type = 'U')
   drop table Mes_InventoryLS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Mes_InventoryTrend')
            and   type = 'U')
   drop table Mes_InventoryTrend
go

/*==============================================================*/
/* Table: Mes_InventoryLS                                       */
/*==============================================================*/
create table Mes_InventoryLS (
   ID                   varchar(50)          not null,
   I_Date               datetime             null,
   I_StockCode          varchar(50)          null,
   I_StockName          varchar(50)          null,
   I_GoodsCode          varchar(50)          null,
   I_GoodsName          varchar(50)          null,
   I_Unit               varchar(50)          null,
   I_Qty                decimal(12,6)        null,
   I_Batch              varchar(50)          null,
   I_Period             varchar(50)          null,
   constraint PK_MES_INVENTORYLS primary key (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʷ����',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ʱ��',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_Date'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ֿ����',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_StockCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ֿ�����',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_StockName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ϱ���',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_GoodsCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_GoodsName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��λ',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_Unit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_Qty'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_Batch'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Mes_InventoryLS', 'column', 'I_Period'
go

/*==============================================================*/
/* Table: Mes_InventoryTrend                                    */
/*==============================================================*/
create table Mes_InventoryTrend (
   ID                   varchar(50)          not null,
   I_OrderKind          varchar(50)          null,
   I_StockCode          varchar(50)          null,
   I_StockName          varchar(50)          null,
   I_GoodsCode          varchar(50)          null,
   I_GoodsName          varchar(50)          null,
   I_Unit               varchar(50)          null,
   I_Batch              varchar(50)          null,
   I_Period             varchar(50)          null,
   I_OrderNo            varchar(50)          null,
   I_QtyOld             decimal(12,6)        null,
   I_QtyNew             decimal(12,6)        null,
   I_QtyTrend           decimal(12,6)        null,
   I_Remark             varchar(50)          null,
   constraint PK_MES_INVENTORYTREND primary key (ID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����ƶ���',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_OrderKind'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ֿ����',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_StockCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ֿ�����',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_StockName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ϱ���',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_GoodsCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_GoodsName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��λ',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_Unit'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_Batch'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_Period'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���ݺ�',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_OrderNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ʼ����',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_QtyOld'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '������',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_QtyNew'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�ƶ�����',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_QtyTrend'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', @CurrentUser, 'table', 'Mes_InventoryTrend', 'column', 'I_Remark'
go

