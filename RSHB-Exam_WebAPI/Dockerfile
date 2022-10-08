#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RSHB-Exam_WebAPI/RSHB-Exam_WebAPI.csproj", "RSHB-Exam_WebAPI/"]
RUN dotnet restore "RSHB-Exam_WebAPI/RSHB-Exam_WebAPI.csproj"
COPY . .
WORKDIR "/src/RSHB-Exam_WebAPI"
RUN dotnet build "RSHB-Exam_WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RSHB-Exam_WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RSHB-Exam_WebAPI.dll"]