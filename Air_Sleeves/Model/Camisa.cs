using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Camisa: DetalhesMaterial
    {

        public Camisa(decimal interna, decimal externa, decimal com, int numero_Camisa)
        {
            Interna = interna;
            Externa = externa;
            Comprimento = com;

            this.num_Camisa = numero_Camisa;
        }

        public int num_Camisa { get; set; }
    
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
        private void CalculaMetrosCadarco()
        {
            decimal aux = this.Interna;

            var voltas = this.num_Camisa == 1 ? 1 : 0.8m;

            decimal parametro_1 = Math.Round(Calculo.pi * aux);

            decimal parametro_2 = Math.Round(this.Comprimento / 40);

            this.Metros_Cadarco = Math.Round((Math.Round(parametro_1 * parametro_2) / 100) * voltas) ;
        }

        private void CalculaPesoCadarco()
        {
            //Metros * Constante 0.0075
            this.Peso_Cadarco = Calculo.Multiplica(this.Metros_Cadarco, 0.0075M, 3);
            this.Peso_Total = Peso_Total + Peso_Cadarco;
        }

        private void CalculaPrecoCadarco()
        {
            //Cardaço
            int id = 11;

            //11 -- id cardaco
            var preco_Cadarco = preco_Material(id);

            this.Preco_Cadarco = Calculo.DivideMultiplica(this.Metros_Cadarco, 50, preco_Cadarco, 2);
            this.Preco_Total = this.Preco_Total + Preco_Cadarco;
        }

        private void CalculaValoresCadarco()
        {
            CalculaMetrosCadarco();
            CalculaPesoCadarco();
            CalculaPrecoCadarco();
        }

        private void CalculaComposto()
        {
            //Tenho que verificar pq muda da 3º camisa para as demais
            decimal fatorComposto = 0.0075M;
            this.Composto_Resina = Calculo.Multiplica(this.Metros_Cadarco, fatorComposto, 3);
        }

        public void CalculaValores()
        {
            LimpaTotais();
            CalculaValoresCadarco();
            CalculaComposto();
            CalculaItens();
        }

        #endregion

    }
}
