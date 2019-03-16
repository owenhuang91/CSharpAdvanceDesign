using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    public class CombineKeyCompare<TKey> : IComparer<Employee>
    {
        public CombineKeyCompare(Func<Employee, TKey> keySelector, IComparer<TKey> keyComparer)
        {
            KeySelector = keySelector;
            KeyComparer = keyComparer;
        }

        public Func<Employee, TKey> KeySelector { get; private set; }
        public IComparer<TKey> KeyComparer { get; private set; }

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
        public void lastName_firstName_Age()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            var firstComparer = new CombineKeyCompare<string>(element => element.LastName, Comparer<string>.Default);
            var secondComparer = new CombineKeyCompare<string>(element => element.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboComparer(firstComparer, secondComparer);

            var thirdComparer = new CombineKeyCompare<int>(element => element.Age, Comparer<int>.Default);

            var finalCombo = new ComboComparer(firstCombo, thirdComparer);

            var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
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