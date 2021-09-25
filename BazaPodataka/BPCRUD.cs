﻿using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BazaPodataka
{
    public class BPCRUD : IBPCRUD
    {

        public void DodajDrzave(IEnumerable<DrzavaWeb> drzave)
        {
            using (var db = new Drzave())
            {
                foreach (var drzava in drzave.ToList())
                {
                    db.Drzavas.Add(WebuBPDrzava(drzava));
                }

                db.SaveChanges();
            }
        }

        public void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrsonje, string imeDrzave)
        {
            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                db.Configuration.AutoDetectChangesEnabled = false;
                
                foreach (var p in potrsonje)
                {
                    var dbModel = WebuBPPotrosnja(p, id);
                        
                    if(dbModel.DatumUTC == null || dbModel.DatumUTC.Date.Equals(new DateTime(1, 1, 1).Date))
                    {
                        continue;
                    }

                    dbModel.DrzavaId = id;
                    db.Potrosnjas.Add(dbModel);
                }

                db.SaveChanges();

                db.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public void DodajVremeDrzave(IEnumerable<VremeWeb> vremena, string imeDrzave)
        {
            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                db.Configuration.AutoDetectChangesEnabled = false;

                foreach (var v in vremena)
                {
                    var dbModel = WebuBPVreme(v, id);
                    dbModel.DrzavaId = id;
                    db.Vremes.Add(dbModel);
                }

                db.SaveChanges();
                db.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        public DrzavaWeb DrzavaPoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, string imeDrzave)
        {
            var drzava = new DrzavaWeb();

            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                db.Potrosnjas.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum).ToList().ForEach(
                    x => drzava.Potrosnje.Add(this.BPuWebPotrosnja(x)));

                db.Vremes.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum).ToList().ForEach(
                    x => drzava.Vremena.Add(this.BPuWebVreme(x)));

                db.SaveChanges();
            }

            return drzava;
        }

        public DrzavaWeb DrzavaPoImenu(string imeDrzave)
        {
            var drzava = new DrzavaWeb();

            int id = IdZaDrzavu(imeDrzave); 
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                drzava = BPuWebDrzava(db.Drzavas.Where(x => x.Id == id).First());
            }

            return drzava;
        }

        public bool DrzavaPostoji(string name)
        {
            var drzava = new DrzavaWeb();

            int id = IdZaDrzavu(name);

            return id != -1;
        }

        public int IdZaDrzavu(string ime)
        {
            int retVal = -1;

            ime = ime.Trim();

            using (var db = new Drzave())
            {
                var dbModel = db.Drzavas.Where(x => x.Naziv.Trim().Equals(ime)).ToList();

                if (dbModel.Count != 0)
                    retVal = dbModel.First().Id;
            }

            return retVal;
        }

        public string KratkoImeDrzave(string punoImeDrzave)
        {
            var retVal = String.Empty;

            using (var db = new Drzave())
            {
                retVal = db.KratkiNaziviDrzavas.Where(x => x.PunNaziv == punoImeDrzave).FirstOrDefault().KratakNaziv;
            }

            if (String.IsNullOrEmpty(retVal))
                throw new Exception("Nema kratkog naziva drzave.");

            return retVal;
        }

        public IEnumerable<PotrsonjaWeb> PotrosnjaPoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, string imeDrzave)
        {
            var retVal = new List<PotrsonjaWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Potrosnjas.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum)
                    .Select(x => BPuWebPotrosnja(x)));

                db.SaveChanges();
            }

            return retVal;
        }

        public IEnumerable<PotrsonjaWeb> PotrosnjaPoImenu(string imeDrzave)
        {
            var retVal = new List<PotrsonjaWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Potrosnjas.Where(x => x.DrzavaId == id)
                    .Select(x => BPuWebPotrosnja(x)));

                db.SaveChanges();
            }

            return retVal;
        }

        public string PunoImeDrzave(string kratkoImeDrzave)
        {
            var retVal = String.Empty;

            using (var db = new Drzave())
            {
                retVal = db.KratkiNaziviDrzavas.Where(x => x.KratakNaziv == kratkoImeDrzave).FirstOrDefault().PunNaziv;
            }

            if (String.IsNullOrEmpty(retVal))
                throw new Exception("Nema kratkog naziva drzave.");

            return retVal;
        }

        public IEnumerable<DrzavaWeb> SveDrzave()
        {
            var retVal = new List<DrzavaWeb>();

            using (var db = new Drzave())
            {
                foreach (var d in db.Drzavas)
                {
                    var webD = BPuWebDrzava(d);
                    retVal.Add(webD);
                }

                db.SaveChanges();
            }

            return retVal;
        }

        public IEnumerable<VremeWeb> VremePoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, string imeDrzave)
        {
            var retVal = new List<VremeWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Vremes.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum)
                    .Select(x => BPuWebVreme(x)));

                db.SaveChanges();
            }

            return retVal;
        }

        public IEnumerable<VremeWeb> VremePoImenu(string imeDrzave)
        {
            var retVal = new List<VremeWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Vremes.Where(x => x.DrzavaId == id)
                    .Select(x => BPuWebVreme(x)));

                db.SaveChanges();
            }

            return retVal;
        }


        public IEnumerable<string> NaziviDrzava()
        {
            List<String> nazivi = new List<string>();

            using (var db = new Drzave())
            {
                db.Drzavas.ToList().ForEach(x => nazivi.Add(x.Naziv));

                db.SaveChanges();
            }

            return nazivi;
        }

        public DrzavaWeb BPuWebDrzava(Drzava drzava)
        {
            return new DrzavaWeb()
            {
                Naziv = drzava.Naziv,
                KratakNaziv = drzava.KratakNaziv,
                Potrosnje = drzava.Potrosnjas.Select(x => BPuWebPotrosnja(x)).ToList(),
                Vremena = drzava.Vremes.Select(x => BPuWebVreme(x)).ToList()
            };
        }

        public PotrsonjaWeb BPuWebPotrosnja(Potrosnja drzava)
        {
            return new PotrsonjaWeb()
            {
                DatumUTC = (DateTime)drzava.DatumUTC,
                Kolicina = drzava.Kolicina
            };
        }

        public VremeWeb BPuWebVreme(Vreme drzava)
        {
            return new VremeWeb()
            {
                AtmosferskiPritisak = drzava.AtmosferskiPritisak,
                BrzinaVetra = drzava.BrzinaVetra,
                DatumUTC = (DateTime)drzava.DatumUTC,
                Temperatura = drzava.Temperatura,
                VlaznostVazduha = drzava.VlaznostVazduha
            };
        }

        public Drzava WebuBPDrzava(DrzavaWeb drzava)
        {
            var id = IdZaDrzavu(drzava.Naziv);

            return new Drzava()
            {
                Id = id,
                KratakNaziv = drzava.KratakNaziv,
                Naziv = drzava.Naziv,
                Potrosnjas = drzava.Potrosnje.Select(x => this.WebuBPPotrosnja(x, id)).ToList(),
                Vremes = drzava.Vremena.Select(x => this.WebuBPVreme(x, id)).ToList()
            };
        }

        public Potrosnja WebuBPPotrosnja(PotrsonjaWeb drzava, int idDrzava)
        {
            return new Potrosnja()
            {
                DrzavaId = idDrzava,
                DatumUTC = drzava.DatumUTC,
                Kolicina = drzava.Kolicina
            };
        }

        public Vreme WebuBPVreme(VremeWeb drzava, int idDrzava)
        {
            return new Vreme()
            {
                DrzavaId = idDrzava,
                AtmosferskiPritisak = drzava.AtmosferskiPritisak,
                BrzinaVetra = drzava.BrzinaVetra,
                DatumUTC = drzava.DatumUTC,
                VlaznostVazduha = drzava.VlaznostVazduha,
                Temperatura = drzava.Temperatura
            };
        }
    }
}
