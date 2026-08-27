using Microsoft.EntityFrameworkCore;
using System;
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
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (string.IsNullOrWhiteSpace(ville))
                throw new ArgumentException("La ville du site ne peut pas être vide.");

            ville = ville.Trim();

            bool existe = ctx.Sites.Any(s => s.Ville.ToLower() == ville.ToLower());

            if (existe)
                throw new ArgumentException("Ce site existe déjà.");

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
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                return null;

            return ctx.Sites.Find(id);
        }


        public static List<Site> SiteLireTous(AnnuaireContext ctx)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            return ctx.Sites
                .OrderBy(s => s.Ville)
                .ToList();
        }


        public static void SiteModifier(
            AnnuaireContext ctx,
            int id,
            string nouvelleVille)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException("Identifiant de site invalide.");

            if (string.IsNullOrWhiteSpace(nouvelleVille))
                throw new ArgumentException("La ville du site ne peut pas être vide.");

            nouvelleVille = nouvelleVille.Trim();

            var site = ctx.Sites.Find(id);

            if (site == null)
                throw new ArgumentException("Le site demandé n'existe pas.");

            bool existe = ctx.Sites.Any(s =>
                s.Id != id &&
                s.Ville.ToLower() == nouvelleVille.ToLower());

            if (existe)
                throw new ArgumentException("Un autre site possède déjà cette ville.");

            site.Ville = nouvelleVille;

            ctx.SaveChanges();
        }


        public static void SiteSupprimer(
            AnnuaireContext ctx,
            int id)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException("Identifiant de site invalide.");

            var site = ctx.Sites.Find(id);

            if (site == null)
                throw new ArgumentException("Le site demandé n'existe pas.");

            bool utilise = ctx.Salaries.Any(s => s.SiteId == id);

            if (utilise)
                throw new InvalidOperationException(
                    "Impossible de supprimer ce site car des salariés y sont encore rattachés.");

            ctx.Sites.Remove(site);

            ctx.SaveChanges();
        }


        // =========================
        // SERVICES
        // =========================

        public static int ServiceAjouter(
            AnnuaireContext ctx,
            string nom)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (string.IsNullOrWhiteSpace(nom))
                throw new ArgumentException("Le nom du service ne peut pas être vide.");

            nom = nom.Trim();

            bool existe = ctx.Services.Any(s =>
                s.Nom.ToLower() == nom.ToLower());

            if (existe)
                throw new ArgumentException("Ce service existe déjà.");

            var service = new Service
            {
                Nom = nom
            };

            ctx.Services.Add(service);
            ctx.SaveChanges();

            return service.Id;
        }


        public static Service ServiceLire(
            AnnuaireContext ctx,
            int id)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                return null;

            return ctx.Services.Find(id);
        }


        public static List<Service> ServiceLireTous(
            AnnuaireContext ctx)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            return ctx.Services
                .OrderBy(s => s.Nom)
                .ToList();
        }


        public static void ServiceModifier(
            AnnuaireContext ctx,
            int id,
            string nouveauNom)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException("Identifiant de service invalide.");

            if (string.IsNullOrWhiteSpace(nouveauNom))
                throw new ArgumentException("Le nom du service ne peut pas être vide.");

            nouveauNom = nouveauNom.Trim();

            var service = ctx.Services.Find(id);

            if (service == null)
                throw new ArgumentException("Le service demandé n'existe pas.");

            bool existe = ctx.Services.Any(s =>
                s.Id != id &&
                s.Nom.ToLower() == nouveauNom.ToLower());

            if (existe)
                throw new ArgumentException(
                    "Un autre service possède déjà ce nom.");

            service.Nom = nouveauNom;

            ctx.SaveChanges();
        }


        public static void ServiceSupprimer(
            AnnuaireContext ctx,
            int id)
        {
            if (ctx == null)
                throw new ArgumentException("La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException("Identifiant de service invalide.");

            var service = ctx.Services.Find(id);

            if (service == null)
                throw new ArgumentException("Le service demandé n'existe pas.");

            bool utilise = ctx.Salaries.Any(s => s.ServiceId == id);

            if (utilise)
                throw new InvalidOperationException(
                    "Impossible de supprimer ce service car des salariés y sont encore rattachés.");

            ctx.Services.Remove(service);

            ctx.SaveChanges();
        }


        // =========================
        // SALARIES
        // =========================

        public static int SalarieAjouter(
            AnnuaireContext ctx,
            Salarie salarie)
        {
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            if (salarie == null)
                throw new ArgumentException(
                    "Le salarié est invalide.");

            VerifierSalarie(ctx, salarie);

            salarie.Nom = salarie.Nom.Trim();
            salarie.Prenom = salarie.Prenom.Trim();
            salarie.TelFixe = salarie.TelFixe?.Trim();
            salarie.TelPortable = salarie.TelPortable?.Trim();
            salarie.Email = salarie.Email?.Trim();

            ctx.Salaries.Add(salarie);

            ctx.SaveChanges();

            return salarie.Id;
        }


        public static Salarie SalarieLire(
            AnnuaireContext ctx,
            int id)
        {
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            if (id <= 0)
                return null;

            return ctx.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .FirstOrDefault(s => s.Id == id);
        }


        public static List<Salarie> SalarieLireTous(
            AnnuaireContext ctx)
        {
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            return ctx.Salaries
                .Include(s => s.Service)
                .Include(s => s.Site)
                .OrderBy(s => s.Nom)
                .ThenBy(s => s.Prenom)
                .ToList();
        }


        public static void SalarieModifier(
            AnnuaireContext ctx,
            int id,
            Salarie nouvellesDonnees)
        {
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException(
                    "Identifiant de salarié invalide.");

            if (nouvellesDonnees == null)
                throw new ArgumentException(
                    "Les données du salarié sont invalides.");

            var salarie = ctx.Salaries.Find(id);

            if (salarie == null)
                throw new ArgumentException(
                    "Le salarié demandé n'existe pas.");

            VerifierSalarie(ctx, nouvellesDonnees);

            salarie.Nom = nouvellesDonnees.Nom.Trim();
            salarie.Prenom = nouvellesDonnees.Prenom.Trim();
            salarie.TelFixe = nouvellesDonnees.TelFixe?.Trim();
            salarie.TelPortable = nouvellesDonnees.TelPortable?.Trim();
            salarie.Email = nouvellesDonnees.Email?.Trim();
            salarie.ServiceId = nouvellesDonnees.ServiceId;
            salarie.SiteId = nouvellesDonnees.SiteId;

            ctx.SaveChanges();
        }


        public static void SalarieSupprimer(
            AnnuaireContext ctx,
            int id)
        {
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            if (id <= 0)
                throw new ArgumentException(
                    "Identifiant de salarié invalide.");

            var salarie = ctx.Salaries.Find(id);

            if (salarie == null)
                throw new ArgumentException(
                    "Le salarié demandé n'existe pas.");

            ctx.Salaries.Remove(salarie);

            ctx.SaveChanges();
        }


        // =========================
        // VERIFICATION SALARIE
        // =========================

        private static void VerifierSalarie(
            AnnuaireContext ctx,
            Salarie salarie)
        {
            if (string.IsNullOrWhiteSpace(salarie.Nom))
                throw new ArgumentException(
                    "Le nom du salarié ne peut pas être vide.");

            if (string.IsNullOrWhiteSpace(salarie.Prenom))
                throw new ArgumentException(
                    "Le prénom du salarié ne peut pas être vide.");

            if (salarie.ServiceId <= 0)
                throw new ArgumentException(
                    "Vous devez sélectionner un service.");

            if (salarie.SiteId <= 0)
                throw new ArgumentException(
                    "Vous devez sélectionner un site.");

            bool serviceExiste =
                ctx.Services.Any(s => s.Id == salarie.ServiceId);

            if (!serviceExiste)
                throw new ArgumentException(
                    "Le service sélectionné n'existe pas.");

            bool siteExiste =
                ctx.Sites.Any(s => s.Id == salarie.SiteId);

            if (!siteExiste)
                throw new ArgumentException(
                    "Le site sélectionné n'existe pas.");

            if (!string.IsNullOrWhiteSpace(salarie.Email))
            {
                if (!salarie.Email.Contains("@"))
                    throw new ArgumentException(
                        "L'adresse email n'est pas valide.");
            }
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
            if (ctx == null)
                throw new ArgumentException(
                    "La connexion à la base de données est invalide.");

            var requete = ctx.Salaries
                .Include(s => s.Site)
                .Include(s => s.Service)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(recherche))
            {
                recherche = recherche.Trim().ToLower();

                requete = requete.Where(s =>
                    s.Nom.ToLower().Contains(recherche)
                    ||
                    s.Prenom.ToLower().Contains(recherche));
            }

            if (siteId.HasValue)
            {
                if (siteId.Value <= 0)
                    throw new ArgumentException(
                        "Identifiant de site invalide.");

                requete = requete.Where(s =>
                    s.SiteId == siteId.Value);
            }

            if (serviceId.HasValue)
            {
                if (serviceId.Value <= 0)
                    throw new ArgumentException(
                        "Identifiant de service invalide.");

                requete = requete.Where(s =>
                    s.ServiceId == serviceId.Value);
            }

            return requete
                .OrderBy(s => s.Nom)
                .ThenBy(s => s.Prenom)
                .ToList();
        }
    }
}