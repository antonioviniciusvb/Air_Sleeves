using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_Sleeves.Model
{
    public class Peça:DetalhesMaterial
    {
        private List<DetalhesMaterial> c;

        public Peça(Camisa camisa_1, Camisa camisa_2, Camisa camisa_3, Eva eva, Filamento filamento, Acabamento acabamento)
        {
            c = new List<DetalhesMaterial>();
            c.Add(camisa_1);
            c.Add(camisa_2);
            c.Add(camisa_3);
            c.Add(filamento);
            c.Add(acabamento);

            //c[0].Preco_Resina

            Preco_Resina = c.Sum(x => x.Preco_Resina);
            Preco_HL918 = c.Sum(x => x.Preco_HL918);
            Preco_A78 = c.Sum(x => x.Preco_A78);
            Preco_HT231 = c.Sum(x => x.Preco_HT231);
            Preco_AntiBolha = c.Sum(x => x.Preco_AntiBolha);
            Preco_K10 = c.Sum(x => x.Preco_K10);
            Preco_Pigmento = c.Sum(x => x.Preco_Pigmento);
            Preco_Cadarco = c.Sum(x => x.Preco_Cadarco);
            Preco_Fio = c.Sum(x => x.Preco_Fio);

            Peso_HL918 = c.Sum(x => x.Peso_HL918);
            Peso_A78 = c.Sum(x => x.Peso_A78);
            Peso_HT231 = c.Sum(x => x.Peso_HT231);
            Peso_AntiBolha = c.Sum(x => x.Peso_AntiBolha);
            Peso_K10 = c.Sum(x => x.Peso_K10);
            Peso_Pigmento = c.Sum(x => x.Peso_Pigmento);
            Peso_Cadarco = c.Sum(x => x.Peso_Cadarco);
            Peso_Fio = c.Sum(x => x.Peso_Fio);

            Metros_Cadarco = c.Sum(x => x.Metros_Cadarco);

            Composto_Resina = c.Sum(x => x.Peso_Resina);
            Preco_Total = c.Sum(x => x.Preco_Total) + eva.Preco;
        }


    }
}
