using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyAggregateTests
    {
        [Test]
        public void drawling_money_that_balance_have_to_be_positive()
        {
            var balance = 100.91m;

            var drawlingList = new List<int>
            {
                30, 80, 20, 40, 25
            };

            //var actual = JoeyAggregate(drawlingList, balance, (seed, current) => seed >= current ? seed - current : seed);
            var actual = JoeyAggregate(drawlingList, balance, (seed, current) => seed - (seed >= current ? current : 0));

            var expected = 10.91m;

            Assert.AreEqual(expected, actual);
        }

        //提供一個暫存值, 然後用你決定的方法來修改這個暫存值, 最後回給你這個值
        private decimal JoeyAggregate(IEnumerable<int> drawlingList, decimal balance, Func<decimal, int, decimal> func)
        {
            var seed = balance;
            var enumerator = drawlingList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                seed = func(seed, current);
            }

            return seed;
        }
    }
}