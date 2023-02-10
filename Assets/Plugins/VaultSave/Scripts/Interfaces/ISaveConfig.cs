namespace ValueSave.Interfaces
{
    public interface ISaveConfig
    {
        string GetExtention();
        ISaver GetSaver();
        ILoader GetLoader();
    }
}