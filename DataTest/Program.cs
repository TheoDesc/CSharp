using System;
using Data;

class Program
{
    static void Main()
    {
        DB.CreerBase();

        Console.WriteLine("===== TEST SITES =====");

        int siteId = DB.SiteAjouter("Rouen");
        Console.WriteLine("Site ajouté : " + siteId);

        Site site = DB.SiteLire(siteId);
        Console.WriteLine("Site lu : " + site.Ville);

        Console.WriteLine("Tous les sites :");

        foreach (Site s in DB.SiteLireTous())
        {
            Console.WriteLine(s.Id + " - " + s.Ville);
        }

        DB.SiteModifier(siteId, "Marseille");

        site = DB.SiteLire(siteId);
        Console.WriteLine("Site modifié : " + site.Ville);

        DB.SiteSupprimer(siteId);
        Console.WriteLine("Site supprimé.");


        Console.WriteLine();
        Console.WriteLine("===== TEST SERVICES =====");

        int serviceId = DB.ServiceAjouter("Informatique");
        Console.WriteLine("Service ajouté : " + serviceId);

        Service service = DB.ServiceLire(serviceId);
        Console.WriteLine("Service lu : " + service.Nom);

        Console.WriteLine("Tous les services :");

        foreach (Service s in DB.ServiceLireTous())
        {
            Console.WriteLine(s.Id + " - " + s.Nom);
        }

        DB.ServiceModifier(serviceId, "Développement");

        service = DB.ServiceLire(serviceId);
        Console.WriteLine("Service modifié : " + service.Nom);


        Console.WriteLine();
        Console.WriteLine("===== TEST SALARIES =====");

        // On recrée un site et un service
        siteId = DB.SiteAjouter("Rouen");
        serviceId = DB.ServiceAjouter("Informatique");

        Salarie salarie = new Salarie
        {
            Nom = "Dupont",
            Prenom = "Jean",
            TelFixe = "0200000000",
            TelPortable = "0600000000",
            Email = "jean.dupont@test.fr",
            ServiceId = serviceId,
            SiteId = siteId
        };

        int salarieId = DB.SalarieAjouter(salarie);
        Console.WriteLine("Salarié ajouté : " + salarieId);

        salarie = DB.SalarieLire(salarieId);
        Console.WriteLine("Salarié lu : "
            + salarie.Prenom + " " + salarie.Nom);

        Console.WriteLine("Tous les salariés :");

        foreach (Salarie s in DB.SalarieLireTous())
        {
            Console.WriteLine(
                s.Id + " - " +
                s.Prenom + " " +
                s.Nom + " - " +
                s.Email
            );
        }

        salarie.Nom = "Martin";
        salarie.Email = "jean.martin@test.fr";

        DB.SalarieModifier(salarieId, salarie);

        salarie = DB.SalarieLire(salarieId);
        Console.WriteLine(
            "Salarié modifié : "
            + salarie.Prenom + " "
            + salarie.Nom
        );

        DB.SalarieSupprimer(salarieId);

        // On peut maintenant supprimer le site et le service
        DB.SiteSupprimer(siteId);
        DB.ServiceSupprimer(serviceId);

        Console.WriteLine("Salarié supprimé.");

        Console.WriteLine();
        Console.WriteLine("Tous les tests sont terminés.");
    }
}