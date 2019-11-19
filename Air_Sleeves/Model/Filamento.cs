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

        public Filamento(Camisa camisa_1, Camisa camisa_3, decimal valor_isopor)
        {
            this.Filamento_A = Calc_Filamento_A(camisa_3, valor_isopor);
            this.Filamento_B = Calc_Filamento_B(camisa_1, camisa_3, valor_isopor);
        }

        public Filamento()
        {

        }

        public decimal Filamento_A { get; set; }
        public decimal Filamento_B { get; set; }


        private decimal Calc_Filamento_A(Camisa c, decimal vlr_isopor)
        {
            decimal ext_quadrado = Calculo.Eleva_Ao_Quadrado(c.Externa);
            decimal int_quadrado = Calculo.Eleva_Ao_Quadrado(c.Interna);

            //return Calculo.Multiplica((Calculo.pi / 4), vlr_isopor, (ext_quadrado - int_quadrado), ft_Filamento, 3);

            //Apenas para testes
            return Calculo.Multiplica((Calculo.pi / 4), 60, (ext_quadrado - int_quadrado), ft_Filamento, 3);
        }

        private decimal Calc_Filamento_B(Camisa c_1, Camisa c_3, decimal vlr_isopor)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(c_1.Externa + 4);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(c_3.Externa);

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
            this.Composto_Resina_Fio = Calc_Composto_Resina_Fio();
            this.Peso_Total = Peso_Total + Composto_Resina_Fio;

            calc_Valores_Fio();
            
            this.Composto_Resina = Calc_Composto_Resina();
            
            Calc_Itens();
            
        }

        public void calc_Valores_Filamento(Camisa camisa_1, Camisa camisa_3, decimal valor_isopor)
        {
            this.Filamento_A = Calc_Filamento_A(camisa_3, valor_isopor);
            this.Filamento_B = Calc_Filamento_B(camisa_1, camisa_3, valor_isopor);

            this.Composto_Resina_Fio = Calc_Composto_Resina_Fio();
            this.Peso_Total = Peso_Total + Composto_Resina_Fio;

            calc_Valores_Fio();

            this.Composto_Resina = Calc_Composto_Resina();

            Calc_Itens();
        }

        private void calc_Valores_Fio()
        {
            //Fio
            int id = 12;
            var preco_Fio = preco_Material(id);

            this.Peso_Fio = Calculo.Multiplica(this.Composto_Resina_Fio, 0.6M, 3);
            this.Preco_Fio = Calculo.Multiplica(this.Peso_Fio, preco_Fio, 2);
            this.Preco_Total = this.Preco_Total + Preco_Fio;
        }


    }
}
