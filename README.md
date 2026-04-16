# ASP.NET MVC Gantt - CRUD URL ADAPTOR

A sample ASP.NET MVC 5 repository demonstrating Syncfusion EJ2 Gantt with a URL adaptor for CRUD operations.

## Project overview

The sample uses Syncfusion EJ2 Gantt in `GanttCrudSql\Views\Home\Index.cshtml` and Entity Framework 5 with a local database in `App_Data\GanttDataSource.mdf`. Task data loads from `Home/UrlDatasource` and saves in batch via `Home/BatchSave`.

## Features

- Remote data loading with `UrlAdaptor`
- Batch insert, update, and delete operations
- Recursive child deletion
- Toolbar support for add/edit/update/delete/cancel/expand/collapse
- Task mappings for `TaskId`, `TaskName`, `StartDate`, `EndDate`, `Duration`, `Progress`, `ParentId`, `Predecessor`

## Requirements

- Visual Studio 2022 or later
- .NET Framework 4.6.1
- LocalDB / SQL Server Express
- NuGet restore for Syncfusion EJ2 MVC libraries

## Setup

1. Clone or extract the repository.
2. Open `GanttCrudSql.sln`.
3. Rebuild to restore packages.
4. Confirm `App_Data\GanttDataSource.mdf` is included.
5. Run the project.

## Key files

- `GanttCrudSql\Controllers\HomeController.cs` — data endpoints and CRUD logic
- `GanttCrudSql\Views\Home\Index.cshtml` — Gantt setup and URL adaptor configuration
- `GanttCrudSql\Models\GanttData.cs` — task model

## Notes

The sample uses Syncfusion controls and requires a valid Syncfusion license. Task IDs are stored as strings and support parent-child relationships.
