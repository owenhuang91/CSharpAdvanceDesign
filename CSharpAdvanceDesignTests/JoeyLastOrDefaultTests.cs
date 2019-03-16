using ExpectedObjects;
using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyLastOrDefaultTests
    {
        //[Test]
        //public void get_null_when_employees_is_empty()
        //{
        //    var employees = new List<Employee>();
        //    var actual = employees.JoeyLastOrDefault();
        //    Assert.IsNull(actual);
        //}

        [Test]
        public void get_last_employee()
        {
            var employees = new[]
            {
                new Employee() {FirstName = "Joey", LastName = "Chen"},
                new Employee() {FirstName = "Cash", LastName = "Chen"},
                new Employee() {FirstName = "David", LastName = "Chen"},
            };
            var actual = employees.JoeyLastOrDefault(source => source.FirstName == "David" && source.LastName == "Chen");
            var expected = new Employee() { FirstName = "David", LastName = "Chen" };
            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}