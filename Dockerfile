FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Копируем исходный код, восстанавливаем зависимости
COPY . ./
RUN dotnet restore AptekaRu.sln

# Сборка проекта слоя Web, DAL слой как библиотека классов 
# автоматический скомпилируется, так как является зависимостью для Web слоя
RUN dotnet publish AptekaRu.sln -c Release -o /publish --no-restore

# Минимальный runtime образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY --from=build publish /home/AptekaRu/

WORKDIR /home/AptekaRu

ENV DLL_DAL=/home/AptekaRu/AptekaRu.DAL.dll
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
#ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

# Запуск итоговой команды
ENTRYPOINT ["sh", "-c", "dotnet AptekaRu.Web.dll $DLL_DAL"]
