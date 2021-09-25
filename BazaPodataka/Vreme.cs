namespace BazaPodataka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vreme")]
    public partial class Vreme
    {
        public int Id { get; set; }

        public int DrzavaId { get; set; }

        public int Temperatura { get; set; }

        public double AtmosferskiPritisak { get; set; }

        public int VlaznostVazduha { get; set; }

        public int BrzinaVetra { get; set; }
        
        public DateTime DatumUTC { get; set; }

        public virtual Drzava Drzava { get; set; }
    }
}
