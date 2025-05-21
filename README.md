<p align="center">
    <img width="100%" src=".github/assets/banner.png" alt="Banner for SuperBiblio.">
</p>
<p align="center">
    <b>A movie's library implemented in C# with an ORM.</b>
</p>

````markdown
# SuperBiblio

**SuperBiblio** is a C# console application for managing a movie library. It provides features to list, add, delete, and count movies, with support for three data access modes: text file, SQL Server, and Entity Framework.

## Features

- List all movies with their directors  
- Add a new movie  
- Delete a movie by its ID  
- Display the total number of movies  
- Switchable data access layer:
  - File (`DataServiceFile`)
  - SQL Server (`DataServiceSqlServer`)
  - Entity Framework (`DataServiceEF`)

## Project Structure

```plaintext
SuperBiblio/
│
├── Program.cs               # Main console interface
├── IDataService.cs         # Interface for data operations
├── DataServiceFile.cs      # File-based implementation
├── DataServiceSqlServer.cs # SQL Server implementation
├── DataServiceEF.cs        # Entity Framework implementation
├── MyContext.cs            # EF DbContext
└── Film.cs                 # Film model
````

## Technologies

* .NET 9
* C# 10
* Entity Framework Core
* SQL Server
* Console Application

## NuGet Dependencies

Make sure to install the following packages:

```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.Data.SqlClient
```

## Switching Data Access Services

In `Program.cs`, uncomment the data service you want to use:

```csharp
// IDataService dataService = new DataServiceMySql();
IDataService dataService = new DataServiceEF(); // default to EF
// IDataService dataService = new DataServiceFile();
```

## SQL Server Configuration

* Database name: `biblioEF`
* Connection strings are defined in `MyContext.cs` and `DataServiceSqlServer.cs`
* Default user: `sa`
* Password: `P@ssw0rd`

> Warning: Change credentials before using in production.

## Example Usage

```bash
> dotnet run
+--------------------------------------+
|         The Super Library            |
+--------------------------------------+

Movie library access:
  1 - List movies
  2 - Add a movie
  3 - Delete a movie
  4 - Movie count
  5 - Exit
```

## Example File Format (for DataServiceFile)

The `films.txt` file should alternate between movie titles and directors:

```
The Lord of the Rings
Peter Jackson
Inception
Christopher Nolan
```

## Notes

* The code is modular, making it easy to add new data service layers.
* The console interface is simple and suitable for demos or basic usage.

---

## Author

Project developed as part of C# and database learning.

---
