# Kajo Template

Part of [MoQa](https://moqa.moshaheen.com/) Project  

Version: [1.0.0](https://github.com/Moqa-Dev/Kajo/releases/tag/1.0.0)  
Author: [Mahmoud Shaheen](https://www.moshaheen.com/)
## Links:
[Download](https://github.com/Moqa-Dev/Kajo/releases/download/1.0.0/DiXi.zip)  
[Website](https://moqa.moshaheen.com/Kajo/)  
[Github](https://github.com/Moqa-Dev/Kajo/)  

## Why?
Kajo is a full fledged template contains common functionality that's mostly required in any project.  
It's ready to use and includes an example of simple business for a topics-posts entities which is implemented as a show case in most of MoQa Templates.  

## Features:
* Identity: 
    * Authentication:
        * Supported using Bearer authentication
        * built in top of Microsoft Identity
    * Authorization:
        * Genric & Extensible Authorization integrated in the template using dynamic permissions
        * roles can be dynamically assigned to users
    * Permissions:
        * Permissions logic is implemented for dynamic assignment of permissions to roles
* OData:
    * OData is used for providing unified simple query interface for Api Endpoints
    * Includes base service & controller for resources that doesn't require business logic
* API Versioning:
    * API Versioning Support.
* Swagger Documentation:
    * Automatic generation of swagger documentation for all endpoint,
    * includes custom documentation using XML comments
* Global Exception Handling:
    * Unified Exception Handling for all API Exceptions
* Logging:
    * Using Log4Net Framework
* CORS:
    * CORS Support included in project,
    * customizable through configuration
* DBContext:
    * EFCore Framework is used for Database context

## How To Run:
* Using Visual Studio
* Update Database context configuration in 'appsettings.json'
* Run 'Update-Database' in PM
* Run using 'Kajo' Configuration

## How To Use:
An Example of Topic-Post is included in the project check it for reference.
