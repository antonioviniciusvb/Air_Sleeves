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

namespace Air_Sleeves.ViewModel
{
    public class ViewModel:BaseInotifyPropertyChanged
    {
        private Camisa _c1, _c2, _c3;
        private Filamento _fil;
        private Acabamento _acabamento;
        private Eva _eva;
        private Peça _peca;
        private bool adicionaMaterias = false;

        public Command ExecutaSimulacao { get; set; }

        public ViewModel()
        {

            //Camisa 1
            _c1 = new Camisa(0, 0, 0, 1);
            _c2 = new Camisa(0, 0, 0, 2);
            _eva = new Eva();
            _c3 = new Camisa(0, 0, 0, 3);
            _fil = new Filamento();
            _acabamento = new Acabamento();
           _peca = new Peça(_c1, _c2, _c3, _fil, _acabamento, _eva);


            ExecutaSimulacao = new Command(simula_Orcamento, () => { return Util.Validacao.Camisa_1(_peca.Camisas_1); });


        }

       

        public Peça Peca
        {
            get { return _peca; }
        }


        private void simula_Orcamento()
        {
            //Adiciona 7 na interna, para a 2º camisa
            //_c2 = new Camisa(_c1.Interna + 7, _c1.Externa, _c1.Comprimento, 2);
            _peca.Camisas_2.Interna = _peca.Camisas_1.Interna + 7;
            _peca.Camisas_2.Externa = _peca.Camisas_1.Externa;
            _peca.Camisas_2.Comprimento = _peca.Camisas_1.Comprimento;


            //Adiciona 11 na interna, diminui 9 interna, diminui 20 interna
            //_c3 = new Camisa(_c1.Interna + 11, _c1.Externa - 9, _c1.Comprimento - 20, 3);

            _peca.Camisas_3.Interna = _peca.Camisas_1.Interna + 11;
            _peca.Camisas_3.Externa = _peca.Camisas_1.Externa - 9;
            _peca.Camisas_3.Comprimento = _peca.Camisas_1.Comprimento - 20;

            _peca.Eva.calc_Valores_Eva(_peca.Camisas_1.Interna, _peca.Camisas_1.Comprimento);
            

            using (var contexto = new EfContext())
            {
                decimal vlr_Isopor = (decimal)contexto.material.Where(x => x.Id == 10).Select(x => x.Preco).Distinct().Sum();

                if (adicionaMaterias == false)
                {
                    adicionaMaterias = true;

                    #region 1ª Camisa
                    _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                    _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                    _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                    _peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                    #endregion

                    #region 2ª Camisa
                    _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                    _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                    _peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                    #endregion

                    #region 3ª Camisa
                    _peca.Camisas_3.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 95);
                    _peca.Camisas_3.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                    _peca.Camisas_3.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 5);
                    _peca.Camisas_3.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                    
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

                }

                _peca.Camisas_1.Calc_Valores_Camisa();
                _peca.Camisas_2.Calc_Valores_Camisa();
                _peca.Camisas_3.Calc_Valores_Camisa();
                _peca.Filamento.calc_Valores_Filamento(_peca.Camisas_1, _peca.Camisas_3, vlr_Isopor);
                _peca.Acabamento.calc_Valores_Acabamento(_peca.Camisas_1, _peca.Camisas_3);

                _peca.Quantidade = 1;
                _peca.Calcula_Valores_Pecas();

            }


        }
    }
}
