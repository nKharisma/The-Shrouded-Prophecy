using UnityEngine;

public class PlaytimeTracker : MonoBehaviour
{
    private float sessionStartTime;
    private CharacterSaveData saveData;
    private SaveGameFileWriter saveFileWriter;

    void Start()
    {
        // Initialize SaveGameFileWriter
        saveFileWriter = new SaveGameFileWriter();
        saveFileWriter.saveFileDirectoryPath = Application.persistentDataPath; // Set save directory
        saveFileWriter.saveFileName = "savefile.json"; // Example save file name

        // Load existing save data or create new one
        if (saveFileWriter.DoesSaveFileExist())
        {
            saveData = saveFileWriter.LoadSaveFile();
        }
        else
        {
            saveData = new CharacterSaveData();
            saveData.secondsPlayed = 0;
        }

        sessionStartTime = Time.time; // Mark when session starts
    }

    void Update()
    {
        // Calculate total playtime (saved time + current session time)
        float currentSessionTime = Time.time - sessionStartTime;
        float updatedPlaytime = saveData.secondsPlayed + currentSessionTime;

        Debug.Log("Current Playtime: " + FormatTime(updatedPlaytime));
    }

    void OnApplicationQuit()
    {
        SavePlaytime();
    }

    void SavePlaytime()
    {
        float finalPlaytime = saveData.secondsPlayed + (Time.time - sessionStartTime);
        saveData.secondsPlayed = finalPlaytime; // Update saved time

        // Convert saveData to JSON and write to file
        string saveJson = JsonUtility.ToJson(saveData, true);
        saveFileWriter.WriteSaveFile(saveJson);

        Debug.Log("Playtime saved: " + FormatTime(finalPlaytime));
    }

    string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}
