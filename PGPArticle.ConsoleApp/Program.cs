using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGPArticle.Contracts;
using PGPArticle.Services;

namespace PGPArticle.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncryptionService encryptionService = new EncryptionService(@"C:\Program Files (x86)\GNU\GnuPG\pub\gpg2.exe");

            //  Change the parameters to your private key's keyuserid and input/output files.
            var encryptedFile = encryptionService.EncryptFile("74AA30C1", @"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner\TestVideo1.mp4", @"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner\TestVideo1.mp4.pgp");
            //var encryptedFile = encryptionService.DecryptFile(@"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner\TestVideo1.mp4.pgp", @"C:\Users\marchena.INCAS\Desktop\Sergio\Dokumenten\VoiceCleaner\TestVideo1.mp4");

            Console.WriteLine(encryptedFile.Name);

            Console.ReadLine();

        }
    }
}
