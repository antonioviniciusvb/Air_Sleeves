using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Air_Sleeves.Model;
namespace Air_Sleeves.Util
{
    public static class Validacao
    {
        public static bool Camisa_1(Camisa c)
        {
            return c.Externa - c.Interna > 1 && c.Interna > 0 && c.Externa > 0 && c.Comprimento > 0;
        } 

    }
}
