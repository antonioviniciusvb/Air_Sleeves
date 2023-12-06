using System;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Isopor : DetalhesMaterial
    {

        public Isopor(decimal interna, decimal externa, decimal com)
        {
            Interna = interna;
            Externa = externa;
            Comprimento = com;

        }

        public decimal Interna { get; set; }

        public decimal Externa { get; set; }

        public decimal Comprimento { get; set; }


        private void OnInternaChanged()
        {
            if (Interna <= 0)
                Interna = 0;
        }

        private void OnExternaChanged()
        {
            if (Externa <= 0)
                Externa = 0;
        }

        private void OnComprimentoChanged()
        {
            if (Comprimento <= 0)
                Comprimento = 0;
        }

        #region Cadarco
        private void CalculaMetrosCadarco(bool type)
        {
            decimal aux =  this.Externa;

            decimal voltas = type ? 0.0010M : 0.0020M;

            decimal parametro_1 = Math.Round(Calculo.pi * aux);

            decimal parametro_2 = Math.Round(this.Comprimento / 40);

            this.Metros_Cadarco = Math.Round(Math.Round(parametro_1 * parametro_2) * voltas, 2);
        }

        private void Calc_Peso_Cadarco(bool type)
        {
            //Metros * Constante 0.020
            decimal constante = 0.020M;

            this.Peso_Cadarco = Calculo.Multiplica(this.Metros_Cadarco, constante, 3);
            this.Peso_Total = Peso_Total + Peso_Cadarco;
        }

        private void Calc_Preco_Cadarco()
        {
            //Cardaço
            int id = 11;

            //11 -- id cardaco
            var preco_Cadarco = preco_Material(id);

            this.Preco_Cadarco = Calculo.DivideMultiplica(this.Metros_Cadarco, 50, preco_Cadarco, 2);
            this.Preco_Total = this.Preco_Total + Preco_Cadarco;
        }

        private void CalculaValoresCadarco(bool type)
        {
            CalculaMetrosCadarco(type);
            Calc_Peso_Cadarco(type);
            Calc_Preco_Cadarco();
        }

        private void Calc_Composto(bool type)
        {
            if (type)
            {
                //Tenho que verificar pq muda da 3º camisa para as demais
                decimal fatorComposto = 0.020M;
                this.Composto_Resina = Calculo.Multiplica(this.Metros_Cadarco, fatorComposto, 3);
            }
            else
            {
                decimal aux = this.Interna;

                decimal parametro_1 = Math.Round(Calculo.pi * aux) / 1000;

                this.Composto_Resina = Math.Round(parametro_1 * this.Comprimento * 0.0003M, 3);
            }
        }


        public void CalculaValores(bool type)
        {
            LimpaTotais();
            CalculaValoresCadarco(type);
            Calc_Composto(type);
            CalculaItens();
        }

        //public override void CalculaItens()
        //{
        //    //Resina 102
        //    int id = 1;

        //    var preco_Resina = preco_Material(id);
        //    var pct_Resina = percentual_Material(id);

        //    Peso_Resina = Calculo.MultiplicaDivide(this.Composto_Resina, pct_Resina, Pct_Total_Materiais, 3);
        //    Preco_Resina = Calculo.Multiplica(Peso_Resina, preco_Resina, 2);

        //    //Resina 101F
        //    id = 15;

        //    preco_Resina = preco_Material(id);
        //    pct_Resina = percentual_Material(id);

        //    Peso_Resina101F = Calculo.MultiplicaDivide(this.Composto_Resina_2, pct_Resina, Pct_Total_Materiais, 3);
        //    Preco_Resina101F = Calculo.Multiplica(Peso_Resina101F, preco_Resina, 2);

        //    //HT231
        //    id = 4;

        //    var preco_HT231 = preco_Material(id);
        //    var pct_HT231 = percentual_Material(id);

        //    Peso_HT231 = Calculo.MultiplicaDivide(this.Composto_Resina, pct_HT231, Pct_Total_Materiais, 3);
        //    Peso_HT231_2 = Calculo.MultiplicaDivide(this.Composto_Resina_2, pct_HT231, Pct_Total_Materiais, 3);

        //    Preco_HT231 = Calculo.Multiplica(this.Peso_HT231, preco_HT231, 2);
        //    Preco_HT231_2 = Calculo.Multiplica(this.Peso_HT231_2, preco_HT231, 2);

        //    Peso_Total = Peso_Total + Peso_Resina101F + Peso_Resina + Peso_HT231 + Peso_HT231_2;
        //    Preco_Total = Preco_Total + Preco_Resina101F + Preco_HT231 + Preco_HT231_2;
        //}




        #endregion

    }
}
