# prototype database  
## setup migration  
### Visual Studio  
open solution in Visual Studio  
build the whole solution  
make prototype_database.Startup the startup project  
open Package Manager Console  
set Default project to prototype_database.Startup  
run ```update-database```  

### dotnet-cli  
```
dotnet restore
cd prototype_database.Startup
dotnet ef database update
dotnet run
```
