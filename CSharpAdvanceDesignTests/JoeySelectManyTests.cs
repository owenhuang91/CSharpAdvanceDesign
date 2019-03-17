using ExpectedObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySelectManyTests
    {
        [Test]
        public void flat_all_cities_and_sections()
        {
            var cities = new List<City>
            {
                new City {Name = "台北市", Sections = new List<string> {"大同", "大安", "松山"}},
                new City {Name = "新北市", Sections = new List<string> {"三重", "新莊"}},
            };

            var actual = JoeySelectMany(cities, (city, sectionName) => $"{city.Name}-{sectionName}");
            var expected = new[]
            {
                "台北市-大同",
                "台北市-大安",
                "台北市-松山",
                "新北市-三重",
                "新北市-新莊",
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<string> JoeySelectMany(IEnumerable<City> cities, Func<City, string, string> joeySelectMany)
        {
            var enumerator = cities.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var city = enumerator.Current;
                var subEnumerator = city.Sections.GetEnumerator();
                while (subEnumerator.MoveNext())
                {
                    var sectionName = subEnumerator.Current;
                    yield return joeySelectMany(city, sectionName);
                }
            }
        }
    }
}