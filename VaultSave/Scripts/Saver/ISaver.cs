using VaultSave.Systems;

namespace VaultSave.Saver
{
    public interface ISaver
    {
        void Execute<T>(T dataToSave, string path, VSSystemData password);
    }
}