using Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Logika
{
    public class LogPisanje : ILogPisanje
    {
        private static String LogFilePath = HttpContext.Current != null ?
            HttpContext.Current.Server.MapPath("~/LogFile/Log.csv") :
            Path.Combine(
            Directory.GetCurrentDirectory(), "WebApplication1\\LogFile\\Log.csv"
            );

        public LogPisanje()
        {
            if (!File.Exists(LogFilePath))
            {
                File.AppendAllText(LogFilePath, String.Format("{0},{1},{2}",
                    "Vreme logovanja:", "Tip log podatka:", "Poruka:"));
                File.AppendAllText(LogFilePath, "\n");
            }
        }

        public void AddLog(LogPodatak logPodatak)
        {
            if (logPodatak == null)
                throw new Exception("Log podatak ne moze biti null");

            File.AppendAllText(LogFilePath, logPodatak.ToString());
            File.AppendAllText(LogFilePath, "\n");
        }
    }
}
