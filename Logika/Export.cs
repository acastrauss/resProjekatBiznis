using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Modeli;
using Modeli.WebModeli;

namespace Logika
{
    public class Export : IExport
    {
        public string SaveData(IEnumerable<PodaciZaPrikaz> podaci)
        {
            if (podaci == null)
            {
                ILogPisanje logPisanje = new LogPisanje();
                logPisanje.AddLog(new LogPodatak()
                {
                    LogTime = DateTime.Now,
                    Message = String.Format("Greska prilikom eksporta, nisu prosledjeni podaci"
                        ),
                    Type = LOG_TYPE.ERROR
                });
                throw new Exception("Lista ne moze biti prazna niti null");
            }
            if (podaci.Count() == 0)
            {
                ILogPisanje logPisanje = new LogPisanje();
                logPisanje.AddLog(new LogPodatak()
                {
                    LogTime = DateTime.Now,
                    Message = String.Format("Greska prilikom eksporta, nisu prosledjeni podaci"
                        ),
                    Type = LOG_TYPE.ERROR
                });
                throw new Exception("Lista ne moze biti prazna niti null");
            }

            String putanjaPuna = String.Empty;

            if (HttpContext.Current == null)
            {
                var putanja = Directory.GetCurrentDirectory();//System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                var arr = putanja.Split('\\').ToList();
                arr.Remove(arr.Last());
                arr.Remove(arr.First());
                var p = String.Join("\\", arr);
                string a = "Output_" + DateTime.Now.ToString("yyy_MM_d_HHmm") + ".csv";
                putanjaPuna = System.IO.Path.Combine(putanja, "WebApplication1\\CSVFiles", a);
            }
            else
            {
                string a = "Output" + DateTime.Now.ToString("yyy_MM_d_HH_mm") + ".csv";

                putanjaPuna = HttpContext.Current.Server.MapPath("~/CSVFiles");
                putanjaPuna = Path.Combine(putanjaPuna, a);
            }

            foreach (var red in podaci)
                {
                    string vrsta = "";

                    if (red.NazivDrzave != null)//prikazaneKolone.Contains("Drzava"))
                    {
                        string NazivDrzave = red.NazivDrzave;
                        vrsta += NazivDrzave + ",";
                    }
                    if (red.DatumUTC != null)//prikazaneKolone.Contains("UTC vreme"))
                    {
                        string Datum  = red.DatumUTC.ToString();
                        vrsta += Datum + ",";
                    }
                    if (red.KolicinaEnergije != null)//prikazaneKolone.Contains("Potrosnja"))
                    {
                        string KolicinaEnergije = red.KolicinaEnergije.ToString();
                        vrsta += KolicinaEnergije + ",";
                    }
                    if (red.Temperatura != null)//prikazaneKolone.Contains("Temperatura"))
                    {
                        string Temperatura = red.Temperatura.ToString();
                        vrsta += Temperatura + ",";
                    }
                    if (red.Pritisak != null)//prikazaneKolone.Contains("Pritisak"))
                    {
                        string Pritisak = red.Pritisak.ToString();
                        vrsta += Pritisak + ",";
                    }
                    if (red.VlaznostVazduha != null)//prikazaneKolone.Contains("Vlaznost"))
                    {
                        string VlaznostVazduha = red.VlaznostVazduha.ToString();
                        vrsta += VlaznostVazduha + ",";
                    }
                    if (red.BrzinaVetra != null)//prikazaneKolone.Contains("Brzina vetra"))
                    {
                        string BrzinaVetra = red.BrzinaVetra.ToString();
                        vrsta += BrzinaVetra + ",";
                    }
                    
                    vrsta += "\n";
                    File.AppendAllText(putanjaPuna, vrsta);

                    vrsta = "";
                }
            //}

            return putanjaPuna;

        }
    }
}
