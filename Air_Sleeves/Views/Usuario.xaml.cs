using System;
using System.Data;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Air_Sleeves.Dal;
using System.Diagnostics;
using System.Data.Entity;

namespace Air_Sleeves.Views
{
    /// <summary>
    /// Lógica interna para Usuario.xaml
    /// </summary>
    public partial class Usuario : MetroWindow
    {
        private EfContext context = new EfContext();

        public Usuario()
        {
            InitializeComponent();
            //context.SaveChanges();

            //context.material

            //context.cliente.OrderBy(c => c.Nome).Load();
            //dt.ItemsSource = context.cliente.Local;
            dt.ItemsSource = context.material.Local.ToBindingList();

            //var teste = DalHelper.GetItems("User");

            //List<User> user = new List<User>();

            //foreach (DataRow item in teste.Rows)
            //{
            //    user.Add(new User
            //    {
            //        Id = int.Parse($"{item[0]}"),
            //        Nome = $"{item[1]}",
            //        Senha = $"{item[2]}"

            //    });

            //    Debug.Write($"{item[1]}");
            //}

            //dt.ItemsSource = user;
        }

    }
    
}
