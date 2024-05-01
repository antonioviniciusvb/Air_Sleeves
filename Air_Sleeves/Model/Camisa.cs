using System;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Camisa: DetalhesMaterial
    {
        public decimal Interna { get; set; }

        public decimal Externa { get; set; }

        public decimal Comprimento { get; set; }

        public decimal Voltas { get; set; }

        public Camisa()
        {
        }

        public Camisa(decimal interna, decimal externa, decimal comprimento)
        {
            Interna = interna;
            Externa = externa;
            Comprimento = comprimento;
        }

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

        private void OnVoltasChanged()
        {
            if(Voltas <= 0 || Voltas > 50)
                Voltas = 1;
        }

        #region Cadarco
        public virtual void CalculaMetrosCadarco()
        {
            decimal parametro_1 = Math.Round(Calculo.pi * Interna, 2);

            decimal parametro_2 = Math.Round(Comprimento / 40, 2);

            Metros_Cadarco = Math.Ceiling((parametro_1 * parametro_2 * Voltas) / 1000);
        }

        public virtual void CalculaPesoCadarco(decimal constante = 0.0075M)
        {
            //Metros * Constante 0.0075
            Peso_Cadarco = Calculo.Multiplica(Metros_Cadarco, constante, 3);
            Peso_Total = Peso_Total + Peso_Cadarco;
        }

        public virtual void CalculaPrecoCadarco()
        {
            //Cardaço
            int id = 11;

            //11 -- id cardaco
            var preco_Cadarco = preco_Material(id);

            Preco_Cadarco = Calculo.DivideMultiplica(Metros_Cadarco, 50, preco_Cadarco, 2);
            Preco_Total = Preco_Total + Preco_Cadarco;
        }

        public virtual void CalculaValoresCadarco()
        {
            CalculaMetrosCadarco();
            CalculaPesoCadarco();
            CalculaPrecoCadarco();
        }

        public virtual void CalculaComposto(decimal fatorComposto = 0.0075M)
        {
            //Tenho que verificar pq muda da 3º camisa para as demais
             
            Composto_Resina = Calculo.Multiplica(Metros_Cadarco, fatorComposto, 3);
        }

        public virtual void CalculaValores()
        {
            LimpaTotais();
            CalculaValoresCadarco();
            CalculaComposto();
            CalculaItens();
        }

        public override void LimpaValores()
        {
            base.LimpaValores();
            Interna = 0;
            Externa = 0;
            Comprimento = 0;     
            Voltas = 0;
        }

        #endregion

    }
}
