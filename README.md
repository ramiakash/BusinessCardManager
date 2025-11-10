# Business Card Manager

a fullstack app that allows creating, viewing, deleting, filtering, and bulk importing/exporting business cards.



##  Architecture & Design


* **Clean Architecture:** The solution is divided into four distinct layers:
    * `Domain`: Contains core logic, entities, and no external dependencies.
    * `Application`: Contains business logic using MediatR and defines interfaces.
    * `Infrastructure`: for external logic (database, file I/O).
    * `API`: entry point that handles requests.

* **Domain-Driven Design (DDD):** The `Domain` layer is protected by DDD patterns.

* **CQRS (Command Query Responsibility Segregation):** The `Application` layer uses the **MediatR** library to separate reads (Queries) from writes (Commands).

* **Global Exception Handling:** The API uses custom middleware to catch exceptions.

## Key Features

* **CRUD Operations:** Full create, read, and delete functionality for business cards.
* **Advanced Filtering:** Reactive filtering on the frontend by Name, Email, Phone, Gender, and DOB.
* **File Import (CSV/XML):** Smart file import that auto-detects the format (CSV or XML).
* **Import Preview:** A user-friendly preview table that allows for importing cards **one by one** after parsing.
* **File Export (CSV/XML):** Export the *currently filtered* list of cards to either format.

## Technology Stack

### Backend
* .NET 8 Web API
* Entity Framework Core
* SQL Server
* **Key Packages:**
    * `MediatR` (for CQRS)
    * `AutoMapper` (for DTO mapping)
    * `FluentValidation` (for request validation)
    * `CsvHelper` & `XmlSerializer` (for file I/O)
    * `xUnit` & `Moq` (for unit testing)
    * `ZXing` for QR Code Parsing.

### Frontend
* Angular 19
* TypeScript
* Tailwind CSS

### Database
* Microsoft SQL Server


##  Setup & Installation

Follow these steps to get the project running.

### Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Node.js and npm](https://nodejs.org/en)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express, Developer, or full)
* [Git](https://git-scm.com/)

### 1. Clone the Repository
```sh
git clone https://github.com/ramiakash/BusinessCardManager.git
cd BusinessCardManager
```

### 2. Backend Setup
1.  Navigate to the `BusinessCardManager` project directory.
2.  Open `appsettings.json`.
3.  Update the `DefaultConnection` string to point to your SQL Server instance:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.\\SQLEXPRESS;Database=BusinessCardDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"
    }
    ```

### 3. Database Setup

1.  Open SQL Server Management Studio (SSMS).
2.  Open the `Database/schema.sql` file provided in this repository.
3.  Execute the script. This will create the `BusinessCardDB` database and the `BusinessCards` table.



### 4. Frontend Setup
1.  Navigate to the `BusinessCard-Frontend` project directory:
    ```sh
    cd BusinessCard-Frontend
    ```
2.  Install all required npm packages:
    ```sh
    npm install
    ```
3. Update apiUrl
    ```
    Inside src/app/features/business-card/services/business-card.service.ts

    Find and update the variable apiUrl to point to the API.

    ```


##  Running the Application

1.  **Run the Backend:**
    * Open a terminal in the `BusinessCardManager` folder and run:
        ```sh
        dotnet run
        ```
        or simply use visual studio IDE and run the application.
    * The API will be running at `http://localhost:5000` (or similar).

2.  **Run the Frontend:**
    * Open a *second* terminal in the `BusinessCard-Frontend` folder and run:
        ```sh
        ng serve --open
        ```
    * This will automatically open the application in your browser at `http://localhost:4200`.
