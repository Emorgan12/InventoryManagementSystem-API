using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagementSystem_API
{
    public class Product
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string category { get; set; }

        public double cost_price { get; set; }
        public double selling_price { get; set; }
        public int quantity { get; set; }

        public double profit_per_unit { get; set; }

        public double total_profit { get; set; }
        public int sold { get; set; }

    }
}