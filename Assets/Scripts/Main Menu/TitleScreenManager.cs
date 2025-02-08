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
    
    [Header("Alerts")]
    [SerializeField] GameObject noSaveSlotsAvailable;
    [SerializeField] Button okButton;
    
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
        
        loadGameButton.Select(); 
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
}
