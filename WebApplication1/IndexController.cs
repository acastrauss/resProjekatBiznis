﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Modeli;
using Modeli.WebModeli;
using Logika;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class IndexController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        [Route("api/Index/GetCSVFile")]
        public IHttpActionResult GetCSVFile([FromBody] object exportData)
        {
            List<PodaciZaPrikaz> podaci = JsonConvert.DeserializeObject<List<PodaciZaPrikaz>>(exportData.ToString());

            IExport export = new Export();

            var fname = export.SaveData(podaci);

            var alltext = File.ReadAllText(fname);
            
            return Ok(alltext);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("api/Index/GetAllData")]
        public IEnumerable<PodaciZaPrikaz> GetAllData()
        {
            IBPPristup bPPristup = new BPPristup();
            List<PodaciZaPrikaz> podaci = new List<PodaciZaPrikaz>();
            var sveDrzave = bPPristup.SveDrzave();

            IKonverzija konv = new Konverzija();

            podaci = konv.ModeliZaPrikaz(sveDrzave).ToList();
            
            return podaci;
        }
        
        [HttpGet]
        [Route("api/Index/GetNaziveDrzava")]
        public IEnumerable<String> GetNaziveDrzava()
        {
            IBPPristup bPPristup = new BPPristup();
            
            return bPPristup.NaziviDrzava();
        }
        
        [HttpGet]
        [Route("api/Index/GetDataDrzava")]
        public IEnumerable<PodaciZaPrikaz> GetDataDrzava(string naziv)
        {
            IBPPristup bPPristup = new BPPristup();
            var drzava = bPPristup.DrzavaPoImenu(naziv);
            IKonverzija konv = new Konverzija();

            return konv.ModeliZaPrikaz(new List<DrzavaWeb>() { drzava});
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