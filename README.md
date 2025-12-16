# 🚲 BikeRental

### Лабораторная работа № 1 по дисциплине **«Разработка корпоративных приложений»**

---

## 🧩 Краткое описание предметной области

Проект **BikeRental** моделирует работу пункта велопроката.
В системе хранятся сведения о:

* велосипедах (с указанием модели, цвета, серийного номера и характеристик);
* арендаторах (ФИО и телефон);
* фактах аренды (время начала и длительность аренды).

Для анализа данных реализованы **unit-тесты** и отдельный **аналитический Web API**, которые выполняют запросы с использованием LINQ/агрегаций:
определяют прибыльные модели, время аренды, статистику клиентов и т.д.

---

## 🏗️ Архитектура проекта

Проект разделён на несколько уровней и проектов решения:

### **1. BikeRental.Domain** — доменная логика

Содержит классы, описывающие предметную область, перечисления и базовые контракты доступа к данным.

#### 📁 Enums

* **BikeType.cs** — перечисление типов велосипедов:
  `Urban`, `Mountain`, `Highway`, `Sports`, `Childrens`.
* **BrakeType.cs** — типы тормозных систем:
  `Disc`, `Rim`, `Drum`.

#### 📄 Контракты

* **IRepository.cs** — универсальный контракт репозитория для CRUD операций:

  ```csharp
  Task<TEntity> Create(TEntity entity)
  Task<TEntity?> Read(TKey entityId)
  Task<IList<TEntity>> ReadAll()
  Task<TEntity> Update(TEntity entity)
  Task<bool> Delete(TKey entityId)
  ```

#### 📄 Модели:

* **Model.cs**

  ```csharp
  public int Id  
  public string? Name  
  public BikeType Type  
  public double WheelSize  
  public double MaxWeight  
  public double Weight  
  public BrakeType Brakes  
  public int Year  
  public decimal PricePerHour
  ```

  *Описание:* справочник моделей велосипедов с их техническими характеристиками и ценой аренды.

* **Bicycle.cs**

  ```csharp
  public int Id  
  public string SerialNumber  
  public string Color  
  public int ModelId  
  public Model? Model
  ```

  *Описание:* конкретный велосипед, привязанный к модели через `ModelId` (FK). Навигация `Model` допускает загрузку по требованию.

* **Renter.cs**

  ```csharp
  public int Id  
  public string FullName  
  public string? Phone
  ```

  *Описание:* арендатор — клиент пункта проката.

* **Rental.cs**

  ```csharp
  public int Id  
  public int BicycleId  
  public Bicycle? Bicycle  
  public int RenterId  
  public Renter? Renter  
  public DateTime StartTime  
  public int DurationHours  
  public decimal TotalPrice => DurationHours * Bicycle!.Model!.PricePerHour
  ```

  *Описание:* запись об аренде велосипеда клиентом. Свойство `TotalPrice` вычисляемое и не хранится в базе данных.

#### 📄 Тестовые данные

* **RentalFixture.cs**
  Инициализирует набор тестовых данных и используется:
  - в модульных тестах;
  - при сидировании данных в базе через EF Core (`HasData`).  
  
  *Примечание:* `RentalFixture` перенесён из тестов в домен и изменён для детерминированных значений (без `DateTime.Now`).

---

### **2. BikeRental.Infrastructure.EfCore** — доступ к данным (EF Core)

Содержит всё, что связано с хранением данных в MS SQL Server.

#### 📄 Основные файлы

* **BikeRentalDbContext.cs**
  - конфигурация таблиц, ограничений и индексов;
  - сидирование данных через `HasData` на базе `RentalFixture`;
  - хранит `DbSet` для `Model`, `Bicycle`, `Renter`, `Rental`.

* **Repositories/**
  Репозитории для каждой сущности, реализующие `IRepository<TEntity, TKey>`:
  - `ModelRepository`
  - `BicycleRepository`
  - `RenterRepository`
  - `RentalRepository`

* **Migrations/**
  Миграции EF Core, создающие структуру БД и добавляющие seed-данные.

---

### **3. BikeRental.Application.Contracts** — DTO, контракты и маппинг

Содержит контракты и DTO, которые используются API и сервисами приложения.

#### 📄 DTO для сущностей

Для каждой сущности определены два DTO:

1) `EntityDto` — DTO для чтения.
2) `EntityCreateUpdateDto` — DTO для создания/обновления.

Примеры:
- `ModelDto`, `ModelCreateUpdateDto`
- `BicycleDto`, `BicycleCreateUpdateDto`
- `RenterDto`, `RenterCreateUpdateDto`
- `RentalDto`, `RentalCreateUpdateDto`

#### 📄 Контракты сервисов

* **IApplicationService.cs** — общий CRUD контракт сервисов приложения.
* **IAnalyticsService.cs** — контракт аналитического сервиса (отдельные отчёты и агрегаты).

#### 📄 AutoMapper

* **BikeRentalProfile.cs**
  Профиль AutoMapper для преобразования сущностей в DTO и обратно.

---

### **4. BikeRental.Application** — прикладная логика

Содержит сервисы уровня приложения, которые реализуют бизнес-логику и используют репозитории.

#### 📄 CRUD сервисы

Для каждой сущности реализован сервис, реализующий `IApplicationService<...>`:

- `ModelService`
- `BicycleService`
- `RenterService`
- `RentalService`

*Особенности сервисов:*
- при `Get(id)` возвращается `null`, если сущность не найдена;
- при `Update(id)` выбрасывается `KeyNotFoundException`, если сущность не найдена;
- при `Create/Update` проверяется существование связанных сущностей (например, `ModelId`, `BicycleId`, `RenterId`), иначе выбрасывается `InvalidOperationException`.

#### 📄 Аналитика

* **AnalyticsService**
  Реализует `IAnalyticsService` и формирует отчёты:
- все спортивные велосипеды;
- топ моделей по прибыли и по суммарной длительности аренды (отдельно);
- минимальная / максимальная / средняя длительность аренды;
- суммарная длительность аренды по типам велосипедов;
- клиенты, которые чаще всего брали велосипеды в прокат.

---

### **5. BikeRental.Api.Host** — Web API

Содержит контроллеры и конфигурацию API.

#### 📄 Основные файлы

* **Program.cs**
  - конфигурация DI;
  - подключение EF Core/репозиториев/сервисов;
  - регистрация AutoMapper;
  - Swagger;
  - интеграция с ServiceDefaults.

#### 📁 Controllers

* **CrudControllerBase.cs**
  Базовый CRUD контроллер, на который опираются контроллеры сущностей.

* Контроллеры сущностей (по одному на сущность):
  - `ModelsController`
  - `BicyclesController`
  - `RentersController`
  - `RentalsController`

* **AnalyticsController**
  Контроллер аналитических запросов.

---

### **6. BikeRental.AppHost** — Aspire (оркестрация)

Используется для локального запуска и управления инфраструктурой.

#### 📄 Что делает

- поднимает контейнер **MS SQL Server**;
- поднимает **BikeRental.Api.Host**;
- связывает сервисы и передаёт строки подключения через Aspire-конфигурацию.

---

### **7. BikeRental.ServiceDefaults** — общие настройки

Содержит общие настройки сервисов (для Aspire и API), например:
- логирование;
- health checks;
- типовые сервисные расширения и настройки окружения.

---

### **8. BikeRental.Tests** — модульное тестирование

Набор тестов, проверяющих корректность аналитических запросов и выборок.

#### 📄 Основные файлы

* **RentalTests.cs**

Примеры тестов (зависит от текущей реализации):
- выборка спортивных велосипедов;
- топ моделей по прибыли;
- статистика длительности аренды;
- топ клиентов по количеству аренд.

*Примечание:* тестовые данные теперь берутся из `BikeRental.Domain.RentalFixture`.

---

## ⚙️ Технологический стек

| Технология / инструмент            | Назначение                                       |
| ---------------------------------- | ------------------------------------------------ |
| **.NET 8.0 (C#)**                  | основной язык и платформа разработки             |
| **ASP.NET Core Web API**           | HTTP API                                         |
| **Entity Framework Core**          | ORM и миграции                                   |
| **MS SQL Server**                  | СУБД                                             |
| **.NET Aspire**                    | оркестрация инфраструктуры (AppHost)             |
| **AutoMapper**                     | маппинг Entity ⇄ DTO                             |
| **xUnit**                          | фреймворк модульного тестирования                |
| **LINQ**                           | запросы, агрегации и аналитика                   |
| **Docker**                         | контейнеризация SQL Server                       |
| **Visual Studio 2022**             | среда разработки                                 |
