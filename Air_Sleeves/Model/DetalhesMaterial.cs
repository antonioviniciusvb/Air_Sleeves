using System.ComponentModel;
using System.Linq;
using Air_Sleeves.Util;
using System.Collections.Generic;
using System;

namespace Air_Sleeves.Model
{
    public class DetalhesMaterial : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Error => throw new NotImplementedException();
        public decimal Composto_Resina { get; set; }
        public decimal Composto_Resina_Fio { get; set; }

        #region Resina 102
        public decimal Preco_Resina { get; set; }
        public decimal Peso_Resina { get; set; }

        #endregion

        #region Resina 101F
        public decimal Preco_Resina101F { get; set; }
        public decimal Peso_Resina101F { get; set; }

        #endregion

        #region HL918

        public decimal Preco_HL918 { get; set; }
        public decimal Peso_HL918 { get; set; }

        #endregion

        #region A78

        public decimal Preco_A78 { get; set; }
        public decimal Peso_A78 { get; set; }

        #endregion

        #region HT231

        public decimal Preco_HT231 { get; set; }
        public decimal Peso_HT231 { get; set; }

        public decimal Preco_HT231_2 { get; set; }
        public decimal Peso_HT231_2 { get; set; }

        #endregion

        #region HT356

        public decimal Preco_HT356 { get; set; }
        public decimal Peso_HT356 { get; set; }

        #endregion

        #region ANTI-BOLHA
        public decimal Preco_AntiBolha { get; set; }
        public decimal Peso_AntiBolha { get; set; }
        #endregion

        #region K10
        public decimal Peso_K10 { get; set; }
        public decimal Preco_K10 { get; set; }
        #endregion

        #region PIGMENTO
        public decimal Preco_Pigmento { get; set; }
        public decimal Peso_Pigmento { get; set; }
        #endregion

        #region FIO
        public decimal Peso_Fio { get; set; }
        public decimal Preco_Fio { get; set; }
        #endregion

        #region EVA
        public decimal Preco_EVA { get; set; }
        #endregion

        #region Cardaco
        public decimal Metros_Cadarco { get; set; }
        public decimal Bobinas_Cadarco { get; set; }
        public decimal Preco_Cadarco { get; set; }
        public decimal Peso_Cadarco { get; set; }
        #endregion

        public decimal Preco_Total { get; set; }
        public decimal Peso_Total { get; set; }

        public List<Material> _materiais;
        private Dictionary<long, decimal> _porcentagem;

        public decimal Pct_Total_Materiais
        {
            get { return calc_Pct_Total_Materiais(); }
        }

        public DetalhesMaterial()
        {
            this._materiais = new List<Material>();
            this._porcentagem = new Dictionary<long, decimal>();
        }

        public void AddMaterial(List<Material> material, decimal porcentagem)
        {
            this._materiais.Add(material.First());
            this._porcentagem.Add(material.First().Id, porcentagem);
        }

        public void LimpaMaterial()
        {
            this._materiais.Clear();
            this._porcentagem.Clear();
        }

        public decimal preco_Material(int id)
        {
            return (decimal)_materiais.Where(x => x.Id == id).Select(x => x.Preco).Distinct().Sum();
        }

        public decimal percentual_Material(int id)
        {
            return (decimal)_porcentagem.Where(x => x.Key == id).Select(x => x.Value).Distinct().Sum();
        }

        public decimal calc_Pct_Total_Materiais()
        {
            //Percentual Total
            decimal pct_Total = _porcentagem.Select(x => x.Value).Sum();
            return pct_Total;
        }

        public virtual void CalculaItens()
        {
            //Resina
            int id = 1;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_Resina = preco_Material(id);
                var pct_Resina = percentual_Material(id);

                Peso_Resina = Calculo.MultiplicaDivide(Composto_Resina, pct_Resina, Pct_Total_Materiais, 3);
                Preco_Resina = Calculo.Multiplica(Peso_Resina, preco_Resina, 2);
            }

            //HL918
            id = 2;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_HL918 = preco_Material(id);
                var pct_HL918 = percentual_Material(id);

                Peso_HL918 = Calculo.MultiplicaDivide(Composto_Resina, pct_HL918, Pct_Total_Materiais, 3);
                Preco_HL918 = Calculo.Multiplica(Peso_HL918, preco_HL918, 2);
            }

            //A78
            id = 3;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_A78 = preco_Material(id);
                var pct_A78 = percentual_Material(id);

                Peso_A78 = Calculo.MultiplicaDivide(Composto_Resina, pct_A78, Pct_Total_Materiais, 3);
                Preco_A78 = Calculo.Multiplica(Peso_A78, preco_A78, 2);
            }

            //HT231
            id = 4;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_HT231 = preco_Material(id);
                var pct_HT231 = percentual_Material(id);

                Peso_HT231 = Calculo.MultiplicaDivide(Composto_Resina, pct_HT231, Pct_Total_Materiais, 3);
                Preco_HT231 = Calculo.Multiplica(Peso_HT231, preco_HT231, 2);
            }

            //ANTI-BOLHA
            id = 5;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_ANTI_BOLHA = preco_Material(id);
                var pct_ANTI_BOLHA = percentual_Material(id);

                Peso_AntiBolha = Calculo.MultiplicaDivide(Composto_Resina, pct_ANTI_BOLHA, Pct_Total_Materiais, 3);

                Preco_AntiBolha = Calculo.Multiplica(Peso_AntiBolha, preco_ANTI_BOLHA, 2);
            }

            //k10
            id = 6;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_K10 = preco_Material(id);
                var pct_K10 = percentual_Material(id);

                Peso_K10 = Calculo.MultiplicaDivide(Composto_Resina, pct_K10, Pct_Total_Materiais, 3);
                Preco_K10 = Calculo.Multiplica(Peso_K10, preco_K10, 2);
            }

            //Pigmento
            id = 7;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_Pigmento = preco_Material(id);
                var pct_Pigmento = percentual_Material(id);

                Peso_Pigmento = Calculo.MultiplicaDivide(Composto_Resina, pct_Pigmento, Pct_Total_Materiais, 3);
                Preco_Pigmento = Calculo.Multiplica(Peso_Pigmento, preco_Pigmento, 2);
            }

            //Resina 101F
            id = 15;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_Resina101F = preco_Material(id);
                var pct_Resina101F = percentual_Material(id);

                Peso_Resina101F = Calculo.MultiplicaDivide(Composto_Resina, pct_Resina101F, Pct_Total_Materiais, 3);
                Preco_Resina101F = Calculo.Multiplica(Peso_Resina101F, preco_Resina101F, 2);

            }


            //HT356
            id = 16;

            if (_materiais.Any(x => x.Id == id))
            {
                var preco_HT356 = preco_Material(id);
                var pct_HT356 = percentual_Material(id);

                Peso_HT356 = Calculo.MultiplicaDivide(Composto_Resina, pct_HT356, Pct_Total_Materiais, 3);
                Preco_HT356 = Calculo.Multiplica(Peso_HT356, preco_HT356, 2);
            }

            Preco_Total = Preco_Total + Preco_Resina + Preco_Resina101F + Preco_Pigmento + Preco_K10 + Preco_AntiBolha + Preco_HT231 + Preco_A78 + Preco_HL918 + Preco_HT356;
            Peso_Total = Peso_Total + Peso_Resina + Peso_Resina101F + Peso_Pigmento + Peso_K10 + Peso_AntiBolha + Peso_HT231 + Peso_A78 + Peso_HL918 + Peso_HT356;

        }

        public void CalculaEVA(decimal fator)
        {
            var p_EVA = preco_Material(14);

            Preco_EVA = Math.Round((Calculo.Multiplica(fator, p_EVA, 2) / 1400) * 2, 2);

            //if (type == false)
            //    Preco_EVA *= 2;

            Preco_Total += Preco_EVA;
        }

        public void LimpaTotais()
        {
            Peso_Total = 0;
            Preco_Total = 0;
        }

        public virtual void LimpaValores()
        {
            Composto_Resina = 0;
            Composto_Resina_Fio = 0;
            Preco_Resina = 0;
            Peso_Resina = 0;
            Preco_Resina101F = 0;
            Peso_Resina101F = 0;
            Preco_HL918 = 0;
            Peso_HL918 = 0;
            Preco_A78 = 0;
            Peso_A78 = 0;
            Preco_HT231 = 0;
            Peso_HT231 = 0;
            Preco_HT231_2 = 0;
            Peso_HT231_2 = 0;
            Preco_HT356 = 0;
            Peso_HT356 = 0;
            Preco_AntiBolha = 0;
            Peso_AntiBolha = 0;
            Peso_K10 = 0;
            Preco_K10 = 0;
            Preco_Pigmento = 0;
            Peso_Pigmento = 0;
            Peso_Fio = 0;
            Preco_Fio = 0;
            Preco_EVA = 0;
            Metros_Cadarco = 0;
            Bobinas_Cadarco = 0;
            Preco_Cadarco = 0;
            Peso_Cadarco = 0;
            Peso_Total = 0;
            Preco_Total = 0;
        }

        #region DataError
        public string this[string columnName]
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
