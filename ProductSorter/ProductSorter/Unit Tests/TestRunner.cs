using System;

namespace ProductSortingApp.UnitTests
{
  
    /// Entry point for executing all unit tests in the application.
    public static class TestRunner
    {
        public static void RunAll() // Runs all unit test classes in the project

        {
            Console.WriteLine("=== RUNNING UNIT TESTS ===\n");

            ProductSorterTests.RunAll();
            CsvReaderTests.RunAll();

            Console.WriteLine("\n=== ALL TESTS COMPLETED ===");
        }
        // Assertion method used to print PASS or FAIL based on the test condition
        public static void Assert(bool condition, string testName)
        {
            if (condition)
                Console.WriteLine($"[PASS] {testName}");
            else
                Console.WriteLine($"[FAIL] {testName}");
        }
    }
}
