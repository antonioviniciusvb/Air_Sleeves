using System.Data.Entity;
using SQLite.CodeFirst;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Air_Sleeves.Model;

namespace Air_Sleeves.Dal
{
    public class EfContext: DbContext
    {
        public DbSet<Cliente> cliente { get; set; }
        public DbSet<Material> material { get; set; }
        public DbSet<Und_Medida> und_Medida { get; set; }
        public DbSet<User> user { get; set; }

        public EfContext() : base()
        {
            //Caso não tenha itens, preencha com valores default
            if (!(und_Medida.Any()))
            {
                und_Medida.Add(new Und_Medida { Id = 1, Nome = "Quilograma", Simbolo = "kg" });
                und_Medida.Add(new Und_Medida { Id = 2, Nome = "Litro", Simbolo = "l" });
                und_Medida.Add(new Und_Medida { Id = 3, Nome = "Milímetro", Simbolo = "mm" });
                und_Medida.Add(new Und_Medida { Id = 4, Nome = "Metro", Simbolo = "m" });
            }

            //Caso não tenha itens, preencha com valores default
            if (!(material.Any()))
            {
                material.Add(new Material { Id = 1, Id_Und_Medida = 1, Nome = "Resina 102", Preco = 109.00F });

                material.Add(new Material { Id = 2, Id_Und_Medida = 1, Nome = "HL918", Preco = 87.00F });
                material.Add(new Material { Id = 3, Id_Und_Medida = 1, Nome = "A78", Preco = 177.00F });
                material.Add(new Material { Id = 4, Id_Und_Medida = 1, Nome = "HT231", Preco = 82.00F });
                material.Add(new Material { Id = 5, Id_Und_Medida = 1, Nome = "ANTI BOLHA", Preco = 370.00F });
                material.Add(new Material { Id = 6, Id_Und_Medida = 1, Nome = "K10", Preco = 58.00F });
                material.Add(new Material { Id = 7, Id_Und_Medida = 1, Nome = "PIGMENTO", Preco = 65.00F });
                material.Add(new Material { Id = 8, Id_Und_Medida = 2, Nome = "COLA", Preco = 5.00F });
                material.Add(new Material { Id = 9, Id_Und_Medida = 1, Nome = "ANEL", Preco = 0.00F });
                material.Add(new Material { Id = 10, Id_Und_Medida = 1, Nome = "ISOPOR", Preco = 0.00F });
                material.Add(new Material { Id = 11, Id_Und_Medida = 4, Nome = "CADARÇO", Preco = 30.00F });
                material.Add(new Material { Id = 12, Id_Und_Medida = 4, Nome = "FIO", Preco = 25.00F });
                material.Add(new Material { Id = 13, Id_Und_Medida = 1, Nome = "EMBALAGEM", Preco = 0.00F });
                material.Add(new Material { Id = 14, Id_Und_Medida = 1, Nome = "EVA", Preco = 8.00F });
                material.Add(new Material { Id = 15, Id_Und_Medida = 1, Nome = "Resina 101F", Preco = 114.00F });
                material.Add(new Material { Id = 16, Id_Und_Medida = 1, Nome = "HT356", Preco = 108.00F });

            }

            if (!(user.Any()))
            {
                user.Add(new User { Id = 1, Nome = "erivaldo", Senha="airsleeves"});
                user.Add(new User { Id = 2, Nome = "t", Senha="123"});
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Elimina a Plurazição do Entity Framework
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            //Cria banco caso não exista
            Database.SetInitializer(new SqliteCreateDatabaseIfNotExists<EfContext>(modelBuilder));
        }
    }
}
