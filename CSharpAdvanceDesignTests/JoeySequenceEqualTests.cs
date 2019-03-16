using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal_length1()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_not_equal_length2()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_empty()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void two_employees_sequence_equal()
        {
            var first = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "456"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "789"},
            };

            var second = new List<Employee>
            {
                new Employee() {FirstName = "Joey", LastName = "Chen", Phone = "123"},
                new Employee() {FirstName = "Tom", LastName = "Li", Phone = "123"},
                new Employee() {FirstName = "David", LastName = "Wang", Phone = "123"},
            };

            IEqualityComparer<Employee> equalityComparer = new JoeyEmployeeWithPhoneEqualityComparer();
            var actual = JoeySequenceEqual(
                first,
                second,
                equalityComparer);

            Assert.IsTrue(actual);
        }

        [Test]
        [Ignore("")]
        public void two_employees_sequence_equal_dict()
        {
            var a = new Employee() { FirstName = "Joey", LastName = "Chen", Phone = "123" };
            var b = new Employee() { FirstName = "Joey", LastName = "Chen", Phone = "456" };
            var c = new Employee() { FirstName = "David", LastName = "Wang", Phone = "789" };

            var aaa = new Dictionary<Employee, int>(new JoeyEmployeeWithPhoneEqualityComparer());
            aaa.Add(a, 1);
            aaa.Add(b, 2);
            aaa.Add(c, 3);
        }

        private bool JoeySequenceEqual<Tsource>(IEnumerable<Tsource> first, IEnumerable<Tsource> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var tempFirst = firstEnumerator.MoveNext();
                var tempSecond = secondEnumerator.MoveNext();
                //長度不同
                if (tempFirst != tempSecond)
                {
                    return false;
                }
                //因為前面已經判斷過長度了，因此若此時first是false,表示second也是false,而且到目前為止資料都相同。故可判斷此時集合已經判斷結束
                if (!tempFirst)
                {
                    return true;
                }
                //資料不同
                if (!firstEnumerator.Current.Equals(secondEnumerator.Current))
                {
                    return false;
                }
            }
        }

        private bool JoeySequenceEqual<Tsource>(IEnumerable<Tsource> first, IEnumerable<Tsource> second, IEqualityComparer<Tsource> compare)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            while (true)
            {
                var tempFirst = firstEnumerator.MoveNext();
                var tempSecond = secondEnumerator.MoveNext();
                //長度不同
                if (tempFirst != tempSecond)
                {
                    return false;
                }
                //因為前面已經判斷過長度了，因此若此時first是false,表示second也是false,而且到目前為止資料都相同。故可判斷此時集合已經判斷結束
                if (!tempFirst)
                {
                    return true;
                }
                //資料不同
                if (!compare.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return false;
                }
            }
        }
    }

    public class JoeyEmployeeWithPhoneEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            return (new { FirstName = obj.FirstName, LastName = obj.LastName }).GetHashCode();
        }
    }
}