using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PropertyChanged;
using System.Threading.Tasks;
using Air_Sleeves.Util;

namespace Air_Sleeves.Model
{
    public class Camisa: INotifyPropertyChanged, IDataErrorInfo
    {
        private const decimal pi = 3.1416M;

        public Camisa(int interna, int externa, int com, int numero_Camisa)
        {
            Interna = interna;
            Externa = externa;
            Comprimento = com;
            this._materiais = new List<Material>();
            this._porcentagem = new Dictionary<long, decimal>();
            this.num_Camisa = numero_Camisa;

            //this._interna = vl;
        }


        public int num_Camisa { get; set; }

        private List<Material> _materiais;
        private Dictionary<long, decimal> _porcentagem;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Error => throw new NotImplementedException();

        public decimal Interna { get; set; }

        public decimal Externa { get; set; }

        public decimal Comprimento { get; set; }

        public void AddMaterial(List<Material> material, decimal porcentagem)
        {
            this._materiais.Add(material.First());
            this._porcentagem.Add(material.First().Id, porcentagem);
        }


        public decimal Composto { get; set; }

        #region Cardaco
        public decimal MetrosCardaco { get; set; }
        public decimal PrecoCardaco { get; set; }
        public decimal PesoCardaco { get; set; }

   
        public void CalcMetrosCardaco()
        {
            if(num_Camisa != 3)
              this.MetrosCardaco = Math.Ceiling((pi * this.Interna * (this.Comprimento / 32)) / 100);
            else
                this.MetrosCardaco = Math.Ceiling((pi * this.Externa * (this.Comprimento / 32)) / 1000);
        }

        #endregion

        #region Resina
        public decimal PrecoResina { get; set; }
        public decimal PesoResina { get; set; }
        #endregion

        #region HL918

        public decimal PrecoHl918 { get; set; }
        public decimal PesoHl918 { get; set; }

        #endregion

        #region A78

        public decimal PrecoA78 { get; set; }
        public decimal PesoA78 { get; set; }

        #endregion

        #region HT231

        public decimal PrecoHT231 { get; set; }
        public decimal PesoHT231 { get; set; }

        #endregion

        #region ANTI-BOLHA
        public decimal PrecoAntiBolha { get; set; }
        public decimal PesoAntiBolha { get; set; }
        public decimal PesoK10 { get; private set; }
        public decimal PrecoK10 { get; private set; }
        #endregion

        public void calc_Valores()
        {
            //Percentual Total
            decimal pct_Total = _porcentagem.Select(x => x.Value).Sum();

            //Cardaço
            int id = 11;

            if (_materiais.Any(x => x.Id == id))
            {
                //Tenho que verificar pq muda da 3º camisa para as demais
                decimal fatorComposto = num_Camisa != 3 ? 0.006M : 0.02M;


                var preco_Cardaco = preco_Material(id);

                CalcMetrosCardaco();
                this.PesoCardaco = Calculo.Multiplica(this.MetrosCardaco, 0.007M, 3);
                this.PrecoCardaco = Calculo.DivideMultiplica(this.MetrosCardaco, 50, preco_Cardaco, 2);

                this.Composto = Calculo.Multiplica(this.MetrosCardaco, fatorComposto, 3);
            }

            //Resina
            id = 1;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_Resina = preco_Material(id);
                var pct_Resina = percentual_Material(id);

                this.PesoResina = Calculo.MultiplicaDivide(this.Composto, pct_Resina, pct_Total, 3);
                this.PrecoResina = Calculo.Multiplica(PesoResina, preco_Resina, 2);
            }

            //HL918
            id = 2;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_HL918 = preco_Material(id);
                var pct_HL918 = percentual_Material(id);

                this.PesoHl918 = Calculo.MultiplicaDivide(this.Composto, pct_HL918, pct_Total, 3);
                this.PrecoHl918 = Calculo.Multiplica(PesoHl918, preco_HL918, 2);
            }

            //A78
            id = 3;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_A78 = preco_Material(id);
                var pct_A78 = percentual_Material(id);

                this.PesoA78 = Calculo.MultiplicaDivide(this.Composto, pct_A78, pct_Total, 3);
                this.PrecoA78 = Calculo.Multiplica(PesoA78, preco_A78, 2);
            }

            //HT231
            id = 4;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_HT231 = preco_Material(id);
                var pct_HT231 = percentual_Material(id);

                this.PesoHT231 = Calculo.MultiplicaDivide(this.Composto, pct_HT231, pct_Total, 3);
                this.PrecoHT231 = Calculo.Multiplica(this.PesoHT231, preco_HT231, 2);
            }

            //ANTI-BOLHA
            id = 5;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_ANTI_BOLHA = preco_Material(id);
                var pct_ANTI_BOLHA = percentual_Material(id);

                this.PesoAntiBolha  = Calculo.MultiplicaDivide(this.Composto, pct_ANTI_BOLHA, pct_Total, 3);
                this.PrecoAntiBolha = Calculo.Multiplica(this.PesoAntiBolha, preco_ANTI_BOLHA, 2);
            }

            //k10
            id = 6;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_K10 = preco_Material(id);
                var pct_K10 = percentual_Material(id);

                this.PesoK10 = Calculo.MultiplicaDivide(this.Composto, pct_K10, pct_Total, 3);
                this.PrecoK10 = Calculo.Multiplica(this.PesoK10, preco_K10, 2);
            }


        }

        private decimal preco_Material(int id)
        {
           return (decimal)_materiais.Where(x => x.Id == id).Select(x => x.Preco).Distinct().Sum();
        }

        private decimal percentual_Material(int id)
        {
            return (decimal)_porcentagem.Where(x => x.Key == id).Select(x => x.Value).Distinct().Sum();
        }



        #region DataError
        public string this[string columnName]
        {
            get
            {
                if (columnName.Equals("Interna"))
                {
                    if(Interna <= 0)
                    {
                        Interna = 0;
                        return "Valor deve ser maior que zero";
                    }
                }

                return null;
            }
        }
        #endregion
    }
}
