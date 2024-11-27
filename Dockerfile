FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

# 複製所有 csproj 文件與 props 文件
COPY ["KeepMoney.Api/KeepMoney.Api.csproj", "KeepMoney.Api/"]
COPY ["KeepMoney.Application/KeepMoney.Application.csproj", "KeepMoney.Application/"]
COPY ["KeepMoney.Domain/KeepMoney.Domain.csproj", "KeepMoney.Domain/"]
COPY ["KeepMoney.Infrastructure/KeepMoney.Infrastructure.csproj", "KeepMoney.Infrastructure/"]
COPY ["Directory.Packages.props", "./"]
COPY ["Directory.Build.props", "./"]

# 還原依賴
RUN dotnet restore "KeepMoney.Api/KeepMoney.Api.csproj"

# 複製其餘程式碼
COPY . .

# 建置與發布
WORKDIR /KeepMoney.Api
RUN dotnet build "KeepMoney.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# 運行 image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=5001
EXPOSE 5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeepMoney.Api.dll"]