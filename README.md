# RapidPay

REST API to simulate a Credit Card creation and update balance through a pay.

## Technologies

* [.Net core 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/)

## System Requirements

* [Microsoft SQL server LocalDb](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15)
* .Net Core 5 SDK
* Visual Studio 2019

## Installation

Follow the next steps to get a working copy of the app

1. Clone the repository. 
2. Execute migrations using: 
```shell 
Update-Database -Context ApplicationDbContext
``` 
or 
```shell
dotnet ef database update --context ApplicationDbContext
```
4. Run the API
5. Test the application, remember to authorize using the ApiKey (It is in the AppSetting.js)

## Notes

* you can test the API outside swagger, using Postman (https://www.postman.com/downloads/?utm_source=postman-home)
* If you have any dependency problem, try running the following command in the root folder:

  ```shell
  dotnet restore
  ```