# 🚲 BikeRental

### Лабораторная работа № 1 по дисциплине **«Разработка корпоративных приложений»**

---

## 🧩 Краткое описание предметной области

Проект **BikeRental** моделирует работу пункта велопроката.
В системе хранятся сведения о:

* велосипедах (с указанием модели, цвета, серийного номера и характеристик);
* арендаторах (ФИО и телефон);
* фактах аренды (время начала и длительность аренды).

Для анализа данных реализованы **unit-тесты**, которые выполняют запросы с использованием LINQ:
определяют прибыльные модели, время аренды, статистику клиентов и т.д.

---

## 🏗️ Архитектура проекта

Проект разделён на два уровня:

### **1. BikeRental.Domain** — доменная логика

Содержит классы, описывающие предметную область.

#### 📁 Enums

* **BikeType.cs** — перечисление типов велосипедов:
  `Urban`, `Mountain`, `Highway`, `Sports`, `Childrens`.
* **BrakeType.cs** — типы тормозных систем:
  `Disc`, `Rim`, `Drum`.
* **RentalStatus.cs** — статусы аренды:
  `Active`, `Completed`, `Cancelled`.

#### 📄 Модели:

* **Model.cs**

  ```csharp
  public int Id  
  public string Name  
  public BikeType Type  
  public double WheelSize  
  public int MaxWeight  
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
  public Model Model
  ```

  *Описание:* конкретный велосипед, привязанный к модели.

* **Renter.cs**

  ```csharp
  public int Id  
  public string FullName  
  public string Phone
  ```

  *Описание:* арендатор — клиент пункта проката.

* **Rental.cs**

  ```csharp
  public int Id  
  public Bicycle Bicycle  
  public Renter Renter  
  public DateTime StartTime  
  public int DurationHours  
  public RentalStatus Status  
  public decimal TotalPrice => DurationHours * Bicycle.Model.PricePerHour
  ```

  *Описание:* запись об аренде велосипеда клиентом.

---

### **2. BikeRental.Tests** — модульное тестирование


#### 📄 RentalFixture.cs
  Инициализирует тестовое окружение и предоставляет доступ к данным.

#### 📄 RentalTests.cs

Набор тестов, проверяющих работу LINQ-запросов:

* **ShouldReturnAllSportBicycles()** — выборка всех спортивных велосипедов.
* **ShouldReturnTop5ModelsByProfit()** — топ-5 моделей по прибыли.
* **ShouldReturnTop5ModelsByDuration()** — топ-5 моделей по длительности аренды.
* **ShouldReturnRentalDurationStats()** — минимальное, максимальное и среднее время аренды.
* **ShouldReturnTotalRentalTimeByBikeType()** — суммарное время аренды по типам велосипедов.
* **ShouldReturnTopRentersByCount()** — клиенты, которые чаще всех брали велосипеды.

---

## ⚙️ Технологический стек

| Технология / инструмент    | Назначение                                       |
| -------------------------- | ------------------------------------------------ |
| **.NET 8.0 (C#)**          | основной язык и платформа разработки             |
| **xUnit**                  | фреймворк модульного тестирования                |
| **LINQ**                   | обработка коллекций и агрегация данных           |
| **Microsoft.NET.Test.Sdk** | инфраструктура для выполнения тестов             |
| **Visual Studio 2022**     | среда разработки                      


