using Air_Sleeves.Util;
using ControlzEx.Standard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Sleeves.Model
{
    public class Peca : DetalhesMaterial
    {
        #region Anel
        public decimal Preco_Anel { get; set; }

        public decimal Preco_Total_Anel { get; set; }

        private void OnPreco_AnelChanged()
        {
            if (Preco_Anel <= 0)
                Preco_Anel = 0;
        }

        #endregion

        #region Embalagem
        public decimal Preco_Embalagem { get; set; }
        public decimal Preco_Total_Embalagem { get; set; }

        private void OnPreco_EmbalagemChanged()
        {
            if (Preco_Embalagem <= 0)
                Preco_Embalagem = 0;
        }

        #endregion

        #region Isopor
        public decimal Preco_Isopor { get; set; }
        public decimal Preco_Total_Isopor { get; set; }


        private void OnPreco_IsoporChanged()
        {
            if (Preco_Isopor <= 0)
                Preco_Isopor = 0;
        }

        #endregion

        public bool Type { get; set; }

        private List<Camisa> _camadas;

        public Camisa Camisas_1 => _camadas[0];
        public Camisa Camisas_2 => _camadas[1];
        public Eva Eva => (Eva)_camadas[2];
        public Isopor Colagem => (Isopor)_camadas[3];
        public Isopor Selagem => (Isopor)_camadas[4];
        public Filamento Filamento => (Filamento)_camadas[5];
        public Acabamento Acabamento => (Acabamento)_camadas[6];
        public decimal IsoporPesoTotal { get; set; }
        public decimal IsoporPrecoTotal { get; set; }


        private int _quantidade;
        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                if (value >= 1)
                    _quantidade = value;

                CalculaValores();
            }
        }

        private int _reducao;
        public int Reducao
        {
            get { return _reducao; }
            set
            {
                if (value >= 0)
                    _reducao = value;

                Calcula_Valores_Pecas_Com_Reducao();

            }
        }

        public Peca()
        {
            _camadas = new List<Camisa>()
            {
                new Camisa(),
                new Camisa(),
                new Eva(),
                new Isopor(),
                new Isopor(),
                new Filamento(),
                new Acabamento()
            };

            Quantidade = 1;
        }

        public void LimpaCamadas()
        {
            Camisas_1.LimpaMaterial();
            Camisas_2.LimpaMaterial();
            Selagem.LimpaMaterial();
            Colagem.LimpaMaterial();
            Filamento.LimpaMaterial();
            Acabamento.LimpaMaterial();
            Eva.LimpaMaterial();

            Camisas_1.LimpaValores();
            Camisas_2.LimpaValores();
            Selagem.LimpaValores();
            Colagem.LimpaValores();
            Filamento.LimpaValores();
            Acabamento.LimpaValores();
            Eva.LimpaValores();
        }

        public void Limpar()
        {
            LimpaValores();
            LimpaCamadas();
        }

        private void Calcula_Valores_Pecas_Com_Reducao()
        {
            CalculaValores();

            Preco_Resina = Calculo.Reducao_Percentual(Preco_Resina, Reducao, 2);
            Preco_HL918 = Calculo.Reducao_Percentual(Preco_HL918, Reducao, 2);
            Preco_A78 = Calculo.Reducao_Percentual(Preco_A78, Reducao, 2);
            Preco_HT231 = Calculo.Reducao_Percentual(Preco_HT231, Reducao, 2);
            Preco_AntiBolha = Calculo.Reducao_Percentual(Preco_AntiBolha, Reducao, 2);
            Preco_K10 = Calculo.Reducao_Percentual(Preco_K10, Reducao, 2);
            Preco_Pigmento = Calculo.Reducao_Percentual(Preco_Pigmento, Reducao, 2);
            Preco_Cadarco = Calculo.Reducao_Percentual(Preco_Cadarco, Reducao, 2);
            Preco_Fio = Calculo.Reducao_Percentual(Preco_Fio, Reducao, 2);
            Preco_EVA = Calculo.Reducao_Percentual(Preco_EVA, Reducao, 2);
            Preco_Resina101F = Calculo.Reducao_Percentual(Preco_Resina101F, Reducao, 2);
            Preco_Total_Anel = Calculo.Reducao_Percentual(Preco_Anel, Reducao, 2);
            Preco_Total_Embalagem = Calculo.Reducao_Percentual(Preco_Isopor, Reducao, 2);
            Preco_Total_Isopor = Calculo.Reducao_Percentual(Preco_Embalagem, Reducao, 2);

            Preco_Total = Calculo.Reducao_Percentual(Preco_Total, Reducao, 2);
        }
        public void CalculaValores()
        {
            Preco_Resina = _camadas.Sum(x => x.Preco_Resina) * Quantidade;
            Preco_HL918 = _camadas.Sum(x => x.Preco_HL918) * Quantidade;
            Preco_A78 = _camadas.Sum(x => x.Preco_A78) * Quantidade;
            Preco_HT231 = _camadas.Sum(x => x.Preco_HT231) * Quantidade;
            Preco_HT356 = _camadas.Sum(x => x.Preco_HT356) * Quantidade;
            Preco_AntiBolha = _camadas.Sum(x => x.Preco_AntiBolha) * Quantidade;
            Preco_K10 = _camadas.Sum(x => x.Preco_K10) * Quantidade;
            Preco_Pigmento = _camadas.Sum(x => x.Preco_Pigmento) * Quantidade;
            Preco_Cadarco = _camadas.Sum(x => x.Preco_Cadarco) * Quantidade;
            Preco_Fio = _camadas.Sum(x => x.Preco_Fio) * Quantidade;
            Preco_EVA = Eva.Preco_EVA * Quantidade;
            Preco_Resina101F = _camadas.Sum(x => x.Preco_Resina101F) * Quantidade;
            Preco_Total_Anel = Preco_Anel * Quantidade;
            Preco_Total_Embalagem = Preco_Embalagem * Quantidade;
            Preco_Total_Isopor = Preco_Isopor * Quantidade;
            IsoporPesoTotal = Colagem.Peso_Total + Selagem.Composto_Resina;
            IsoporPrecoTotal = Colagem.Preco_Total + Selagem.Preco_Total;

            Peso_Resina = _camadas.Sum(x => x.Peso_Resina) * Quantidade;
            Peso_HL918 = _camadas.Sum(x => x.Peso_HL918) * Quantidade;
            Peso_A78 = _camadas.Sum(x => x.Peso_A78) * Quantidade;
            Peso_HT231 = _camadas.Sum(x => x.Peso_HT231) * Quantidade;
            Peso_HT356 = _camadas.Sum(x => x.Peso_HT356) * Quantidade;
            Peso_AntiBolha = _camadas.Sum(x => x.Peso_AntiBolha) * Quantidade;
            Peso_K10 = _camadas.Sum(x => x.Peso_K10) * Quantidade;
            Peso_Pigmento = _camadas.Sum(x => x.Peso_Pigmento) * Quantidade;
            Peso_Cadarco = _camadas.Sum(x => x.Peso_Cadarco) * Quantidade;
            Peso_Fio = _camadas.Sum(x => x.Peso_Fio) * Quantidade;
            Peso_Resina101F = _camadas.Sum(x => x.Peso_Resina101F) * Quantidade;
            Bobinas_Cadarco = (_camadas.Sum(x => x.Metros_Cadarco) / 50) * Quantidade;
            Metros_Cadarco = _camadas.Sum(x => x.Metros_Cadarco) * Quantidade;
            Preco_Total = (_camadas.Sum(x => x.Preco_Total) + Preco_Anel +
                           Preco_Embalagem + Preco_Isopor) * Quantidade;
            Peso_Total = (_camadas.Sum(x => x.Peso_Total) - _camadas.Sum(x => x.Peso_Cadarco)) * Quantidade;

        }
    }
}
