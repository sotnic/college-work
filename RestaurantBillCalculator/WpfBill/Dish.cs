using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBill
{
    class Dish
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public Dish() { }
        public Dish(Dish last)
        {
            Quantity = last.Quantity;
            Price = last.Price;
            Name = last.Name;
            Type = last.Type;
        }
    }
}
