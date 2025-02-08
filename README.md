# Code Review Hub Backend API (v1)
REST API для системы обзоров и обсёров кода

# Эндпоинты

## Пользователь

🤪 <span style="color:rgb(59, 154, 255)">POST</span> /api/v1/user/register
==========================

Запрос на регистрацию нового пользователя в системе.

### Тело
```json
{
    "username": "string",   // Имя пользователя
    "email": "string",      // Email адрес
    "password": "string"    // Пароль
}
```

### Ответ
```json
{
    "status 200": "success"
    "Id": "<id>"
}
```

🤪 <span style="color:rgb(59, 154, 255)">POST</span> /api/v1/user/login
==========================

Запрос на аутентификацию

### Тело
```json
{
    "email": "string",      // Email адрес
    "password": "string"    // Пароль
}
```

### Ответ
```json
{
    "status 200": "success",
    "cookie": {
        "name": "Authentication",
        "value": "___JWT__TOKEN___",
    }
}
```

## Публикации кода

👀 <span style="color:rgb(59, 255, 157)">GET</span> /api/v1/publication
==========================

Получение всех публикаций

### Ответ
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "description": "string",
    "code": "string",
    "lang": "string",
    "rating": 0,
    "postedTime": "2025-02-08T09:50:23.830Z",
    "creator": {
      "username": "string",
      "id": "string"
    }
  }
]
```

🤪 <span style="color:rgb(59, 154, 255)">POST</span> /api/v1/publication
==========================

Создание новой публикации

### Тело
```json
{
    "description": "string", //Описание
    "code": "string", // Код
    "lang": "string" // Язык программирования
}
```

### Ответ
```json
{
    "status 200": "success",
    "Id": "<id>"
}
```

👀 <span style="color:rgb(59, 255, 157)">GET</span> /api/v1/publication/{id}
==========================

Получение публикации по id

### Параметры
```http
 - 'id': string (guid)
```

### Ответ
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "description": "string",
  "code": "string",
  "lang": "string",
  "rating": 0,
  "postedTime": "2025-02-08T09:51:44.636Z",
  "creator": {
    "username": "string"
  }
}
```


🔄 <span style="color:rgb(255, 148, 60)">PUT</span> /api/v1/publication/{id}
==========================

Изменение публикации по id

### Параметры
```http
 - 'id': string (guid)
```

### Тело
```json
{
    "description": "string", //Описание
    "code": "string", // Код
    "lang": "string" // Язык программирования
}
```

### Ответ
```json
{
    "status 200": "success",
    "Id": "<id>"
}
```


❌ <span style="color:rgb(255, 81, 81)">DELETE</span> /api/v1/publication/{id}
==========================

Удаление публикации по id

### Параметры
```http
 - 'id': string (guid)
```

### Ответ
```json
{
    "status 200": "success",
}
```