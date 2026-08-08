using System;
using System.IO;


namespace WPF.Services
{
    public static class LogService
    {

        private static readonly string fichier =
            "logs.txt";



        public static void EcrireLog(string message)
        {

            string ligne =
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                + " - "
                + message;



            File.AppendAllText(
                fichier,
                ligne + Environment.NewLine
            );

        }

    }
}