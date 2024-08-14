//this empty line for UTF-8 BOM header
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour, ISaveManager
{
    public const int maxLevelIndex = 3;

    private int[] savedProgress;

    private void Awake()
    {
        InitSavedProgress();
    }

    private void InitSavedProgress()
    {
        SaveData saveData = GetSavedData();
        savedProgress = saveData.progress;
    }

    private SaveData CreateSaveData()
    {
        List<int> progress = new List<int>();

        for (int i = 0; i <= maxLevelIndex; i++)
        {
            progress.Add(0);
        }

        SaveData newSaveData = new SaveData()
        {
            progress = progress.ToArray(),
        };

        Save(newSaveData);
        return newSaveData;
    }

    public int? GetLastLevelWithProgress()
    {
        if (savedProgress == null)
        {
            return null;
        }

        int? lastLevelIndexWithProgress = null;

        for (int i = 0; i <= maxLevelIndex; i++)
        {
            if (savedProgress[i] > 0)
            {
                if (lastLevelIndexWithProgress.HasValue == false
                    || i > lastLevelIndexWithProgress)
                { 
                    lastLevelIndexWithProgress = i;
                }
            }
        }

        return lastLevelIndexWithProgress;
    }

    public int GetProgress(int level)
    {
        if (savedProgress == null)
        {
            InitSavedProgress();
        }

        return savedProgress[level];
    }

    public void SaveProgress(int level, int progress)
    {
        if (progress > savedProgress[level])
        {
            savedProgress[level] = progress;
        }

        SaveData newSaveData = new SaveData()
        {
            progress = savedProgress,
        };

        Save(newSaveData);
    }

    private void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "UserProgress.json"), json);
    }

    private SaveData GetSavedData()
    {
        string path = Path.Combine(Application.persistentDataPath, "UserProgress.json");

        if (File.Exists(path) == false)
        {
            return CreateSaveData();
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        return data;
    }
}

public class SaveData
{
    public int[] progress;
}
