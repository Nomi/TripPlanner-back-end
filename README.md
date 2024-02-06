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
or the full url:
```
http://127.0.0.1:7034/api/index.html
```

**Front-end:**
```
http://127.0.0.1:7034/
```

**Front-end (Admin Panel):**
Need to serve the frontend separately if you want to use it (check instructions on the readme.md in the sourcecode for the frontend inside wwwrootfolder).
To access this, use the url format:
\*BASE_URL\*/admin
