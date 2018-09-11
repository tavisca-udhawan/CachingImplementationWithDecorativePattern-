using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace CachingImplementation
{
    class ProductItems:IProduct
    {
        private const string Key = "products";
       
        public void AddItem(List<string> item)
        {
            ObjectCache cache = MemoryCache.Default;
            IEnumerable availableProducts = item;
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(1.0);
            cache.Add(Key, availableProducts, cacheItemPolicy);
            Console.WriteLine("Product added successfully");
        }
        public List<string> GetAvailableItems()
        {
            ObjectCache cache = MemoryCache.Default;

            if (cache.Contains(Key))
                return (List<string>)cache.Get(Key);
            else
            {
                return GetDefaultData();
            }
        }
        public List<string> GetDefaultData()
        {
            List<string> defaultList = new List<string>();
            defaultList.Add("Data Not Found");
            return defaultList;
        }
    }
}
