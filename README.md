# Loan API 

# Contents
* [Programs](#Programs)
* [About](#About)
* [NugetPackages](#nugetpackages)
* [Installation](#Installation)
* [Features](#Features)
* [Testing](#testing)

## Programs
* Visual Studio 2022
* SQL Server Management Studio
* Swagger

## About

* NET 5
* MSSQL Server
* SeriLog
* FluentValidation
* JWT Authentication and Authorization
* HASH256
* Entity Framework Core

## NugetPackages

* Syste.Data.SqlClient
* FluentValidation
* Microsoft.AspNet.WebApi.Core
* Microsoft.Entity.FrameworkCore
* Microsoft.EntityFrameworkCore.Tools
* Microsoft.AspNetCore.Authentication.JwtBearer
* Serilog.AspNetCore
* Serilog.Sinks.Console
* Serilog.Sinks.MSSqlServer


## Installation

* Clone github repository.
* Add Migration
* Create Migration  

<pre>Add-Migration *migrationname*</pre>
 <pre>update-database</pre>

* Change appsettingJsonSettings


* Create WebApiLogs Table In Data Base (Script)
    
<pre>
CREATE TABLE [dbo].[WebApiLogs](
 [Id] [int] IDENTITY(1,1) NOT NULL,
 [Message] [nvarchar](max) NULL,
 [MessageTemplate] [nvarchar](max) NULL,
 [Level] [nvarchar](128) NULL,
 [TimeStamp] [datetime] NOT NULL,
 [Exception] [nvarchar](max) NULL,
 [Properties] [nvarchar](max) NULL,
 [UserId] [varchar](100) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
 [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

</pre>

    
## Features

* User registration;
<pre>{
  "FirstName": "string",
  "LastName": "string",
  "Age": 0,
  "Email" : "Email",
  "SalaryPerMont" : 0,
  "UserName": "string",
  "Password": "string",
}</pre>
* User Login;
<pre>{
  "UserName" : "String",
  "Password" : "String"
}</pre>
* Password hashing;
* Role-based authorization;
* User Login via access token creation;
 * Role - Accountant;
 * Generate Role Accoutant ;
 * Accoutant UserName/Passowrd;
<pre>
{
  "userName": "admin000",
  "password": "admin007"
}</pre>
 


* Role - Accountant
  * Get All User
  * Delete Loan Of User
  * Delete User By User Id
  * Accept Loan By User Id
  * Block Loan By User Id
  * Block/Unblock"
  <pre>{
      "Id" : 0,
      "Block" : "Block/Unblock"
  }</pre>
  * Update Loan By User Id
  * Update User Status
  * Get Loan By User Email
  
* Role - User
  * Add Loan (if IsBlocked = false)
  * Delete Loan (if IsBlocked = false, Loan Status = "Requested")
  * Update Loan (if IsBlocked = false, Loan Status = "Requested")
  * Update User Info (if IsBlocked = false)
  * Get User Info
  * Get Loan 
  * Change Password
  
## Testing
The first step is to log in (username and password). Get Generated Token. Token format is "Bearer Access Token".

## Photo
![App UI](C:\Users\lasha\Desktop\monit/319844875_912308333102919_3874487259071747550_n.jpg)

