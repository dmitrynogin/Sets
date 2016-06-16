using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
