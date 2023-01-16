# InnoGotchi.Back


This application is a Web API for managing virtual pets.
For more convenient application management, there is a web client written in ASP.NET MVC: [InnoGotchi.Front](https://github.com/IlyaBuryn/InnoGotchi.Front)

Both applications will work separately, but I advise you to also run the front-end solution when you launch the back-end part.

____

To run the application you need:
* Use the Package Manager Console in Visual Studio. Select the `InnoGotchi.DataAccess` project and enter the `update-database` command. (must have MSSQL Server installed)
* Next, you either use Visual Studio and run InnoGotchi.Api, or use the comand line terminal to navigate to the InnoGotchi.Api/ directory and use the `dotnet run` command. Then find in the terminal information about the running server on the local host in the form: https://localhost:7015 or https://localhost:5015
* Next, when the API is successfully launched, you need to use Swagger or Postman to call the POST method `https://localhost:port/innogotchi/account/login` and pass the data for authorization:
`{
   "username": "admin@m.com",
   "password": "qweqwe"
}`
after which you will receive a response in the form of user data and jwt token, which must be passed with each call to the API.