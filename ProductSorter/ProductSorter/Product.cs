using System;

namespace ProductSortingApp
{
    // The defined properties of product objects 
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; } // optional property for ProductList_2 objects
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
