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

        public Acabamento(Camisa camisa_1, Isopor isopor)
        {
           this.Composto_Resina =  Calc_Composto_Resina(camisa_1, isopor);
        }

        public Acabamento()
        {

        }

        private decimal Calc_Composto_Resina(Camisa c_1, Isopor isopor)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(c_1.Externa + 1);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(isopor.Externa - 0.4M);

            return Calculo.Multiplica((Calculo.pi / 4), c_1.Comprimento, (ext_1_quadrado - ext_3_quadrado), ft_Acabamento, 2.0M, 3);
        }

        public void Calc_Valores_Acabamento()
        {
            LimpaTotais();
            Calc_Itens();
        }

        public void calc_Valores_Acabamento(Camisa camisa_1, Isopor isopor)
        {
            LimpaTotais();
            this.Composto_Resina = Calc_Composto_Resina(camisa_1, isopor);
            Calc_Itens();
        }


        public void Adiciona25pct()
        {
            Peso_Resina = Calculo.Adiciona_Percentual(Peso_Resina, 25, 3);
            Peso_HT231 = Calculo.Adiciona_Percentual(Peso_HT231, 25, 3);
            Peso_AntiBolha = Calculo.Adiciona_Percentual(Peso_AntiBolha, 25, 3);
            Peso_K10 = Calculo.Adiciona_Percentual(Peso_K10, 25, 3);
            Peso_Pigmento = Calculo.Adiciona_Percentual(Peso_Pigmento, 25, 3);
            Composto_Resina = Calculo.Adiciona_Percentual(Composto_Resina, 25, 3);

            Preco_Resina = Calculo.Adiciona_Percentual(Preco_Resina, 25, 3);
            Preco_HT231 = Calculo.Adiciona_Percentual(Preco_HT231, 25, 3);
            Preco_AntiBolha = Calculo.Adiciona_Percentual(Preco_AntiBolha, 25, 3);
            Preco_K10 = Calculo.Adiciona_Percentual(Preco_K10, 25, 3);
            Preco_Pigmento = Calculo.Adiciona_Percentual(Preco_Pigmento, 25, 3);


            Preco_Total = Calculo.Adiciona_Percentual(Preco_Total, 25, 3);
            Peso_Total = Calculo.Adiciona_Percentual(Peso_Total, 25, 3);
        }
    }
}
