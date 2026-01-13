using System.Collections.Generic;
using System.Linq;

/*
 *  The ProductSorter file provides reusable sorting and grouping methods for Product 
 *  objects. Products can be sorted by price, quantity, or name, and grouped by the first
 *  word of their name with each group sorted by price. 
 */

namespace ProductSortingApp
{
    public static class ProductSorter
    {
        public static List<Product> SortByPrice(List<Product> products) // Returns list sorted by price
        {
            return products.OrderBy(p => p.Price).ToList();
        }

        public static List<Product> SortByQuantity(List<Product> products) // Returns list sorted by quantity
            {
            return products.OrderBy(p => p.Quantity).ToList();
        }

        public static List<Product> SortByName(List<Product> products) // Returns list sorted by quantity
        {
            return products.OrderBy(p => p.Name).ToList();
        }

        // A dictionary was used to sort the list alphbetically. The first word in their name is used a key before
        // sorting each group by price.
        public static Dictionary<string, List<Product>> GroupByFirstWordThenSortByPrice(List<Product> products)
        {
            return products
                .GroupBy(p => p.Name.Split(' ')[0])  // Used to identify the first word of Name
                .OrderBy(g => g.Key)                 // sort list alphabetically
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(p => p.Price).ToList()   // sortgroups by price
                );
        }
    }
}
