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

namespace Testovi
{
    public class TestImport
    {
        private IImport import;

        [SetUp]
        public void SetUp()
        {
            Mock<Import> MokImporta = new Mock<Import>();
            import = MokImporta.Object;
        }

        [Test]
        public void VremeTestFajlNull()
        {
            try
            {
                import.LoadVreme(null, "Serbia", DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Fajl ne sme biti null"));
            }
        }
        [Test]
        public void VremeTestDrzavaNull()
        {
            try
            {
                string putanja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\PodaciZaTest\\VremeTest.csv";
                import.LoadVreme(putanja,null, DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Drzava ne sme biti null"));
            }
        }
        [Test]
        public void VremeTestDrzavaPrazna()
        {
            try
            {
                string putanja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\PodaciZaTest\\VremeTest.csv";
                import.LoadVreme(putanja,"", DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Drzava ne sme biti null"));
            }
        }

        [Test]
        public void PotrosnjaTestFajlNull()
        {
            try
            {
                import.LoadPotrosnja(null, "Serbia", DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Fajl ne sme biti null"));
            }
        }
        [Test]
        public void PotrosnjaTestDrzavaNull()
        {
            try
            {
                string putanja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\PodaciZaTest\\PotrosnjaTest.csv";
                import.LoadPotrosnja(putanja, null, DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Drzava ne sme biti null"));
            }
        }
        [Test]
        public void PotrosnjaTestDrzavaPrazna()
        {
            try
            {
                string putanja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\PodaciZaTest\\PotrosnjaTest.csv";
                import.LoadPotrosnja(putanja, "", DateTime.Parse("05/05/2016"), DateTime.Parse("05/05/2021"));
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Drzava ne sme biti null"));
            }
        }

        [Test]
        public void PotrosnjaTestOk()
        {
            string putanjaPotrosnja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\Test\\PotrosnjaTest.xlsx";
            try
            {
                import.LoadPotrosnja(putanjaPotrosnja, "Serbia", DateTime.Parse("05/05/2011"), DateTime.Parse("05/05/2021"));
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }

        }
        [Test]
        public void VremeTestOk()
        {
            string putanjaVreme = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\Test\\VremeTest.csv";
            try
            {
                import.LoadVreme(putanjaVreme, "Serbia", DateTime.Parse("05/05/2011"), DateTime.Parse("05/05/2021"));
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void AllTestOk()
        {
            string putanjaVreme = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\Test\\VremeTest.csv";
            string putanjaPotrosnja = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)) + "\\Test\\PotrosnjaTest.xlsx";
            List<string> lista = new List<string>();
            lista.Add(putanjaVreme);
            try
            {
                import.Load(putanjaPotrosnja, lista, "Serbia", DateTime.Parse("05/05/2011"), DateTime.Parse("05/05/2021"));
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}
