using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public interface IExport
    {
        string SaveData(IEnumerable<PodaciZaPrikaz> podaci);
    }
}
