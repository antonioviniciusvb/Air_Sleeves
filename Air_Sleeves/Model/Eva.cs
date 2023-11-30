using System;
using System.Linq;
using System.Text;
using PropertyChanged;
using Air_Sleeves.Util;
using System.ComponentModel;

namespace Air_Sleeves.Model
{
    public class Eva: DetalhesMaterial
    {
        decimal fatorPesoResina;

        public Eva()
        {
        }

        private void calc_CompostoResina(decimal c_interna, decimal comprimento)
        {
            var parametro_1 = (c_interna * Calculo.pi);

            fatorPesoResina = Math.Round((parametro_1 * comprimento) / 1000, 2);

            this.Composto_Resina = Math.Round(fatorPesoResina * 0.0002M, 3);

        }

        public void calc_Valores_Eva(decimal interna, decimal comprimento, bool type)
        {
            LimpaTotais();
            calc_CompostoResina(interna, comprimento);
            Calc_Itens();
            Calc_EVA(fatorPesoResina, type);
        }

        //public decimal Comprimento { get; set; }
        public decimal Preco { get; set; }
    }
}
