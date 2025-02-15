# Code Review Hub Backend API (v1)
## REST API для системы обзоров и обсёров кода


## Запуск:
### Линукс:
#### 1) Установить Docker:
    sudo apt-get update
    sudo apt-get install -y docker.io
#### 2) Установить docker-compose
    sudo curl -L "https://github.com/docker/compose/releases/download/$(curl -s https://api.github.com/repos/docker/compose/releases/latest | grep -Po '"tag_name": "\K.*\d')/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
    sudo chmod +x /usr/local/bin/docker-compose
#### 3) Создать директорию для проекта
    mkdir ~/codereviewhub
    cd ~/codereviewhub
#### 4) Скопировать отсюда docker-compose.yml
#### 5) Стянуть образы
    docker-compose pull
#### 6) Запустить
    docker-compose up -d
### Доступ к эндпоинтам по порту 5024.
