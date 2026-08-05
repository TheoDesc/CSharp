using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataEF
{
    public static class DbEF
    {
        // -------- Sites --------

        // Ajoute un site et retourne son Id généré automatiquement.
        // Le "ctx" est maintenant fourni par l'appelant (DataTestEF),
        // au lieu d'être créé ici avec une connexion codée en dur.
        public static int SiteAjouter(AnnuaireContext ctx, string ville)
        {
            var site = new Site { Ville = ville };
            ctx.Sites.Add(site);
            ctx.SaveChanges();

            return site.Id;
        }

        // Lit un site à partir de son Id. Retourne null si non trouvé.
        public static Site SiteLire(AnnuaireContext ctx, int id)
        {
            return ctx.Sites.Find(id);
        }

        // Lit tous les sites, triés par ville.
        public static List<Site> SiteLireTous(AnnuaireContext ctx)
        {
            return ctx.Sites.OrderBy(s => s.Ville).ToList();
        }

        // Modifie la ville d'un site existant.
        public static void SiteModifier(AnnuaireContext ctx, int id, string nouvelleVille)
        {
            var site = ctx.Sites.Find(id);
            if (site == null) return;

            site.Ville = nouvelleVille;
            ctx.SaveChanges();
        }

        // Supprime un site à partir de son Id.
        public static void SiteSupprimer(AnnuaireContext ctx, int id)
        {
            var site = ctx.Sites.Find(id);
            if (site == null) return;

            ctx.Sites.Remove(site);
            ctx.SaveChanges();
        }

        // -------- Services --------

        // Ajoute un service et retourne son Id généré automatiquement.
        public static int ServiceAjouter(AnnuaireContext ctx, string nom)
        {
            var service = new Service { Nom = nom };
            ctx.Services.Add(service);
            ctx.SaveChanges();

            return service.Id;
        }

        // Lit un service à partir de son Id. Retourne null si non trouvé.
        public static Service ServiceLire(AnnuaireContext ctx, int id)
        {
            return ctx.Services.Find(id);
        }

        // Lit tous les services, triés par nom.
        public static List<Service> ServiceLireTous(AnnuaireContext ctx)
        {
            return ctx.Services.OrderBy(s => s.Nom).ToList();
        }

        // Modifie le nom d'un service existant.
        public static void ServiceModifier(AnnuaireContext ctx, int id, string nouveauNom)
        {
            var service = ctx.Services.Find(id);
            if (service == null) return;

            service.Nom = nouveauNom;
            ctx.SaveChanges();
        }

        // Supprime un service à partir de son Id.
        public static void ServiceSupprimer(AnnuaireContext ctx, int id)
        {
            var service = ctx.Services.Find(id);
            if (service == null) return;

            ctx.Services.Remove(service);
            ctx.SaveChanges();
        }

        // -------- Salaries --------

        // Ajoute un salarié et retourne son Id généré automatiquement.
        public static int SalarieAjouter(AnnuaireContext ctx, Salarie s)
        {
            ctx.Salaries.Add(s);
            ctx.SaveChanges();

            return s.Id;
        }

        // Lit un salarié à partir de son Id, avec son Service et son Site chargés.
        // Retourne null si non trouvé.
        public static Salarie SalarieLire(AnnuaireContext ctx, int id)
        {
            // Include() dit à EF : "charge aussi l'objet Service lié"
            // (et pareil pour Site). Sans ça, salarie.Service serait null.
            return ctx.Salaries
                .Include(sa => sa.Service)
                .Include(sa => sa.Site)
                .FirstOrDefault(sa => sa.Id == id);
        }

        // Lit tous les salariés (avec Service et Site chargés), triés par prénom puis nom.
        public static List<Salarie> SalarieLireTous(AnnuaireContext ctx)
        {
            return ctx.Salaries
                .Include(sa => sa.Service)
                .Include(sa => sa.Site)
                .OrderBy(sa => sa.Prenom).ThenBy(sa => sa.Nom)
                .ToList();
        }

        // Modifie un salarié existant (toutes ses colonnes).
        public static void SalarieModifier(AnnuaireContext ctx, int id, Salarie nouvellesDonnees)
        {
            var s = ctx.Salaries.Find(id);
            if (s == null) return;

            s.Nom = nouvellesDonnees.Nom;
            s.Prenom = nouvellesDonnees.Prenom;
            s.TelFixe = nouvellesDonnees.TelFixe;
            s.TelPortable = nouvellesDonnees.TelPortable;
            s.Email = nouvellesDonnees.Email;
            s.ServiceId = nouvellesDonnees.ServiceId;
            s.SiteId = nouvellesDonnees.SiteId;

            ctx.SaveChanges();
        }

        // Supprime un salarié à partir de son Id.
        public static void SalarieSupprimer(AnnuaireContext ctx, int id)
        {
            var s = ctx.Salaries.Find(id);
            if (s == null) return;

            ctx.Salaries.Remove(s);
            ctx.SaveChanges();
        }
    }
}