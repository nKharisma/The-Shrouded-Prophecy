using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerSaveSlot : MonoBehaviour
{
    SaveGameFileWriter saveGameFileWriter;
    
    [Header("Save Slot")]
    public SaveSlot characterSaveSlot;
    
    [Header("UI Elements/Player Data")]
    public TextMeshProUGUI questNameText;
    public TextMeshProUGUI timePlayedText;
    public TextMeshProUGUI datePlayedText;
    
    //add level and stats system later (?)
    
    private void OnEnable() 
    {
        LoadSaveSlot();
    }
    
    private void LoadSaveSlot()
    {
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //maybe see about creating a directory for the save files later
        
        //Debug.Log("Does this print?");
        
        if(characterSaveSlot == SaveSlot.Slot1)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist()) {
                questNameText.text = WorldSaveGameManager.instance.saveSlot01.questName; 
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot01.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                questNameText.text = "Empty";
                gameObject.SetActive(false);
            }
        }else if(characterSaveSlot == SaveSlot.Slot2)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist())
            {
                questNameText.text = WorldSaveGameManager.instance.saveSlot02.questName;
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot02.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }else if(characterSaveSlot == SaveSlot.Slot3)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist())
            {
                questNameText.text = WorldSaveGameManager.instance.saveSlot03.questName;
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot03.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }else if(characterSaveSlot == SaveSlot.Slot4)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist())
            {
                questNameText.text = WorldSaveGameManager.instance.saveSlot04.questName;
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot04.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }else if(characterSaveSlot == SaveSlot.Slot5)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist())
            {
                questNameText.text = WorldSaveGameManager.instance.saveSlot05.questName;
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot05.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }else if(characterSaveSlot == SaveSlot.Slot6)
        {
            saveGameFileWriter.saveFileName = WorldSaveGameManager.instance.WhichSaveFile(characterSaveSlot);
            
            if(saveGameFileWriter.DoesSaveFileExist())
            {
                questNameText.text = WorldSaveGameManager.instance.saveSlot06.questName;
                timePlayedText.text = WorldSaveGameManager.instance.saveSlot06.secondsPlayed.ToString();
                datePlayedText.text = saveGameFileWriter.GetFileCreationDate().ToString();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    public void LoadGameFromSaveSlot()
    {
        WorldSaveGameManager.instance.currentSaveSlot = characterSaveSlot;
        WorldSaveGameManager.instance.LoadGame();
    }
}
