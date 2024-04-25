using MahApps.Metro.Controls;
using Air_Sleeves.Dal;
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
