using System.IO;

namespace PGPArticle.Contracts
{
  public interface IEncryptionService
  {
    FileInfo DecryptFile(string encryptedSourceFile, string decryptedFile);
    FileInfo EncryptFile(string keyUserId, string sourceFile, string encryptedFile);
  }
}
