namespace AuthorizationMicroService.Services
{
  public interface IEncryptionService
  {
    byte[] HashData(string stringToBeHashed);
    string DecryptData(byte[] dataToDecrypt);
  }
}
