using DataEF;
using Microsoft.EntityFrameworkCore;

// C'est ICI, dans le programme, que la chaîne de connexion est décidée.
// La librairie DataEF ne sait plus rien sur "où" se trouve la base.
var options = new DbContextOptionsBuilder<AnnuaireContext>()
    .UseSqlite("Data Source=annuaire_ef.db")
    .Options;

using var ctx = new AnnuaireContext(options);
ctx.Database.EnsureCreated(); // Crée la base si elle n'existe pas encore.

// --- Sites ---
int idSite = DbEF.SiteAjouter(ctx, "Bordeaux");
Console.WriteLine($"Site ajouté, Id = {idSite}");

var site = DbEF.SiteLire(ctx, idSite);
Console.WriteLine($"Site lu : {site.Ville}");

DbEF.SiteModifier(ctx, idSite, "Nantes");
Console.WriteLine("Site modifié");

foreach (var s in DbEF.SiteLireTous(ctx))
    Console.WriteLine($"- {s.Ville}");

// --- Services ---
int idService = DbEF.ServiceAjouter(ctx, "Comptabilité");
Console.WriteLine($"Service ajouté, Id = {idService}");

var service = DbEF.ServiceLire(ctx, idService);
Console.WriteLine($"Service lu : {service.Nom}");

DbEF.ServiceModifier(ctx, idService, "Ressources Humaines");
Console.WriteLine("Service modifié");

foreach (var sv in DbEF.ServiceLireTous(ctx))
    Console.WriteLine($"- {sv.Nom}");

// --- Salaries ---
int idSalarie = DbEF.SalarieAjouter(ctx, new Salarie
{
    Nom = "Martin",
    Prenom = "Claire",
    TelFixe = "0102030405",
    TelPortable = "",
    Email = "claire.martin@test.fr",
    ServiceId = idService,
    SiteId = idSite
});
Console.WriteLine($"Salarié ajouté, Id = {idSalarie}");

var salarie = DbEF.SalarieLire(ctx, idSalarie);
Console.WriteLine($"Salarié lu : {salarie.Prenom} {salarie.Nom} - Service: {salarie.Service.Nom} - Ville: {salarie.Site.Ville}");

DbEF.SalarieModifier(ctx, idSalarie, new Salarie
{
    Nom = "Martin",
    Prenom = "Claire-Alice",
    TelFixe = "0102030405",
    TelPortable = "0600000000",
    Email = "claire.martin@test.fr",
    ServiceId = idService,
    SiteId = idSite
});
Console.WriteLine("Salarié modifié");

foreach (var sal in DbEF.SalarieLireTous(ctx))
    Console.WriteLine($"- {sal.Prenom} {sal.Nom} | {sal.Service.Nom} | {sal.Site.Ville}");