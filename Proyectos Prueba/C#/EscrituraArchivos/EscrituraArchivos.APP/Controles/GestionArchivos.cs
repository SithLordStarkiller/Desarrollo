using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscrituraArchivos.APP.Controles
{
    public class GestionArchivos
    {
        private readonly string _myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private StringBuilder sB = new StringBuilder();
        private string line;

        public string CreaDirectorio()
        {
            var lista = new List<string>();

            foreach (var item in Directory.EnumerateFiles(_myDocPath, "*.DOCX"))
            {
                using (var sr = new StreamReader(item))
                {
                    sB.AppendLine(item.ToString());
                    sB.AppendLine("= = = = = =");
                    sB.Append(sr.ReadToEnd());
                    sB.AppendLine();
                    sB.AppendLine();
                }
            }

            using (var outFile = new StreamWriter(_myDocPath + @"\ArchivosDoc.txt", true))
            {
                outFile.Write(sB.ToString());
            }

            try
            {
                using (var sr = new StreamReader(_myDocPath + @"\ArchivosDoc.txt"))
                {
                    line = sr.ReadToEnd();
                    //Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                line = @"The file could not be read:" + e.Message;
            }

            return line;
        }
    }
}
