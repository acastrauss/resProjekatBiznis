using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class FilterDatum : IFilterDatum
    {
        public IEnumerable<PodaciZaPrikaz> FiltrirajPoDatumu(IEnumerable<PodaciZaPrikaz> podaci, DateTime pocetni, DateTime krajnji)
        {
            var povratna = new List<PodaciZaPrikaz>();

            foreach (var podatak in podaci)
            {
                if (
                    DateTime.Compare((DateTime)podatak.DatumUTC, pocetni) >= 0 &&
                    DateTime.Compare((DateTime)podatak.DatumUTC, krajnji) <= 0 
                    )
                {
                    povratna.Add(podatak);
                }
            }

            return povratna;
        }
    }
}
