using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    static class Repository
    {
        public static IEnumerable<Customer> Customers => new[]
        {
            new Customer
            {
                Name = "Tom the loyal debtor",
                Orders = new List<Order>
                {
                    new Order { Total = 10000 },
                    new Order { Total = 20000 },
                    new Order { Total = 40000 },
                    new Order { Total = 10000 },
                },
                Invoices = new List<Invoice>
                {
                    new Invoice { Total = 10000 },
                    new Invoice { Total = 20000 }
                }
            },
            new Customer
            {
                Name = "John the lucky",
                Orders = new List<Order>
                {
                    new Order { Total = 10000 },
                    new Order { Total = 20000 },
                },
                Invoices = new List<Invoice>
                {
                    new Invoice { Total = 10000 },
                    new Invoice { Total = 20000 }
                }
            }
        };
    }
}
