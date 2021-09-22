using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
using Modeli.WebModeli;
using Microsoft.VisualBasic.FileIO;

namespace Logika
{
    public class Import : IImport
    {

        public void Load(string fajl ,List<string> fajlovi, string drzava, DateTime DatumPocetka, DateTime DatumKraja)
        {
            LoadPotrosnja(fajl, drzava, DatumPocetka, DatumKraja);

            foreach(string s in fajlovi)
            {
                LoadVreme(s, drzava, DatumPocetka, DatumKraja);
            }


        }

        public void LoadPotrosnja(string fajl, string drzava, DateTime DatumPocetka, DateTime DatumKraja)
        {
            if (String.IsNullOrEmpty(fajl))
                return;

            BPCRUD bpcrud = new BPCRUD();
            using(TextFieldParser csvParser = new TextFieldParser(fajl))
            {
                String stateName = String.Empty;
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();
                List<PotrsonjaWeb> potrosnje = new List<PotrsonjaWeb>();
                while (!csvParser.EndOfData)
                {
                    PotrsonjaWeb potrosnja = new PotrsonjaWeb();
                    double vrednost;
                    string[] fields = csvParser.ReadFields();

                    string kod = fields[5];
                    string punNaziv = bpcrud.PunoImeDrzave(kod);
                    if (punNaziv != drzava)
                        continue;
                    potrosnja.DatumUTC  = DateTime.ParseExact(fields[2], "M/d/yyyy", null);
                    if (potrosnja.DatumUTC < DatumPocetka || potrosnja.DatumUTC >= DatumKraja)
                        continue;
                    potrosnja.Kolicina = double.TryParse(fields[7], out vrednost) ? vrednost : 0;
                    potrosnje.Add(potrosnja);
                }
                bpcrud.DodajPotrosnjuDrzave(potrosnje, drzava);
            }
        }

        public void LoadVreme(string fajl, string drzava, DateTime DatumPocetka, DateTime DatumKraja)
        {
            BPCRUD bpcrud = new BPCRUD();
            using (TextFieldParser csvParser = new TextFieldParser(fajl))
            {
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();
                List<VremeWeb> vremena = new List<VremeWeb>();

                while (!csvParser.EndOfData)
                {
                    double pritisak;
                    int temperatura;
                    int vlaznost;
                    int brzinaVetra;
                    VremeWeb vreme = new VremeWeb();
                    string[] fields = new string[0];

                    try
                    {
                        fields = csvParser.ReadFields();
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if(fields[0].Contains("Local") || fields[0].StartsWith("#") || String.IsNullOrEmpty(fields[0]))               
                       continue;
                        vreme.DatumUTC = DateTime.ParseExact(fields[0], "dd.MM.yyyy HH:mm", null);
                    if(vreme.DatumUTC <= DatumPocetka || vreme.DatumUTC >= DatumKraja)
                        continue;
                    vreme.Temperatura = int.TryParse(fields[1], out temperatura) ? temperatura : 0;
                    vreme.AtmosferskiPritisak = double.TryParse(fields[2], out pritisak) ? pritisak : 0;
                    vreme.VlaznostVazduha = int.TryParse(fields[4], out vlaznost) ? vlaznost : 0;
                    vreme.BrzinaVetra = int.TryParse(fields[6], out brzinaVetra) ? brzinaVetra : 0;
                    vremena.Add(vreme);
                }
                bpcrud.DodajVremeDrzave(vremena, drzava);
            }
        }
    }
}
