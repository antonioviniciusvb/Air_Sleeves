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
            if (Interna < 0)
                Interna = 0;
        }

        private void OnExternaChanged()
        {
            if (Externa < 0)
                Externa = 0;

            if (Externa - Interna <= 1)
                Externa = Interna + 2;
        }

        private void OnComprimentoChanged()
        {
            if (Comprimento < 0)
                Comprimento = 0;
        }

        #region Cadarco
        private void Calc_MetrosCadarco()
        {
            decimal parametro_2 = num_Camisa != 3 ? this.Interna : this.Externa;
            decimal parametro_3 = num_Camisa != 3 ? 100 : 1000;

            this.Metros_Cadarco = Math.Ceiling((Calculo.pi * parametro_2 * (this.Comprimento / 32)) / parametro_3);
        }

        private void Calc_Peso_Cadarco()
        {
            this.Peso_Cadarco = Calculo.Multiplica(this.Metros_Cadarco, 0.007M, 3);
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

        private void Calc_Valores_Cadarco()
        {
            Calc_MetrosCadarco();
            Calc_Peso_Cadarco();
            Calc_Preco_Cadarco();
        }

        private void Calc_Composto()
        {
            //Tenho que verificar pq muda da 3º camisa para as demais
            decimal fatorComposto = num_Camisa != 3 ? 0.006M : 0.02M;
            this.Composto_Resina = Calculo.Multiplica(this.Metros_Cadarco, fatorComposto, 3);
            this.Peso_Total = Peso_Total + Composto_Resina;
        }

        public void Calc_Valores_Camisa()
        {
            Calc_Valores_Cadarco();
            Calc_Composto();
            Calc_Itens();
        }

        #endregion

    }
}
