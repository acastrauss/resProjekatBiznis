using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli.WebModeli;

namespace Logika
{
    public interface IFilterDatum
    {
        IEnumerable<PodaciZaPrikaz> FiltrirajPoDatumu(IEnumerable<PodaciZaPrikaz> podaci, DateTime pocetni, DateTime krajnji);
    }
}
