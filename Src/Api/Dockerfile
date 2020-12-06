FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 
#
# copy csproj and restore as distinct layers

COPY *.sln ./
COPY Src/Api/*.csproj ./Src/Api/
COPY Src/DataAccess/*.csproj ./Src/DataAccess/
COPY Tests/Api.Tests/*.csproj ./Tests/Api.Tests/
#
RUN dotnet restore 
#
# copy everything else and build app
COPY Src/Api/. ./Api/
COPY Src/DataAccess/. ./DataAccess/
#
WORKDIR /app/Api

RUN dotnet publish -c Release -o out 
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet ef database update
#
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 
#
COPY --from=build /app/Api/out ./

ENTRYPOINT ["dotnet", "Api.dll"]