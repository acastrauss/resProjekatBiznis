using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeli.WebModeli
{
    public class PodaciZaPrikaz
    {
        public String NazivDrzave { get; set; }
        public DateTime? DatumUTC { get; set; }
        public double? KolicinaEnergije { get; set; }
        public int? Temperatura { get; set; }
        public double? Pritisak { get; set; }
        public int? VlaznostVazduha { get; set; }
        public int? BrzinaVetra { get; set; }

        public PodaciZaPrikaz() { }

        public PodaciZaPrikaz(string naziv,DateTime datum,double kolicina,int temp,double pritisak,int vlaznost,int brzina)
        {
            this.NazivDrzave = naziv;
            this.DatumUTC = datum;
            this.KolicinaEnergije = kolicina;
            this.Temperatura = temp;
            this.Pritisak = pritisak;
            this.VlaznostVazduha = vlaznost;
            this.BrzinaVetra = brzina;
        }
    }
}
