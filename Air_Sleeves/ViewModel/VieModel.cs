using System.Linq;
using Air_Sleeves.Model;
using Air_Sleeves.Dal;
using Air_Sleeves.Util;
using System.Windows;

namespace Air_Sleeves.ViewModel
{
    public class ViewModel : BaseInotifyPropertyChanged
    {
        public Peca Peca { get; set; }
        public Peca AuxPeca {  get; set; }
        public bool CalculaSegundaCamisa { get; set; }
        public bool CalculaEva { get; set; }
        public bool CalculaIsopor { get; set; }
        public bool CalculaFilamento { get; set; }
        public bool CalculaAcabamento { get; set; }

        #region Visibilidade dos Componentes
        public Visibility ExibirSegundaCamisa
        {
            get
            {
                return CalculaSegundaCamisa ? Visibility.Visible : Visibility.Collapsed;
            }

        }

        public Visibility ExibirEva
        {
            get
            {
                return CalculaEva ? Visibility.Visible : Visibility.Collapsed;
            }

        }

        public Visibility ExibirIsopor
        {
            get
            {
                return CalculaIsopor ? Visibility.Visible : Visibility.Collapsed;
            }

        }

        public Visibility ExibirFilamento
        {
            get
            {
                return CalculaFilamento ? Visibility.Visible : Visibility.Collapsed;
            }

        }

        public Visibility ExibirAcabamento
        {
            get
            {
                return CalculaAcabamento ? Visibility.Visible : Visibility.Collapsed;
            }

        }

        #endregion

        public Command ExecutaSimulacao { get; set; }

        public Command Limpar { get; set; }

        public ViewModel()
        {
            CalculaSegundaCamisa = CalculaEva = CalculaIsopor = CalculaFilamento = CalculaAcabamento = true;

            Peca = new Peca();
            AuxPeca = new Peca();

            AuxPeca.Camisas_1.Voltas = 10;
            AuxPeca.Camisas_2.Voltas = 8;
            AuxPeca.Selagem.Voltas = 2;


            ExecutaSimulacao = new Command(simula_Orcamento, () =>
            {
                return Validacao.Camisa_1(AuxPeca.Camisas_1);
            });

            Limpar = new Command(Clear, () => { return true; });
        }
        private void simula_Orcamento()
        {
            Clear();

            //Camisa 1
            Peca.Camisas_1.Interna = AuxPeca.Camisas_1.Interna;
            Peca.Camisas_1.Externa = AuxPeca.Camisas_1.Externa;
            Peca.Camisas_1.Comprimento = AuxPeca.Camisas_1.Comprimento;
            Peca.Camisas_1.Voltas = AuxPeca.Camisas_1.Voltas;

            //Acrescenta 50 mm caso seja Orçamento
            if (Peca.Type == false)
                Peca.Camisas_1.Comprimento += 50;

            if (CalculaSegundaCamisa)
            {
                Peca.Camisas_2.Interna = Peca.Camisas_1.Interna + 7;
                Peca.Camisas_2.Externa = Peca.Camisas_1.Externa;
                Peca.Camisas_2.Comprimento = Peca.Camisas_1.Comprimento;
                Peca.Camisas_2.Voltas = AuxPeca.Camisas_2.Voltas;
            }
            else
                Peca.Camisas_2.Interna = Peca.Camisas_2.Externa =
                Peca.Camisas_2.Comprimento = Peca.Camisas_2.Voltas = 0;

            if (CalculaIsopor)
            {
                Peca.Selagem.Interna = Peca.Camisas_1.Interna + 10;
                Peca.Selagem.Externa = Peca.Camisas_1.Externa - 8;
                Peca.Selagem.Comprimento = Peca.Camisas_1.Comprimento;
                Peca.Selagem.Voltas = AuxPeca.Selagem.Voltas;

                Peca.Colagem.Interna = Peca.Camisas_1.Interna + 10;
                Peca.Colagem.Externa = Peca.Camisas_1.Externa - 8;
                Peca.Colagem.Comprimento = Peca.Camisas_1.Comprimento;
                Peca.Colagem.Voltas = AuxPeca.Colagem.Voltas;
            }
            else
                Peca.Selagem.Interna = Peca.Selagem.Externa =
                Peca.Selagem.Comprimento = Peca.Selagem.Voltas =
                Peca.Colagem.Interna = Peca.Colagem.Externa =
                Peca.Colagem.Comprimento = Peca.Colagem.Voltas = 0;

            using (var contexto = new EfContext())
            {
                #region EVA
                if (CalculaEva)
                {

                    Peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                    Peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                    Peca.Eva.AddMaterial(contexto.material.Where(x => x.Id == 14).ToList<Material>(), 0);

                    Peca.Eva.Interna = Peca.Camisas_1.Interna + 3;
                    Peca.Eva.Comprimento = Peca.Camisas_1.Comprimento;

                    Peca.Eva.CalculaValores();

                }

                #endregion

                #region 1ª Camisa
                Peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                Peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                Peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                Peca.Camisas_1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                Peca.Camisas_1.CalculaValores();
                #endregion

                #region 2º Camisa
                if (CalculaSegundaCamisa)
                {
                    #region 2ª Camisa
                    Peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                    Peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 16).ToList<Material>(), 20);
                    Peca.Camisas_2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                    #endregion

                    Peca.Camisas_2.CalculaValores();
                }
                #endregion

                #region Isopor
                if (CalculaIsopor)
                {
                    #region Isopor
                    Peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                    Peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 16).ToList<Material>(), 20);
                    Peca.Colagem.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                    Peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 15).ToList<Material>(), 100);
                    Peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                    Peca.Selagem.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                    #endregion

                    Peca.Colagem.CalculaColagem();
                    Peca.Selagem.CalculaSelagem();
                    
                }

                #endregion

                #region Filamento
                if (CalculaFilamento)
                {
                    Peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                    Peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);
                    Peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);
                    Peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                    Peca.Filamento.AddMaterial(contexto.material.Where(x => x.Id == 12).ToList<Material>(), 0);

                    Peca.Filamento.CalculaValores(Peca.Camisas_1, Peca.Selagem);
                }
                #endregion

                #region Acabamento

                if (CalculaAcabamento)
                {
                    Peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);
                    Peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);
                    Peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 5).ToList<Material>(), 2);
                    Peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 10);
                    Peca.Acabamento.AddMaterial(contexto.material.Where(x => x.Id == 7).ToList<Material>(), 2);
                    
                    Peca.Acabamento.CalculaValores(Peca.Camisas_1, Peca.Selagem);
                }

                #endregion

                Peca.Quantidade = 1;
                Peca.CalculaValores();

            }
        }

        private void Clear()
        {
            Peca.Limpar();
        }
    }
}
