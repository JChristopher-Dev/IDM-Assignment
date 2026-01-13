# Product Sorting Console Application (C#)

## Overview

This repository contains a **C# console application** developed as part of a technical assessment. The application reads product data from CSV files, allows users to apply various sorting and grouping operations, and displays the results in a formatted and user-friendly console output.

The project follows standard software design practices, avoids the use of external NuGet packages, and includes custom-built unit tests to validate core functionality.

---

## Features

* Reads product data from CSV files with support for multiple file formats
* Sorts products by:

  * Price (ascending)
  * Quantity (ascending)
  * Product name (alphabetical)
* Groups products by product name and sorts each group by price
* Clear, menu-driven console interface
* Robust error handling and input validation
* Built-in unit testing without external dependencies

---

## Project Structure

* `Program.cs` – Application entry point and user interface logic
* `Product.cs` – Product data model
* `CsvReader.cs` – CSV file parsing and validation
* `ProductSorter.cs` – Sorting and grouping logic
* `UnitTests/` – Custom unit tests and lightweight test runner

---

## How to Run the Application

1. Clone or download this repository.
2. Open the solution in **Visual Studio**.
3. **Important:** Update the folder path in `Program.cs` to match the location of the CSV files on your machine.
4. Build and run the project.

When executed, the application will prompt you to:

* Select a CSV file to load
* Choose a sorting or grouping option
* Optionally run unit tests directly from the console menu

---

## File Path Configuration (Important)

Please note the following before running the application:

* The folder path defined in **`Program.cs`** must be updated to reflect the local directory containing the CSV files.
* The original CSV files provided with the assessment were named:

  * `Technical Assessment Dev 1\ProductList`
  * `Technical Assessment Dev 1\ProductList_2`
* These files have been **renamed** to:

  * `ProductList.csv`
  * `ProductList_2.csv`
* The application expects the renamed files and will not run correctly unless:

  * The folder path is correct
  * The expected file names are present

---

## Unit Testing

* Custom unit tests are implemented without using external testing frameworks.
* Tests cover:

  * Sorting by price, quantity, and product name
  * Grouping and sorting logic
  * CSV parsing behavior
* Unit tests can be executed directly from the application menu.

---

## Technologies Used

* C# (.NET Console Application)
* LINQ
* File I/O
* Custom test runner (no external dependencies)


