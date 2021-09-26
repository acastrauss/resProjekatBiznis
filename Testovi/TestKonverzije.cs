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
    public class TestKonverzije
    {
        private IKonverzija konverzija;
        [SetUp]
        public void SetUp()
        {
            Mock<Konverzija> MokKonverzija = new Mock<Konverzija>();
            konverzija = MokKonverzija.Object;
        }
        [Test]
        public void TestKonverzijaNull()
        {
            try
            {
                konverzija.ModeliZaPrikaz(null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Lista ne sme biti null"));
            }
        }

        #region podaci
        private static readonly object[] podatak =
        {
            new object[]
            {
                new List<DrzavaWeb>()
                {
                    new DrzavaWeb()
                    {
                        Naziv = "Serbia",
                        KratakNaziv = "RS",
                        Vremena = new List<VremeWeb>(){
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016"))
                        },
                        Potrosnje = new List<PotrsonjaWeb>()
                        {
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016"))
                        }
                    },
                    new DrzavaWeb()
                    {
                         Naziv = "Serbia",
                        KratakNaziv = "RS",
                        Vremena = new List<VremeWeb>(){
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016"))
                        },
                        Potrosnje = new List<PotrsonjaWeb>()
                        {
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016"))
                        }
                    },
                    new DrzavaWeb()
                    {
                        Naziv = "Serbia",
                        KratakNaziv = "RS",
                        Vremena = new List<VremeWeb>(){
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                            new VremeWeb(1,1,10,33.33,11,11,DateTime.Parse("05/05/2016")),
                        },
                        Potrosnje = new List<PotrsonjaWeb>()
                        {
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2016")),
                            new PotrsonjaWeb(1,1,22.22,DateTime.Parse("05/05/2013"))
                        }
                    }
                }
            }
        };

        #endregion

        [Test]
        [TestCaseSource("podatak")]
        public void MapCWTestOk(List<DrzavaWeb> drzave)
        {

            IEnumerable<PodaciZaPrikaz> podaci = konverzija.ModeliZaPrikaz(drzave);
            bool provera = true;
            podaci.ToList();
            foreach (PodaciZaPrikaz data in podaci)
            {
                if (!(
                    (data.NazivDrzave == "Serbia") &&
                    (data.KolicinaEnergije == 22.22 || data.KolicinaEnergije == null) &&
                    (DateTime.Compare((DateTime)data.DatumUTC, new DateTime(2016, 5, 5)) == 0 ||
                    DateTime.Compare((DateTime)data.DatumUTC, new DateTime(2013, 5, 5)) == 0) &&
                    (data.Temperatura == 10 || data.Temperatura == null) &&
                    (data.BrzinaVetra == 11 || data.BrzinaVetra == null) &&
                    (data.Pritisak == 33.33 || data.Pritisak == null) &&
                    (data.VlaznostVazduha == 11 || data.VlaznostVazduha == null)
                    )
                    )
                {
                    provera = false;
                    break;
                }
            }
            Assert.IsTrue(provera);
        }


    }
}
