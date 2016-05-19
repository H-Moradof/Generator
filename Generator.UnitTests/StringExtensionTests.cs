using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.UnitTests
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ToCamelCaseFormat__if_give_two_part_string_value_return_correct_value()
        {
            var testString = "TestCase";

            var result = testString.ToCamelCaseFormat();

            Assert.AreEqual(result , "testCase");
        }


        [Test]
        public void ToCamelCaseFormat__if_give_one_part_string_value_return_correct_value()
        {
            var testString = "Test";

            var result = testString.ToCamelCaseFormat();

            Assert.AreEqual(result, "test");
        }


    }
}
