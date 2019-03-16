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

    public class ComboComparer : IComparer<Employee>
    {
        public ComboComparer(IComparer<Employee> firstCompare, IComparer<Employee> secondCompare)
        {
            FirstCompare = firstCompare;
            SecondCompare = secondCompare;
        }

        public IComparer<Employee> FirstCompare { get; private set; }
        public IComparer<Employee> SecondCompare { get; private set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstCompare.Compare(x, y);
            if (firstCompareResult == 0)
            {
                return SecondCompare.Compare(x, y);
            }

            return firstCompareResult;
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
            var actual = JoeyOrderBy(employees, new ComboComparer(firstCompare, lastCompare));

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderBy(IEnumerable<Employee> employees, ComboComparer comboComparer)
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

                    if (comboComparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }
    }
}