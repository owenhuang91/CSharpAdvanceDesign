using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyCastTests
    {
        [Test]
        public void cast_int_exception_when_ArrayList_has_string()
        {
            var arrayList = new ArrayList { 1, "2", 3 };

            void TestDelegate() => JoeyCast<int>(arrayList).ToList();

            Assert.Throws<JoeyCastTestException>(TestDelegate);
        }

        private IEnumerable<T> JoeyCast<T>(IEnumerable source)
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator is T item)
                {
                    yield return item;
                }
                else
                {
                    throw new JoeyCastTestException();
                }
            }
        }
    }

    public class JoeyCastTestException : Exception
    {
    }
}