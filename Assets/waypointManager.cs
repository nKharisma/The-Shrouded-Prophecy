using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class waypointManager : MonoBehaviour
{
    public static waypointManager instance;
    public List<Waypoint_Indicator> waypoints = new List<Waypoint_Indicator>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GameEventsManager.instance.playerEvents.onPlayerWaypointChange += ToggleNextWaypoint;
    }

    public void ToggleNextWaypoint(int index)
    {
        foreach (Waypoint_Indicator waypoint in waypoints)
        {
            waypoint.enabled = false;
        }

        waypoints[index].enabled = true;
    }
}
