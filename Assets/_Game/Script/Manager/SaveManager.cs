using System.IO;
using UnityEngine;
 
public class SaveManager : Singleton<SaveManager>
{
    private const string PATH = "/savegame.json";
    private int unlockLevel;
    public int UnlockLevel
    {
        get { return unlockLevel; }
        set
        {
            unlockLevel = value;
            SaveData();
        }
    }

    private void Awake()
    {
        LoadData();
    }
    public void SaveData()
    {
        //Custom data before saving
        GameData saveData = new GameData
        {
            unlockLevel = this.UnlockLevel,

        };

        string path = Application.persistentDataPath + PATH;
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + PATH;
        //Custom default data
        GameData defaultData = new GameData
        {
            unlockLevel = 0,
        };
        if (!File.Exists(path))
        {
            Debug.Log("Cann't load data, file not found");
            this.unlockLevel = defaultData.unlockLevel;
            SaveData();
            return;
        }
        string json = File.ReadAllText(path);
        defaultData = JsonUtility.FromJson<GameData>(json);
        this.unlockLevel = defaultData.unlockLevel;
    }

}


public class GameData
{
    public int unlockLevel;
}


