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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Air_Sleeves.Dal;
using Air_Sleeves.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Markup;
using System.Globalization;

namespace Air_Sleeves
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        private ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            //Setando a culture atual
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            InitializeComponent();

            //C#
            using (var contexto = new EfContext())
            {
                contexto.SaveChanges();
            }

            this._viewModel = new ViewModel.ViewModel();
            DataContext = this._viewModel;
        }
    }
}
