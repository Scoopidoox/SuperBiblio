using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;



namespace SuperBiblio;

// Regroupement des methodes d'accès aux données
public interface IDataService
{
    public List<int> GetId();
    public List<string> GetFilm();
    public List<string> GetRealisateur();
    public void AddFilm(string film, string real);
    public void DeleteFilm(int id);
    public int GetFilmCount();
}

// DataServiceFile.cs

public class DataServiceFile : IDataService
{
    private const string Path = "./films.txt";

    public DataServiceFile()
    {
        if (!File.Exists(Path))
            File.Create(Path);
    }

    public List<int> GetId()
    {
        var lines = File.ReadAllLines(Path).ToList();
        return lines.Select((line, index) => index).ToList();
    }
    public List<string> GetFilm()
    {
        return File.ReadAllLines(Path).ToList();
    }
    
    public List<string> GetRealisateur()
    {
        return File.ReadAllLines(Path).ToList();
    }

    public void AddFilm(string film, string real)
    {
        File.AppendAllText(Path, "\n"+film);
        File.AppendAllText(Path, "\n"+real);
    }

    public void DeleteFilm(int id)
    {
        var lines = File.ReadAllLines(Path).ToList();
        if (id < 0 || id >= lines.Count)
        {
            Console.WriteLine("ID invalide.");
            return;
        }
        lines.RemoveAt(id);
        File.WriteAllLines(Path, lines);
    }
    
    public int GetFilmCount()
    {
        return File.ReadAllLines(Path).Length;
    }
}

// DataServiceSqlServer.cs

public class DataServiceSqlServer : IDataService
{
    private SqlConnection Connection { get; }

    public DataServiceSqlServer()
    {
        var connectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "localhost", // ou "localhost\\NomDeVotreInstance" si vous utilisez une instance nommée
            InitialCatalog = "biblioEF", // nom de la base de données
            UserID = "sa", // par exemple, "sa" pour l'administrateur système
            Password = "P@ssw0rd", // mot de passe de l'utilisateur
            Encrypt = false
            // Integrate Security = true; // Utilisez cette option pour l'authentification Windows
        };

        Connection = new SqlConnection(connectionStringBuilder.ConnectionString);
        Connection.Open();
    }

    public List<int> GetId()
    {
        var list = new List<int>();
        using var command = new SqlCommand("SELECT id FROM films", Connection);
        using var result = command.ExecuteReader();
        while (result.Read())
        {
            list.Add(result.GetInt32(result.GetOrdinal("id")));
        }

        return list;
    }
    public List<string> GetFilm()
    {
        var list = new List<string>();
        using var command = new SqlCommand("SELECT titre FROM films", Connection);
        using var result = command.ExecuteReader();
        while (result.Read())
        {
            list.Add(result.GetString(result.GetOrdinal("titre")));
        }

        return list;
    }
    
    public List<string> GetRealisateur()
    {
        var list = new List<string>();
        using var command = new SqlCommand("SELECT realisateur FROM films", Connection);
        using var result = command.ExecuteReader();
        while (result.Read())
        {
            list.Add(result.GetString(result.GetOrdinal("realisateur")));
        }

        return list;
    }

    public void AddFilm(string titre, string real)
    {
        using var command = new SqlCommand("INSERT INTO films (titre, realisateur) VALUES (@titre, @realisateur)", Connection);
        command.Parameters.AddWithValue("@titre", titre);
        command.Parameters.AddWithValue("@realisateur", real);
        command.ExecuteNonQuery();
    }

    public void DeleteFilm(int id)
    {
        using var command = new SqlCommand("DELETE FROM films WHERE id = @id", Connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public int GetFilmCount()
    {
        using var command = new SqlCommand("SELECT COUNT(*) FROM films", Connection);
        return Convert.ToInt32(command.ExecuteScalar());
    }
}

// DataServiceEF.cs

public class DataServiceEF : IDataService
{
    private readonly MyContext _context = new MyContext();

    public DataServiceEF()
    {
        _context.Database.EnsureCreated();
        _context.Films.Load();
    }

    public List<int> GetId()
    {
       return _context.Films.Select(x => x.Id ?? 0).ToList();
    }
    public List<string> GetFilm()
    {
        return _context.Films.Select(x => x.Titre).ToList();
    }

    public List<string> GetRealisateur()
    {
        return _context.Films.Select(x => x.Realisateur).ToList();
    }

    public void AddFilm(string titre, string real)
    {
        var film = new Film
        {
            Titre = titre,
            Realisateur = real
        };
        _context.Films.Add(film);
        _context.SaveChanges();
    }

    public void DeleteFilm(int id)
    {
        var film = _context.Films.Find(id);
        if (film != null)
        {
            _context.Films.Remove(film);
            _context.SaveChanges();
        }
    }

    public int GetFilmCount()
    {
        return _context.Films.Count();
    }
}
