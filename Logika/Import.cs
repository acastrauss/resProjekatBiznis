using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BazaPodataka;
using Modeli.WebModeli;
using Microsoft.VisualBasic.FileIO;
using ExcelDataReader;
using System.IO;
using System.Data;

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
                throw new Exception("Fajl ne sme biti null");

            if (String.IsNullOrEmpty(drzava))
                throw new Exception("Drzava ne sme biti null");

            IBPPristup bpcrud = new BPPristup();
                
            String stateName = String.Empty;
            List<PotrsonjaWeb> potrosnje = new List<PotrsonjaWeb>();

            using (var stream = File.Open(fajl, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = fajl.EndsWith(".xls") ? 
                    ExcelReaderFactory.CreateReader(stream) :
                    ExcelReaderFactory.CreateOpenXmlReader(stream)
                    )
                {
                        
                    int it = 0;
                    var kratakNaziv = bpcrud.KratakNazivDrzave(drzava);

                    if(reader != null)
                    {
                        DataSet content = reader.AsDataSet();

                        foreach (DataTable tbl in content.Tables)
                        {
                            foreach (DataRow r in tbl.Rows)
                            {
                                if (it++ == 0) continue;

                                var itc = 0;
                                var potr = new PotrsonjaWeb();
                                bool add = true;

                                foreach (DataColumn c in tbl.Columns)
                                {
                                    try
                                    {
                                        if (itc == 1)
                                        {
                                            potr.DatumUTC = DateTime.Parse(r[c].ToString());
                                            if (potr.DatumUTC < DatumPocetka || potr.DatumUTC > DatumKraja)
                                            {
                                                add = false;
                                                break;
                                            }
                                        }
                                        else if (itc == 5)
                                        {
                                            var code = (string)r[c];
                                            if (!code.Equals(kratakNaziv))
                                            {
                                                add = false;
                                                break;
                                            }
                                        }
                                        else if (itc == 7)
                                        {
                                            potr.Kolicina = float.Parse(r[c].ToString());
                                        }

                                        itc++;

                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }

                                if(add)
                                    potrosnje.Add(potr);
                            }
                        }
                    }

                    reader.Close();

                }    

                if (potrosnje.Count == 0)
                    throw new Exception("Funkcija nije odradila posao");

                bpcrud.DodajPotrosnjuDrzave(potrosnje, drzava);
            }
        }

        public void LoadVreme(string fajl, string drzava, DateTime DatumPocetka, DateTime DatumKraja)
        {
            if (String.IsNullOrEmpty(fajl))
                throw new Exception("Fajl ne sme biti null");

            if (String.IsNullOrEmpty(drzava))
                throw new Exception("Drzava ne sme biti null");

            IBPPristup bpcrud = new BPPristup();

            using (TextFieldParser csvParser = new TextFieldParser(fajl))
            {
                csvParser.SetDelimiters(new string[] { ";" });
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine();
                List<VremeWeb> vremena = new List<VremeWeb>();

                while (!csvParser.EndOfData)
                {
                    double pritisak;
                    double temperatura;
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
                    vreme.Temperatura = double.TryParse(fields[1], out temperatura) ? (int)temperatura : 0;
                    vreme.AtmosferskiPritisak = double.TryParse(fields[2], out pritisak) ? pritisak : 0;
                    vreme.VlaznostVazduha = int.TryParse(fields[4], out vlaznost) ? vlaznost : 0;
                    vreme.BrzinaVetra = int.TryParse(fields[6], out brzinaVetra) ? brzinaVetra : 0;
                    vremena.Add(vreme);
                }

                if (vremena.Count == 0)
                    throw new Exception("Funkcija nije odradila posao");

                bpcrud.DodajVremenaDrzave(vremena, drzava);
            }
        }
    }
}
