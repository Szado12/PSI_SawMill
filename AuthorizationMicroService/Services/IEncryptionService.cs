namespace AuthorizationMicroService.Services
{
  public interface IEncryptionService
  {
    byte[] HashString(string stringToBeHashed);
    string EncryptData(string dataToEncrypt);
    string DecryptData(string dataToEncrypt);
  }
}
