
 WebAPI Menu Backend .NET

Application that manages menus in a restaurant. You can create, modify, and delete menus, obtain a specific menu, and view the total number of registered menus.
This project is a REST API built with .NET 8, Entity Framework Core, and SQL Server. It also includes JWT authentication and a CRUD.

## How to run the project

- Clone the Backend repository locally
- Run-> dotnet restore
- Verify a local SQL Server database and configure the connection string in appsettings.json
- Run the migrations to create the database (DB_restaurant) and tables (Users, Menus)
  You must run both lines:
1 add-migration Initial
2 update-database
  
  or dotnet ef database update

- Run the project with interface or dotnet run on http, the API will be at http://localhost:5080

## Swagger
It can be found at http://localhost:5080/swagger/index.html


## Endpoints
# Authentication
POST /api/Authentication/Register
POST /api/Authentication/Login 

# Menus
GET /api/Menus → List menus
POST /api/Menus → Create menu
GET /api/Menus/{id} → List specific menu
PUT /api/Menus/{id} → Update menu
DELETE /api/Menus/{id} 

## Authentication and Authorization
The API has authentication and authorization. First, a user must be registered and assigned a role to access the API.
-User permissions: GetAll and GetById
-Admin permissions: GetAll, GetById, Post, Put, and Delete

## Unit testing
Run-> dotnet test

