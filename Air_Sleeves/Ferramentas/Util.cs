using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Air_Sleeves.Ferramentas
{
    public static class Util
    {
        public static bool fileExist(string pathDataBase)
        {
            return File.Exists($"{pathDataBase}");
        }
    }
}
