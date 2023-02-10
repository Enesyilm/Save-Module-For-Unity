using ValueSave.Systems;

namespace ValueSave.Interfaces
{
    public interface ISaver
    {
        void Execute<T>(T dataToSave, string path, SystemData password) where T : ISaveableEntity;
    }
}