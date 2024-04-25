using System;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Eva: Camisa
    {
        decimal fatorPesoResina;
        public decimal Preco { get; set; }

        public void CalculaComposto()
        {
            decimal parametro_1 = Math.Round(Calculo.pi * Interna, 2);         
            fatorPesoResina = Math.Round((parametro_1 * Comprimento) / 1000, 2);

            this.Composto_Resina = Math.Round(fatorPesoResina * 0.0002M, 3);
        }

        public override void CalculaValores()
        {
            LimpaTotais();
            CalculaComposto();
            CalculaItens();
            CalculaEVA(fatorPesoResina);
        }      
    }
}
