using VaultSave.Systems;

namespace VaultSave.Saver
{
    public interface ILoader
    {
        T Execute<T>(string path, SystemData password) where T : ISaveableEntity;
    }
}