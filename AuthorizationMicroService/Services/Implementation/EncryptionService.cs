using System.Security.Cryptography;
using System.Text;

namespace AuthorizationMicroService.Services.Implementation;

public class EncryptionService : IEncryptionService
{
  private SHA512 _sha512;

  public EncryptionService()
  {
    _sha512 = SHA512.Create();
  }

  public byte[] HashString(string stringToBeHashed)
  {
    return _sha512.ComputeHash(Encoding.UTF8.GetBytes(stringToBeHashed));
  }

  public string EncryptData(string dataToEncrypt)
  {
    throw new NotImplementedException();
  }

  public string DecryptData(string dataToEncrypt)
  {
    throw new NotImplementedException();
  }
}