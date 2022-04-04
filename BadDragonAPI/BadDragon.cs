using BadDragonAPI.Data;
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
        public BadDragon()
        {

        }

        public Inventory GetInventory(int page)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(String.Format("https://bad-dragon.com/api/inventory-toys?price[min]=0&price[max]=300&sort[field]=price&&sort[direction]=asc&page={0}&limit=60", page)).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return Inventory.FromJson(responseBody);
        }

        public Products[] GetProducts()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("https://bad-dragon.com/api/products").Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return Products.FromJson(responseBody);
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
