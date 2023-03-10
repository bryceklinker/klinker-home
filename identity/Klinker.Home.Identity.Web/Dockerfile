FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN apt-get install --yes curl
RUN curl --silent --location https://deb.nodesource.com/setup_18.x | bash -
RUN apt-get install --yes nodejs
RUN npm install --global yarn

WORKDIR /src

COPY ["identity/Klinker.Home.Identity.Web/Klinker.Home.Identity.Web.csproj", "identity/Klinker.Home.Identity.Web/"]
RUN dotnet restore "identity/Klinker.Home.Identity.Web/Klinker.Home.Identity.Web.csproj"

COPY . .
WORKDIR "/src/identity/Klinker.Home.Identity.Web"
RUN dotnet build "Klinker.Home.Identity.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Klinker.Home.Identity.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Klinker.Home.Identity.Web.dll"]
