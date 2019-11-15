using Air_Sleeves.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Air_Sleeves.Model
{
    public class Material
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public Und_Medida Und_Medida { get; set; }
        public long Id_Und_Medida { get; set; }
        public int Medida { get; set; }
    }
}
