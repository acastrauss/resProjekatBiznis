using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
using Modeli;
using System.Diagnostics.CodeAnalysis;

namespace Logika
{
    [ExcludeFromCodeCoverage]
    public class BPPristup : IBPPristup
    {
        public void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrosnje, string drzava)
        {
            IBPCRUD bp = new BPCRUD();

            bp.DodajPotrosnjuDrzave(potrosnje, drzava);

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Dodato {0} entiteta podataka o potrosnji za drzavu:{1}",
                    potrosnje.Count(), drzava),
                Type = LOG_TYPE.INFO
            });
        }

        public void DodajVremenaDrzave(IEnumerable<VremeWeb> vremena, string drzava)
        {
            IBPCRUD bp = new BPCRUD();
            bp.DodajVremeDrzave(vremena, drzava);

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Dodato {0} entiteta podataka o vremenu za drzavu:{1}",
                    vremena.Count(), drzava),
                Type = LOG_TYPE.INFO
            });
        }

        public string PunoImeDrzave(string kratakNaziv)
        {
            IBPCRUD bp = new BPCRUD();

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Zatrazeno puno ime drzave za kratak naziv:{0}", kratakNaziv
                    ),
                Type = LOG_TYPE.INFO
            });

            return bp.PunoImeDrzave(kratakNaziv);
        }

        public string KratakNazivDrzave(string punNaziv)
        {
            IBPCRUD bp = new BPCRUD();

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Zatrazeno kratko ime drzave za pun naziv:{0}", punNaziv
                    ),
                Type = LOG_TYPE.INFO
            });

            return bp.KratkoImeDrzave(punNaziv);
        }

        public DrzavaWeb DrzavaPoImenu(string naziv)
        {
            IBPCRUD bp = new BPCRUD();

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Zatrazeni podaci za drzavu: {0}", naziv
                    ),
                Type = LOG_TYPE.INFO
            });

            return bp.DrzavaPoImenu(naziv);
        }

        public IEnumerable<string> NaziviDrzava()
        {
            IBPCRUD bp = new BPCRUD();

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Zatrazeni svi nazivi drzava."
                    ),
                Type = LOG_TYPE.INFO
            });

            return bp.NaziviDrzava();
        }

        public IEnumerable<DrzavaWeb> SveDrzave()
        {
            IBPCRUD bp = new BPCRUD();

            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Zatrazeni svi podaci za drzave."
                    ),
                Type = LOG_TYPE.INFO
            });

            return bp.SveDrzave();
        }

        public void DodajDrzavu(string naziv, string kratakNaziv)
        {
            IBPCRUD bp = new BPCRUD();
            ILogPisanje logPisanje = new LogPisanje();
            logPisanje.AddLog(new LogPodatak()
            {
                LogTime = DateTime.Now,
                Message = String.Format("Dodata drzava:{0}", naziv
                    ),
                Type = LOG_TYPE.INFO
            });

            bp.DodajDrzavu(naziv, kratakNaziv);
        }
    }
}
