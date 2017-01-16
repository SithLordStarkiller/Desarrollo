using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace PubliPayments
{
    public static class Rar
    {
        //public static string Unrar(string archivo, string rutaExtract)
        //{
        //    var p = new Process {StartInfo = {UseShellExecute = false, RedirectStandardOutput = true}};
        //    // Redirect the output stream of the child process.
        //    var ruta = System.Reflection.Assembly.GetExecutingAssembly().Location;
        //    p.StartInfo.FileName = Path.GetDirectoryName(ruta) + "\\unrar\\unrar.exe";
        //    p.StartInfo.Arguments = "x -o+ \"" + archivo + "\" \"" + rutaExtract + "\"";
        //    p.Start();
        //    var output = p.StandardOutput.ReadToEnd();
        //    p.WaitForExit();
        //    return output;
        //}


        public static string Compress(string archivo, string rutaCompress, string rarNombre)
        {
            string output = "";
            try
            {
                RegistryKey theReg = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Classes").OpenSubKey("WinRAR").OpenSubKey("Shell").OpenSubKey("Open").OpenSubKey("Command");
    
                Object theObj = theReg.GetValue("");
                String theRar = theObj.ToString();
                theReg.Close();
                theRar = theRar.Substring(1, theRar.Length - 7);
                String theInfo = " a -ep \"" + rarNombre + "\" \"" + archivo+"\"";
                
                var p = new Process { StartInfo = { UseShellExecute = false, RedirectStandardOutput = true } };
                p.StartInfo.FileName = theRar;
                p.StartInfo.Arguments = theInfo;
                p.StartInfo.WorkingDirectory = rutaCompress;
                p.Start();
                 output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();
            }
            catch (Exception ex)
            {
               output+=" Ex..."+ex.Message;
            }
            return output;
        }
    }
}
