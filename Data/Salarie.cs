namespace Data
{
    // Cette classe représente un salarié.
    public class Salarie
    {
        // Identifiant du salarié.
        public int Id { get; set; }

        // Nom du salarié.
        public string Nom { get; set; }

        // Prénom du salarié.
        public string Prenom { get; set; }

        // Numéro de téléphone fixe.
        public string TelFixe { get; set; }

        // Numéro de téléphone portable.
        public string TelPortable { get; set; }

        // Adresse e-mail.
        public string Email { get; set; }

        // Identifiant du service du salarié.
        public int ServiceId { get; set; }

        // Identifiant du site du salarié.
        public int SiteId { get; set; }
    }
}