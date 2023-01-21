namespace EmployeeMicroservice.Services.Interfaces
{
    public interface IEncryptionService
    {
        public byte[] EncryptData(string dataToEncrypt);
        public string DecryptData(byte[] dataToDecrypt);
        public byte[] HashData(string dataToHash);
    }
}
