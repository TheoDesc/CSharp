using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace PdfGen
{
    public static class PdfService
    {
        public static void GenererPdf(
            string chemin,
            string nom,
            string prenom,
            string telFixe,
            string telPortable,
            string email,
            string service,
            string site)
        {

            // Création du fichier PDF
            PdfWriter writer = new PdfWriter(chemin);

            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf);



            // Titre
            Paragraph titre = new Paragraph("FICHE SALARIE")
                .SetFontSize(20)
                .SetTextAlignment(TextAlignment.CENTER);


            document.Add(titre);



            // Informations salarié

            document.Add(new Paragraph(
                "Nom : " + nom));

            document.Add(new Paragraph(
                "Prénom : " + prenom));

            document.Add(new Paragraph(
                "Téléphone fixe : " + telFixe));

            document.Add(new Paragraph(
                "Téléphone portable : " + telPortable));

            document.Add(new Paragraph(
                "Email : " + email));

            document.Add(new Paragraph(
                "Service : " + service));

            document.Add(new Paragraph(
                "Site : " + site));



            // Fermeture du PDF
            document.Close();
        }
    }
}