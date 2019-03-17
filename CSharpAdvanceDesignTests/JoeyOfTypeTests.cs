﻿using Lab;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyOfTypeTests
    {
        [Test]
        public void get_special_type_value_from_arguments()
        {
            //ActionExecutingContext.ActionArguments: Dictionary<string,object>

            var arguments = new Dictionary<string, object>
            {
                {"model", new Product {Price = 100, Cost = 111}},
                {"validator1", new ProductValidator()},
                {"validator2", new ProductValidator2()},
            };

            var validators = JoeyOfType<IValidator<Product>>(arguments.Values);

            Assert.AreEqual(2, validators.Count());
        }

        private IEnumerable<TResult> JoeyOfType<TResult>(IEnumerable argumentsValues)
        {
            var enumerator = argumentsValues.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is TResult item)
                {
                    yield return item;
                }
            }
        }
    }
}