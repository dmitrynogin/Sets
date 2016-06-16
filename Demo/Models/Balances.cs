using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    static class Balances
    {
        public static decimal Invoiced(this Customer customer) =>
            customer.Invoices.Sum(i => i.Total);

        public static decimal Ordered(this Customer customer) =>
            customer.Orders.Sum(o => o.Total);

        public static decimal Balance(this Customer customer) =>
            customer.Ordered() - customer.Invoiced();
    }
}
