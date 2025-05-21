namespace SuperBiblio;

class Program
{
    static void Main(string[] args)
    {
        // IDataService dataService = new DataServiceMySql();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║       La super bibliothèque          ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        IDataService dataService = new DataServiceEF();

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Accès films de la biblio :");
            Console.WriteLine("  1 - Lister");
            Console.WriteLine("  2 - Ajouter");
            Console.WriteLine("  3 - Supprimer");
            Console.WriteLine("  4 - Nombre de films");
            Console.WriteLine("  5 - Fermer");
            Console.ResetColor();
            if (!int.TryParse(Console.ReadLine(), out var choix)) continue;
            Console.Write(Environment.NewLine);

            switch (choix)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Contenu de la bibliothèque :");
                    Console.ResetColor();
                    var films = dataService.GetFilm();
                    var realisateur = dataService.GetRealisateur();
                    var idFilm = dataService.GetId();
                    for (int i = 0; i < films.Count; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"{idFilm[i]}. {films[i]} , réalisé par : {realisateur[i]}");
                        Console.ResetColor();
                    }
                    break;

                case 2:
                    Console.Write("Nom du film : ");
                    var titre = Console.ReadLine();
                    Console.Write("Nom du réalisateur : ");
                    var real = Console.ReadLine();
                    dataService.AddFilm(titre, real);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film ajouté !");
                    Console.ResetColor();
                    break;

                case 3:
                    Console.Write("Rentrez l'id du film à supprimer : ");
                    if (int.TryParse(Console.ReadLine(), out var id))
                    {
                        dataService.DeleteFilm(id);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Film supprimé !");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ID invalide.");
                        Console.ResetColor();
                    }
                    break;

                case 4:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Il y a {dataService.GetFilmCount()} films dans la bibliothèque.");
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Au revoir !");
                    Console.ResetColor();
                    return;

                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
            Console.Write(Environment.NewLine);
        }
    }
}