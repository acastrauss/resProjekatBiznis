using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeli.WebModeli
{
    public class PotrsonjaWeb
    {
        #region Polja
        public int Id { get; set; }
        public int DrzavaId { get; set; }
        public double Kolicina { get; set; }
        public DateTime DatumUTC { get; set; }

        #endregion

        public PotrsonjaWeb() { }

        public PotrsonjaWeb(int id,int drzavaid,double kolicina,DateTime datum)
        {
            this.Id = id;
            this.DrzavaId = drzavaid;
            this.Kolicina = kolicina;
            this.DatumUTC = datum;
        }




    }
}
