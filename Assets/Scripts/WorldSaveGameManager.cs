using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //SceneMangement is a library that allows you to do some functionality with scenes such as loading a new scene

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance; //This is a reference to the WorldSaveGameManager instance
    
    [SerializeField] int worldSceneIndex; //index of the world scene 
    
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
    
    public IEnumerator LoadNewGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex); //Load the world scene asynchronously
        while (!asyncLoad.isDone) //While the scene is not done loading
        {
            yield return null; //Return null
        }
    }
    
    public int GetWorldSceneIndex()
    {
        return worldSceneIndex; //Return the world scene index
    }
}


