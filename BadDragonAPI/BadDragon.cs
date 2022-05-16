using BadDragonAPI.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BadDragonAPI
{
    public class BadDragon
    {
        public Dictionary<String, Product> CachedProducts = new Dictionary<string, Product>();

        public BadDragon()
        {

        }

        public void Init()
        {
            CachedProducts = new Dictionary<string, Product>();
            Product[] prods = GetProducts();
            foreach (Product prod in prods)
            {
                CachedProducts.Add(prod.Sku, prod);
            }
        }

        public ulong GetTotalInv()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://bad-dragon.com/api/inventory-toys/total?type[]=ready_made&price[min]=0&price[max]=300&").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Dictionary<String, ulong>>(responseBody)["total"];
        }

        public Inventory GetInventory(int page)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(String.Format("https://bad-dragon.com/api/inventory-toys?price[min]=0&price[max]=300&sort[field]=price&&sort[direction]=asc&page={0}&limit=60", page)).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return Inventory.FromJson(responseBody);
        }

        public Product[] GetProducts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://bad-dragon.com/api/products").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return Product.FromJson(responseBody);
        }

        public ProductList[] GetProductsList()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://bad-dragon.com/api/inventory-toys/product-list").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return ProductList.FromJson(responseBody);
        }
    }
}
