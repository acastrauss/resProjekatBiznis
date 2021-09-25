using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Logika;
using Modeli.WebModeli;

namespace Testovi
{
    public class TestFilter
    {
        private IFilterDatum filter;
        private List<PodaciZaPrikaz> lista;

        [SetUp]
        public void SetUp()
        {
            Mock<FilterDatum> MokFilter = new Mock<FilterDatum>();
            filter = MokFilter.Object;
            Mock<List<PodaciZaPrikaz>> mockList = new Mock<List<PodaciZaPrikaz>>();
            PodaciZaPrikaz p1 = new PodaciZaPrikaz("Serbia", Convert.ToDateTime("05/05/2016"),33.3333,34,44.44444,12,32);
            PodaciZaPrikaz p2 = new PodaciZaPrikaz("Croatia", Convert.ToDateTime("03/03/2010"), 33.3333, 34, 44.44444, 12, 32);
            PodaciZaPrikaz p3 = new PodaciZaPrikaz("Slovenia", Convert.ToDateTime("01/01/2011"), 33.3333, 34, 44.44444, 12, 32);
            PodaciZaPrikaz p4 = new PodaciZaPrikaz("Bih", Convert.ToDateTime("09/09/2009"), 33.3333, 34, 44.44444, 12, 32);
            PodaciZaPrikaz p5 = new PodaciZaPrikaz("Macedonia", Convert.ToDateTime("05/05/2000"), 33.3333, 34, 44.44444, 12, 32);
            mockList.Object.Add(p1);
            mockList.Object.Add(p2);
            mockList.Object.Add(p3);
            mockList.Object.Add(p4);
            mockList.Object.Add(p5);
            lista = mockList.Object;
        }

        [Test]
        [TestCase("01/01/2011", "01/01/2021")]
        public void TestFilterRegularan(string pocetni, string krajnji)
        {
            DateTime Pocetni = DateTime.Parse(pocetni);
            DateTime Krajnji = DateTime.Parse(krajnji);
            lista = filter.FiltrirajPoDatumu(lista, Pocetni, Krajnji).ToList();
            Assert.IsTrue(lista.ToList().All(x => x.DatumUTC >= Pocetni && x.DatumUTC <= Krajnji));
            this.SetUp();
        }

        [Test]
        [TestCase("12/31/9999", "01/01/0001")]
        public void TestFilterPrazan(string pocetni, string krajnji)
        {
            DateTime Pocetni = DateTime.Parse(pocetni);
            DateTime Krajnji = DateTime.Parse(krajnji);
            List<PodaciZaPrikaz> lis = filter.FiltrirajPoDatumu(lista, Pocetni, Krajnji).ToList();
            Assert.IsTrue(lis.Count() == 0);
            this.SetUp();
        }

        [Test]
        [TestCase("01/01/0001", "12/31/9999")]
        public void TestFilterByTimeAll(string pocetni, string krajnji)
        {
            bool upit = true;
            List<PodaciZaPrikaz> temp = lista;
            DateTime Pocetni = DateTime.Parse(pocetni);
            DateTime Krajnji = DateTime.Parse(krajnji);
            lista = filter.FiltrirajPoDatumu(lista, Pocetni, Krajnji).ToList();
            for (int i = 0; i < temp.Count(); i++)
            {
                if (lista.Count() == temp.Count())
                {
                    if (!lista[i].Equals(temp[i]))
                    {
                        upit = false;
                    }
                }
            }
            Assert.IsTrue(upit);
            this.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            lista.Clear();
        }


    }

}
