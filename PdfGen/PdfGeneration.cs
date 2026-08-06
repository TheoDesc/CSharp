using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace PdfGen
{
    public static class PdfService
    {
        public static void GenererPdf(
            string chemin,
            string nom,
            string prenom,
            string telFixe,
            string portable,
            string email,
            string service,
            string site)
        {
            PdfWriter writer = new PdfWriter(chemin);

            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf);

            document.Add(new Paragraph("FICHE SALARIÉ"));
            document.Add(new Paragraph(" "));

            document.Add(new Paragraph($"Nom : {nom}"));
            document.Add(new Paragraph($"Prénom : {prenom}"));
            document.Add(new Paragraph($"Téléphone fixe : {telFixe}"));
            document.Add(new Paragraph($"Portable : {portable}"));
            document.Add(new Paragraph($"Email : {email}"));
            document.Add(new Paragraph($"Service : {service}"));
            document.Add(new Paragraph($"Site : {site}"));

            document.Close();
        }
    }
}