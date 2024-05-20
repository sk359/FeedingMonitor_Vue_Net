# FeedingMonitor_Vue_Net
Feeding Monitor mit .NET Backend und VueJS Frontend

https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-asp-net-core-with-vue?view=vs-2022

https://www.freecodecamp.org/news/how-to-build-an-spa-with-vuejs-and-c-using-net-core/

https://www.youtube.com/watch?v=DH2yUVQDB0I&list=PL-6aiWy3GcL77KR334ZKiAcL_4QEF4XBf

Web API Controller:

https://learn.microsoft.com/en-us/training/modules/build-web-api-aspnet-core/

## Frontend

```
nvm use 18.20.2
npm create vue@latest
cd frontend
npm install

npm run dev
```

### Style

```
npm i --save @fortawesome/fontawesome-svg-core
```


## Backend

```
dotnet new web -o backend
cd backend
dotnet add package Microsoft.AspNetCore.SpaServices.Extensions
```


## Backend Unittests

```
dotnet new xunit -o backend.Tests
dotnet sln add ./backend.Tests/backend.Tests.csproj
dotnet add ./backend.Tests/backend.Tests.csproj reference backend/backend.csproj

dotnet test
```
