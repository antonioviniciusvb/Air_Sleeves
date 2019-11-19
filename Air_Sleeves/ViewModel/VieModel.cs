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


            ExecutaSimulacao = new Command(simula_Orcamento, () => { return Util.Validacao.Camisa_1(_c1); });

        }

        public Camisa Camisas_1
        {
            get { return _c1; }
        }

        public Camisa Camisas_2
        {
            get { return _c2; }
        }

        public Camisa Camisas_3
        {
            get { return _c3; }
        }

        public Eva Eva
        {
            get { return _eva; }
        }

        public Filamento Filamento
        {
            get { return _fil; }
        }

        public Acabamento Acabamento
        {
            get { return _acabamento; }
        }


        private void simula_Orcamento()
        {
            //Adiciona 7 na interna, para a 2º camisa
            //_c2 = new Camisa(_c1.Interna + 7, _c1.Externa, _c1.Comprimento, 2);
            _c2.Interna = _c1.Interna + 7;
            _c2.Externa = _c1.Externa;
            _c2.Comprimento = _c1.Comprimento;

            //Adiciona 11 na interna, diminui 9 interna, diminui 20 interna
            //_c3 = new Camisa(_c1.Interna + 11, _c1.Externa - 9, _c1.Comprimento - 20, 3);

            _c3.Interna = _c1.Interna + 11;
            _c3.Externa = _c1.Externa - 9;
            _c3.Comprimento = _c1.Comprimento - 20;

            //E.V.A
            _eva.calc_Valores_Eva(_c1.Interna, _c1.Comprimento);


            using (var contexto = new EfContext())
            {
                #region 1ª Camisa

                _c1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                _c1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                _c1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                _c1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                _c1.Calc_Valores_Camisa();

                #endregion

                #region 2ª Camisa
                _c2.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                _c2.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);

                _c2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                _c2.Calc_Valores_Camisa();
                #endregion

                #region 3ª Camisa
                _c3.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 95);

                _c3.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);

                _c3.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 5);

                _c3.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                _c3.Calc_Valores_Camisa();
                #endregion

                #region Filamento

                decimal vlr_Isopor = (decimal)contexto.material.
                                       Where(x => x.Id == 10).
                                       Select(x => x.Preco).Distinct().Sum();

                //Filamento
                //_fil = new Filamento(_c1, _c3, vlr_Isopor);

                _fil.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                _fil.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);
                _fil.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);
                _fil.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                _fil.AddMaterial(contexto.material.Where(x => x.Id == 12).ToList<Material>(), 0);

                _fil.calc_Valores_Filamento(_c1, _c3, vlr_Isopor);
                #endregion

                #region Acabamento
                //_acabamento = new Acabamento(_c1, _c3);
                _acabamento.Calc_Valores_Acabamento(_c1, _c3);

                _acabamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                _acabamento.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                _acabamento.AddMaterial(contexto.material.Where(x => x.Id == 5).ToList<Material>(), 2);
                _acabamento.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 10);
                _acabamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);

                _acabamento.Calc_Valores_Acabamento();

                #endregion
            }

            Peça peca = new Peça(_c1, _c2, _c3, _eva, _fil, _acabamento);
        }
    }
}
