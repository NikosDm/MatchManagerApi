The purpose of this project is the creation of RESTful web service which will support CRUD operations regarding football and basketball matches as well as their odds. 

For development I used Visual Studio Code as IDE and SQL Server.
The project consists of one controller which receives requests regarding CRUD operations Matches and their odds. The connection with the database as well as the CRUD operations 
take place on a class called MatchService which is injected in controller. 

In order to have a clearer view of the RESTful service and the endpoints that exposes, I used Swagger for documentation. 
The document is a json and is located on the project directory and can be easily viewed by using the online editor https://editor.swagger.io/

In order to run the application download the project and run on a IDE like Visual Studio Code or Visual Studio and execute the following commands: 
- dotnet clean
- dotnet build
- dotnet run

or use the executable file **MatchManagerApi.exe** contained in the ZIP file I have attached in my response email. 
In any of the abovementioned cases, don't forget to change the connection string mentioned in appsettings.Development.json and appsettings.json, so that the web service points to the requested database. 
