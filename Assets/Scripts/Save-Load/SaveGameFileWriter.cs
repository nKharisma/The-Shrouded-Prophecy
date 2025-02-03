using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameFileWriter
{
    public string saveFileName = "";
    public string saveFileDirectoryPath = "";
    
    public bool DoesSaveFileExist()
    {
        return File.Exists(saveFileDirectoryPath + saveFileName); //returns true if the file exists
    }
    
    public void CreateNewSaveFile(CharacterSaveData characterSaveData)
    {
    
        string savePath = Path.Combine(saveFileDirectoryPath, saveFileName);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
            //File.Create(saveFileDirectoryPath + saveFileName); //creates a new save file
            Debug.Log("New save file created at: " + saveFileDirectoryPath + saveFileName); 
            
            string saveData = JsonUtility.ToJson(characterSaveData, true); //converts the save data to a json string
            
            using (FileStream stream = new FileStream(savePath, FileMode.Create)) //creates the file for writing
            {
                using (StreamWriter writer = new StreamWriter(stream)) //creates a stream writer to write to the file
                {
                    writer.Write(saveData); //writes the save data to the file
                }
            }
        } catch (System.Exception e) {
            Debug.LogError("Error creating new save file:" + saveFileDirectoryPath + saveFileName + "\n" + e.Message);
        }
        
    }
    
    public CharacterSaveData LoadSaveFile()
    {
        CharacterSaveData characterData = new CharacterSaveData(); //creates a new instance of the save data class
        
        string loadedDataPath = saveFileDirectoryPath + saveFileName; //gets the path of the save file
        
        if(File.Exists(loadedDataPath)) //checks if the file exists
        {
            try {
                string data = "";
                using (FileStream stream = new FileStream(loadedDataPath, FileMode.Open)) //opens the file for reading
                {
                    using (StreamReader reader = new StreamReader(stream)) //creates a stream reader to read the file
                    {
                        data = reader.ReadToEnd(); //reads the entire file
                    }
                }
                
                characterData = JsonUtility.FromJson<CharacterSaveData>(data); //converts the json string to the save data class
            
            }catch (System.Exception e) {
                Debug.LogError("Error loading save file:" + loadedDataPath + "\n" + e.Message);
            }
            
        }
        
        return characterData; //returns the loaded save data
    }
    
    public void WriteSaveFile(string saveData)
    {
        File.WriteAllText(saveFileDirectoryPath + saveFileName, saveData); //writes the save data to the file
    }
    
    public void DeleteSaveFile()
    {
        File.Delete(saveFileDirectoryPath + saveFileName); //deletes the save file
    }
}
