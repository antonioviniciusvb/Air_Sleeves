using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Air_Sleeves.Views;

namespace Air_Sleeves.Views
{
    /// <summary>
    /// Interação lógica para Gerenciamento.xam
    /// </summary>
    public partial class Gerenciamento : UserControl
    {
        public Gerenciamento()
        {
            InitializeComponent();
        }

        private void TileUsuario(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario.Show();
        }
    }
}
