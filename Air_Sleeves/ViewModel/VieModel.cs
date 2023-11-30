using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Air_Sleeves.Model;
using Air_Sleeves.Dal;
using Air_Sleeves.Util;
using System.ComponentModel;
using System.Diagnostics;

namespace Air_Sleeves.ViewModel
{
    public class ViewModel:BaseInotifyPropertyChanged
    {
        public Camisa Camisa { get; set; }
        private Peça _peca;

  
        public Command ExecutaSimulacao { get; set; }

        public ViewModel()
        {

            Camisa = new Camisa(0, 0, 0, 1);

            _peca = new Peça(new Camisa(0, 0, 0, 1),
                             new Camisa(0, 0, 0, 2),
                             new Isopor(0, 0, 0),
                             new Filamento(),
                             new Acabamento(),
                             new Eva());

            ExecutaSimulacao = new Command(simula_Orcamento, () => { return Validacao.Camisa_1(Camisa) && Peca.Preco_Anel > 0 &&
                                                                                                          Peca.Preco_Embalagem > 0 &&
                                                                                                          Peca.Preco_Isopor > 0; });
        }

        public Peça Peca
        {
            get { return _peca; }
        }


        private void simula_Orcamento()
        {
            //Camisa 1
            _peca.Camisas_1.Interna = Camisa.Interna;
            _peca.Camisas_1.Externa = Camisa.Externa;
            _peca.Camisas_1.Comprimento = Camisa.Comprimento;

            //Acrescenta 50 mm caso seja Orçamento
            if (_peca.Type == false)
                _peca.Camisas_1.Comprimento += 50;

            //Adiciona 7 na interna, para a 2º camisa
            _peca.Camisas_2.Interna = _peca.Camisas_1.Interna + 7;
            _peca.Camisas_2.Externa = _peca.Camisas_1.Externa;
            _peca.Camisas_2.Comprimento = _peca.Camisas_1.Comprimento;

            //Adiciona 10 na interna, diminui 9 interna, diminui 20 interna
            _peca.Isopor.Interna = _peca.Camisas_1.Interna + 10;
            _peca.Isopor.Externa = _peca.Camisas_1.Externa - 9;
            _peca.Isopor.Comprimento = _peca.Camisas_1.Comprimento;

            using (var contexto = new EfContext())
            {
                Peca.LimpaMaterial();

                #region EVA

                _peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                _peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                _peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 14).ToList<Material>(), 0);

                _peca.Eva.calc_Valores_Eva(_peca.Camisas_1.Interna + 3, _peca.Camisas_1.Comprimento, Peca.Type);

                 #endregion

                #region 1ª Camisa
                    _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                #endregion

                #region 2ª Camisa
                _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 16).ToList<Material>(), 20);
                _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                #endregion

                #region Isopor
                _peca.Isopor.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                _peca.Isopor.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                _peca.Isopor.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                #endregion

                #region Filamento
                _peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                _peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);
                _peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);
                _peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                _peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 12).ToList<Material>(), 0);
                #endregion

                #region Acabamento
                _peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                _peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                _peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 5).ToList<Material>(), 2);
                _peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 10);
                _peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                #endregion

                _peca.Camisas_1.Calc_Valores_Camisa();
                _peca.Camisas_2.Calc_Valores_Camisa();
                _peca.Isopor.Calc_Valores_Isopor(Peca.Type);
                _peca.Filamento.calc_Valores_Filamento(_peca.Camisas_1, _peca.Isopor);
                _peca.Acabamento.calc_Valores_Acabamento(_peca.Camisas_1, _peca.Isopor);

                if (Peca.Type == false)
                {
                    Peca.Filamento.Adiciona10pct();
                    Peca.Acabamento.Adiciona25pct();
                }



                _peca.Quantidade = 1;
                _peca.Calcula_Valores_Pecas();
            }
        }
    }
}
