namespace DataEF
{
    public class Salarie
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string TelFixe { get; set; }
        public string TelPortable { get; set; }
        public string Email { get; set; }

        // ServiceId : la clé étrangère "brute" (un simple entier).
        // Service   : la "propriété de navigation" -> l'objet Service complet.
        // EF utilise le nom "ServiceId" + "Service" pour comprendre
        // automatiquement le lien entre les deux, sans rien configurer de plus.
        public int ServiceId { get; set; }
        public Service Service { get; set; } // Clef étrangère vers Service

        public int SiteId { get; set; }
        public Site Site { get; set; }  // Clef étrangère vers Site
    }
}