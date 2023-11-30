using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;
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
        private void Calc_MetrosCadarco(bool type)
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
            decimal voltas = type ? 0.010M : 0.020M;

            this.Peso_Cadarco = Calculo.Multiplica(this.Metros_Cadarco, voltas, 3);
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

        private void Calc_Valores_Cadarco(bool type)
        {
            Calc_MetrosCadarco(type);
            Calc_Peso_Cadarco(type);
            Calc_Preco_Cadarco();
            //Calc_Qntd_Bobinas();
        }

        private void Calc_Composto(bool type)
        {
            //Tenho que verificar pq muda da 3º camisa para as demais
            decimal fatorComposto = type ? 0.010M : 0.020M;
            this.Composto_Resina = Calculo.Multiplica(this.Metros_Cadarco, fatorComposto, 3);
        }


        private void Calc_Composto_2()
        {
            decimal aux = this.Interna;

            decimal parametro_1 = Math.Round(Calculo.pi * aux) / 1000;

            this.Composto_Resina_2 = Math.Round(parametro_1 * this.Comprimento * 0.0003M, 3);
        }
        public void Calc_Valores_Isopor(bool type)
        {
            LimpaTotais();
            Calc_Valores_Cadarco(type);
            Calc_Composto(type);
            Calc_Composto_2();

            Calc_Itens_Isopor();
        }

 

        #endregion

    }
}
