using PdfGen;

namespace PdfGenTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string chemin = @"C:\Users\theod\source\repos\Annuaire\PdfGenTest\FicheSalarie.pdf";

            PdfService.GenererPdf(
                chemin,
                "Dupont",
                "Jean",
                "01 02 03 04 05",
                "06 01 02 03 04",
                "jean.dupont@test.fr",
                "Informatique",
                "Paris");

            Console.WriteLine("PDF créé avec succès !");
            Console.WriteLine(chemin);
            Console.ReadKey();
        }
    }
}