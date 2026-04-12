# Stage 1: Build môi trường SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Cài EF tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy các file .csproj để restore (tối ưu hóa layer cache của Docker)
# Đường dẫn tính từ thư mục gốc của project
COPY ["LunchSync.Api/LunchSync.Api.csproj", "LunchSync.Api/"]
COPY ["LunchSync.Core/LunchSync.Core.csproj", "LunchSync.Core/"]
COPY ["LunchSync.Infrastructure/LunchSync.Infrastructure.csproj", "LunchSync.Infrastructure/"]

# Restore các dependencies
RUN dotnet restore "LunchSync.Api/LunchSync.Api.csproj"

# Copy toàn bộ mã nguồn của các project liên quan
COPY . .

# Chuyển vào thư mục Api để build
WORKDIR "/src/LunchSync.Api"
RUN dotnet build "LunchSync.Api.csproj" -c Release -o /app/build

# Stage 2: Publish ứng dụng
FROM build AS publish
RUN dotnet publish "LunchSync.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime image (nhẹ hơn, chỉ chứa môi trường chạy)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN apt-get update && apt-get install -y \
    python3 \
    python3-pip \
    && rm -rf /var/lib/apt/lists/*

RUN pip3 install psycopg2-binary --break-system-packages

COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy DLL migrations
COPY --from=publish /app/publish .

COPY LunchSync.Infrastructure/Seed/AddData.py .
COPY LunchSync.Infrastructure/Seed/*.json .

#ENTRYPOINT ["dotnet"]
CMD ["dotnet", "LunchSync.Api.dll"]
