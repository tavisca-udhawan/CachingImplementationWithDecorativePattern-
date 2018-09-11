using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingImplementation
{
    interface IProduct
    {
        void AddItem(List<string> item);
        List<string> GetAvailableItems();
        List<string> GetDefaultData();
    }
}
