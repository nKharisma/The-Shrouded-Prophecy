using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    // Update is called once per frame
    
    void Start()
    {
        if(WorldSaveGameManager.instance != null)
        {
            player = WorldSaveGameManager.instance.player.transform;
        }else{
            Debug.LogError("WorldSaveGameManager instance is null. Make sure it is initialized before using FollowPlayer.");
        }
    }
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 3, -7);
    }
}
