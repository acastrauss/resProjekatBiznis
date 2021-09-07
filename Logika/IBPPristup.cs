using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli.WebModeli;

namespace Logika
{
    public interface IBPPristup
    {
        IEnumerable<DrzavaWeb> SveDrzave();
        IEnumerable<String> NaziviDrzava();

        DrzavaWeb DrzavaPoImenu(String naziv);
    }
}
