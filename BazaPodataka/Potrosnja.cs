namespace BazaPodataka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Potrosnja")]
    public partial class Potrosnja
    {
        public int Id { get; set; }

        public int DrzavaId { get; set; }

        public double Kolicina { get; set; }

        public DateTime DatumUTC { get; set; }

        public virtual Drzava Drzava { get; set; }
    }
}
