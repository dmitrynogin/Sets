using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static Infra.Sets.Universe;
using Demo.Models;

namespace Demo
{
    class Program
    {
        public static void Main(string[] args)
        {
            var loyal = Set<Customer>(с => с.Invoiced() > 10000);
            var debtors = Set<Customer>(c => c.Balance() > 0);
            var creditable = Set<Customer>(c => c.Balance() < 5000) & loyal;
            var bulk = Set<Customer>(c => c.Orders.Any(o => o.Total > 2000));
            var tenOff = bulk & loyal & !debtors;
            var fiveOff = bulk & loyal & creditable - tenOff;
            var noOff = !tenOff & !fiveOff;

            foreach (var c in Repository.Customers.Intersect(fiveOff))
                WriteLine($"-5%: {c.Name}");

            foreach (var c in Repository.Customers.Intersect(tenOff))
                WriteLine($"-10%: {c.Name}");

            foreach (var c in Repository.Customers.Intersect(noOff))
                WriteLine($"0%: {c.Name}");
        }
    }
}
