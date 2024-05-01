using MahApps.Metro.Controls;
using Air_Sleeves.Dal;
using System.Windows.Markup;
using System.Globalization;
using AutoUpdaterDotNET;
using System;
using Newtonsoft.Json;
using System.Net;
using Air_Sleeves.Properties;
using System.Reflection;

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
            AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
            
            AutoUpdater.Start(
                "ftp://antonioviniciusvb@ftp.drivehq.com/Software/autoupdater.json",
                new NetworkCredential(Settings.Default.user, Settings.Default.pass),
                Assembly.GetExecutingAssembly());
            AutoUpdater.Synchronous = true;
            AutoUpdater.UpdateMode = Mode.Forced;
            AutoUpdater.TopMost = true;
            AutoUpdater.ExecutablePath = "setup.exe";


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

        private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        {
            dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
            args.UpdateInfo = new UpdateInfoEventArgs
            {
                CurrentVersion = json.version,
                DownloadURL = json.url,
                
            };
        }
    }
}
