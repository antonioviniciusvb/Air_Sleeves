using System;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Isopor : Camisa
    {
        public void CalculaMetrosCadarco(decimal medida)
        {
            decimal parametro_1 = Math.Round(Calculo.pi * medida, 2);

            decimal parametro_2 = Math.Round(Comprimento / 40, 2);

            Metros_Cadarco = Math.Ceiling((parametro_1 * parametro_2 * Voltas) / 1000);
        }

        public void CalculaCompostoCola()
        {
            Composto_Resina = Math.Round((Calculo.pi * Interna * Comprimento * 0.0003M) / 1000, 3);
        }

        public void CalculaColagem()
        {
            LimpaTotais();
            CalculaCompostoCola();
            CalculaItens();
        }

        public void CalculaSelagem()
        {
            LimpaTotais();
            CalculaMetrosCadarco(medida: Externa);
            CalculaPesoCadarco(constante: 0.020M);
            CalculaPrecoCadarco();
            CalculaComposto(fatorComposto: 0.020M);
            CalculaItens();
        }
    }
}
