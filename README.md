# TripPlanner (full-stack)

## How to build and run server:
\[frontend non-static/source codes' instructions are in the readme.md in its folder,but as long as no changes are made to the frontend sourcecode, you can just ignore building that because wwwroot contains a build (which is the latest as of writing this).\]

### Prerequisites
Before you begin, ensure you have the following prerequisites installed:
- .NET SDK: You can download and install it from [here](https://dotnet.microsoft.com/download).
- MS SQL Server (**change connection string as needed**).
- dotnet Entity Framework Core tools.


### Database creation and migration:
You will need to do this process on the first run, **BUT, also** after any changes to the Model.
```bash
dotnet ef migrations add *REPLACE_WITH_NAME_FOR_MIGRATION*
```
```bash
dotnet ef database update
```
### Build
```bash
dotnet build
```
### Run
```bash
dotnet run
```

## How to access the appliation:

***If you have a different URL for the server, replace the base part of the URLs in the following before using them.***

**API Spec:** 

URL:
```
http://127.0.0.1:7034/api
```
or the full url: *http://127.0.0.1:7034/api/index.html*

**Front-end:**
```
http://127.0.0.1:7034/
```

**Front-end (Admin Panel):**

To make sure Admin account exists, send an empty HTTP Post request to the endpoint "​/api​/Account​/ensure-Admin@101-created" (if it needs content type, try the JSON content type).
I realize now after so long that there were better ways to handle default Admin account creation (and honestly, a lot of things) but I have improved on it and everything else in the WebAPIs I made afterwords.

```
http://127.0.0.1:7034/admin
```

Default Admin Username: Admin@101
Default Admin Password: Admin@101
