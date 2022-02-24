# Project 1 - Tuan Anh Nguyen

> My Project1 is focus on the Web APIs with Authenticaion and Authorization by using the ASP.NET Web APIs and ASP.NET Identity.

# Table of Contents
- [Project 1 - Tuan Anh Nguyen](#project-1---tuan-anh-nguyen)
- [Table of Contents](#table-of-contents)
- [Features](#features)
  - [Authentication Controller](#authentication-controller)
  - [Admin Controller](#admin-controller)
  - [Customer Controller](#customer-controller)
  - [Store Controller](#store-controller)
  - [Home Controller](#home-controller)
- [Technologies](#technologies)
- [Changelog](#changelog)
  - [v1.0.0](#v100)
- [Contributing](#contributing)
- [Contacts](#contacts)

# Features

I seperated all of my methods into 5 main Controllers which have its own Roles to access:
## Authentication Controller
> Roles: Everyone (AllowAnonymous)
- Register (By default, new user will be Customer, if you want to open a store, you have to request to become a Store Manager in the System)
- Login
## Admin Controller
> Roles: Admin
- Add Roles to user
- Manage the Product in the system
## Customer Controller
> Roles: Customer
- Manage Profile
- Manage their own orders(Check History, Status, Add more items to the order if the status still Order Placed, Cancel Order)
- Request to open a new Store in the system
## Store Controller
> Roles: Store Manager
- Manage Profile
- Manage store's inventory(Import new product, replenish inventory)
- Manage store's orders(View, Accept, Reject, Complete and Manage Shipment of order)
## Home Controller
> Roles: Everyone (AllowAnonymous)
- Search Product and Store Information
  
# Technologies
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/)
- [Visual Studio Code](https://code.visualstudio.com)
- [JSON](https://www.json.org/json-en.html)
- [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview)
- [xUnit](https://xunit.net)
- [SeriLog](https://serilog.net)
- [RESTful API](https://restfulapi.net)
- [Azure](https://azure.microsoft.com/en-us/)
- [Azure SQL Server](https://azure.microsoft.com/en-us/services/sql-database/campaign/)
- [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/)
- [ASP.NET Core Web APIs](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [ASP.NET Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity)
- [SonarQube](https://www.sonarqube.org)
- [Swagger](https://swagger.io)
- [DBeaver](https://dbeaver.io)
- [Git](https://git-scm.com)
- [Markdown](https://daringfireball.net/projects/markdown/)

# Changelog
## v1.0.0
- Release

# Contributing
As I did this project for the course, so if you want to have more features, please give me a request or just [open an issue](https://github.com/220118-Reston-NET/TuanAnh-Nguyen-P1/issues) and tell me your ideas.

# Contacts
- Github: [@kirasn](https://github.com/kirasn)
- Website: [http://www.kiranguyen.com](http://www.kiranguyen.com)

[Back To Top](#project-1---tuan-anh-nguyen)