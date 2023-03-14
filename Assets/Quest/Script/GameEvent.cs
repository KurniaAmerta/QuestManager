using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public string _eventDescription;
}

public class BuildingGameEvent : GameEvent {
    public string _buildingName;

    public BuildingGameEvent(string name) {
        _buildingName = name;
    }
} 
