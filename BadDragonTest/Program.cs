using BadDragonAPI;
using BadDragonAPI.Data;
using System;

namespace BadDragonTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BadDragon badDragon = new BadDragon();
            Inventory inv = badDragon.GetInventory(1);
            foreach (Toy toy in inv.Toys)
            {
                Console.WriteLine("Name : {0}, Price : {1}; Size : {2}", toy.Sku, toy.Price, toy.Size);
            }

            Product[] products = badDragon.GetProducts();
            foreach (Product product in products)
            {
                Console.WriteLine("Name : {0}, Title : {1};  Preview Image: {2}", product.Sku, product.FurryTitle, product.PreviewImage);
            }

            ProductList[] productList = badDragon.GetProductsList();
            foreach (ProductList ProdList in productList)
            {
                Console.WriteLine("Sku : {0} = Name : {1}", ProdList.Sku, ProdList.Name);
            }

            Console.WriteLine("Total Inv : " + badDragon.GetTotalInv());
        }
    }
}
