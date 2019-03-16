using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyWhereTests
    {
        [Test]
        public void find_products_that_price_between_200_and_500()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.JoeyWhere(product => product.Price > 200 && product.Price < 500);

            var expected = new List<Product>
            {
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            var testDatas = products.JoeyWhere(m => m.Price > 200 && m.Price < 500).JoeyWhere(m => m.Cost > 2).JoeySelect(m => m.Price);
            foreach (var testData in testDatas)
            {
            }

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_products_that_price_between_200_and_500_and_cost_more_than_30()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var actual = products.JoeyWhere(p => p.Price > 200 && p.Price < 500 && p.Cost > 30);

            var expected = new List<Product>
            {
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"}
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_short_names()
        {
            var names = new List<string> { "Joey", "Cash", "William", "Sam", "Brian", "Jessica" };
            var actual = names.JoeyWhere(n => n.Length < 5);
            var expected = new[]
            {
                "Joey", "Cash", "Sam"
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void find_odd_names()
        {
            var names = new List<string> { "Joey", "Cash", "William", "Sam", "Brian", "Jessica" };
            var actual = names.JoeyWhere((n, i) => i % 2 == 0);
            var expected = new[]
            {
                "Joey", "William", "Brian"
            };
            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void group_sum_group_count_is_3_sum_cost()
        {
            var products = new List<Product>
            {
                new Product {Id = 1, Cost = 11, Price = 110, Supplier = "Odd-e"},
                new Product {Id = 2, Cost = 21, Price = 210, Supplier = "Yahoo"},
                new Product {Id = 3, Cost = 31, Price = 310, Supplier = "Odd-e"},
                new Product {Id = 4, Cost = 41, Price = 410, Supplier = "Odd-e"},
                new Product {Id = 5, Cost = 51, Price = 510, Supplier = "Momo"},
                new Product {Id = 6, Cost = 61, Price = 610, Supplier = "Momo"},
                new Product {Id = 7, Cost = 71, Price = 710, Supplier = "Yahoo"},
                new Product {Id = 8, Cost = 18, Price = 780, Supplier = "Yahoo"}
            };

            var expected = new[]
            {
                63,
                153,
                89
            };

            var actual = products.JoeyGroupSum(3, m => m.Cost);

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        //private IEnumerable<int> JoeyGroupSum1(List<Product> products)
        //{
        //    var count = products.Count / 3 + 1;
        //    for (int i = 0; i < count; i++)
        //    {
        //        yield return products.Skip(i * 3).Take(3).Sum(m => m.Cost);
        //    }
        //}
    }
}