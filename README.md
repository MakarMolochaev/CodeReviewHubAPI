# Code Review Hub Backend API (v1)
## REST API для системы обзоров и обсёров кода


## Запуск:
### Команды написаны для PS Windows, но думаю не сильно отличаются на линуксе
#### 1) Выполнить сборку образа приложения
    docker build -t my-aspnet-api .
#### 2) Запускаете контейнеры PostgreSQL (CodeReviewHub-Db) и самого API (CodeReviewHub-API)
    docker-compose up -d

### Доступ к эндпоинтам по порту 5024.
    http://localhost:5024/... 