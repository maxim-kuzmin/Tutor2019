# Запуск приложения

cd ..

docker build . -t tutor2019appsdockerwebmvc:dev -f ./Tutor2019.Apps.DockerWebMvc/Dockerfile

docker create -p 55148:80 -p 44356:443 --name tutor2019appsdockerwebmvc-55148-44356 tutor2019appsdockerwebmvc:dev

docker start tutor2019appsdockerwebmvc-55148-44356

# Образы

## Построение образа

docker build <Путь к папке с файлом .dockerignore> -t <Имя образа>:<Тэг образа> -f <Путь к docker-файлу>

### Пример:

- <Путь к папке с файлом .dockerignore> => .

- <Имя образа> => tutor2019appsdockerwebmvc

- <Тэг образа> => dev

- <Путь к docker-файлу> => ./Tutor2019.Apps.DockerWebMvc/Dockerfile

## Получение списка образов

docker images

## Удаление образа
 
docker rmi -f <ID образа>

## Удаление всех образов

docker rmi -f $(docker images -q)

# Контейнеры

## Создание контейнера

docker create -p <Номер порта снаружи контейнера 1>:<Номер порта внутри контейнера 1> -p <Номер порта снаружи контейнера 2>:<Номер порта внутри контейнера 2> --name <Имя контейнера> <Имя образа>

### Пример:

- <Номер порта снаружи контейнера 1> => 55148

- <Номер порта внутри контейнера 1> => 80

- <Номер порта снаружи контейнера 2> => 44356

- <Номер порта внутри контейнера 2> => 443

- <Имя контейнера> => tutor2019appsdockerwebmvc-55148-44356

- <Имя образа> => tutor2019appsdockerwebmvc:dev

## Получение списка всех доступных (запущенных и нет) контейнеров

docker ps -a

## Запуск контейнера

docker start <Имя или ID контейнера>

## Запуск всех контейнеров

docker start $(docker ps -aq)

## Остановка контейнера

docker stop <Имя или ID контейнера>

## Остановка всех контейнеров

docker stop $(docker ps -q)

## Удаление контейнера

docker rm <Имя или ID контейнера>