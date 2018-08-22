SOLUTION=$1
dotnet new sln --name $SOLUTION

dotnet new classlib --name $SOLUTION.Dal --framework netcoreapp2.1
dotnet new classlib --name $SOLUTION.Contract --framework netcoreapp2.1
dotnet new classlib --name $SOLUTION.AppService --framework netcoreapp2.1
dotnet new classlib --name $SOLUTION.AppServiceMock --framework netcoreapp2.1
dotnet new classlib --name $SOLUTION.WebApi --framework netcoreapp2.1
dotnet new webapi --name $SOLUTION.Startup --framework netcoreapp2.1

dotnet sln $SOLUTION.sln add **/*.csproj

dotnet add $SOLUTION.AppService/$SOLUTION.AppService.csproj reference $SOLUTION.Dal/$SOLUTION.Dal.csproj $SOLUTION.Contract/$SOLUTION.Contract.csproj  

dotnet add $SOLUTION.WebApi/$SOLUTION.WebApi.csproj reference $SOLUTION.Contract/$SOLUTION.Contract.csproj 

dotnet add $SOLUTION.Startup/$SOLUTION.Startup.csproj reference $SOLUTION.Contract/$SOLUTION.Contract.csproj $SOLUTION.Dal/$SOLUTION.Dal.csproj $SOLUTION.WebApi/$SOLUTION.WebApi.csproj $SOLUTION.AppService/$SOLUTION.AppService.csproj

dotnet add $SOLUTION.WebApi/$SOLUTION.WebApi.csproj package Microsoft.AspNetCore.All --version 2.1.2

dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.EntityFrameworkCore
dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.EntityFrameworkCore.Sqlite.Core
dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.EntityFrameworkCore.Tools --version 2.1.1 
dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.Extensions.Configuration.FileExtensions --version 2.1.1
dotnet add $SOLUTION.Dal/$SOLUTION.Dal.csproj package Microsoft.Extensions.Configuration.Json --version 2.1.1 

dotnet add $SOLUTION.Startup/$SOLUTION.Startup.csproj package Microsoft.EntityFrameworkCore
dotnet add $SOLUTION.Startup/$SOLUTION.Startup.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add $SOLUTION.Startup/$SOLUTION.Startup.csproj package Microsoft.EntityFrameworkCore.Sqlite.Core
