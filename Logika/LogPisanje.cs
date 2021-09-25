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
    public class LogPisanje
    {
        private static String LogFilePath = HttpContext.Current.Server.MapPath("~/LogFile/Log.csv");

        public static void AddLog(LogPodatak logPodatak)
        {
            if (!File.Exists(LogFilePath))
            {
                File.AppendAllText(LogFilePath, String.Format("{0},{1},{2}",
                    "Vreme logovanja:", "Tip log podatka:", "Poruka:"));
                File.AppendAllText(LogFilePath, "\n");
            }

            File.AppendAllText(LogFilePath, logPodatak.ToString());
            File.AppendAllText(LogFilePath, "\n");

        }
    }
}
