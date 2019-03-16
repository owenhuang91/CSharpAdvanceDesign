using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyZipTests
    {
        [Test]
        public void pair_girls_and_keys()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girl, key) => $"{girl.Name}-{key.Owner}");

            var expected = new[]
            {
                "Mary-Joey",
                "Jessica-David",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        [Test]
        public void pair_girls_and_keys_car_type()
        {
            var girls = new List<Girl>
            {
                new Girl() {Name = "Mary"},
                new Girl() {Name = "Jessica"},
            };

            var keys = new List<Key>
            {
                new Key() {Type = CardType.BMW, Owner = "Joey"},
                new Key() {Type = CardType.TOYOTA, Owner = "David"},
                new Key() {Type = CardType.Benz, Owner = "Tom"},
            };

            var pairs = JoeyZip(girls, keys, (girl, key) => $"{girl.Name}-{key.Type}");

            var expected = new[]
            {
                "Mary-BMW",
                "Jessica-TOYOTA",
            };

            expected.ToExpectedObject().ShouldMatch(pairs);
        }

        private IEnumerable<TResult> JoeyZip<TSource1, TSource2, TResult>(IEnumerable<TSource1> source1, IEnumerable<TSource2> source2, Func<TSource1, TSource2, TResult> func)
        {
            var source1Enumerator = source1.GetEnumerator();
            var source2Enumerator = source2.GetEnumerator();

            while (source1Enumerator.MoveNext() && source2Enumerator.MoveNext())
            {
                var result1 = source1Enumerator.Current;
                var result2 = source2Enumerator.Current;
                yield return func(result1, result2);
            }
        }
    }
}