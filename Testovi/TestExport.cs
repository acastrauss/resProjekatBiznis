using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logika;
using Modeli.WebModeli;
using Moq;
using NUnit.Framework;


namespace Testovi
{
    [TestFixture]
    public class TestExport
    {
        private IExport eksport;
        private List<PodaciZaPrikaz> lista;

        [SetUp]
        public void SetUp()
        {
            Mock<List<PodaciZaPrikaz>> listaMokova = new Mock<List<PodaciZaPrikaz>>();
            Mock<Export> mokEksporta = new Mock<Export>();
            eksport = mokEksporta.Object;
            PodaciZaPrikaz p1 = new PodaciZaPrikaz("drzava1",DateTime.Parse("01/01/2001"),22.2222,10,10.0,50,30);
            PodaciZaPrikaz p2 = new PodaciZaPrikaz("drzava2", DateTime.Parse("02/02/2002"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p3 = new PodaciZaPrikaz("drzava3", DateTime.Parse("03/03/2003"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p4 = new PodaciZaPrikaz("drzava4", DateTime.Parse("04/04/2004"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p5 = new PodaciZaPrikaz("drzava5", DateTime.Parse("05/05/2005"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p6 = new PodaciZaPrikaz("drzava6", DateTime.Parse("06/06/2006"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p7 = new PodaciZaPrikaz("drzava7", DateTime.Parse("07/07/2007"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p8 = new PodaciZaPrikaz("drzava8", DateTime.Parse("08/08/2008"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p9 = new PodaciZaPrikaz("drzava9", DateTime.Parse("09/09/2009"), 22.2222, 10, 10.0, 50, 30);
            PodaciZaPrikaz p10 = new PodaciZaPrikaz("drzava10", DateTime.Parse("10/10/2010"), 22.2222, 10, 10.0, 50, 30);
            listaMokova.Object.Add(p1);
            listaMokova.Object.Add(p2);
            listaMokova.Object.Add(p3);
            listaMokova.Object.Add(p4);
            listaMokova.Object.Add(p5);
            listaMokova.Object.Add(p6);
            listaMokova.Object.Add(p7);
            listaMokova.Object.Add(p8);
            listaMokova.Object.Add(p9);
            listaMokova.Object.Add(p10);
            lista = listaMokova.Object;
        }
        [Test]
        public void TestEksportNull()
        {
            try
            {
                eksport.SaveData(null);
                Assert.Fail();
                this.SetUp();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Lista ne moze biti prazna niti null"));
            }
        }
        [Test]
        public void TestEksportPrazan()
        {
            try
            {
                List<PodaciZaPrikaz> lista = new List<PodaciZaPrikaz>();
                eksport.SaveData(lista);
                Assert.Fail();
                this.SetUp();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Lista ne moze biti prazna niti null"));
            }
        }
        [Test]
        public void TestEksportRegularan()
        {
            string putanja = eksport.SaveData(lista);
            Assert.IsTrue(File.Exists(putanja));
            this.SetUp();
        }


        [TearDown]
        public void TearDown()
        {
            lista.Clear();
        }

    }
}
