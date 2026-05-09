
Front-end
Use Node.js version v25.1.0 detected. Angular 21.2.10

หากต้องการรัน ให้ติดตั้ง node js: https://nodejs.org/en/download
```
// Check version node
node -v
// v25.1.0

// ติดตั้ง angular
npm install -g @angular/cli

// ติดตั้ง node_module และ รัน
npm i
ng serve -o

// Application จะถูกเปิดที่ URL: http://localhost:4200
```

Back-end Use dotnet v.10.0.7
```
//package install dotnet
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore 
dotnet add package Microsoft.EntityFrameworkCore.Design


//start program 
dotnet run
```