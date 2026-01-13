using ProductSortingApp.UnitTests;


/*
 *  This console application reads product data from CSV files and provides professional 
 *  sorting and grouping functionality. On startup, it scans a specified folder for 
 *  all CSV files matching the pattern "*ProductList*.csv" and presents a numbered list 
 *  for user selection or the option to run unit tests.
 *  
 *  Upon selecting a file, the program parses the data into Product objects via csvReader, 
 *  performs input validation, and stores the records in a list. Users can then choose 
 *  from sorting options by price (ascending), quantity (ascending), or alphabetically sort
 *  product name(ascending), or Group by Product Name and sort each group by Price(ascending).
 *  
 *  Results are displayed in well-formatted columns, and the application handles missing 
 *  files, empty datasets, invalid CSV formats, and incorrect user input gracefully.
 *  
 *  The program ensures:
 *    - Support for multiple CSV files
 *    - Accurate sorting and grouping of product data
 *    - Clear and professional user prompts
 *    - Robust error handling and validation
 *    - Execution of unit tests for csvReader and ProductSorter files
 */

namespace ProductSortingApp
{
    class Program
    {
        static void Main()
        {
            RunApplication();
        }

        // Run the main application
        static void RunApplication()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                //  Starting menu to select .csv file or run unit tests 
                string folderPath = @"E:\University\Work\Assessment\ProductSorter";
                string[] csvFiles = Directory.GetFiles(folderPath, "*ProductList*.csv");

                Console.WriteLine("\nAvailable CSV files:");
                for (int i = 0; i < csvFiles.Length; i++)
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
                Console.WriteLine($"{csvFiles.Length + 1}. Run Unit Tests");
                Console.Write("Select a file by number, run unit tests, or type 0 to exit: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                if (!int.TryParse(input, out int fileChoice) || fileChoice < 0 || fileChoice > csvFiles.Length + 1)
                {
                    Console.WriteLine("Invalid selection. Please enter a valid number.");
                    continue;
                }

                if (fileChoice == 0)
                {
                    exitProgram = true;
                    break;
                }

                if (fileChoice == csvFiles.Length + 1)
                {
                    RunTests();
                    continue; // return to file selection after tests
                }

                string filePath = csvFiles[fileChoice - 1];

                //  Read products from CSV 
                List<Product> products = CsvReader.ReadProducts(filePath);
                if (products.Count == 0)
                {
                    Console.WriteLine("No products to display. Returning to file selection.");
                    continue;
                }

                bool backToFileSelect = false;
                while (!backToFileSelect)
                {
                    //  Display sorting menu 
                    Console.WriteLine("\nChoose a sorting option:");
                    Console.WriteLine("1. Sort by Price (ascending)");
                    Console.WriteLine("2. Sort by Quantity (ascending)");
                    Console.WriteLine("3. Sort by Product Name (ascending)");
                    Console.WriteLine("4. Group by Product Name and sort each group by Price (ascending)");
                    Console.WriteLine("5. Return to file selection");
                    Console.WriteLine("6. Exit program");
                    Console.Write("Choice: ");
                    string sortChoice = Console.ReadLine();
                    Console.WriteLine();

                    bool hasCategory = !string.IsNullOrEmpty(products[0].Category);

                    switch (sortChoice)
                    {
                        case "1":
                            DisplayProducts(ProductSorter.SortByPrice(products), hasCategory);
                            break;
                        case "2":
                            DisplayProducts(ProductSorter.SortByQuantity(products), hasCategory);
                            break;
                        case "3":
                            DisplayProducts(ProductSorter.SortByName(products), hasCategory);
                            break;
                        case "4":
                            DisplayGroupedProducts(
                                ProductSorter.GroupByFirstWordThenSortByPrice(products), hasCategory);
                            break;
                        case "5":
                            backToFileSelect = true;
                            break;
                        case "6":
                            exitProgram = true;
                            backToFileSelect = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option selected. Please try again.");
                            break;
                    }
                }
            }

            Console.WriteLine("\nProgram exited. Press any key to close...");
            Console.ReadKey();
        }

        //  Displays a list of products in formatted columns
        static void DisplayProducts(List<Product> products, bool hasCategory)
        {
            if (hasCategory)
            {
                Console.WriteLine(
                    "{0,-25} {1,-15} {2,12} {3,10}",
                    "Product Name", "Category", "Price (ZAR)", "Quantity");
                Console.WriteLine(new string('-', 70));
                foreach (var p in products)
                {
                    Console.WriteLine(
                        "{0,-25} {1,-15} {2,12:F2} {3,10}",
                        p.Name, p.Category, p.Price, p.Quantity);
                }
            }
            else
            {
                Console.WriteLine(
                    "{0,-25} {1,12} {2,10}",
                    "Product Name", "Price (ZAR)", "Quantity");
                Console.WriteLine(new string('-', 55));
                foreach (var p in products)
                {
                    Console.WriteLine(
                        "{0,-25} {1,12:F2} {2,10}",
                        p.Name, p.Price, p.Quantity);
                }
            }
        }

        //  Displays grouped products in formatted columns
        static void DisplayGroupedProducts(
            Dictionary<string, List<Product>> grouped, bool hasCategory)
        {
            foreach (var group in grouped)
            {
                Console.WriteLine($"\nGroup: {group.Key}");
                if (hasCategory)
                {
                    Console.WriteLine(
                        "{0,-25} {1,-15} {2,12} {3,10}",
                        "Product Name", "Category", "Price (ZAR)", "Quantity");
                    Console.WriteLine(new string('-', 70));
                    foreach (var p in group.Value)
                    {
                        Console.WriteLine(
                            "{0,-25} {1,-15} {2,12:F2} {3,10}",
                            p.Name, p.Category, p.Price, p.Quantity);
                    }
                }
                else
                {
                    Console.WriteLine(
                        "{0,-25} {1,12} {2,10}",
                        "Product Name", "Price (ZAR)", "Quantity");
                    Console.WriteLine(new string('-', 55));
                    foreach (var p in group.Value)
                    {
                        Console.WriteLine(
                            "{0,-25} {1,12:F2} {2,10}",
                            p.Name, p.Price, p.Quantity);
                    }
                }
            }
        }

        // Run all unit tests
        static void RunTests()
        {
            Console.WriteLine("Running unit tests...\n");
            ProductSorterTests.RunAll();
            CsvReaderTests.RunAll();
            Console.WriteLine("\nAll tests executed. Press any key to return to file selection...");
            Console.ReadKey();
        }
    }
}
