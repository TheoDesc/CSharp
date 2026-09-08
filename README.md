# Annuaire d'Entreprise WPF

Application de bureau C# / WPF développée dans le cadre du Bloc 2 du BTS / Bac+1 informatique (CESI). Elle permet la gestion et la recherche de salariés au sein d'une entreprise multi-sites.

---

## Fonctionnalités

* **Visiteur** : Recherche par nom, site ou service, affichage de la fiche et export PDF.
* **Administrateur** : Accès par raccourci `Ctrl` + `Shift` + `A`, gestion (CRUD) des salariés, sites et services, et enregistrement des logs.

---

## Technique

| Composant | Techno |
| --- | --- |
| **IHM** | WPF (.NET) |
| **Langage** | C# |
| **BDD** | SQLite |
| **Accès BDD** | EF Core (Salariés) + SQL brut (Sites & Services) |
| **Outils** | iText7 (PDF), RandomUser.me (Jeu de données) |

*Note : La double approche EF Core / SQL brut répond aux contraintes pédagogiques du sujet.*

---

## Structure

```text
├── WPF/        # Vues et services
├── DataEF/     # Entity Framework
├── Data/       # SQL brut
└── annuaire.db # Base de données

```

---

## Lancement

1. Cloner le projet.
2. Ouvrir la solution dans Visual Studio et restaurer les packages NuGet.
3. Lancer l'application (`F5`).

---

**Auteur :** Théo D. — CESI (Bloc 2)
