using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Modeli;
using Modeli.WebModeli;
using Logika;

namespace WebApplication1
{
    public class IndexController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Index/GetAllData")]
        public IEnumerable<PodaciZaPrikaz> GetAllData()
        {
            List<PodaciZaPrikaz> allData = new List<PodaciZaPrikaz>();

            IBPPristup bPPristup = new BPPristup();
            List<PodaciZaPrikaz> podaci = new List<PodaciZaPrikaz>();
            var sveDrzave = bPPristup.SveDrzave();

            sveDrzave.ToList().ForEach(x =>
            {
                x.Potrosnje.ForEach(y =>
                {
                    var pzp = new PodaciZaPrikaz()
                    {
                        DatumUTC = y.DatumUTC,
                        KolicinaEnergije = y.Kolicina,
                        NazivDrzave = x.Naziv
                    };
                    podaci.Add(pzp);
                });

                var toAdd = new List<PodaciZaPrikaz>();

                x.Vremena.ForEach(y =>
                {
                    var indx = podaci.FindIndex(z => z.DatumUTC.Equals(y.DatumUTC) && z.NazivDrzave.Equals(x.Naziv));

                    if (indx != -1)
                    {
                        
                        podaci[indx].BrzinaVetra = y.BrzinaVetra;
                        podaci[indx].NazivDrzave = x.Naziv;
                        podaci[indx].Pritisak = y.AtmosferskiPritisak;
                        podaci[indx].Temperatura = y.Temperatura;
                        podaci[indx].VlaznostVazduha = y.VlaznostVazduha;
                    }
                    else
                    {
                        var pzp = new PodaciZaPrikaz()
                        {
                            BrzinaVetra = y.BrzinaVetra,
                            NazivDrzave = x.Naziv,
                            Pritisak = y.AtmosferskiPritisak,
                            Temperatura = y.Temperatura,
                            VlaznostVazduha = y.VlaznostVazduha,
                            DatumUTC = y.DatumUTC
                        };

                        toAdd.Add(pzp);
                    }
                });

                podaci.AddRange(toAdd);
            });

            return podaci;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}