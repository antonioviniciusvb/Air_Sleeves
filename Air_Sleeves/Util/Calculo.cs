using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Air_Sleeves.Util
{
    public static class Calculo
    {

        public static decimal Multiplica(decimal vl_1, decimal vl_2, int casas_Decimais)
        {
            return Math.Round((vl_1 * vl_2), casas_Decimais);
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

    }
}
