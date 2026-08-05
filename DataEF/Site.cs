namespace DataEF
{
    public class Site
    {
        public int Id { get; set; }
        public string Ville { get; set; }

        // Liste des salariés qui travaillent sur ce site via la relation de clé étrangère.
        public List<Salarie> Salaries { get; set; } = new List<Salarie>();
    }
}