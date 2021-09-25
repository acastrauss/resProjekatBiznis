using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
using Modeli;

namespace Logika
{
    public class BPPristup : IBPPristup
    {
        public void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrosnje, string drzava)
        {
            IBPCRUD bp = new BPCRUD();

            bp.DodajPotrosnjuDrzave(potrosnje, drzava);
        }

        public void DodajVremenaDrzave(IEnumerable<VremeWeb> vremena, string drzava)
        {
            IBPCRUD bp = new BPCRUD();
            bp.DodajVremeDrzave(vremena, drzava);
        }

        public string PunoImeDrzave(string kratakNaziv)
        {
            IBPCRUD bp = new BPCRUD();
            return bp.PunoImeDrzave(kratakNaziv);
        }

        public string KratakNazivDrzave(string punNaziv)
        {
            IBPCRUD bp = new BPCRUD();
            return bp.KratkoImeDrzave(punNaziv);
        }

        public DrzavaWeb DrzavaPoImenu(string naziv)
        {
            IBPCRUD bp = new BPCRUD();

            LogPisanje.AddLog(new LogPodatak(
                LOG_TYPE.INFO, String.Format("Zatrazeni podaci iz baze o drzavi sa nazivom: {0}.", naziv), DateTime.Now));

            return bp.DrzavaPoImenu(naziv);
        }

        public IEnumerable<string> NaziviDrzava()
        {
            IBPCRUD bp = new BPCRUD();

            LogPisanje.AddLog(new LogPodatak(
                LOG_TYPE.INFO, String.Format("Zatrazeni svi nazivi drzava iz baze."), DateTime.Now));

            return bp.NaziviDrzava();
        }

        public IEnumerable<DrzavaWeb> SveDrzave()
        {
            IBPCRUD bp = new BPCRUD();

            LogPisanje.AddLog(new LogPodatak(
                LOG_TYPE.INFO, String.Format("Zatrazeni svi podaci drzava iz baze."), DateTime.Now));

            return bp.SveDrzave();
        }
    }
}
