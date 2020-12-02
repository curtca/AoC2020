dotnet new sln -o %1
cd %1
dotnet new classlib -o lib
dotnet sln add ./lib/lib.csproj
dotnet new xunit -o test
dotnet add ./test/test.csproj reference ./lib/lib.csproj
dotnet sln add ./test/test.csproj