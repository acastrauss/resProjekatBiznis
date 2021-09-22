using BazaPodataka;
using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public interface IImport
    {
       void Load(string fajl,List<string> fajlovi, string drzava, DateTime DatumPocetka, DateTime DatumKraja);
       void LoadVreme(string fajl, string drzava, DateTime DatumPocetka, DateTime DatumKraja);
       void LoadPotrosnja(string fajl, string drzava,DateTime DatumPocetka,DateTime DatumKraja);
    }
}
