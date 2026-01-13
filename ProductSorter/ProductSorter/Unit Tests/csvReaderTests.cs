using System;
using System.IO;
using System.Linq;

namespace ProductSortingApp.UnitTests
{
    /*
    *  This unit test module validates the functionality and robustness of the CsvReader 
    *  component. It focuses on ensuring accurate parsing of valid CSV files and verifying 
    *  that error handling behaves correctly when encountering missing files or invalid 
    *  data formats.
    *  
    *  The tests cover several input scenarios, including successful parsing of well-formed 
    *  CSV data, graceful handling of non-existent files, and selective processing of 
    *  records containing invalid values. Temporary test files are created and removed 
    *  during execution to maintain isolation and repeatability.
    */
    public static class CsvReaderTests
    {
        public static void RunAll()
        {
            TestValidCsvParsing();
            TestMissingFileHandling();
            TestInvalidCsvFormat();
        }

        //  Test reading a valid CSV 
        private static void TestValidCsvParsing()
        {
            string path = "test_products.csv";

            File.WriteAllLines(path, new[]
            {
                "Product Name,Price (ZAR),Quantity",
                "Widget A,10.99,100"
            });

            var products = CsvReader.ReadProducts(path);
            TestRunner.Assert(products.Count == 1, "CSV Parsing - Valid File");

            File.Delete(path);
        }

        //  Test to ensure missing files returns empty list and correcct error message
        private static void TestMissingFileHandling()
        {
            var products = CsvReader.ReadProducts("non_existing.csv");
            TestRunner.Assert(products.Count == 0, "CSV Missing File Handling");
        }

        //  Test to ensure invalid .csv lines are skipped 
        private static void TestInvalidCsvFormat()
        {
            string path = "invalid.csv";

            File.WriteAllLines(path, new[]
            {
                "Product Name,Price (ZAR),Quantity",
                "Widget A,NOT_A_NUMBER,100",  // Invalid price
                "Gadget B,24.95,50"           // Valid line
            });

            var products = CsvReader.ReadProducts(path);

            // Only valid line should be included
            TestRunner.Assert(products.Count == 1 && products[0].Name == "Gadget B",
                              "CSV Invalid Format Handling");

            File.Delete(path);
        }
    }
}
