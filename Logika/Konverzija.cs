using Modeli;
using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public class Konverzija : IKonverzija
    {
        public IEnumerable<PodaciZaPrikaz> ModeliZaPrikaz(IEnumerable<DrzavaWeb> drzave)
        {
            if (drzave == null)
            {
                ILogPisanje logPisanje = new LogPisanje();
                logPisanje.AddLog(new LogPodatak()
                {
                    LogTime = DateTime.Now,
                    Message = String.Format("Greska prilikom konverzije"
                        ),
                    Type = LOG_TYPE.ERROR
                });
                throw new Exception("Lista ne sme biti null");
            }

            List<PodaciZaPrikaz> podaci = new List<PodaciZaPrikaz>();

            foreach (var d in drzave)
            {
                List<PodaciZaPrikaz> jednaDrzava = new List<PodaciZaPrikaz>();

                foreach (var v in d.Vremena)
                {
                    PodaciZaPrikaz pzp = new PodaciZaPrikaz()
                    {
                        BrzinaVetra = v.BrzinaVetra,
                        DatumUTC = v.DatumUTC,
                        NazivDrzave = d.Naziv,
                        Pritisak = v.AtmosferskiPritisak,
                        Temperatura = v.Temperatura,
                        VlaznostVazduha = v.VlaznostVazduha,
                        KolicinaEnergije = null
                    };

                    jednaDrzava.Add(pzp);
                }

                List<PodaciZaPrikaz> drzavaPotrosnje = new List<PodaciZaPrikaz>();

                foreach (var p in d.Potrosnje)
                {
                    var indx = jednaDrzava.FindIndex(x => x.DatumUTC.Equals(p.DatumUTC));
                    // ako postoje podaci koji se poklapaju po datumu

                    if(indx != -1)
                    {
                        jednaDrzava[indx].KolicinaEnergije = p.Kolicina;
                    }
                    else
                    {
                        PodaciZaPrikaz pzp = new PodaciZaPrikaz()
                        {
                            KolicinaEnergije = p.Kolicina,
                            DatumUTC = p.DatumUTC,
                            NazivDrzave = d.Naziv,
                            BrzinaVetra = null,
                            Pritisak = null,
                            Temperatura = null,
                            VlaznostVazduha = null
                        };

                        drzavaPotrosnje.Add(pzp);
                    }
                }

                jednaDrzava.AddRange(drzavaPotrosnje);
                podaci.AddRange(jednaDrzava);
            }

            return podaci;
        }

    }
}
