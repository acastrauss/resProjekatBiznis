using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeli.WebModeli
{
    public class VremeWeb
    {
        public int Id { get; set; }
        public int DrzavaId { get; set; }
        public int Temperatura { get; set; }
        public double AtmosferskiPritisak { get; set; }
        public int VlaznostVazduha { get; set; }
        public int BrzinaVetra { get; set; }
        public DateTime DatumUTC { get; set; }

        public VremeWeb() { }
        public VremeWeb(int id,int drzavaid,int temp,double pritisak,int vlaznost,int brzina,DateTime datum)
        {
            this.Id = id;
            this.DrzavaId = drzavaid;
            this.Temperatura = temp;
            this.AtmosferskiPritisak = pritisak;
            this.VlaznostVazduha = vlaznost;
            this.BrzinaVetra = brzina;
            this.DatumUTC = datum;

        }

    }
}
