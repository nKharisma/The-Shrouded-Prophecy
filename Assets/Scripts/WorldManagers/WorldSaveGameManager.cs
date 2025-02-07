using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManagement is a library that allows you to do some functionality with scenes such as loading a new scene

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance; //a reference to the WorldSaveGameManager instance
    
    [SerializeField] PlayerManager player;
    [SerializeField] int worldSceneIndex; //index of the world scene 
    
    [Header("Character Data")]
    public CharacterSaveData currentSaveData; //a reference to the current character data
    private SaveGameFileWriter saveGameFileWriter;
    
    public Enums.SaveSlot currentSaveSlot; //This is a reference to the current save slot
    private string saveFileName;
    
    [Header("Save/Load")]
    [SerializeField] bool isSaving, isLoading; //These are bools to check if the game is saving or loading
    
    [Header("Save Slots")]
    public CharacterSaveData saveSlot01, saveSlot02, saveSlot03, saveSlot04, saveSlot05; //These are references to the save slots
    
    
    private void Awake()
    {
        if (instance == null) //If the instance is null
        {
            instance = this; //Set the instance to this
            DontDestroyOnLoad(gameObject); //Don't destroy the game object when loading a new scene
        }
        else
        {
            Destroy(gameObject); //Destroy the game object if the instance is not null
        }
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject); //Don't destroy the game object when loading a new scene
    }
    
    private void Update()
    {
        if (isSaving) //If the game is saving
        {
            SaveGame(); //Save the game
            isSaving = false; //Set isSaving to false
        }
        
        if (isLoading) //If the game is loading
        {
            LoadGame(); //Load the game
            isLoading = false; //Set isLoading to false
        }
    }
    
    public void NewGame()
    {
        WhichSaveFile();
        
        currentSaveData = new CharacterSaveData();
    }
    
    public void SaveGame()
    {
        WhichSaveFile();
        
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        saveGameFileWriter.saveFileName = saveFileName; //set the save file name
        
        player.SavePlayerData(ref currentSaveData); //save the player data
        
        saveGameFileWriter.CreateNewSaveFile(currentSaveData); //create a new save file
    }
    
    public void LoadGame()
    {
        WhichSaveFile();
        
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        saveGameFileWriter.saveFileName = saveFileName; //set the save file name
        
        currentSaveData = saveGameFileWriter.LoadSaveFile(); //load the save file
        
        StartCoroutine(LoadWorldScene()); //load the world scene while the save file is loading
    }
    
    public IEnumerator LoadWorldScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex); //Load the world scene asynchronously
        while (!asyncLoad.isDone) //while the scene is not done loading
        {
            yield return null; //return null
        }
    }
    
    private void WhichSaveFile()
    {
        switch (currentSaveSlot) //Switch statement for the current save slot
        {
            case Enums.SaveSlot.Slot1: 
                saveFileName = "slot_01";
                break;
            case Enums.SaveSlot.Slot2: 
                saveFileName = "slot_02";
                break;
            case Enums.SaveSlot.Slot3: 
                saveFileName = "slot_03"; 
                break;
            case Enums.SaveSlot.Slot4: 
                saveFileName = "slot_04"; 
                break;
            case Enums.SaveSlot.Slot5: 
                saveFileName = "slot_05"; 
                break;
            default: //Default case
                Debug.LogError("No save slot selected"); //Log an error message
                break;
        }
    }
    
    public int GetWorldSceneIndex()
    {
        return worldSceneIndex; //Return the world scene index
    }
}


