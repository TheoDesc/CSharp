using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataEF
{
    public static class DbEF
    {
        // =========================
        // SITES
        // =========================

        public static int SiteAjouter(AnnuaireContext ctx, string ville)
        {
            var site = new Site
            {
                Ville = ville
            };

            ctx.Sites.Add(site);
            ctx.SaveChanges();

            return site.Id;
        }

        public static Site SiteLire(AnnuaireContext ctx, int id)
        {
            return ctx.Sites.Find(id);
        }

        public static List<Site> SiteLireTous(AnnuaireContext ctx)
        {
            return ctx.Sites
                .OrderBy(s => s.Ville)
                .ToList();
        }

        public static void SiteModifier(AnnuaireContext ctx, int id, string nouvelleVille)
        {
            var site = ctx.Sites.Find(id);

            if (site == null)
                return;

            site.Ville = nouvelleVille;

            ctx.SaveChanges();
        }

        public static void SiteSupprimer(AnnuaireContext ctx, int id)
        {
            var site = ctx.Sites.Find(id);

            if (site == null)
                return;

            ctx.Sites.Remove(site);

            ctx.SaveChanges();
        }


        // =========================
        // SERVICES
        // =========================

        public static int ServiceAjouter(AnnuaireContext ctx, string nom)
        {
            var service = new Service
            {
                Nom = nom
            };

            ctx.Services.Add(service);
            ctx.SaveChanges();

            return service.Id;
        }

        public static Service ServiceLire(AnnuaireContext ctx, int id)
        {
            return ctx.Services.Find(id);
        }

        public static List<Service> ServiceLireTous(AnnuaireContext ctx)
        {
            return ctx.Services
                .OrderBy(s => s.Nom)
                .ToList();
        }

        public static void ServiceModifier(AnnuaireContext ctx, int id, string nouveauNom)
        {
            var service = ctx.Services.Find(id);

            if (service == null)
                return;

            service.Nom = nouveauNom;

            ctx.SaveChanges();
        }

        public static void ServiceSupprimer(AnnuaireContext ctx, int id)
        {
            var service = ctx.Services.Find(id);

            if (service == null)
                return;

            ctx.Services.Remove(service);

            ctx.SaveChanges();
        }


        // =========================
        // SALARIES
        // =========================

        public static int SalarieAjouter(AnnuaireContext ctx, Salarie salarie)
        {
            ctx.Salaries.Add(salarie);

            ctx.SaveChanges();

            return salarie.Id;
        }

        public static Salarie SalarieLire(AnnuaireContext ctx, int id)
        {
            return ctx.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefault(s => s.Id == id);
        }

        public static List<Salarie> SalarieLireTous(AnnuaireContext ctx)
        {
            return ctx.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .OrderBy(s => s.Nom)
                .ThenBy(s => s.Prenom)
                .ToList();
        }

        public static void SalarieModifier(AnnuaireContext ctx, int id, Salarie nouvellesDonnees)
        {
            var salarie = ctx.Salaries.Find(id);

            if (salarie == null)
                return;

            salarie.Nom = nouvellesDonnees.Nom;
            salarie.Prenom = nouvellesDonnees.Prenom;
            salarie.TelFixe = nouvellesDonnees.TelFixe;
            salarie.TelPortable = nouvellesDonnees.TelPortable;
            salarie.Email = nouvellesDonnees.Email;
            salarie.ServiceId = nouvellesDonnees.ServiceId;
            salarie.SiteId = nouvellesDonnees.SiteId;

            ctx.SaveChanges();
        }

        public static void SalarieSupprimer(AnnuaireContext ctx, int id)
        {
            var salarie = ctx.Salaries.Find(id);

            if (salarie == null)
                return;

            ctx.Salaries.Remove(salarie);

            ctx.SaveChanges();
        }


        // =========================
        // RECHERCHE
        // =========================

        public static List<Salarie> RechercherSalaries(
            AnnuaireContext ctx,
            string recherche,
            int? siteId,
            int? serviceId)
        {

            var requete =
                ctx.Salaries
                .Include(s => s.Site)
                .Include(s => s.Service)
                .AsQueryable();



            if (!string.IsNullOrWhiteSpace(recherche))
            {

                recherche = recherche.ToLower();


                requete = requete.Where(s =>
                        s.Nom.ToLower().Contains(recherche) || s.Prenom.ToLower().Contains(recherche)
                    );

            }



            if (siteId.HasValue)
            {

                requete =
                    requete.Where(s =>
                        s.SiteId == siteId.Value);

            }



            if (serviceId.HasValue)
            {

                requete =
                    requete.Where(s =>
                        s.ServiceId == serviceId.Value);

            }



            return requete
                .OrderBy(s => s.Nom)
                .ThenBy(s => s.Prenom)
                .ToList();

        }
    }
}