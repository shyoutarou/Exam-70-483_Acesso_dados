using System;
using System.Collections.Generic;
using System.Linq;

namespace UsingLINQ
{

    class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
    }
    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class OrderLine : IEquatable<OrderLine>
    {
        public int Amount { get; set; }
        public Product Product { get; set; }

        public bool Equals(OrderLine other)
        {
            if (Product.Description == other.Product.Description)
                return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            OrderLine other = (OrderLine)obj;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            //custom implementation of hashcode
            string hash = this.Product.Description + this.Amount;
            return hash.GetHashCode();
        }

        public static bool operator ==(OrderLine orderline1, OrderLine orderline2)
        {
            if (((object)orderline1) == null || ((object)orderline2) == null)
                return Object.Equals(orderline1, orderline2);
            return orderline1.Equals(orderline2);
        }

        public static bool operator !=(OrderLine orderline1, OrderLine orderline2)
        {
            if (((object)orderline1) == null || ((object)orderline2) == null)
                return !Object.Equals(orderline1, orderline2);
            return !(orderline1.Equals(orderline2));
        }
    }
    public class Order
    {
        public string VendorName { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }

    public static class LinqExtensions
    {
        public static IEnumerable<TSource> Where<TSource>(
        this IEnumerable<TSource> source,
        Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }

    class Program
    {
        private static List<Person> persons;
        private static List<Order> orders;

        static void Main(string[] args)
        {
            SeedData();

            //1- First Step (Obtaining the Data Source)
            int[] data = { 1, 2, 5, 8, 11 };

            //2- Second Step (Creation of Query)
            var result = from d in data
                         where d % 2 == 0
                         select d;

            //3-Third Step (Execution of Query)
            foreach (int i in result)
            {
                Console.WriteLine(i); // Displays 2 8
            }

            //Deferred Execution - Execução diferida
            List<int> def_data = new List<int>() { 1, 2, 5, 8, 11 };
            var query = from p in def_data
                        select p;
            int count = 0;
            count = query.Count();
            Console.WriteLine(count); //Counts 5 records
            def_data.Add(12);
            count = query.Count();
            Console.WriteLine(count); //Count 6 records
                                      //System.Linq.Enumerable+WhereSelectListIterator`2[System.Int32,System.Int32]
            Console.WriteLine(query);

            //Immediate Execution
            List<int> imed_data = new List<int>() { 1, 2, 5, 8, 11 };
            var imed_query = (from p in imed_data
                              select p).ToList();
            int imed_count = 0;
            imed_count = imed_query.Count();
            Console.WriteLine(imed_count); //Counts 5 records
            imed_data.Add(12);
            imed_count = imed_query.Count();
            Console.WriteLine(imed_count); //Count 5 records
                                           //System.Collections.Generic.List`1[System.Int32]
            Console.WriteLine(imed_query);

            foreach (var item in imed_query)
            {
                Console.WriteLine(item); // 1, 2, 5, 8, 11 
            }

            //LINQ Operators
            //Select_Operator();
            Filtering_Operator();
            //Ordening_Operator();
            //Join_Operator();
            //Groupby_Operator();
            //Partition_Operator();
            //Aggregate_Operator();

            Console.ReadKey();
        }

        static bool IsEvenAndGT5(int i) { return (i % 2 == 0 && i > 5); }

        private static void Select_Operator()
        {
            //Projection Operator - Select
            IEnumerable<string> vendors = from p in orders
                                          select p.VendorName;
            foreach (var name in vendors)
            {
                Console.WriteLine(name);
            }

            int[] data1 = { 1, 2, 5 };
            int[] data2 = { 2, 4, 6 };
            var result5 = from d1 in data1
                          from d2 in data2
                          select d1 * d2;
            Console.WriteLine(string.Join(", ", result5)); // Displays 2, 4, 6, 4, 8, 12, 10, 20, 30

            //Projection Operator - SelectMany
            var pedidos = (from p in orders
                           select new
                           {
                               VendorName = p.VendorName,
                               countorders = p.OrderLines.Count,
                               produto = p.OrderLines.FirstOrDefault().Product.Description,
                               preco = p.OrderLines.FirstOrDefault().Product.Price
                           });

            foreach (var item in pedidos)
            {
                Console.WriteLine(item.VendorName + "\t" + item.produto + "\t" + item.countorders);
            }

            var pedidosmany = orders.SelectMany(x => x.VendorName);

            //foreach (var item in pedidosmany)
            //{
            //    Console.WriteLine(item);
            //}

            var subordersmany = orders.SelectMany(x => x.OrderLines);

            //foreach (var item in subordersmany)
            //{
            //    Console.WriteLine(item);
            //}

            //var pricemany = subordersmany.SelectMany(x => x.Product.Price);
            //var countmany = orders.SelectMany(x => x.OrderLines.Count);
        }

        private static void Filtering_Operator()
        {
            int[] data = { 1, 2, 5, 8, 11 };
            //Filtering Operator 
            IEnumerable<Person> people3 = from p in persons
                                          where p.Name.Length > 4
                                          select p;
            foreach (var item in people3)
            {
                Console.WriteLine(item.ID + "\t" + item.Name + "\t" + item.Address);
            }

            people3 = from p in persons
                      where p.Name.Length > 4
                      where p.Name.Length > 4
                      select p;

            var result2 = data.Where(d => d % 2 == 0);
            Console.WriteLine(string.Join(", ", result2)); // Displays 2, 8

            var result3 = from d in data
                          where d % 2 == 0
                          where d > 5
                          select d;
            Console.WriteLine(string.Join(", ", result3)); // Displays 8

            var evenNumbers = from i in data
                              where IsEvenAndGT5(i)
                              select i;
            foreach (int i in evenNumbers)
            {
                Console.WriteLine(i);
            }


            var distinctArray = orders.SelectMany(x => x.OrderLines).Select(x => x.Product.Description).Distinct();
            foreach (var i in distinctArray)
            {
                Console.WriteLine(i);
            }

            //deve implementar a interface IEquatable <T>
            var distinctobj = orders.SelectMany(x => x.OrderLines).Distinct();
            foreach (var i in distinctobj)
            {
                Console.WriteLine(i.Product.Description);
            }

            OrderLine orderline1 = new OrderLine() { Product = new Product { Description = "Coca" } };
            if (distinctobj.Contains(orderline1)) // True
                Console.WriteLine("The list already contains this orderline.");
        }

        private static void Ordening_Operator()
        {
            int[] data = { 1, 2, 5, 8, 11 };
            //Operador Ordenação
            var result4 = from d in data
                          where d > 5
                          orderby d descending
                          select d;
            Console.WriteLine(string.Join(", ", result4)); // Displays 11, 8

            var suborders0 = orders.SelectMany(x => x.OrderLines);
            var orderna_que = from d in suborders0
                              orderby d.Product.Description descending, d.Amount ascending
                              select d;

            var orderna_met = suborders0.OrderByDescending(d => d.Product.Description).ThenBy(d => d.Amount);

            foreach (var item in orderna_que)
            {
                Console.WriteLine(string.Join(", ", string.Format("Prod.: {0} - Amo.:{1}", item.Product.Description, item.Amount)));
            }
        }

        private static void Join_Operator()
        {
            //Operador Junção 
            var table1 = new Dictionary<int, string>();
            table1.Add(1, "Coca");
            table1.Add(2, "Batata");
            table1.Add(3, "Arroz");
            table1.Add(5, "Feijão");

            var table2 = new Dictionary<int, string>();
            table2.Add(1, "Coca");
            table2.Add(8, "Batata");
            table2.Add(9, null);
            table2.Add(10, null);

            var popularProducts = from t1 in table1
                                  join t2 in table2 on t1.Value equals t2.Value
                                  select new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value };


            var popularMethod = table1.Join(table2, t1 => t1.Value, t2 => t2.Value, (t1, t2) =>
            new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value });

            foreach (var item in popularProducts)
            {
                Console.WriteLine("Join t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

            //Operador Junção - OUTER JOIN
            var tab1totab2 = from t1 in table1
                             join t2 in table2
                             on t1.Value equals t2.Value into outerGroup
                             from t2 in outerGroup.DefaultIfEmpty()
                             select new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value };

            foreach (var item in tab1totab2)
            {
                Console.WriteLine("Left Outer t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

            var tab1totab2_met1 = table1.GroupJoin(table2, t1 => t1.Value, t2 => t2.Value, (t1, outerGroup) => outerGroup.
                                    DefaultIfEmpty(new KeyValuePair<int, string> { }).
                                    Select(t2 => new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value })).SelectMany(output => output);


            foreach (var item in tab1totab2_met1)
            {
                Console.WriteLine("Left Outer_met1 t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

            var tab1totab2_met2 = table1.GroupJoin(table2, t1 => t1.Value, t2 => t2.Value, (t1, outerGroup) => outerGroup.DefaultIfEmpty().
                                    Select(t2 => new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value })).SelectMany(output => output);


            foreach (var item in tab1totab2_met2)
            {
                Console.WriteLine("Left Outer_met2 t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

            var tab2totab1 = from t2 in table2
                             join t1 in table1
                             on t2.Key equals t1.Key into outerGroup //Não deixa colocar on t1.Key
                             from t1 in outerGroup.DefaultIfEmpty() //Não deixa colocar from t2
                             select new { t2Key = t2.Key, t2Value = t2.Value, t1Key = t1.Key, t1Value = t1.Value };

            foreach (var item in tab2totab1)
            {
                Console.WriteLine("'Right' Outer t2:" + item.t2Key + ", " + item.t2Value + " -t1: " + item.t1Key + ", " + item.t1Value);
            }

            // Operador Junção - CHAVE COMPOSTA
            var chavecomposta = from t1 in table1
                                join t2 in table2
                                on new { City = t1.Key, State = t1.Value } equals
                                    new { City = t2.Key, State = t2.Value }
                                select new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value };

            foreach (var item in chavecomposta)
            {
                Console.WriteLine("Composta: t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

            var chavecomposta_met = table1.Join(table2, t1 => new { City = t1.Key, State = t1.Value },
                                                        t2 => new { City = t2.Key, State = t2.Value },
                                                        (t1, t2) => new { t1Key = t1.Key, t1Value = t1.Value, t2Key = t2.Key, t2Value = t2.Value });

            foreach (var item in chavecomposta_met)
            {
                Console.WriteLine("Composta_met: t1:" + item.t1Key + ", " + item.t1Value + " -t2: " + item.t2Key + ", " + item.t2Value);
            }

        }

        private static void Groupby_Operator()
        {
            // Operador Agrupamento

            var result6 = from o in orders
                          from l in o.OrderLines
                          group l by l.Product into p
                          select new
                          {
                              Product = p.Key,
                              Amount = p.Sum(x => x.Amount)
                          };

            foreach (var i in result6.ToList())
            {
                Console.WriteLine(string.Format("Product: {0}; Amount: {1}", i.Product.Description, i.Amount));
            }

            var suborders = orders.SelectMany(x => x.OrderLines).GroupBy(x => x.Product.Description);

            foreach (var product in suborders)
            {
                Console.WriteLine("Product:" + product.Key + " Vendas:" + product.Sum(x => x.Product.Price));
            }

            var employeesByState = orders.SelectMany(x => x.OrderLines).GroupBy(e => new { e.Product.Description, e.Product.Price });


            int[] myArray = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var groupedNumbers = from i in myArray
                                 group i by (i % 2 == 0 ? "Even" : "Odd");

            foreach (var groupNumber in groupedNumbers)
            {
                Console.WriteLine(groupNumber.Key + ": " + groupNumber.Sum());
                Console.WriteLine(string.Join(", ", groupNumber));
            }

            var selectgrouped = from i in myArray
                                group i by (i % 2 == 0 ? "Even" : "Odd") into grupo
                                select new { Key = grupo.Key, Soma = grupo.Sum(), Numbers = grupo };

            foreach (var groupNumber in selectgrouped)
            {
                Console.WriteLine(groupNumber.Key + ": " + groupNumber.Soma);
                Console.WriteLine(string.Join(", ", groupNumber.Numbers));
            }
        }

        private static void Partition_Operator()
        {
            // Operador Partição
            var pageIndex = 2;
            var pageSize = 10;
            var pagedOrders = orders.Skip((pageIndex - 1) * pageSize).Take(pageSize);


            var first = orders.Where(i => i.VendorName == "João").First();
            var last = orders.Where(i => i.VendorName == "João").Last();
        }

        private static void Aggregate_Operator()
        {
            // Operador Agregação
            var average = orders.Average(o => o.OrderLines.Count);
            var sum = orders.Sum(o => o.OrderLines.Capacity);
            var min = orders.Min(o => o.OrderLines.Count);
            var max = orders.Max(o => o.OrderLines.Count);
            Console.WriteLine(string.Format("Média: {0}; Min:{1}; Max: {2}", average, min, max));
        }

        private static void SeedData()
        {
            persons = new List<Person>()
            {
            new Person() { ID=1,Name="Ali Asad",Address="Pakistan",Salary=10000},
            new Person() { ID=5,Name="Hamza Ali",Address="Pakistan",Salary=20000},
            new Person() { ID=3,Name="John Snow",Address="Canada",Salary=15000},
            new Person() { ID=2,Name="Lakhtey",Address="Pakistan",Salary=5000},
            new Person() { ID=4,Name="Umar",Address="UK",Salary=25000},
            new Person() { ID=6,Name="Mubashar",Address="Pakistan",Salary=8000},
            };

            orders = new List<Order>()
                {
                    new Order()
                        {
                            VendorName = "João",
                            OrderLines = new List<OrderLine>()
                            {
                                new OrderLine(){ Amount= 3, Product = new Product(){ Description="Coca", Price=2 } },
                                new OrderLine(){ Amount= 42, Product = new Product(){ Description="Fanta", Price=15 } },
                                new OrderLine(){ Amount= 91, Product = new Product(){ Description="Bife", Price=54 } },
                            }
                        },
                    new Order()
                        {
                            VendorName = "Marcos",
                            OrderLines = new List<OrderLine>()
                            {
                                new OrderLine(){ Amount= 3, Product = new Product(){ Description="Coca", Price=2 } },
                                new OrderLine(){ Amount= 44, Product = new Product(){ Description="Lanche", Price=8 } },
                                new OrderLine(){ Amount= 91, Product = new Product(){ Description="Bife", Price=54 } },
                            }
                        },
                };
        }
    }
}
