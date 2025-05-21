using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace SuperBiblio;

public partial class MyContext : DbContext
{
    public virtual DbSet<Film> Films { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        var connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "localhost", // ou "localhost\\NomDeVotreInstance" si vous utilisez une instance nommée
            InitialCatalog = "biblioEF", // nom de la base de données
            UserID = "sa", // par exemple, "sa" pour l'administrateur système
            Password = "P@ssw0rd", // mot de passe de l'utilisateur
            Encrypt = false // Désactive le chiffrement SSL
            // Integrate Security = true; // Utilisez cette option pour l'authentification Windows
        };
        optionsBuilder.UseSqlServer(connectionStringBuilder.ConnectionString);
    }
}