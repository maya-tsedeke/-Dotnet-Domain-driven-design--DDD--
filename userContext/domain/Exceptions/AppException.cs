using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Exceptions
{
    public class AppException : Exception
    {
        public string LogsLink { get; set; }
        public int HttpStatusCode { get; set; }

        public AppException(string message, string logsLink = null, int httpStatusCode = 500) : base(message)
        {
            LogsLink = logsLink;
            HttpStatusCode = httpStatusCode;
        }
    }
}
