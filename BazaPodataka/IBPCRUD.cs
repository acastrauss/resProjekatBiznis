using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli;
using Modeli.WebModeli;

namespace BazaPodataka
{
    public interface IBPCRUD
    {
        void DodajDrzavu(String naziv, String kratakNaziv);
        int IdZaDrzavu(string ime);
        void DodajVremeDrzave(IEnumerable<VremeWeb> vremena, String imeDrzave);
        void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrsonje, String imeDrzave);
        DrzavaWeb DrzavaPoImenu(String imeDrzave);
        IEnumerable<DrzavaWeb> SveDrzave();
        String KratkoImeDrzave(String punoImeDrzave);
        String PunoImeDrzave(String kratkoImeDrzave);
        DrzavaWeb BPuWebDrzava(Drzava drzava);
        Drzava WebuBPDrzava(DrzavaWeb drzava);
        VremeWeb BPuWebVreme(Vreme drzava);
        Vreme WebuBPVreme(VremeWeb drzava, int idDrzava);
        PotrsonjaWeb BPuWebPotrosnja(Potrosnja drzava);
        Potrosnja WebuBPPotrosnja(PotrsonjaWeb drzava, int idDrzava);
        IEnumerable<String> NaziviDrzava();
    }
}
