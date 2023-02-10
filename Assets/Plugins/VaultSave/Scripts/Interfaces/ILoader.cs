using ValueSave.Systems;

namespace ValueSave.Interfaces
{
    public interface ILoader
    {
        T Execute<T>(string path, SystemData password) where T : ISaveableEntity;
    }
}