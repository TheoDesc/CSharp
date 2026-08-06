namespace Data
{
    // Cette classe représente un site.
    public class Site
    {
        // Identifiant du site.
        public int Id { get; set; }

        // Ville du site.
        public string Ville { get; set; }

        public override string ToString()
        {
            return " > " + Ville;
        }
    }
}