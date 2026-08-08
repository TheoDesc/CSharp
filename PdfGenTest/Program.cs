using PdfGen;


string dossier =
    @"C:\Users\theod\source\repos\Annuaire\pdf";


// Création du dossier si absent
if (!Directory.Exists(dossier))
{
    Directory.CreateDirectory(dossier);
}


string chemin =
    dossier + @"\test.pdf";



PdfService.GenererPdf(
    chemin,
    "Dupont",
    "Jean",
    "0102030405",
    "0607080910",
    "jean.dupont@test.fr",
    "Informatique",
    "Paris"
);



Console.WriteLine("PDF créé :");
Console.WriteLine(chemin);