using System.Collections.Generic;
using System.Linq;
using ProductSortingApp;


namespace ProductSortingApp.UnitTests
{

     /* 
     *   This unit test module verifies the correctness and reliability of the product 
     *   sorting and grouping logic implemented in the ProductSorter class.Appropriate   
     *  access rights are used to protect all functions unless they are used 
     *  elsewhere in the system. 
     * 
     *  A predefined set of sample Product objects is used to ensure consistent and 
     *  repeatable test results. Each test validates a specific requirement, including 
     *  sorting by price, quantity, and product name, as well as grouping products by 
     *  their name and sorting each group by price.
     */
    public static class ProductSorterTests
    {
        // RunAll method used to run all sort tests at once
        public static void RunAll()
        {
            TestSortByPrice();
            TestSortByQuantity();
            TestSortByName();
            TestGroupByFirstWordAndSortByPrice();
        }

        // First a set of sample products are created for testing
        private static List<Product> GetSampleProducts()
        {
            return new List<Product>
    {
        new Product { Name = "Widget A", Price = 10.99m, Quantity = 100 },
        new Product { Name = "Widget B", Price = 8.99m, Quantity = 120 },
        new Product { Name = "Gadget C", Price = 24.95m, Quantity = 50 },
        new Product { Name = "Gadget D", Price = 19.99m, Quantity = 60 }
    };
        }

        // Tests that products are sorted by price in ascending order
        private static void TestSortByPrice()
        {
            var sorted = ProductSorter.SortByPrice(GetSampleProducts());
            TestRunner.Assert(sorted.First().Price == 8.99m, "SortByPrice - Ascending"); // Checks the first element of the sorted list = 8.99m
        }

        // Tests that products are sorted by quantity in ascending order
        private static void TestSortByQuantity()
        {
            var sorted = ProductSorter.SortByQuantity(GetSampleProducts());
            TestRunner.Assert(sorted.First().Quantity == 50, "SortByQuantity - Ascending"); // Checks the first element of the sorted list = 50
        }

        // Tests that products are sorted alphabetically by name
        private static void TestSortByName()
        {
            var sorted = ProductSorter.SortByName(GetSampleProducts());
            TestRunner.Assert(sorted.First().Name.StartsWith("Gadget"), "SortByName - Alphabetical"); // Checks the first element of the sorted list = "Gadget"
        }

        // Tests grouping by the first word of the product name,
        // and checks that each group is sorted by price
        private static void TestGroupByFirstWordAndSortByPrice()
        {
            var grouped = ProductSorter.GroupByFirstWordThenSortByPrice(GetSampleProducts());

            bool condition =
                grouped.ContainsKey("Widget") &&
                grouped["Widget"].First().Price == 8.99m;

            TestRunner.Assert(condition, "GroupByFirstWordAndSortByPrice");
        }
    }
}
