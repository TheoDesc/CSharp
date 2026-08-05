using System.Collections.Generic;

namespace DataEF
{
    public class Service
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        public List<Salarie> Salaries { get; set; } = new List<Salarie>(); // Liste des salariés qui travaillent dans ce service via la relation de clé étrangère. 
    }
}