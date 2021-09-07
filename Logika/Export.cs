using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modeli.WebModeli;

namespace Logika
{
    public class Export : IExport
    {
        public string SaveData(IEnumerable<PodaciZaPrikaz> podaci)
        {
            var putanja = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            var arr = putanja.Split('\\').ToList();
            arr.Remove(arr.Last());
            arr.Remove(arr.First());
            var p = String.Join("\\", arr);
            string a = "Output_" + DateTime.Now.ToString("yyy_MM_d_HH_mm") + ".csv";
            string putanjaPuna = System.IO.Path.Combine(p, "CSVFiles", a);
            //putanjaPuna = putanjaPuna.Substring(6, putanjaPuna.Length - 6);
            
            using (var csvWrite = File.CreateText(putanjaPuna))
            {
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

                    csvWrite.WriteLine(vrsta);
                    vrsta = "";
                }
            }

            return putanjaPuna;

        }
    }
}
