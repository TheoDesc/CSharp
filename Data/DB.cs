using Microsoft.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

namespace Data
{
    public static class DB
    {
        public static string NomFichierSQLite { get; set; }
        
        private static string ConnectionString { get { return "Data Source=" + DB.NomFichierSQLite; } }

        // =========================
        // CREATION DES TABLES
        // =========================

        public static void CreerBase()
        {
            // Création base (fichier) vide si existe pas
            if (!System.IO.File.Exists(NomFichierSQLite))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(NomFichierSQLite))
                {
                    // fs.Dispose() automatiquement à la sortie du using => pas de verrou sur le fichier
                }
            }

            // Connexion et création des tables si elles n'existent pas
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        PRAGMA foreign_keys = ON;

                        CREATE TABLE IF NOT EXISTS Sites
                        (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Ville TEXT NOT NULL UNIQUE
                        );

                        CREATE TABLE IF NOT EXISTS Services
                        (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nom TEXT NOT NULL UNIQUE
                        );

                        CREATE TABLE IF NOT EXISTS Salaries
                        (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Nom TEXT NOT NULL,
                            Prenom TEXT NOT NULL,
                            TelFixe TEXT,
                            TelPortable TEXT,
                            Email TEXT,
                            ServiceId INTEGER,
                            SiteId INTEGER,
                            FOREIGN KEY(ServiceId) REFERENCES Services(Id),
                            FOREIGN KEY(SiteId) REFERENCES Sites(Id)
                        );
                    ";

                    command.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // SITES
        // =========================

        public static int SiteAjouter(string ville)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Sites (Ville)
                        VALUES (@Ville);
                        SELECT last_insert_rowid();
                    ";

                    command.Parameters.AddWithValue("@Ville", ville);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static Site SiteLire(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Ville
                        FROM Sites
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Site
                            {
                                Id = reader.GetInt32(0),
                                Ville = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<Site> SiteLireTous()
        {
            List<Site> sites = new List<Site>();

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Ville
                        FROM Sites
                    ";

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sites.Add(new Site
                            {
                                Id = reader.GetInt32(0),
                                Ville = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return sites;
        }

        public static void SiteModifier(int id, string ville)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Sites
                        SET Ville = @Ville
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Ville", ville);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SiteSupprimer(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        DELETE FROM Sites
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // SERVICES
        // =========================

        public static int ServiceAjouter(string nom)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Services (Nom)
                        VALUES (@Nom);
                        SELECT last_insert_rowid();
                    ";

                    command.Parameters.AddWithValue("@Nom", nom);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static Service ServiceLire(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Nom
                        FROM Services
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Service
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<Service> ServiceLireTous()
        {
            List<Service> services = new List<Service>();

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Nom
                        FROM Services
                    ";

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            services.Add(new Service
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return services;
        }

        public static void ServiceModifier(int id, string nom)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Services
                        SET Nom = @Nom
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nom", nom);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ServiceSupprimer(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        DELETE FROM Services
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // SALARIES
        // =========================

        public static int SalarieAjouter(Salarie salarie)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Salaries
                        (Nom, Prenom, TelFixe, TelPortable, Email, ServiceId, SiteId)
                        VALUES
                        (@Nom, @Prenom, @TelFixe, @TelPortable, @Email, @ServiceId, @SiteId);

                        SELECT last_insert_rowid();
                    ";

                    command.Parameters.AddWithValue("@Nom", salarie.Nom);
                    command.Parameters.AddWithValue("@Prenom", salarie.Prenom);
                    command.Parameters.AddWithValue("@TelFixe", salarie.TelFixe ?? "");
                    command.Parameters.AddWithValue("@TelPortable", salarie.TelPortable ?? "");
                    command.Parameters.AddWithValue("@Email", salarie.Email ?? "");
                    command.Parameters.AddWithValue("@ServiceId", salarie.ServiceId);
                    command.Parameters.AddWithValue("@SiteId", salarie.SiteId);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public static Salarie SalarieLire(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Nom, Prenom, TelFixe, TelPortable,
                               Email, ServiceId, SiteId
                        FROM Salaries
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Salarie
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Prenom = reader.GetString(2),
                                TelFixe = reader.GetString(3),
                                TelPortable = reader.GetString(4),
                                Email = reader.GetString(5),
                                ServiceId = reader.GetInt32(6),
                                SiteId = reader.GetInt32(7)
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static List<Salarie> SalarieLireTous()
        {
            List<Salarie> salaries = new List<Salarie>();

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT Id, Nom, Prenom, TelFixe, TelPortable,
                               Email, ServiceId, SiteId
                        FROM Salaries
                    ";

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salaries.Add(new Salarie
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Prenom = reader.GetString(2),
                                TelFixe = reader.GetString(3),
                                TelPortable = reader.GetString(4),
                                Email = reader.GetString(5),
                                ServiceId = reader.GetInt32(6),
                                SiteId = reader.GetInt32(7)
                            });
                        }
                    }
                }
            }

            return salaries;
        }

        public static void SalarieModifier(int id, Salarie salarie)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Salaries
                        SET Nom = @Nom,
                            Prenom = @Prenom,
                            TelFixe = @TelFixe,
                            TelPortable = @TelPortable,
                            Email = @Email,
                            ServiceId = @ServiceId,
                            SiteId = @SiteId
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nom", salarie.Nom);
                    command.Parameters.AddWithValue("@Prenom", salarie.Prenom);
                    command.Parameters.AddWithValue("@TelFixe", salarie.TelFixe ?? "");
                    command.Parameters.AddWithValue("@TelPortable", salarie.TelPortable ?? "");
                    command.Parameters.AddWithValue("@Email", salarie.Email ?? "");
                    command.Parameters.AddWithValue("@ServiceId", salarie.ServiceId);
                    command.Parameters.AddWithValue("@SiteId", salarie.SiteId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SalarieSupprimer(int id)
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        DELETE FROM Salaries
                        WHERE Id = @Id
                    ";

                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}