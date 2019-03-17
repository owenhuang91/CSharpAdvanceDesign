using ExpectedObjects;
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
            var actual = Distinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            var enumerator = numbers.GetEnumerator();
            var checkLookup = new Dictionary<int, int>();
            while (enumerator.MoveNext())
            {
                if (!checkLookup.ContainsKey(enumerator.Current))
                {
                    checkLookup.Add(enumerator.Current, enumerator.Current);
                    yield return enumerator.Current;
                }
            }
        }
    }
}