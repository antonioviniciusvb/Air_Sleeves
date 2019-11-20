using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Air_Sleeves.Util
{
    public static class Calculo
    {
        public const decimal pi = 3.1416M;
        
        public static decimal Reducao_Percentual(decimal vl, decimal percent, int digit)
        {
            return Math.Round((vl - ((vl * percent) / 100)), digit);
        }

        public static decimal Multiplica(decimal vl_1, decimal vl_2, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2), casas_Decimais);
        }

        public static decimal Multiplica(decimal vl_1, decimal vl_2, decimal vl_3, decimal vl_4, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2 * vl_3 * vl_4), casas_Decimais);
        }

        public static decimal Multiplica(decimal vl_1, decimal vl_2, decimal vl_3, decimal vl_4, decimal vl_5, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2 * vl_3 * vl_4 * vl_5), casas_Decimais);
        }

        public static decimal MultiplicaDivide(decimal vl_1, decimal vl_2, decimal vl_3, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2 / vl_3), casas_Decimais);
        }

        public static decimal DivideMultiplica(decimal vl_1, decimal vl_2, decimal vl_3, int casas_Decimais)
        {
            return Math.Round(((vl_1 / vl_2) * vl_3), casas_Decimais);
        }

        public static decimal MultiplicaSoma(decimal vl_1, decimal vl_2, decimal vl_3, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2) + vl_3, casas_Decimais);
        }

        public static decimal Eleva_Ao_Quadrado(decimal vl)
        {
            return vl * vl;
        }

    }
}
