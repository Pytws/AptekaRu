## Общая архитектура
Проект AptekaRu представляет собой веб-приложение, разработанное на платформе .NET, 
предназначенное для управления аптечным каталогом. Архитектура проекта разделена на несколько ключевых компонентов:

* **AptekaRu.DAL:**
Этот слой отвечает за доступ к данным и взаимодействие с базой данных PostgreSQL.
Он содержит модели данных и репозитории для выполнения операций CRUD.

* **AptekaRu.Web:**
Веб-интерфейс приложения, реализованный с использованием ASP.NET Core MVC.
Этот модуль обрабатывает HTTP-запросы, управляет маршрутизацией и предоставляет пользовательский интерфейс.

* База данных PostgreSQL:
Для хранения данных используется PostgreSQL. В репозитории присутствует директория postgresql,
содержащая скрипты для создания и инициализации базы данных.

* Docker и Docker Compose:
Для облегчения развертывания и управления зависимостями проект использует Docker.
Файлы Dockerfile и docker-compose.yml обеспечивают контейнеризацию приложения и его компонентов.
Запустить приложение можно через docker-compose.yml находясь в одной директории с проектом:
```
docker-compose up -d
```
### Nuget пакеты
```
   [net8.0]:
   Top-level Package                                   Requested   Resolved
   > Dapper                                            2.1.35      2.1.35
   > Microsoft.EntityFrameworkCore.Design              9.0.0       9.0.0
   > Npgsql.EntityFrameworkCore.PostgreSQL             9.0.2       9.0.2
   > Npgsql.EntityFrameworkCore.PostgreSQL.Design      1.1.0       1.1.0
```
### ERD-схема
![erd-aptekaru](https://github.com/user-attachments/assets/34d530e5-7262-4470-8e56-ba6ca3cdc157)
### Общая схема проекта
![schema-project](https://github.com/user-attachments/assets/61224b4d-76b8-424b-8bf4-90a2b31237ba)
