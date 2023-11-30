using Air_Sleeves.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Sleeves.Model
{
    public class Peça:DetalhesMaterial
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

        private bool _type;

        public bool Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        private List<Camisa> _c;

        private Isopor _isopor;

        public Camisa Camisas_1
        {
            get { return _c[0]; }
        }

        public Camisa Camisas_2
        {
            get { return _c[1]; }
        }

        public Isopor Isopor
        {
            get { return _isopor; }
        }


        public new void LimpaMaterial()
        {
            Camisas_1.LimpaMaterial();
            Camisas_2.LimpaMaterial();
            Isopor.LimpaMaterial();
            Filamento.LimpaMaterial();
            Acabamento.LimpaMaterial();
            Eva.LimpaMaterial();

        }

        private Filamento _f;
        public Filamento Filamento
        {
            get { return _f; }
        }

        private Acabamento _ac;

        public Acabamento Acabamento
        {
            get { return _ac; }
        }


        private Eva _eva;

        public Eva Eva
        {
            get { return _eva; }
        }




        private int _quantidade;
        public int Quantidade
        {
            get { return _quantidade; }
            set
            {
                if (value >= 1)
                    _quantidade = value;

                Calcula_Valores_Pecas();
                
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

        private void Calcula_Valores_Pecas_Com_Reducao()
        {
            Calcula_Valores_Pecas();

            Preco_Resina = Calculo.Reducao_Percentual(Preco_Resina, Reducao,2);
            Preco_HL918 = Calculo.Reducao_Percentual(Preco_HL918, Reducao,2);
            Preco_A78 = Calculo.Reducao_Percentual(Preco_A78, Reducao,2);
            Preco_HT231 = Calculo.Reducao_Percentual(Preco_HT231, Reducao,2);
            Preco_AntiBolha = Calculo.Reducao_Percentual(Preco_AntiBolha, Reducao,2);
            Preco_K10 = Calculo.Reducao_Percentual(Preco_K10, Reducao,2);
            Preco_Pigmento = Calculo.Reducao_Percentual(Preco_Pigmento, Reducao,2);
            Preco_Cadarco = Calculo.Reducao_Percentual(Preco_Cadarco, Reducao,2);
            Preco_Fio = Calculo.Reducao_Percentual(Preco_Fio, Reducao,2);
            Preco_EVA = Calculo.Reducao_Percentual(Preco_EVA, Reducao, 2);
            Preco_Resina101F = Calculo.Reducao_Percentual(Preco_Resina101F, Reducao, 2);
            Preco_Total_Anel = Calculo.Reducao_Percentual(Preco_Anel, Reducao, 2);
            Preco_Total_Embalagem = Calculo.Reducao_Percentual(Preco_Isopor, Reducao, 2);
            Preco_Total_Isopor = Calculo.Reducao_Percentual(Preco_Embalagem, Reducao, 2);

            //Peso_Resina = Calculo.Reducao_Percentual(Peso_Resina, Reducao,3);
            //Peso_HL918 = Calculo.Reducao_Percentual(Peso_HL918, Reducao,3);
            //Peso_A78 = Calculo.Reducao_Percentual(Peso_A78, Reducao,3);
            //Peso_HT231 = Calculo.Reducao_Percentual(Peso_HT231, Reducao,3);
            //Peso_AntiBolha = Calculo.Reducao_Percentual(Peso_AntiBolha, Reducao,3);
            //Peso_K10 = Calculo.Reducao_Percentual(Peso_K10, Reducao,3);
            //Peso_Pigmento = Calculo.Reducao_Percentual(Peso_Pigmento, Reducao,3);
            //Peso_Cadarco = Calculo.Reducao_Percentual(Peso_Cadarco, Reducao,3);
            //Peso_Fio = Calculo.Reducao_Percentual(Peso_Fio, Reducao,3);
            //Peso_Total = Calculo.Reducao_Percentual(Peso_Total, Reducao,3);
            //Bobinas_Cadarco = Calculo.Reducao_Percentual(Bobinas_Cadarco, Reducao,2);
            //Peso_Resina101F = Calculo.Reducao_Percentual(Peso_Resina101F, Reducao, 3);

            //Metros_Cadarco = Calculo.Reducao_Percentual(Metros_Cadarco, Reducao,2);

            Preco_Total = Calculo.Reducao_Percentual(Preco_Total, Reducao,2);
        }

        public void Calcula_Valores_Pecas()
        {
            Preco_Resina = (_c.Sum(x => x.Preco_Resina) + _isopor.Preco_Resina + _f.Preco_Resina + _ac.Preco_Resina + _eva.Preco_Resina) * Quantidade;
            Preco_HL918 = (_c.Sum(x => x.Preco_HL918) + _f.Preco_HL918 + _ac.Preco_HL918) * Quantidade;
            Preco_A78 = (_c.Sum(x => x.Preco_A78) + _f.Preco_A78 + _ac.Preco_A78) * Quantidade;
            Preco_HT231 = (_c.Sum(x => x.Preco_HT231) + _isopor.Preco_HT231 + _f.Preco_HT231 + _ac.Preco_HT231 + _eva.Preco_HT231) * Quantidade;
            Preco_HT365 = (_c.Sum(x => x.Preco_HT365) + _isopor.Preco_HT365 + _f.Preco_HT365 + _ac.Preco_HT365 + _eva.Preco_HT365) * Quantidade;
            Preco_AntiBolha = (_c.Sum(x => x.Preco_AntiBolha) + _f.Preco_AntiBolha + _ac.Preco_AntiBolha) * Quantidade;
            Preco_K10 = (_c.Sum(x => x.Preco_K10) + _f.Preco_K10 + _ac.Preco_K10) * Quantidade;
            Preco_Pigmento = (_c.Sum(x => x.Preco_Pigmento) + _f.Preco_Pigmento + _ac.Preco_Pigmento) * Quantidade;
            Preco_Cadarco = (_c.Sum(x => x.Preco_Cadarco) + _isopor.Preco_Cadarco + _f.Preco_Cadarco + _ac.Preco_Cadarco) * Quantidade;
            Preco_Fio = (_c.Sum(x => x.Preco_Fio) + _f.Preco_Fio + _ac.Preco_Fio) * Quantidade;
            Preco_EVA = Eva.Preco_EVA * Quantidade;
            Preco_Resina101F = (_c.Sum(x => x.Preco_Resina101F) + Isopor.Preco_Resina101F) * Quantidade;
            Preco_Total_Anel = Preco_Anel * Quantidade;
            Preco_Total_Embalagem = Preco_Embalagem * Quantidade;
            Preco_Total_Isopor = Preco_Isopor * Quantidade;


            Peso_Resina = ((_c.Sum(x => x.Peso_Resina) +_isopor.Peso_Resina + _f.Peso_Resina + _ac.Peso_Resina + _eva.Peso_Resina) * Quantidade);
            Peso_HL918 = ((_c.Sum(x => x.Peso_HL918) + _f.Peso_HL918 + _ac.Peso_HL918) * Quantidade);
            Peso_A78 = ((_c.Sum(x => x.Peso_A78) + _f.Peso_A78 + _ac.Peso_A78) * Quantidade);
            Peso_HT231 = ((_c.Sum(x => x.Peso_HT231) + _isopor.Peso_HT231 + _f.Peso_HT231 + _ac.Peso_HT231 + _eva.Peso_HT231) * Quantidade);
            Peso_HT365 = ((_c.Sum(x => x.Peso_HT365) + _isopor.Peso_HT365 + _f.Peso_HT365 + _ac.Peso_HT365 + _eva.Peso_HT365) * Quantidade);
            Peso_AntiBolha = ((_c.Sum(x => x.Peso_AntiBolha) + _f.Peso_AntiBolha + _ac.Peso_AntiBolha) * Quantidade);
            Peso_K10 = ((_c.Sum(x => x.Peso_K10) + _f.Peso_K10 + _ac.Peso_K10) * Quantidade);
            Peso_Pigmento = ((_c.Sum(x => x.Peso_Pigmento) + _f.Peso_Pigmento + _ac.Peso_Pigmento) * Quantidade);
            Peso_Cadarco = ((_c.Sum(x => x.Peso_Cadarco) + _isopor.Peso_Cadarco + _f.Peso_Cadarco + _ac.Peso_Cadarco) * Quantidade);
            Peso_Fio = ((_c.Sum(x => x.Peso_Fio) + _f.Peso_Fio + _ac.Peso_Fio) * Quantidade);
            Peso_Total = ((_c.Sum(x => x.Peso_Total) + _f.Peso_Total + _ac.Peso_Total + _isopor.Peso_Total + Eva.Peso_Total) * Quantidade);
            Peso_Resina101F = (_c.Sum(x => x.Peso_Resina101F) + Isopor.Peso_Resina101F) * Quantidade;

            Bobinas_Cadarco = (_c.Sum(x => x.Metros_Cadarco) + _isopor.Metros_Cadarco) / 50 * Quantidade;
            Metros_Cadarco = (_c.Sum(x => x.Metros_Cadarco) + _isopor.Metros_Cadarco) * Quantidade;
            Preco_Total = (_c.Sum(x => x.Preco_Total) + _isopor.Preco_Total + Eva.Preco_Total + 
                           _f.Preco_Total + _ac.Preco_Total + Preco_Anel + Preco_Embalagem + Preco_Isopor ) * Quantidade;
        }


        public Peça(Camisa camisa_1, Camisa camisa_2, Isopor Isopor, Filamento filamento, Acabamento acabamento, Eva eva)
        {
            _c = new List<Camisa>();
            _c.Add(camisa_1);
            _c.Add(camisa_2);
            _isopor = Isopor;

            _f = filamento;
            _ac = acabamento;
            _eva = eva;
            _quantidade = 1;
        }


        public Peça()
        {

        }

    }
}
