using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Air_Sleeves.Model;
using Air_Sleeves.Dal;

namespace Air_Sleeves.ViewModel
{
    public class ViewModel:BaseInotifyPropertyChanged
    {
        private Camisa _camisa_1, _camisa_2, _camisa_3;

        public ViewModel()
        {


            this._camisa_1 = new Camisa(105,156,1200, 1);

            //Adiciona 7 na interna, para a 2º camisa
            this._camisa_2 = new Camisa(105 + 7, 156, 1200, 2);


            //Adiciona 11 na interna, diminui 9 interna, diminui 20 interna
            this._camisa_3 = new Camisa(105 + 11, 156 - 9, 1200-20, 3);

            using (var contexto = new EfContext())
            {
                #region 1ª Camisa
                                               
                this._camisa_1.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                this._camisa_1.AddMaterial(contexto.material.Where(x => x.Id == 2).ToList<Material>(), 80);

                this._camisa_1.AddMaterial(contexto.material.Where(x => x.Id == 3).ToList<Material>(), 5);

                this._camisa_1.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);
                #endregion

                _camisa_1.calc_Valores();

                #region 2ª Camisa
                this._camisa_2.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 100);

                this._camisa_2.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);

                this._camisa_2.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                _camisa_2.calc_Valores();
                #endregion

                #region 3ª Camisa
                this._camisa_3.AddMaterial(contexto.material.Where(x => x.Id == 1).ToList<Material>(), 95);

                this._camisa_3.AddMaterial(contexto.material.Where(x => x.Id == 4).ToList<Material>(), 20);

                this._camisa_3.AddMaterial(contexto.material.Where(x => x.Id == 6).ToList<Material>(), 5);

                this._camisa_3.AddMaterial(contexto.material.Where(x => x.Id == 11).ToList<Material>(), 0);

                _camisa_3.calc_Valores();
                #endregion

            }

        }

        public Camisa Camisas
        {
            get { return this._camisa_1; }
        }
    }
}
