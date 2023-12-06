using System;
using System.Linq;
using System.Text;
using PropertyChanged;
using Air_Sleeves.Util;


namespace Air_Sleeves.Model
{
    public class Filamento:DetalhesMaterial
    {

        decimal ft_Filamento = 0.0000014M;

        public Filamento(Camisa camisa_1, Isopor isopor)
        {
            this.Filamento_A = Calc_Filamento_A(isopor);
            this.Filamento_B = Calc_Filamento_B(camisa_1, isopor);
        }

        public Filamento()
        {

        }

        public decimal Filamento_A { get; set; }
        public decimal Filamento_B { get; set; }


        private decimal Calc_Filamento_A(Isopor i)
        {
            decimal ext_quadrado = Calculo.Eleva_Ao_Quadrado(i.Externa);
            decimal int_quadrado = Calculo.Eleva_Ao_Quadrado(i.Interna);

            //Apenas para testes
            return Calculo.Multiplica((Calculo.pi / 4), 60, (ext_quadrado - int_quadrado), ft_Filamento, 3);
        }

        private decimal Calc_Filamento_B(Camisa c_1, Isopor i)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(c_1.Externa + 4);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(i.Externa);

            return Calculo.Multiplica((Calculo.pi / 4), c_1.Comprimento, (ext_1_quadrado - ext_3_quadrado), ft_Filamento, 3);
        }

        private decimal Calc_Composto_Resina_Fio()
        {
             return this.Filamento_A + this.Filamento_B;
        }

        private decimal Calc_Composto_Resina()
        {
            return (this.Composto_Resina_Fio - this.Peso_Fio) + 0.7M;
        }

        public void calc_Valores_Filamento()
        {
            LimpaTotais();

            this.Composto_Resina_Fio = Calc_Composto_Resina_Fio();

            calc_Valores_Fio();
            
            this.Composto_Resina = Calc_Composto_Resina();
            
            CalculaItens();
            
        }

        public void CalculaValores(Camisa camisa_1, Isopor isopor)
        {
            LimpaTotais();

            this.Filamento_A = Calc_Filamento_A(isopor);
            this.Filamento_B = Calc_Filamento_B(camisa_1, isopor);

            this.Composto_Resina_Fio = Calc_Composto_Resina_Fio();

            calc_Valores_Fio();

            this.Composto_Resina = Calc_Composto_Resina();

            CalculaItens();

        }

        public void Adiciona10pct()
        {
            Composto_Resina = Calculo.Adiciona_Percentual(Composto_Resina, 10, 3);
            Peso_Resina = Calculo.Adiciona_Percentual(Peso_Resina, 10, 3);
            Peso_HL918 = Calculo.Adiciona_Percentual(Peso_HL918, 10, 3);
            Peso_A78 = Calculo.Adiciona_Percentual(Peso_A78, 10, 3);
            Peso_Pigmento = Calculo.Adiciona_Percentual(Peso_Pigmento, 10, 3);

            Preco_Resina = Calculo.Adiciona_Percentual(Preco_Resina, 10, 3);
            Preco_HL918 = Calculo.Adiciona_Percentual(Preco_HL918, 10, 3);
            Preco_A78 = Calculo.Adiciona_Percentual(Preco_A78, 10, 3);
            Preco_Pigmento = Calculo.Adiciona_Percentual(Preco_Pigmento, 10, 3);

            Preco_Total = Preco_Pigmento + Preco_A78 + Preco_HL918 + Preco_Resina + Preco_Fio;
            Peso_Total = Composto_Resina + Peso_Fio;
        }

        private void calc_Valores_Fio()
        {
            //Fio
            int id = 12;
            var preco_Fio = preco_Material(id);

            this.Peso_Fio = Calculo.Multiplica(this.Composto_Resina_Fio, 0.6M, 3);
            this.Peso_Total = Peso_Total + Peso_Fio;


            this.Preco_Fio = Calculo.Multiplica(this.Peso_Fio, preco_Fio, 2);
            this.Preco_Total = this.Preco_Total + Preco_Fio;
        }


    }
}
