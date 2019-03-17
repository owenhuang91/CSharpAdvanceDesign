using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyJoinTests
    {
        [Test]
        public void all_pets_and_owner()
        {
            var david = new Employee { FirstName = "David", LastName = "Li" };
            var joey = new Employee { FirstName = "Joey", LastName = "Chen" };
            var tom = new Employee { FirstName = "Tom", LastName = "Wang" };

            var employees = new[]
            {
                david,
                joey,
                tom
            };

            var pets = new Pet[]
            {
                new Pet() {Name = "Lala", Owner = joey},
                new Pet() {Name = "Didi", Owner = david},
                new Pet() {Name = "Fufu", Owner = tom},
                new Pet() {Name = "QQ", Owner = joey},
            };

            var actual = JoeyJoin(employees, pets, employee1 => employee1, pet1 => pet1.Owner, (employee, pet) => $"{pet.Name}-{employee.LastName}", EqualityComparer<Employee>.Default);

            var expected = new[]
            {
                "Didi-Li",
                "Lala-Chen",
                "QQ-Chen",
                "Fufu-Wang"
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TOuter, TInner, TKey, TResult>(
            IEnumerable<TOuter> outers,
            IEnumerable<TInner> inners,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector,
            IEqualityComparer<TKey> comparer)
        {
            var outerEnumerator = outers.GetEnumerator();
            while (outerEnumerator.MoveNext())
            {
                var innerEnumerator = inners.GetEnumerator();
                while (innerEnumerator.MoveNext())
                {
                    var outer = outerEnumerator.Current;
                    var inner = innerEnumerator.Current;

                    if (comparer.Equals(outerKeySelector(outer), innerKeySelector(inner)))
                    {
                        yield return resultSelector(outer, inner);
                    }
                }
                innerEnumerator.Reset();
            }
        }
    }
}