using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager instance;

    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadGameMenu;
    
    [Header("Buttons")]
    [SerializeField] Button newGameButton;
    [SerializeField] Button loadGameButton;
    [SerializeField] Button returnGameButton;
    [SerializeField] Button deleteGameButton;
    
    [Header("Alerts")]
    [SerializeField] GameObject noSaveSlotsAvailable;
    [SerializeField] Button okButton;
    [SerializeField] GameObject deleteSlotPopUp;
    
    [Header("Save Slot")]
    public SaveSlot currentSaveSlot = SaveSlot.NO_SLOT;
    
    [Header("Title Screen Inputs")]
    [SerializeField] bool deleteCharacterSlot = false;
    
    private void Awake() {
        if (instance == null) 
        {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void StartNewGame()
    {
        WorldSaveGameManager.instance.NewGame(); //Create a new game
    }
    
    public void LoadGameMenu()
    {
        mainMenu.SetActive(false);
        loadGameMenu.SetActive(true);
        
        returnGameButton.Select(); 
    }
    
    public void CloseLoadGameMenu()
    {
        loadGameMenu.SetActive(false);
        mainMenu.SetActive(true);
        
        returnGameButton.Select();
    }
    
    public void DetermineFreeSaveSlots()
    {
        noSaveSlotsAvailable.SetActive(true);
        okButton.Select();
    }
    
    public void CloseNoSaveSlotsAvailable()
    {
        noSaveSlotsAvailable.SetActive(false);
        newGameButton.Select();
    }
    
    public void SelectSaveSlot(SaveSlot saveSlot)
    {
        currentSaveSlot = saveSlot;
    }
    
    public void SelectNoSlot()
    {
        currentSaveSlot = SaveSlot.NO_SLOT;
    }
    
    public void AttemptToDeleteSaveSlot()
    {
        if(currentSaveSlot != SaveSlot.NO_SLOT)
        {
            deleteSlotPopUp.SetActive(true);
            deleteGameButton.Select();
        }
    }
    
    public void DeleteSaveSlot()
    {
        deleteSlotPopUp.SetActive(false);
        WorldSaveGameManager.instance.DeleteGame(currentSaveSlot); //Delete the save slot
        loadGameMenu.SetActive(false);
        loadGameMenu.SetActive(true);
        returnGameButton.Select();
    }
    
    public void CloseDeleteSlotPopUp() 
    {
        deleteSlotPopUp.SetActive(false);
        returnGameButton.Select();
    }
}
