# Sets

Thesis
There is a full featured support of countable sets in .NET: `IEnumerable<T>`. What about uncountable sets; sets defined by predicate? How can they be manipulated and interact with `IEnumerable<T>`?

Solution
Let’s introduce two library classes: Universe and Set, where Universe is a factory of Sets and Sets are defined by predicate, condition like `Func<T, bool>`. Example:

    using static Universe; 
    Set<int> integers = Set<int>(); 
    Set<int> zero = Set<int>(i => i == 0); 
    Set<int> positive = Set<int>(i => i > 0);

We define some basic calculus on sets:

    Set<int> nonPositive = !positive;
    Set<int> negative = !positive – zero;
    Set<int> nonZero = positive | negative;

Intersection:

    Set<int> liquidFreshWaterC = Universe.Set<int>(t => t > 0 && t < 100);
    Set<int> liquidSaltWaterC = Universe.Set<int>(t => t > -21.1 && t < 102);
    Set<int> liquidWaterC = liquidFreshWaterC & liquidSaltWaterC; // = 0 … 100

Now, how to test the set (it is just a combined condition underneath, nothing else)? 

    bool isLiquidWater = liquidWaterC[25]; // = true

Actually, tests return `Intersection<T>`, which is truthy; it could be falsy if empty. It implements `IEnumerable<T>`, so we can iterate the result, getting 0 or 1 element.

Union operator provides us with an another set:

    Set<int> temperatures = liquidWaterC | 200; // 0 … 100, 200

The most useful feature is an integration with `IEnumerable<T>`. Let’s have:

    int[] tempC = new[] {-100, -10, 0, 10, 100, 200};

We can test them:

    Intersection<T> t = temperatures[tempC]; // = 0, 10, 100, 200

We can join them, so result will be another `Set<T>`:

    Set<T> joined = temperatures & tempC; // -100, -10, 0 … 100, 200

We can even exclude set from enumeration getting an enumeration, or exclude enumeration from set – getting set as a result.

Demo
 

Let’s define `Customer`, `Order`, `Invoice` to calculate discounts (full solution is available online to play with):

    class Customer
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
        public List<Invoice> Invoices { get; set; }
    }

    class Order
    {
        public decimal Total { get; set; }
    }

    class Invoice
    {
        public decimal Total { get; set; }
    } 

Now helpers:

    static class Balances
    {
        public static decimal Invoiced(this Customer customer) =>
          customer.Invoices.Sum(i => i.Total);

        public static decimal Ordered(this Customer customer) =>
           customer.Orders.Sum(o => o.Total);

        public static decimal Balance(this Customer customer) =>
          customer.Ordered() - customer.Invoiced();
    }

Our discount rules are going to be:

    var loyal = Set<Customer>(с => с.Invoiced() > 10000);
    var debtors = Set<Customer>(c => c.Balance() > 0);
    var creditable = Set<Customer>(c => c.Balance() < 5000) & loyal;
    var bulk = Set<Customer>(c => c.Orders.Any(o => o.Total > 2000));
    var tenOff = bulk & loyal & !debtors;
    var fiveOff = bulk & loyal & creditable - tenOff;
    var noOff = !tenOff & !fiveOff;

Let’s test the sets:

    foreach (var c in Repository.Customers.Intersect(fiveOff))
        WriteLine($"-5%: {c.Name}");

    foreach (var c in Repository.Customers.Intersect(tenOff))
        WriteLine($"-10%: {c.Name}");

    foreach (var c in Repository.Customers.Intersect(noOff))
        WriteLine($"0%: {c.Name}");
