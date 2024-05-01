using Air_Sleeves.Dal;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Shapes;

namespace Air_Sleeves.Views
{
    /// <summary>
    /// Lógica interna para Produtos.xaml
    /// </summary>
    /// 
    public partial class Produtos : Window
    {

        EfContext context = new EfContext();

        public Produtos()
        {
            InitializeComponent();

            var query = (from m in context.material
                         select m).ToList<Model.Material>();

            dt.ItemsSource = query;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            context.SaveChanges();
            MessageBox.Show($"Os próximos cálculos utilizaram a base de dados atualizada.",
                            "Atualização realizada com sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
