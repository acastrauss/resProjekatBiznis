namespace BazaPodataka
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Drzava")]
    public partial class Drzava
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Drzava()
        {
            Potrosnjas = new HashSet<Potrosnja>();
            Vremes = new HashSet<Vreme>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(2)]
        public string KratakNaziv { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Potrosnja> Potrosnjas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vreme> Vremes { get; set; }
    }
}
