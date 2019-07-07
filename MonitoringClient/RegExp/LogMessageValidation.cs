using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonitoringClient.RegExp
{
    public class LogMessageValidation
    {
        public string PodValidation(string pod)
        {
            if (pod == null)
            {
                return null;
            }
            if (!Regex.IsMatch(pod, @"^(?!\s*$).+"))
            {
                return "Pod darf nicht leer sein!";
            }
            return null;
        }

        public string SeverityValidation(string severity)
        {
            if (severity == null)
            {
                return null;
            }
            if (!Regex.IsMatch(severity, "(^[0-9])"))
            {
                return "Severity darf nur Zahlen enthalten!";
            }
            return null;
        }

        public string HostnameValidation(string hostname)
        {
            if (hostname == null)
            {
                return null;
            }
            if (!Regex.IsMatch(hostname, @"^(?!\s*$).+"))
            {
                return "Hostname darf nicht leer sein!";
            }
            return null;
        }
        public string MessageValidation(string message)
        {
            if (message == null)
            {
                return null;
            }
            if (!Regex.IsMatch(message, @"^(?!\s*$).+"))
            {
                return "Message darf nicht leer sein!";
            }
            return null;
        }
    }
}
