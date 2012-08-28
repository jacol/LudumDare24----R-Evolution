using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _R_Evolution.Helpers
{
    class Logger
    {
        internal static void Log(string from, string msg)
        {            
            var textOut = new StreamWriter(new FileStream("log.log", FileMode.Append, FileAccess.Write));
            textOut.WriteLine(string.Format("{0} {1}::{2}", DateTime.Now.ToString("ddMMyyyy HH:mm:ss"), from, msg));
            textOut.Close();            
        }
    }
}
