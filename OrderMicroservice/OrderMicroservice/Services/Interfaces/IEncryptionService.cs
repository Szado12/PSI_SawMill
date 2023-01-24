namespace OrderMicroservice.Services.Interfaces
{
    public interface IEncryptionService
    {
        string DecryptData(byte[] dataToDecrypt);
        byte[] EncryptData(string dataToEncrypt);
        byte[] HashData(string dataToHash);
    }
}
