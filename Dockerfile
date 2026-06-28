FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /App

COPY . ./
RUN dotnet restore SemanticSentry.App/SemanticSentry.App.csproj
RUN dotnet publish SemanticSentry.App/SemanticSentry.App.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
COPY --from=build-env /App/out .

ENV ASPNETCORE_URLS=http://+:5128
EXPOSE 5128
ENTRYPOINT ["dotnet", "SemanticSentry.App.dll"]