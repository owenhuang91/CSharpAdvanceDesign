using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyCompare : IComparer<Employee>
    {
        public CombineKeyCompare(Func<Employee, string> keySelector, IComparer<string> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        public Func<Employee, string> KeySelector { get; private set; }
        public IComparer<string> KeyComparer { get; private set; }

        public int Compare(Employee element, Employee minElement)
        {
            return this.KeyComparer.Compare(this.KeySelector(element), this.KeySelector(minElement));
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        [Test]
        public void orderBy_lastName_and_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstCompare = new CombineKeyCompare(element => element.LastName, Comparer<string>.Default);
            var lastCompare = new CombineKeyCompare(element => element.FirstName, Comparer<string>.Default);
            var actual = JoeyOrderByLastNameAndFirstName(employees, firstCompare, lastCompare);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByLastNameAndFirstName(IEnumerable<Employee> employees, IComparer<Employee> firstCompare, IComparer<Employee> secondCompare)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];

                    var firstCompareResult = firstCompare.Compare(element, minElement);
                    if (firstCompareResult < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                    else if (firstCompareResult == 0)
                    {
                        var secondCompareResult = secondCompare.Compare(element, minElement);
                        if (secondCompareResult < 0)
                        {
                            minElement = element;
                            index = i;
                        }
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}