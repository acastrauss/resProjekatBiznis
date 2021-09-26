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
        void DodajDrzavu(String naziv, String kratakNaziv);
        IEnumerable<DrzavaWeb> SveDrzave();
        IEnumerable<String> NaziviDrzava();

        DrzavaWeb DrzavaPoImenu(String naziv);

        String PunoImeDrzave(String kratakNaziv);
        void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrosnje, String drzava);
        void DodajVremenaDrzave(IEnumerable<VremeWeb> vremena, String drzava);
        string KratakNazivDrzave(string punNaziv);


    }
}
