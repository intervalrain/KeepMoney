FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["KeepMoney.Api/KeepMoney.Api.csproj", "KeepMoney.Api/"]
COPY ["KeepMoney.Application/KeepMoney.Application.csproj", "KeepMoney.Application/"]
COPY ["KeepMoney.Domain/KeepMoney.Domain.csproj", "KeepMoney.Domain/"]
COPY ["KeepMoney.Infrastructure/KeepMoney.Infrastructure.csproj", "KeepMoney.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]
RUN dotnet restore "KeepMoney.Api/KeepMoney.Api.csproj"
COPY . ../
WORKDIR /src/KeepMoney.Api
RUN dotnet build "KeepMoney.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeepMoney.Api.dll"]