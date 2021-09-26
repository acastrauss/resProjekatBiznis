using System;
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
using ExcelDataReader;

namespace WebApplication1
{
    public class IndexController : ApiController
    {
        [HttpPost]
        [Route("api/Index/DodajDrzavu")]
        public IHttpActionResult DodajDrzavu([FromBody]object podaci)
        {
            try
            {
                var nazivDic = JsonConvert.DeserializeObject<Dictionary<string, string>>(podaci.ToString());
                var naziv = nazivDic["naziv"];

                IBPPristup bp = new BPPristup();
                var kratakNaziv = bp.KratakNazivDrzave(naziv);
                bp.DodajDrzavu(naziv, kratakNaziv);

                return Ok(naziv);
            }
            catch (Exception e)
            {
                return InternalServerError(e.InnerException);
            }
        }


        [HttpPost]
        [Route("api/Index/FilterDatum")]
        public IHttpActionResult FilterDatum(object podaci)
        {
            IFilterDatum filterDatum = new FilterDatum();
            
            var podaciZaFilter = JsonConvert.DeserializeObject<IEnumerable<PodaciZaPrikaz>>(podaci.ToString());
            var pocetni = Request.Headers.GetValues("startDate").First();
            var krajnji = Request.Headers.GetValues("endDate").First();

            var dtP = DateTime.Parse(pocetni);
            var dtK = DateTime.Parse(krajnji);

            return Ok(filterDatum.FiltrirajPoDatumu(podaciZaFilter, dtP, dtK));
        }

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

        // GET api/<controller>
        [HttpGet]
        [Route("api/Index/GetLogFile")]
        public IHttpActionResult GetLogFile()
        {
            var fname = HttpContext.Current.Server.MapPath("~/LogFile/Log.csv");

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

        [HttpPost]

        [Route("api/Index/PostCSVFile")]
        public async Task<IHttpActionResult> PostCSVFile()
        {
            var countryName = Request.Headers.GetValues("countryName").First();
            var startDate = DateTime.Parse(Request.Headers.GetValues("startDate").First());
            var endDate = DateTime.Parse(Request.Headers.GetValues("endDate").First());
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var prov = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(prov);

            var weatherFiles = new List<String>();
            var consFile = String.Empty;
            
            foreach (var f in prov.Contents)
            {
                var fHeader = f.Headers.ContentDisposition.FileName;
                var name = DateTime.Now.ToString().Replace('/', '_').Replace(' ', '_').Replace(':', '_');

                if(fHeader != null)
                {
                    
                    if (fHeader.Contains(".xls"))
                    {
                        fHeader = fHeader.Substring(1);
                        fHeader = fHeader.Substring(0, fHeader.Length - 1);

                        if (fHeader.EndsWith(".xlsx"))
                        {
                            name += ".xlsx";
                        }
                        else
                        {
                            name += ".xls";
                        }
                    }
                    else
                    {
                        name += ".csv"; 
                    }
                }
                else
                {
                    name += ".csv";
                }

                var byteArr = await f.ReadAsByteArrayAsync();
                var mainPath = HttpContext.Current.Server.MapPath("~/SourceFiles/");
                var pathFile = Path.Combine(mainPath, name);
                
                try
                {
                    var fstream = File.Create(pathFile);
                    fstream.Write(byteArr, 0, byteArr.Length);
                    fstream.Close();
                
                }
                catch (Exception e)
                {
                    return InternalServerError(e.InnerException);
                }

                if (pathFile.EndsWith(".csv"))
                {
                    weatherFiles.Add(pathFile);
                }
                else
                {
                    consFile = pathFile;
                }
            }


            IImport import = new Import();
            import.Load(consFile, weatherFiles, countryName, startDate, endDate);

            return Ok("uploaded");
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