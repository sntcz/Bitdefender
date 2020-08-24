using BDModule.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace BDModule.UnitTest
{
    [TestClass]
    public class TestGetReportList : BDModuleCmdletTestBase
    {

        [TestMethod]
        public void Complex_Test()
        {
            IList<PSObject> result = Invoke(typeof(GetReportsList), "Type", ReportType.MalwareStatus);
            Assert.IsNotNull(result);
        }
    }
}
