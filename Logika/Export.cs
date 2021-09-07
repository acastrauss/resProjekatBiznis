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
        public string SaveData(IEnumerable<PodaciZaPrikaz> podaci, string[] prikazaneKolone)
        {           
            var putanja = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); 
            string a = "Output_" + DateTime.Now.ToString("yyy_MM_d_HH_mm") + ".csv";
            string[] kombinovano = { @putanja, a };
            string putanjaPuna = System.IO.Path.Combine(kombinovano);
            putanjaPuna = putanjaPuna.Substring(6, putanjaPuna.Length - 6);

            using (var csvWrite = File.CreateText(putanjaPuna))
            {
                foreach (var red in podaci)
                {
                    string vrsta = "";

                    if (prikazaneKolone.Contains("Drzava"))
                    {
                        string NazivDrzave = red.NazivDrzave;
                        vrsta += NazivDrzave + ",";
                    }
                    if (prikazaneKolone.Contains("UTC vreme"))
                    {
                        string Datum  = red.DatumUTC.ToString();
                        vrsta += Datum + ",";
                    }
                    if (prikazaneKolone.Contains("Potrosnja"))
                    {
                        string KolicinaEnergije = red.KolicinaEnergije.ToString();
                        vrsta += KolicinaEnergije + ",";
                    }
                    if (prikazaneKolone.Contains("Temperatura"))
                    {
                        string Temperatura = red.Temperatura.ToString();
                        vrsta += Temperatura + ",";
                    }
                    if (prikazaneKolone.Contains("Pritisak"))
                    {
                        string Pritisak = red.Pritisak.ToString();
                        vrsta += Pritisak + ",";
                    }
                    if (prikazaneKolone.Contains("Vlaznost"))
                    {
                        string VlaznostVazduha = red.VlaznostVazduha.ToString();
                        vrsta += VlaznostVazduha + ",";
                    }
                    if (prikazaneKolone.Contains("Brzina vetra"))
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
