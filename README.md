# APPPInCSharp_Payroll

無瑕的程式碼 : 物件導向原則、設計模式與C#實踐 Agile Principles, Patterns, and Practices in C#

## 案例研究

* User Story => Use Case Analysis(思考系統行為) => 尋找抽象概念

### 員工支付種類抽象 PaymentClassification

* 時薪
* 月薪
* 佣金

### 支付時間表抽象 PaymentSchedule

* 每周五支付
* 每個月最後一天支付
* 每兩周支付

### 支付方式 PaymentMethod

### 從屬關係 Affiliation

* 員工可以從屬許多組織，並自動從該員工薪水中支付組織的費用

## DB Schema

```sql
USE [Payroll]
GO
/****** Object:  Table [dbo].[Affiliation]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Affiliation](
    [Id] [int] NOT NULL,
    [Dues] [decimal](9, 2) NULL,
PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CommissionedClassification]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommissionedClassification](
    [Salary] [decimal](9, 2) NULL,
    [Commission] [decimal](9, 2) NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DirectDepositAccount]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DirectDepositAccount](
    [Bank] [nvarchar](50) NOT NULL,
    [Account] [nvarchar](50) NOT NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
    [EmpId] [int] NOT NULL,
    [Name] [nvarchar](50) NULL,
    [Address] [nvarchar](50) NULL,
    [ScheduleType] [nvarchar](50) NULL,
    [PaymentMethodType] [nvarchar](50) NULL,
    [PaymentClassificationType] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
    [EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeeAffiliation]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeAffiliation](
    [EmpId] [int] NOT NULL,
    [AffiliationId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HourlyClassification]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HourlyClassification](
    [HourlyRate] [decimal](9, 2) NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaycheckAddress]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaycheckAddress](
    [Address] [nvarchar](50) NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalariedClassification]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalariedClassification](
    [Salary] [decimal](9, 2) NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesReceipt]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReceipt](
    [Date] [datetime] NULL,
    [Amount] [int] NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceCharge]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCharge](
    [Date] [datetime] NULL,
    [Amount] [decimal](9, 2) NULL,
    [AffiliationId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeCard]    Script Date: 2019/5/2 下午 08:41:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeCard](
    [Date] [datetime] NULL,
    [Hours] [decimal](9, 2) NULL,
    [EmpId] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CommissionedClassification]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DirectDepositAccount]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeAffiliation]  WITH CHECK ADD FOREIGN KEY([AffiliationId])
REFERENCES [dbo].[Affiliation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeAffiliation]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
GO
ALTER TABLE [dbo].[HourlyClassification]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaycheckAddress]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalariedClassification]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SalesReceipt]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceCharge]  WITH CHECK ADD FOREIGN KEY([AffiliationId])
REFERENCES [dbo].[Affiliation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeCard]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employee] ([EmpId])
ON DELETE CASCADE
GO
```
