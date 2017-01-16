using System;
using System.Linq;
using Renci.SshNet;

namespace PubliPayments.Negocios
{
// ReSharper disable once InconsistentNaming
    public class SFTP
    {
        public string CheckSftp(string host, int port, string username, string password, string remoteDirectory)
        {
            string resultado = "";
            using (var sftp = new SftpClient(host, 22, username, password))
            {
                sftp.OperationTimeout = TimeSpan.FromMilliseconds(10000);
                sftp.Connect();
                var files = sftp.ListDirectory(remoteDirectory);
                if (files.Any())
                    resultado = "OK";
                sftp.Disconnect();
            }
            return resultado;
        }
    }
}
