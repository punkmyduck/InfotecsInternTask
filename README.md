# InfotecsInternTask
Тестовое задание на стажировку в Инфотекс

## Описание проекта
Веб-приложение на ASP.NET Core для загрузки, валидации, обработки и фильтрации данных из CSV-файлов.  
Проект реализует 3 основных сценария по техническому заданию:

1. **Загрузка CSV файла** с вычислением интегральных значений (минимум, максимум, среднее, медиана, время выполнения) и сохранением в PostgreSQL.
2. **Фильтрация результатов** по имени файла, диапазону дат, среднему значению и среднему времени выполнения.
3. **Получение последних 10 значений** по имени файла, отсортированных по дате.

---

## Технологии
- **.NET 8.0 / ASP.NET Core Web API**
- **Entity Framework Core** (Npgsql provider)
- **PostgreSQL**
- **Swagger UI**

---

## Архитектура
Проект разделён на 4 слоя:
- InfotecsInternTask.DomainLayer // Сущности, интерфейсы, DTO
- InfotecsInternTask.ApplicationLayer // Бизнес-логика, сервисы, валидация
- InfotecsInternTask.InfrastructureLayer // Доступ к БД, репозитории, парсинг CSV
- InfotecsInternTask (Web API) // Контроллеры, точка входа

### Поток данных (пример для загрузки файла)
FileUploadController → CsvProcessingService → ICsvParser + ICsvValuesValidator + IIntegralCalculator + IResultAggregateMapper → IResultRepository → EF Core → PostgreSQL

---

## Методы API

### 1. Загрузка CSV файла
**POST** `/api/upload`  
Загружает CSV, валидирует, вычисляет интегральные значения и сохраняет в БД.

### 2. Фильтрация результатов
**GET** `/Results/FilterResults`  
Параметры (query):
- `FileName` *(string, optional)*
- `MinStartDate`, `MaxStartDate` *(DateTime?, optional)*
- `MinAverageValue`, `MaxAverageValue` *(double?, optional)*
- `MinAverageExecutionTime`, `MaxAverageExecutionTime` *(int?, optional)*

### 3. Последние 10 значений
**GET** `/Values/Last10ByFile?fileName=example.csv`

## Пример CSV файла:
Date;ExecutionTime;Value
2024-01-01T10:00:00.0000Z;120;35.5
2024-01-01T10:02:00.0000Z;140;36.2
2024-01-01T10:05:00.0000Z;100;34.8

## Несколько иллюстраций

### 1. Открытие Swagger'а
<img width="1444" height="552" alt="image" src="https://github.com/user-attachments/assets/c9889e5c-5244-40b3-9139-2ee119764fbe" />

### 2. Успешная загрузка файла
<img width="1427" height="649" alt="image" src="https://github.com/user-attachments/assets/910c58cb-7018-46d0-a082-175a86cf28c5" />

### 3. Получение выборки значений по названию файла
<img width="1406" height="860" alt="image" src="https://github.com/user-attachments/assets/d0c94fc6-22c3-4a2d-81cd-8c67d52f638f" />

### 4. Фильтрация данных
<img width="1120" height="929" alt="image" src="https://github.com/user-attachments/assets/1cef2e27-e9ed-4066-b3f7-9cc8e80a79ab" />

### 5. Select в СУБД по таблице Results
<img width="1247" height="168" alt="image" src="https://github.com/user-attachments/assets/95188daf-1da1-4e91-9e39-ea283d8ee518" />

### 6. По таблице Values
<img width="639" height="430" alt="image" src="https://github.com/user-attachments/assets/135dd32a-ff56-42b9-97a8-c37f83bfe52b" />
