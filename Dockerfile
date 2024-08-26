FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.sln .
COPY be/*.csproj ./be/
RUN dotnet restore

COPY be/. ./be/
WORKDIR /source/be
RUN dotnet publish -c release -o /published --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV TZ="Asia/Ho_Chi_Minh"
WORKDIR /app
COPY --from=build /published ./
ENTRYPOINT ["dotnet", "be.dll"]