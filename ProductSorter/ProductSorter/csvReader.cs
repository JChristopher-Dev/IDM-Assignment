using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

/*
 *  The csvReader file reads data from specified .csv files and parses each record into 
 *  appropriate product objects. Each object is then added to and returned as a "products"
 *  list. Multiple validation and error handling methods are implemented to handle edge 
 *  cases and provide clear error messages when needed.
*/
namespace ProductSortingApp
{
    public static class CsvReader
    {
        // The ReadProduct methods takesn in a .csv file and returns a list Product objects
        public static List<Product> ReadProducts(string filePath)
        {
            var products = new List<Product>();
            // Checks file exists before attmepting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {filePath}");
                return products;
            }

            try
            {   // If file exists the lines are read into: string[] lines
                string[] lines = File.ReadAllLines(filePath);
                // Check to ensure the file contains at least a header and one row of data
                if (lines.Length < 2)
                {
                    Console.WriteLine("Error: The CSV file is empty or missing headers.");
                    return products;
                }
                // The first row of headers are split into 4 Categories
                bool hasCategory = lines[0].Split(',').Length == 4;

                // Each row is then processed and split into appropriate variables
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');

                    if ((hasCategory && columns.Length != 4) || (!hasCategory && columns.Length != 3))
                    {
                        Console.WriteLine($"Warning: Skipping invalid line {i + 1}.");
                        continue;
                    }
                    // The values of each field are assigned
                    string name = columns[0].Trim();
                    string category = hasCategory ? columns[1].Trim() : null;
                    int priceIndex = hasCategory ? 2 : 1;
                    int quantityIndex = hasCategory ? 3 : 2;
                    // Each variable is parsed and error catching is used to handle invalid data
                    if (!decimal.TryParse(columns[priceIndex].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                    {
                        Console.WriteLine($"Warning: Invalid price on line {i + 1}. Skipping.");
                        continue;
                    }
                    if (!int.TryParse(columns[quantityIndex].Trim(), out int quantity))
                    {
                        Console.WriteLine($"Warning: Invalid quantity on line {i + 1}. Skipping.");
                        continue;
                    }
                    // Creates and adds a valid Product object to the result list
                    products.Add(new Product
                    {
                        Name = name,
                        Category = category,
                        Price = price,
                        Quantity = quantity
                    });
                }
            }
            catch (Exception ex)
            {   // Handles unexpected file or IO-related errors gracefully
                Console.WriteLine($"Error reading file: {ex.Message}");
            }
            // Returns all successfully parsed products
           return products; 
        }
    }
}
