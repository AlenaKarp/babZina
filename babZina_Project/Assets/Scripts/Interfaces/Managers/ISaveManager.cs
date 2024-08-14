//this empty line for UTF-8 BOM header
public interface ISaveManager
{
    int? GetLastLevelWithProgress();
    int GetProgress(int level);
    void SaveProgress(int level, int progress);
}
