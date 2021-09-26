using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli.WebModeli;

namespace Logika
{
    /// <summary>
    /// Konverzija podataka iz web modela u model za prikaz na UI-u
    /// </summary>
    public interface IKonverzija
    {
        IEnumerable<PodaciZaPrikaz> ModeliZaPrikaz(IEnumerable<DrzavaWeb> drzave);
    }
}
