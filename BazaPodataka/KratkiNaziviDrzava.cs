namespace BazaPodataka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KratkiNaziviDrzava")]
    public partial class KratkiNaziviDrzava
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string PunNaziv { get; set; }

        [Required]
        [StringLength(2)]
        public string KratakNaziv { get; set; }
    }
}
