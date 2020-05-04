FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CreativeTim.Argon.DotNetCore.Free/CreativeTim.Argon.DotNetCore.Free.csproj", "CreativeTim.Argon.DotNetCore.Free/"]
RUN dotnet restore "CreativeTim.Argon.DotNetCore.Free/CreativeTim.Argon.DotNetCore.Free.csproj"
COPY . .
WORKDIR "/src/CreativeTim.Argon.DotNetCore.Free"
RUN dotnet build "CreativeTim.Argon.DotNetCore.Free.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CreativeTim.Argon.DotNetCore.Free.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CreativeTim.Argon.DotNetCore.Free.dll"]
