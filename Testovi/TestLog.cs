using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Logika;
using Modeli.WebModeli;
using System.IO;
using Modeli;

namespace Testovi
{
    public class TestLog
    {
        private ILogPisanje log;

        [SetUp]
        public void SetUp()
        {
            Mock<LogPisanje> MokLog = new Mock<LogPisanje>();
            log = MokLog.Object;
        }

        [Test]
        public void LogTestOk()
        {
            try
            {
                LogPodatak lp = new LogPodatak(LOG_TYPE.INFO, "Poruka", DateTime.Parse("05/05/2011"));
                log.AddLog(lp);
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
        [Test]
        public void LogTestNull()
        {
            try
            {
                log.AddLog(null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Log podatak ne moze biti null"));
            }
        }
    }
}
