using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Acabamento: Camisa
    {
        const decimal ft_Acabamento = 0.00000011M;
        public Acabamento()
        {

        }
        private decimal CalculaCompostoResina(Camisa camisa_1, Isopor isopor)
        {
            decimal ext_1_quadrado = Calculo.Eleva_Ao_Quadrado(camisa_1.Externa + 1);
            decimal ext_3_quadrado = Calculo.Eleva_Ao_Quadrado(isopor.Externa - 0.4M);

            return Calculo.Multiplica((Calculo.pi / 4), camisa_1.Comprimento, (ext_1_quadrado - ext_3_quadrado), ft_Acabamento, 2.0M, 3);
        }

        public void CalculaValores(Camisa camisa_1, Isopor isopor)
        {
            LimpaTotais();

            //Isso irá aumentar o custo em 25% de todos os produtos
            Composto_Resina = Calculo.Adiciona_Percentual(CalculaCompostoResina(camisa_1, isopor), 25,3);
            CalculaItens();
        }
    }
}
