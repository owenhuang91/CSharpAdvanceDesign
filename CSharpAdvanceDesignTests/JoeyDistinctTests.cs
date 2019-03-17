using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = JoeyDistinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employees()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyDistinctWithCompare(employees, new EmployeeCompare());

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyDistinctWithCompare<TSource>(IEnumerable<TSource> sources, IEqualityComparer<TSource> compare)
        {
            var enumerator = sources.GetEnumerator();
            var hash = new HashSet<TSource>(compare);
            while (enumerator.MoveNext())
            {
                if (hash.Add(enumerator.Current))
                {
                    yield return enumerator.Current;
                }
            }
        }

        private IEnumerable<TSource> JoeyDistinct<TSource>(IEnumerable<TSource> numbers)
        {
            return JoeyDistinctWithCompare(numbers, EqualityComparer<TSource>.Default);
        }
    }
}