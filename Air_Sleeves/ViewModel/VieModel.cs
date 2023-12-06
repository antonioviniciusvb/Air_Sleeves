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
        private Peça peca;

  
        public Command ExecutaSimulacao { get; set; }

        public ViewModel()
        {

            Camisa = new Camisa(0, 0, 0, 1);

            peca = new Peça(new Camisa(0, 0, 0, 1),
                             new Camisa(0, 0, 0, 2),
                             new Isopor(0, 0, 0),
                             new Isopor(0, 0, 0),
                             new Filamento(),
                             new Acabamento(),
                             new Eva());

            ExecutaSimulacao = new Command(simula_Orcamento, () => { return Validacao.Camisa_1(Camisa) && Peca.Preco_Anel > 0 &&
                                                                                                          Peca.Preco_Embalagem > 0 &&
                                                                                                          Peca.Preco_Isopor > 0;
            });
        }

        public Peça Peca
        {
            get { return peca; }
        }


        private void simula_Orcamento()
        {
            //Camisa 1
            peca.Camisas_1.Interna = Camisa.Interna;
            peca.Camisas_1.Externa = Camisa.Externa;
            peca.Camisas_1.Comprimento = Camisa.Comprimento;

            //Acrescenta 50 mm caso seja Orçamento
            if (peca.Type == false)
                peca.Camisas_1.Comprimento += 50;

            //Adiciona 7 na interna, para a 2º camisa
            peca.Camisas_2.Interna = peca.Camisas_1.Interna + 7;
            peca.Camisas_2.Externa = peca.Camisas_1.Externa;
            peca.Camisas_2.Comprimento = peca.Camisas_1.Comprimento;

            //Adiciona 10 na interna, diminui 9 interna, diminui 20 interna
            peca.Selagem.Interna = peca.Colagem.Interna = peca.Camisas_1.Interna + 10;
            peca.Selagem.Externa = peca.Colagem.Externa = peca.Camisas_1.Externa - 8;
            peca.Selagem.Comprimento = peca.Colagem.Comprimento = peca.Camisas_1.Comprimento;

            using (var contexto = new EfContext())
            {
                Peca.LimpaMaterial();

                #region EVA

                peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 14).ToList<Material>(), 0);

                peca.Eva.calc_Valores_Eva(peca.Camisas_1.Interna + 3, peca.Camisas_1.Comprimento, Peca.Type);

                 #endregion

                #region 1ª Camisa
                    peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                #endregion

                #region 2ª Camisa
                peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 16).ToList<Material>(), 20);
                peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                #endregion

                #region Isopor
                peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);     
                peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                #endregion

                #region Filamento
                peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);
                peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);
                peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 12).ToList<Material>(), 0);
                #endregion

                #region Acabamento
                peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 5).ToList<Material>(), 2);
                peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 10);
                peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                #endregion

                peca.Camisas_1.CalculaValores();
                peca.Camisas_2.CalculaValores();
                peca.Selagem.CalculaValores(Peca.Type);
                peca.Colagem.CalculaValores(false);
                peca.Filamento.CalculaValores(peca.Camisas_1, peca.Selagem);
                peca.Acabamento.CalculaValores(peca.Camisas_1, peca.Selagem);

                if (Peca.Type == false)
                {
                    Peca.Filamento.Adiciona10pct();
                    Peca.Acabamento.Adiciona25pct();
                }



                peca.Quantidade = 1;
                peca.CalculaValores();
            }
        }
    }
}
