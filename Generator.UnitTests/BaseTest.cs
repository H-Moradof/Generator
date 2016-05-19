using Generator.Base;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.UnitTests
{
    [TestFixture]
    public abstract class BaseTest
    {
        public const string APPLICATION_ROOT_PATH = @"D:\Hamid Projects\Generator\Generator_v10.2\Generator";
        public const string TARGET_DATABASE_NAME = @"LahniDB";


        // this is my fist change

        [SetUp]
        public void SetUp()
        {
            
        }

    }
}
