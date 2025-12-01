using System.Collections;
using System.IO;
using UnityEngine;

public class SaveJSONData : MonoBehaviour
{
    private DataProcessing dataProcessing;
    private string path;

    void Start()
    {
        SetPath();
    }

    private void SetPath()
    {
        path = Application.persistentDataPath + "/" + System.DateTime.UtcNow.ToLocalTime().
        ToString("yyyy-MM-dd_HH-mm-ss") + "_saveData.json";
    }

    void OnApplicationQuit()
    {
        GameController.Instance.CalculatePlayTime();
        CreateDataToSave();
        SaveData();
        StartCoroutine(ExitPause());
    }

    private IEnumerator ExitPause()
    {
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
    }

    private void CreateDataToSave()
    {
        dataProcessing = new DataProcessing(
            GetComponent<UserProfileData>().moneyMade,
            GetComponent<UserProfileData>().ordersCompleted,
            GetComponent<UserProfileData>().ordersFailed,
            GetComponent<UserProfileData>().timePlayed
            );
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(dataProcessing);
        StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
        writer.Close();
    }
}
