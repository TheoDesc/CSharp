using Data;
using System.Text.Json;

namespace DataGeneration
{
    internal class Program
    {
        static readonly Random Rnd = new Random();  // Pour la génération de nombres entiers aléatoires

        // Domaines pour la génération des adresses mails !
        static readonly string[] Domaines = { "gmail.com", "orange.fr", "free.fr", "hotmail.fr", "outlook.fr", "wanadoo.fr", "laposte.net", "yahoo.fr" };

        static string GetEmail(string nom, string prenom)
        {
            var dom = Domaines[Rnd.Next(0, 8)];

            switch (Rnd.Next(1, 6))
            {
                case 1: return $"{prenom}.{nom}@{dom}";
                case 2: return $"{nom}.{prenom}@{dom}";
                case 3: return $"{prenom[0]}{nom}@{dom}";
                case 4: return $"{prenom}{nom[0]}@{dom}";
                default: return $"{prenom}{Rnd.Next(10, 99)}@{dom}"; // case 5
            }
        }

        static void Main(string[] args)
        {
            /*
             * On suppose que l'on part d'une base vide
             */

            DB.NomFichierSQLite = @"C:\Users\theod\source\repos\Annuaire\annuaire.db";
            DB.CreerBase();

            // Sites
            //-------
            foreach (var ville in new string[] { "Paris", "Marseille", "Lyon", "Toulouse", "Nice", "Nantes", "Strasbourg" })
            {
                DB.SiteAjouter(ville);
            }

            // Services
            //----------
            foreach (var service in new string[] { "Direction générale", "Ressources humaines", "Comptabilité", "Informatique", "Support", "Commercial", "Communication" })
            {
                DB.ServiceAjouter(service);
            }

            // Salariés 
            //----------
            // Via l'API RandomUser
            // On demande 100000 personnes et on génèrera nous même l'adresse mail (car via l'API toutes sont en prenom.nom@exemple.com: pas réaliste)
            var url = "https://randomuser.me/api/?results=100000&nat=fr&inc=name,phone,cell";

            // Appel API
            var client = new HttpClient();
            string json = client.GetStringAsync(url).Result;

            using (var doc = JsonDocument.Parse(json))
            {
                var results = doc.RootElement.GetProperty("results");
                foreach (var p in results.EnumerateArray())
                {
                    var name = p.GetProperty("name");
                    var first = name.GetProperty("first").GetString();
                    var last = name.GetProperty("last").GetString();

                    var phone = p.GetProperty("phone").GetString();
                    var cell = p.GetProperty("cell").GetString();

                    // Ajouts en base !
                    DB.SalarieAjouter(
                        new Salarie()
                        {
                            Nom = last,
                            Prenom = first,
                            TelFixe = phone,
                            TelPortable = cell,
                            Email = GetEmail(first, last),
                            ServiceId = Rnd.Next(1, 8),  // Service
                            SiteId = Rnd.Next(1, 8)   // Site
                        }
                    );
                }
            }

            Console.WriteLine("Génération terminée");
            Console.WriteLine("Appuyer sur une touche...");
            Console.ReadKey();
        }
    }
}