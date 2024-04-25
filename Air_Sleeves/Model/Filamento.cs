using Air_Sleeves.Util;


namespace Air_Sleeves.Model
{
    public class Filamento:Camisa
    {
        const decimal fatorFilamento = 0.0000014M;

        public Filamento(Camisa camisa_1, Isopor isopor)
        {
            FilamentoA = CalculaFilamentoA(isopor);
            FilamentoB = CalculaFilamentoB(camisa_1, isopor);
        }

        public Filamento()
        {

        }

        public decimal FilamentoA { get; set; }
        public decimal FilamentoB { get; set; }

        private decimal CalculaFilamentoA(Isopor isopor)
        {
            decimal ext_quadrado = Calculo.Eleva_Ao_Quadrado(isopor.Externa);
            decimal int_quadrado = Calculo.Eleva_Ao_Quadrado(isopor.Interna);

            //Apenas para testes
            return Calculo.Multiplica((Calculo.pi / 4), 60, (ext_quadrado - int_quadrado), fatorFilamento, 3);
        }

        private decimal CalculaFilamentoB(Camisa camisa1, Isopor isopor)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(camisa1.Externa + 4);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(isopor.Externa);

            return Calculo.Multiplica((Calculo.pi / 4), camisa1.Comprimento, (ext_1_quadrado - ext_3_quadrado), fatorFilamento, 3);
        }
        private decimal CalculaCompostoResinaFio()
        {
             return this.FilamentoA + this.FilamentoB;
        }
        private decimal CalculaCompostoResina()
        {
            return (this.Composto_Resina_Fio - this.Peso_Fio) + 0.7M;
        }
        public void CalculaValores(Camisa camisa_1, Isopor isopor)
        {
            LimpaTotais();

            FilamentoA = CalculaFilamentoA(isopor);
            FilamentoB = CalculaFilamentoB(camisa_1, isopor);

            Composto_Resina_Fio = CalculaCompostoResinaFio();

            CalculaValoresFio();

            //Isso irá aumentar o custo em 10% de todos os produtos, exceto o FIO
            Composto_Resina = Calculo.Adiciona_Percentual(CalculaCompostoResina(), 10, 3);

            CalculaItens();
        }

        private void CalculaValoresFio()
        {
            //Fio
            int id = 12;
            var preco_Fio = preco_Material(id);

            Peso_Fio = Calculo.Multiplica(this.Composto_Resina_Fio, 0.6M, 3);
            Peso_Total = Peso_Total + Peso_Fio;


            Preco_Fio = Calculo.Multiplica(this.Peso_Fio, preco_Fio, 2);
            Preco_Total = this.Preco_Total + Preco_Fio;
        }

        public override void LimpaValores()
        {
            base.LimpaValores();
            FilamentoA = 0;
            FilamentoB = 0;
        }
    }
}
