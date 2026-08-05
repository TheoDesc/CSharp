using Data;

namespace PdfTest
{
    internal class Program
    {
        static void Main()
        {
            DB.NomFichierSQLite = @"C:\Users\theod\source\repos\Annuaire\annuaire.db";

            // On récupère un salarié existant
            Salarie salarie = DB.SalarieLire(268);

            Console.WriteLine($"PDF du salarié : {salarie.Prenom} {salarie.Nom}");

            PdfService.GenererPdf(salarie);

            Console.WriteLine("PDF généré !");
        }
    }
}