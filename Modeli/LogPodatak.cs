using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeli
{
    public enum LOG_TYPE { INFO, WARNING, ERROR, NO_TYPE};

    public class LogPodatak
    {
        public LOG_TYPE Type;
        public String Message;
        public DateTime LogTime;

        public LogPodatak() 
        {
            Type = LOG_TYPE.NO_TYPE;
            Message = String.Empty;
            LogTime = DateTime.Now;
        }

        public LogPodatak(LOG_TYPE type, String message, DateTime dateTime)
        {
            this.Type = type;
            this.Message = message;
            this.LogTime = dateTime;
        }

        public LogPodatak(LogPodatak refL)
        {
            this.Type = refL.Type;
            this.Message = refL.Message;
            this.LogTime = refL.LogTime;
        }

        public override string ToString()
        {
            var dtStr =
                LogTime.ToLongDateString() + " " + LogTime.ToLongTimeString();

            dtStr.Replace(',', '_');

            return String.Format("{0},{1},{2}",
                dtStr,
                Enum.GetName(typeof(LOG_TYPE), Type), Message);
        }
    }
}
