namespace EUJIT.Interface
{
    public interface ISecureStorage
    {
        bool Contains(string key);
        void Delete(string key);
        byte[] Retrieve(string key);
        void Store(string key, byte[] dataBytes);
        string RetrieveString(string key);
        void StoreString(string key, string datastr);
        void StoreBool(string key, bool datastr);
        bool RetriveBool(string key);
    }
}
