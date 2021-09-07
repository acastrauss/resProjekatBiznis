using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BazaPodataka
{
    public partial class Drzave : DbContext
    {
        public Drzave()
            : base("name=Drzave")
        {
        }

        public virtual DbSet<Drzava> Drzavas { get; set; }
        public virtual DbSet<KratkiNaziviDrzava> KratkiNaziviDrzavas { get; set; }
        public virtual DbSet<Potrosnja> Potrosnjas { get; set; }
        public virtual DbSet<Vreme> Vremes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drzava>()
                .Property(e => e.Naziv)
                .IsFixedLength();

            modelBuilder.Entity<Drzava>()
                .Property(e => e.KratakNaziv)
                .IsFixedLength();

            modelBuilder.Entity<Drzava>()
                .HasMany(e => e.Potrosnjas)
                .WithRequired(e => e.Drzava)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Drzava>()
                .HasMany(e => e.Vremes)
                .WithRequired(e => e.Drzava)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KratkiNaziviDrzava>()
                .Property(e => e.PunNaziv)
                .IsFixedLength();

            modelBuilder.Entity<KratkiNaziviDrzava>()
                .Property(e => e.KratakNaziv)
                .IsFixedLength();
        }
    }
}
