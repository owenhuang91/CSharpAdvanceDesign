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

            var actual = JoeyJoin(employees, pets, employee1 => employee1, pet1 => pet1.Owner, (employee, pet) => $"{pet.Name}-{employee.LastName}");

            var expected = new[]
            {
                "Didi-Li",
                "Lala-Chen",
                "QQ-Chen",
                "Fufu-Wang"
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TResult> JoeyJoin<TKey, TResult>(IEnumerable<Employee> employees, IEnumerable<Pet> pets,
            Func<Employee, TKey> employeeKeySelector, Func<Pet, TKey> petKeySelector,
            Func<Employee, Pet, TResult> resultSelector)
        {
            var firstEnumerator = employees.GetEnumerator();
            while (firstEnumerator.MoveNext())
            {
                var secondEnumerator = pets.GetEnumerator();
                while (secondEnumerator.MoveNext())
                {
                    var employee = firstEnumerator.Current;
                    var pet = secondEnumerator.Current;
                    if (employeeKeySelector(employee).Equals(petKeySelector(pet)))
                    {
                        yield return resultSelector(employee, pet);
                    }
                }
                secondEnumerator.Reset();
            }
        }
    }
}