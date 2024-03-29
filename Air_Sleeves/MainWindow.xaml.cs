﻿using System;
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

        //private EfContext context = new EfContext();
        private ViewModel.ViewModel _viewModel;

        public MainWindow()
        {
            //Setando a culture atual
            this.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            InitializeComponent();
            //DalHelper.CriarBancoSQLite();
            //DalHelper.CriarTabelaSQlite();

            //C#
            using (var contexto = new EfContext())
            {
                //foreach (var c in contexto.cliente)
                //    contexto.cliente.Remove(c);

                contexto.SaveChanges();

                //    contexto.cliente.Add(new Cliente() { Nome = "Novo Cliente EF" });
                //contexto.SaveChanges();

                //    //// C#
                //    //var cliente = contexto.cliente.First();
                //    //cliente.Nome = "Novo Cliente EF Alterado";
                //    //contexto.SaveChanges();

                //    ////C#


                var query = from m in contexto.material
                            where m.Preco >= 50 && m.Nome.Substring(0, 1) == "H"
                            select m;

                //txtTeste.AppendText($"----- >= 50 -----------{"\n"}");

                //foreach (var item in query)
                //{
                //    txtTeste.AppendText($"{item.Nome} - R$ {item.Preco}{"\n"}");
                //}


                //foreach (var material in contexto.material)
                //{
                //    txtTeste.AppendText($"Nome do Cliente: {material.Nome}-{material.Preco}" + Environment.NewLine);
                //}
            }

            this._viewModel = new ViewModel.ViewModel();
            DataContext = this._viewModel;
        }
    }
}
