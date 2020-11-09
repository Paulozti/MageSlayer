using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public bool level1 = false, level2 = false, level3 = false, level4 = false, level5 = false, level6 = false, level7 = false;
    public bool collectable1 = false, collectable2 = false, collectable3 = false, collectable4 = false, collectable5 = false, collectable6 = false, collectable7 = false;

    public SaveData LoadData()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
        SaveData save = JsonUtility.FromJson<SaveData>(json);
        return save;
    }

    public void SaveDataToFile(SaveData save)
    {
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
    }
}
