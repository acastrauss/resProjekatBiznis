using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli;
using Modeli.WebModeli;

namespace DatabaseAccess
{
    public interface IBPCRUD
    {
        int IdZaDrzavu(string ime);
        bool DrzavaPostoji(string ime);
        void DodajDrzave(IEnumerable<DrzavaWeb> drzave);
        void DodajVremeDrzave(IEnumerable<VremeWeb> vremena, String imeDrzave);
        void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrsonje, String imeDrzave);
        DrzavaWeb DrzavaPoImenu(String imeDrzave);
        IEnumerable<PotrsonjaWeb> PotrosnjaPoImenu(String imeDrzave);
        IEnumerable<VremeWeb> VremePoImenu(String imeDrzave);
        IEnumerable<DrzavaWeb> SveDrzave();
        DrzavaWeb DrzavaPoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, String imeDrzave);
        IEnumerable<PotrsonjaWeb> PotrosnjaPoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, String imeDrzave);
        IEnumerable<VremeWeb> VremePoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, String imeDrzave);
        String KratkoImeDrzave(String punoImeDrzave);
        String PunoImeDrzave(String kratkoImeDrzave);
        DrzavaWeb BPuWebDrzava(Drzava drzava);
        Drzava WebuBPDrzava(DrzavaWeb drzava);
        VremeWeb BPuWebVreme(Vreme drzava);
        Vreme WebuBPVreme(VremeWeb drzava);
        PotrsonjaWeb BPuWebPotrosnja(Potrosnja drzava);
        Potrosnja WebuBPPotrosnja(PotrsonjaWeb drzava);

    }
}
