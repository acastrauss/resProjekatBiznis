using Modeli.WebModeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class BPCRUD : IBPCRUD
    {
        public DrzavaWeb BPuWebDrzava(Drzava drzava)
        {
            throw new NotImplementedException();
        }

        public PotrsonjaWeb BPuWebPotrosnja(Potrosnja drzava)
        {
            throw new NotImplementedException();
        }

        public VremeWeb BPuWebVreme(Vreme drzava)
        {
            throw new NotImplementedException();
        }

        public void DodajDrzave(IEnumerable<DrzavaWeb> drzave)
        {
            using (var db = new Drzave())
            {
                foreach (var drzava in drzave.ToList())
                {
                    // proveri dal postoji i dal je validna

                    db.Drzavas.Add(WebuBPDrzava(drzava));
                }
            }
        }

        public void DodajPotrosnjuDrzave(IEnumerable<PotrsonjaWeb> potrsonje, string imeDrzave)
        {
            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                foreach (var p in potrsonje)
                {
                    var dbModel = WebuBPPotrosnja(p);
                    dbModel.Id = id;
                    db.Potrosnjas.Add(dbModel);
                }
            }
        }

        public void DodajVremeDrzave(IEnumerable<VremeWeb> vremena, string imeDrzave)
        {
            int id = IdZaDrzavu(imeDrzave);
            if (id == -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                foreach (var v in vremena)
                {
                    var dbModel = WebuBPVreme(v);
                    dbModel.Id = id;
                    db.Vremes.Add(dbModel);
                }
            }
        }

        public DrzavaWeb DrzavaPoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, string imeDrzave)
        {
            var drzava = new DrzavaWeb();

            int id = IdZaDrzavu(imeDrzave);
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                db.Potrosnjas.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum).ToList().ForEach(
                    x => drzava.Potrosnje.Add(this.BPuWebPotrosnja(x)));

                db.Vremes.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum).ToList().ForEach(
                    x => drzava.Vremena.Add(this.BPuWebVreme(x)));
            }

            return drzava;
        }

        public DrzavaWeb DrzavaPoImenu(string imeDrzave)
        {
            var drzava = new DrzavaWeb();

            int id = IdZaDrzavu(imeDrzave);
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                db.Potrosnjas.Where(x => x.DrzavaId == id).ToList().ForEach(
                    x => drzava.Potrosnje.Add(this.BPuWebPotrosnja(x)));

                db.Vremes.Where(x => x.DrzavaId == id).ToList().ForEach(
                    x => drzava.Vremena.Add(this.BPuWebVreme(x)));
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

            using (var db = new Drzave())
            {
                var dbModel = db.Drzavas.Where(x => x.Naziv == ime).ToList();

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
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Potrosnjas.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum)
                    .Select(x => BPuWebPotrosnja(x)));
            }

            return retVal;
        }

        public IEnumerable<PotrsonjaWeb> PotrosnjaPoImenu(string imeDrzave)
        {
            var retVal = new List<PotrsonjaWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Potrosnjas.Where(x => x.DrzavaId == id)
                    .Select(x => BPuWebPotrosnja(x)));
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
                    db.Potrosnjas.Where(x => x.DrzavaId == d.Id).ToList().ForEach(x => webD.Potrosnje.Add(BPuWebPotrosnja(x)));
                    db.Vremes.Where(x => x.DrzavaId == d.Id).ToList().ForEach(x => webD.Vremena.Add(BPuWebVreme(x)));
                }
            }

            return retVal;
        }

        public IEnumerable<VremeWeb> VremePoDatumu(DateTime pocetniDatum, DateTime krajnjiDatum, string imeDrzave)
        {
            var retVal = new List<VremeWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Vremes.Where(x => x.DrzavaId == id && x.DatumUTC >= pocetniDatum && x.DatumUTC <= krajnjiDatum)
                    .Select(x => BPuWebVreme(x)));
            }

            return retVal;
        }

        public IEnumerable<VremeWeb> VremePoImenu(string imeDrzave)
        {
            var retVal = new List<VremeWeb>();

            int id = IdZaDrzavu(imeDrzave);
            if (id != -1)
                throw new Exception("Drzava ne postoji.");

            using (var db = new Drzave())
            {
                retVal.AddRange(db.Vremes.Where(x => x.DrzavaId == id)
                    .Select(x => BPuWebVreme(x)));
            }

            return retVal;
        }

        public Drzava WebuBPDrzava(DrzavaWeb drzava)
        {
            throw new NotImplementedException();
        }

        public Potrosnja WebuBPPotrosnja(PotrsonjaWeb drzava)
        {
            throw new NotImplementedException();
        }

        public Vreme WebuBPVreme(VremeWeb drzava)
        {
            throw new NotImplementedException();
        }
    }
}
