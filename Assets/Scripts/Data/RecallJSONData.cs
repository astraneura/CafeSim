using UnityEngine;
using System.IO;

public class RecallJSONData : MonoBehaviour
{
    private DataProcessing dataProcessing;
    private string path = "";

    private void OnEnable()
    {
        SetPath();

        if (File.Exists(path))
        {
            LoadData();
        }
    }

    private void SetPath()
    {
        path = Application.persistentDataPath + "/latest_saveData.json";
    }

    private void LoadData()
    {
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        dataProcessing = JsonUtility.FromJson<DataProcessing>(json);
        SaveImportedData();
    }

    private void SaveImportedData()
    {
        GetComponent<UserProfileData>().moneyMade = dataProcessing.moneyMade;
        GetComponent<UserProfileData>().ordersCompleted = dataProcessing.ordersCompleted;
        GetComponent<UserProfileData>().ordersFailed = dataProcessing.ordersFailed;
        GetComponent<UserProfileData>().timePlayed = dataProcessing.timePlayed;
    }
}
