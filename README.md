# FeedingMonitor_Vue_Net
Feeding Monitor mit .NET Backend und VueJS Frontend

Die Anwendung besteht aus zwei Teilen: den Bewegdaten (durchgeführte Fütterungen) und den Stammdaten (Hersteller und Produktbezeichnung), wobei das Produkt per Fremdschlüssel auf einen Hersteller referenziert. Als Datenbank wird Postgresql verwendet.

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
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design

oder alle Pakete:
dotnet restore

dotnet run
```

### Migration mit EF

Database first:

```
dotnet tool install --global dotnet-ef
dotnet tool list --global // check ob installiert
dotnet ef dbcontext scaffold <string mit details>
```

Code first:

```
export PATH="$PATH:$HOME/.dotnet/tools/"
dotnet-ef migrations add InitialCreate
dotnet-ef database update
```



## Backend Unittests

```
dotnet new xunit -o backend.Tests
dotnet sln add ./backend.Tests/backend.Tests.csproj
dotnet add ./backend.Tests/backend.Tests.csproj reference backend/backend.csproj

dotnet test
```
