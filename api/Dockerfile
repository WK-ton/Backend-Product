FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.18 AS base

#กำหนด directory 
WORKDIR /app

#ASP.NET เรียกใช้
EXPOSE 5183

#ให้ application รับ HTTP ที่ port 5183
ENV ASPNETCORE_URLS=http://+:5183

#กำหนด User เป็น app
USER app

#copy ไฟล์จาก local ไปยัง container
COPY wwwroot/images /app/wwwroot/images

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine3.18 AS build

#กำหนดตัวแปร env ใน dockerfile 
ARG configuration=Release
WORKDIR /src

#copy ทั้งหมดจาก local ไปยัง /scr
COPY . .
#copy .csproj ไปยัง /src
COPY ["api.csproj", "./"]

#run คำสั่ง และดึก dependencies ของ project
RUN dotnet restore "api.csproj"
WORKDIR "/src/."

#build โปรเจ็ค และกำหนดให้ output ไปที่ /app/build
RUN dotnet build "api.csproj" -c Release -o /app/build

#สร้าง stage จาก build stage โดยใช้ "AS publish"
FROM build AS publish
ARG configuration=Release

#สร้่างคำสั่ง publish เพื่อสร้าง output สำหรับการ deploy ที่ 'app/publish' โดยไม่ใช้ AppHost
RUN dotnet publish "api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

#Copy output จาก publish stage ไปยัง workdir ใน container
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "api.dll"]

