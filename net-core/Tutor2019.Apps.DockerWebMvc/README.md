# Запуск приложения

cd ..

docker build . -t tutor2019appsdockerwebmvc:dev -f ./Tutor2019.Apps.DockerWebMvc/Dockerfile

docker volume create --name productdata

docker run -d --name mysql -v productdata:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=mysecret -e bind-address=0.0.0.0 mysql:latest

docker network inspect bridge # Узнаём IP-адрес контейнера mysql, чтобы подставить его в следующую команду как значение переменной окружения DBHOST

#docker run -d --name tutor2019appsdockerwebmvc -p 44356:443 -e DBHOST=172.17.0.2 -e ASPNETCORE_URLS=https://+:443;http://+:80 -e ASPNETCORE_HTTPS_PORT=44356 tutor2019appsdockerwebmvc:dev
#ENV ASPNETCORE_URLS=https://+:443;http://+:80
#ENV ASPNETCORE_HTTPS_PORT=44356

docker run -d --name tutor2019appsdockerwebmvc -p 44356:443 -e DBHOST=172.17.0.2  tutor2019appsdockerwebmvc:dev

docker logs -f tutor2019appsdockerwebmvc

# Образы

## Загрузка образа

docker pull <Репозиторий образа>:<Тэг образа>

### Пример:

- <Репозиторий образа> => mysql

- <Тэг образа> => latest

## Просмотр образа

docker inspect <Репозиторий образа>:<Тэг образа>

### Пример:

- <Репозиторий образа> => mysql

- <Тэг образа> => latest

## Построение образа

docker build <Путь к папке с файлом .dockerignore> -t <Репозиторий образа>:<Тэг образа> -f <Путь к docker-файлу>

### Пример:

- <Путь к папке с файлом .dockerignore> => .

- <Репозиторий образа> => tutor2019appsdockerwebmvc

- <Тэг образа> => dev

- <Путь к docker-файлу> => ./Tutor2019.Apps.DockerWebMvc/Dockerfile

## Получение списка образов

docker images

## Удаление образа
 
docker rmi -f <ID образа>

## Удаление всех образов

docker rmi -f $(docker images -q)

## Запуск образа (создание и запуск контейнера)

docker run -d --name <Имя контейнера> -v <Имя тома>:<Путь к папке внутри образа> -e <Имя переменной окружения 1>=<Значение переменной окружения 1> -e <Имя переменной окружения 2>=<Значение переменной окружения 2> <Репозиторий образа>:<Тэг образа>

###, где

- -d - запуск в фоновом режиме, чтобы можно было вводить новые команды в консоль после запуска

### Пример:

- <Имя контейнера> => mysql

- <Имя тома> => productdata

- <Путь к папке внутри образа> => /var/lib/mysql

- <Имя переменной окружения 1> => MYSQL_ROOT_PASSWORD

- <Значение переменной окружения 1> => mysecret

- <Имя переменной окружения 2> => bind-address

- <Значение переменной окружения 2> => 0.0.0.0

- <Репозиторий образа> => mysql

- <Тэг образа> => latest

# Контейнеры

## Создание контейнера

docker create -p <Номер порта снаружи контейнера 1>:<Номер порта внутри контейнера 1> -p <Номер порта снаружи контейнера 2>:<Номер порта внутри контейнера 2> --name <Имя контейнера> <Репозиторий образа>

### Пример:

- <Номер порта снаружи контейнера 1> => 55148

- <Номер порта внутри контейнера 1> => 80

- <Номер порта снаружи контейнера 2> => 44356

- <Номер порта внутри контейнера 2> => 443

- <Имя контейнера> => tutor2019appsdockerwebmvc-55148-44356

- <Репозиторий образа> => tutor2019appsdockerwebmvc:dev

## Получение списка всех доступных (запущенных и нет) контейнеров

docker ps -a

## Запуск контейнера

docker start <Имя контейнера>

## Запуск всех контейнеров

docker start $(docker ps -aq)

## Остановка контейнера

docker stop <Имя или ID контейнера>

## Остановка всех контейнеров

docker stop $(docker ps -q)

## Удаление контейнера

docker rm <Имя или ID контейнера>

## Удаление всех контейнеров

docker rm -f $(docker ps -aq)

## Просмотр логов контейнера

docker logs -f <Имя или ID контейнера>

# Тома

## Создание тома

docker volume create --name <Имя тома>

### Пример:

- <Имя тома> => productdata

# Сети

## Просмотр сети

docker network inspect bridge