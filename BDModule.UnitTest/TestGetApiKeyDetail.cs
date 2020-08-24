using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BDModule.Models;

namespace BDModule.UnitTest
{
    [TestClass]
    public class TestGetApiKeyDetail : BDModuleCmdletTestBase
    {

        [TestMethod]
        public void Complex_Test()
        {
            IList<PSObject> result = Invoke(typeof(GetApiKeyDetail));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.IsNotNull(result[0].BaseObject);
            Assert.IsInstanceOfType(result[0].BaseObject, typeof(ApiKeyDetail));
        }
    }
}
