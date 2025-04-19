using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneManagement is a library that allows you to do some functionality with scenes such as loading a new scene
using System.IO;
using UnityEngine.UI;
using TMPro;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance; //a reference to the WorldSaveGameManager instance
    
    [SerializeField] public PlayerManager player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] int worldSceneIndex; //index of the world scene 
    
    [Header("Character Data")]
    public CharacterSaveData currentSaveData; //a reference to the current character data
    private SaveGameFileWriter saveGameFileWriter;
    
    public SaveSlot currentSaveSlot; //This is a reference to the current save slot
    private string saveFileName;
    
    private bool isNewGame = false;
    
    [Header("Save/Load")]
    [SerializeField] bool isSaving, isLoading; //These are bools to check if the game is saving or loading
    
    [Header("Save Slots")]
    public CharacterSaveData saveSlot01, saveSlot02, saveSlot03, saveSlot04, saveSlot05, saveSlot06; //These are references to the save slots

    // for loading screen
    public GameObject LoadingScreen;
    public TextMeshProUGUI ProgressText;
    
    
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
        LoadAllSaveSlots();
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
        isNewGame = true;
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath;
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot1);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot1;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot2);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot2;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot3);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot3;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot4);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot4;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot5);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot5;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot6);
        
        if(!saveGameFileWriter.DoesSaveFileExist())
        {
            currentSaveSlot = SaveSlot.Slot6;
            
            currentSaveData = new CharacterSaveData();
            
            GameObject playerInstance = Instantiate(playerPrefab);
            PlayerManager playerManager = playerInstance.GetComponent<PlayerManager>();
            if (playerManager != null)
            {
                currentSaveData.xPosition = playerManager.transform.position.x;
                currentSaveData.yPosition = playerManager.transform.position.y;
                currentSaveData.zPosition = playerManager.transform.position.z;
            }
            Destroy(playerInstance);
            
            StartCoroutine(LoadWorldScene());
            return;
        }
        TitleScreenManager.instance.DetermineFreeSaveSlots(); //if we reach here, there are no save slots available
    }
    
    public void SaveGame()
    {
        saveFileName = WhichSaveFile(currentSaveSlot);
        
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        saveGameFileWriter.saveFileName = saveFileName; //set the save file name
        
        player.SavePlayerData(ref currentSaveData); //save the player data
        QuestManager.instance.SaveQuest(ref currentSaveData); //save the quest data
        
        Debug.Log(currentSaveData.questDataList.Count);
        
        saveGameFileWriter.CreateNewSaveFile(currentSaveData); //create a new save file
    }
    
    public void LoadGame()
    {
        saveFileName = WhichSaveFile(currentSaveSlot);
        
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        saveGameFileWriter.saveFileName = saveFileName; //set the save file name
        
        currentSaveData = saveGameFileWriter.LoadSaveFile(); //load the save file
        
        Debug.Log(currentSaveData.questDataList.Count);
        
        StartCoroutine(LoadWorldScene()); //load the world scene while the save file is loading
    }
    
    public void DeleteGame(SaveSlot saveSlot)
    {
        
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        saveGameFileWriter.saveFileName = WhichSaveFile(saveSlot); //set the save file name
        
        saveGameFileWriter.DeleteSaveFile();
    }
    
    private void LoadAllSaveSlots() //preload all save slots
    {
        saveGameFileWriter = new SaveGameFileWriter();
        saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; //set the save file directory path
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot1);
        saveSlot01 = saveGameFileWriter.LoadSaveFile(); 
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot2); 
        saveSlot02 = saveGameFileWriter.LoadSaveFile(); 
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot3); 
        saveSlot03 = saveGameFileWriter.LoadSaveFile(); 
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot4); 
        saveSlot04 = saveGameFileWriter.LoadSaveFile(); 
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot5); 
        saveSlot05 = saveGameFileWriter.LoadSaveFile(); 
        
        saveGameFileWriter.saveFileName = WhichSaveFile(SaveSlot.Slot6);
        saveSlot06 = saveGameFileWriter.LoadSaveFile();
    }
    
    private void AutoSave()
    {
    saveFileName = WhichSaveFile(currentSaveSlot);

    saveGameFileWriter = new SaveGameFileWriter();
    saveGameFileWriter.saveFileDirectoryPath = Application.persistentDataPath; // Set the save file directory path
    saveGameFileWriter.saveFileName = saveFileName; // Set the save file name

    player.SavePlayerData(ref currentSaveData); // Save the player data
    QuestManager.instance.SaveQuest(ref currentSaveData); // Save the quest data

    saveGameFileWriter.CreateNewSaveFile(currentSaveData); // Create a new save file
    Debug.Log("Game auto-saved.");
    }
    
    public IEnumerator LoadWorldScene()
{
    //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex); // Load the world scene asynchronously
    
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currentSaveData.sceneIndex);
    
    while (!asyncLoad.isDone) // while the scene is not done loading
    {
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex); //Load the world scene asynchronously

        LoadingScreen.SetActive(true); //loading screen
        
        
        while (!asyncLoad.isDone) //while the scene is not done loading
        {
            Debug.Log("LOADING");
            //show percentage of loading
            float progressValue = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            int percentage = Mathf.RoundToInt(progressValue * 100);
            ProgressText.text = percentage + "%";
            yield return null; //return null
        }
        
        player.LoadPlayerData(ref currentSaveData);
    }
    
    if (currentSaveData == null)
    {
        Debug.LogError("currentSaveData is null");
        yield break;
    }
    
    if (QuestManager.instance == null)
    {
        Debug.LogError("QuestManager.instance is null");
        yield break;
    }
    
    if (!isNewGame)
        {
            foreach (Quest quest in QuestManager.instance.questMap.Values) // for each quest in the quest map
            {
                if (currentSaveData.questName == null)
                {
                    Debug.LogError("currentSaveData.questName is null");
                    continue;
                }
                
                Debug.Log(currentSaveData.questDataList.Count);
                
                QuestInfoSO questInfoSO = quest.questInfoSO;
                if (questInfoSO == null)
                {
                    Debug.LogError($"Failed to load quest info SO: {currentSaveData.questName}");
                    continue;
                }
                
                Quest loadedQuest = QuestManager.instance.LoadQuest(ref currentSaveData, questInfoSO);
                
                if (loadedQuest == null)
                {
                    Debug.LogError("loadedQuest is null");
                    continue;
                }
            }
        }
    
    if (player == null)
    {
        Debug.LogError("player is null");
        yield break;
    }
    
    player.LoadPlayerData(ref currentSaveData);
}
    
    public IEnumerator LoadSceneInGame(int sceneIndex, Vector3 spawnPosition)
    {
        AutoSave();
    
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex); // Load the world scene asynchronously
        
        while (!asyncLoad.isDone) // while the scene is not done loading
        {
            yield return null; // return null
        }
        Debug.Log(player.transform.position);
        
        //player.transform.position = spawnPosition;
        
        Debug.Log(spawnPosition);
        if (player == null)
        {
            Debug.LogError("player is null");
            yield break;
        }
        
        /*
        if(QuestManager.instance != null)
        {
            foreach (Quest quest in QuestManager.instance.questMap.Values)
            {
                if (currentSaveData.questName == null)
                {
                    Debug.LogError("currentSaveData.questName is null");
                    continue;
                }
    
                QuestInfoSO questInfoSO = quest.questInfoSO;
                if (questInfoSO == null)
                {
                    Debug.LogError($"Failed to load quest info SO: {currentSaveData.questName}");
                    continue;
                }

                Quest loadedQuest = QuestManager.instance.LoadQuest(ref currentSaveData, questInfoSO);

                if (loadedQuest == null)
                {
                    Debug.LogError("loadedQuest is null");
                    continue;
                }
            }
        }    */    
        AutoSave();
    }
    public string WhichSaveFile(SaveSlot characterSlot) //This function returns the save file name
    {
        string saveFileName = "";
    
        switch (characterSlot) //Switch statement for the current save slot
        {
            case SaveSlot.Slot1: 
                saveFileName = "slot_01";
                break;
            case SaveSlot.Slot2: 
                saveFileName = "slot_02";
                break;
            case SaveSlot.Slot3: 
                saveFileName = "slot_03"; 
                break;
            case SaveSlot.Slot4: 
                saveFileName = "slot_04"; 
                break;
            case SaveSlot.Slot5: 
                saveFileName = "slot_05"; 
                break;
            case SaveSlot.Slot6: 
                saveFileName = "slot_06"; 
                break;
            default: //Default case
                Debug.LogError("No save slot selected"); //Log an error message
                break;
        }
        
        return saveFileName;
    }
    
    public int GetWorldSceneIndex()
    {
        return worldSceneIndex; //Return the world scene index
    }
}


