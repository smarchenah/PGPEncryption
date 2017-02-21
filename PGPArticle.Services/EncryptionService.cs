﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PGPArticle.Contracts;
//using Starksoft.Cryptography.OpenPGP;
using System.IO;
using System.Configuration;
//using Starksoft.Aspen.GnuPG;


namespace PGPArticle.Services
{
    public class EncryptionService : IEncryptionService 
    {
        Gpg gpg = new Gpg();
        private string appPath;

        #region Constructors

        public EncryptionService()
        {

        }

        public EncryptionService(string appPath)
        {
            this.appPath = appPath;
        }

        #endregion  

        public FileInfo EncryptFile(string keyUserId, string sourceFile, string encryptedFile)
        {
            // check parameters
            if (string.IsNullOrEmpty(keyUserId))
                throw new ArgumentException("keyUserId parameter is either empty or null", "keyUserId");
            if (string.IsNullOrEmpty(sourceFile))
                throw new ArgumentException("sourceFile parameter is either empty or null", "sourceFile");
            if (string.IsNullOrEmpty(encryptedFile))
                throw new ArgumentException("encryptedFile parameter is either empty or null", "encryptedFile");
           
            
            // Create streams - for each of the unencrypted source file and decrypted destination file
            using (Stream sourceFileStream = new FileStream(sourceFile, FileMode.Open))
            {
                using (Stream encryptedFileStream = new FileStream(encryptedFile, FileMode.Create))
                {
                    //  Specify the directory containing gpg.exe (not sure why).
                    try
                    {
                        gpg.BinaryPath = appPath;
                        gpg.Recipient = keyUserId;

                        //  Perform encryption
                        gpg.Encrypt(sourceFileStream, encryptedFileStream);

                        return new FileInfo(encryptedFile);
                    }
                    catch (Exception ex) { throw new ArgumentException(ex.Message, "encryptedFile"); }
                }
            }
        }

        public FileInfo DecryptFile(string encryptedSourceFile, string decryptedFile)
        {
            // check parameters
            if (string.IsNullOrEmpty(encryptedSourceFile))
                throw new ArgumentException("encryptedSourceFile parameter is either empty or null", "encryptedSourceFile");
            if (string.IsNullOrEmpty(decryptedFile))
                throw new ArgumentException("decryptedFile parameter is either empty or null", "decryptedFile");

            using (FileStream encryptedSourceFileStream = new FileStream(encryptedSourceFile, FileMode.Open))
            {
                //  make sure the stream is at the start.
                encryptedSourceFileStream.Position = 0;

                using (FileStream decryptedFileStream = new FileStream(decryptedFile, FileMode.Create))
                {
                    //  Specify the directory containing gpg.exe (again, not sure why).
                    gpg.BinaryPath = Path.GetDirectoryName(appPath);

                    //  Decrypt
                    gpg.Decrypt(encryptedSourceFileStream, decryptedFileStream);
                }
            }
            return new FileInfo(decryptedFile);
        }

        //public static string ReadKey(string pubkeyFile)
        //{
        //Stream fs = File.OpenRead(pubkeyFile);

        ////
        //// Read the public key rings
        ////
        //PgpPublicKeyRingBundle pubRings = new PgpPublicKeyRingBundle(PgpUtilities.GetDecoderStream(fs));
        //fs.Close();

        //foreach (PgpPublicKeyRing pgpPub in pubRings.GetKeyRings())
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (ArmoredOutputStream aos = new ArmoredOutputStream(ms))
        //            pgpPub.Encode(aos);
        //        return System.Text.Encoding.ASCII.GetString(ms.ToArray());
        //    }
        //}
        //return null;
        //}
    }
}
