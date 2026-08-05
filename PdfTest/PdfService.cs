using Data;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;


namespace PdfTest
{
    public static class PdfService
    {
        public static void GenererPdf(Salarie salarie)
        {
            string chemin = "FicheSalarie.pdf";

            PdfWriter writer = new PdfWriter(chemin);

            PdfDocument pdf = new PdfDocument(writer);

            Document document = new Document(pdf);


            document.Add(new Paragraph("ANNUAIRE ENTREPRISE")
                .SetFontSize(20));


            document.Add(new Paragraph(
                $"Nom : {salarie.Nom}\n" +
                $"Prénom : {salarie.Prenom}\n" +
                $"Téléphone fixe : {salarie.TelFixe}\n" +
                $"Téléphone portable : {salarie.TelPortable}\n" +
                $"Email : {salarie.Email}\n" +
                $"Service ID : {salarie.ServiceId}\n" +
                $"Site ID : {salarie.SiteId}"
            ));


            document.Close();
        }
    }
}