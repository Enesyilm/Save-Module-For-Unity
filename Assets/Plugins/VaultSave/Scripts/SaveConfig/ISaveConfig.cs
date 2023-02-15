using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public interface ISaveConfig
    {
        string GetExtention();
        ISaver GetSaver();
        ILoader GetLoader();
    }
}