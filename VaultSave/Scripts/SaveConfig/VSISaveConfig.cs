using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public interface VSISaveConfig
    {
        string GetExtention();
        ISaver GetSaver();
        ILoader GetLoader();
    }
}