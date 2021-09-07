using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;

namespace Logika
{
    public class BPPristup : IBPPristup
    {

        public IEnumerable<DrzavaWeb> SveDrzave()
        {
            IBPCRUD bp = new BPCRUD();

            return bp.SveDrzave();
        }
    }
}
