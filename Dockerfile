# Используем официальный образ .NET 9.0 SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем все файлы из текущей папки (где находится Dockerfile) в /src
COPY . .

# Восстанавливаем зависимости и собираем проект
RUN dotnet restore "API.csproj"
RUN dotnet build "API.csproj" -c Release -o /app/build

# Публикуем проект
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Используем официальный образ .NET 9.0 Runtime для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Копируем собранные файлы из предыдущего этапа
COPY --from=build /app/publish .

# Указываем порт, который будет использовать контейнер
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "API.dll"]