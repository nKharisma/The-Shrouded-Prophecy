using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    public void StartNewGame()
    {
        WorldSaveGameManager.instance.NewGame(); //Create a new game
        StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene()); //Start a new game
    }
}
