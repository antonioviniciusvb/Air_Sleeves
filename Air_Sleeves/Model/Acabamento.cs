using System;
using System.Linq;
using System.Text;
using PropertyChanged;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Acabamento: DetalhesMaterial
    {
        decimal ft_Acabamento = 0.00000011M;

        public Acabamento(Camisa camisa_1, Camisa camisa_3)
        {
           this.Composto_Resina =  Calc_Composto_Resina(camisa_1, camisa_3);
        }

        public Acabamento()
        {

        }

        private decimal Calc_Composto_Resina(Camisa c_1, Camisa c_3)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(c_1.Externa + 1);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(c_3.Externa - 0.4M);

            return Calculo.Multiplica((Calculo.pi / 4), c_1.Comprimento, (ext_1_quadrado - ext_3_quadrado), ft_Acabamento, 2.0M, 3);
        }

        public void Calc_Valores_Acabamento()
        {
            limpaTotais();
            Calc_Itens();
        }

        public void calc_Valores_Acabamento(Camisa camisa_1, Camisa camisa_3)
        {
            limpaTotais();
            this.Composto_Resina = Calc_Composto_Resina(camisa_1, camisa_3);
            Calc_Itens();
        }
    }
}
