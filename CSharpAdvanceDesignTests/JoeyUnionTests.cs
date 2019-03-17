using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyUnionTests
    {
        [Test]
        public void union_numbers()
        {
            var first = new[] { 1, 3, 5, 3, 1 };
            var second = new[] { 5, 3, 7, 7 };

            var actual = JoeyUnion(first, second);
            var expected = new[] { 1, 3, 5, 7 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyUnion(IEnumerable<Employee> first, IEnumerable<Employee> second, EmployeeCompare comparer)
        {
            var hash = new HashSet<Employee>(comparer);
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (hash.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                if (hash.Add(secondEnumerator.Current))
                {
                    yield return secondEnumerator.Current;
                }
            }
        }

        private IEnumerable<int> JoeyUnion(IEnumerable<int> first, IEnumerable<int> second)
        {
            var hash = new HashSet<int>();
            var enumerator = first.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (hash.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }

            var secondEnumerator = second.GetEnumerator();
            while (secondEnumerator.MoveNext())
            {
                if (hash.Add(secondEnumerator.Current))
                {
                    yield return secondEnumerator.Current;
                }
            }
        }
    }
}