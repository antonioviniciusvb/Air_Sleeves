using System;
using System.Linq;
using System.Text;
using PropertyChanged;
using Air_Sleeves.Util;
using System.ComponentModel;

namespace Air_Sleeves.Model
{
    public class Eva:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Eva()
        {
        }

        private void calc_Eva()
        {
            this.Preco =  Math.Round(((Largura * Comprimento * 13.0M) / 2.5M) / 1000.0M, 2);
        }

        private void calc_Largura(decimal c_interna)
        {
            this.Largura =  Math.Round(((c_interna * Calculo.pi) + 40) / 1000.0M,3);
        }

        public void calc_Valores_Eva(decimal interna, decimal comprimento)
        {
            calc_Largura(interna);
            this.Comprimento = comprimento;
            calc_Eva();
        }



        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Preco { get; set; }
    }
}
