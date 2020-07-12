using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventDataDB", menuName = "Game/EventDataDB")]
public class EventDataDB : ScriptableObject
{

    public int totalCloth = 0;
    public List<EventData> database = new List<EventData>();

    public static EventDataDB Load(string fileName = "Events/EventDataDB")
    {
        return Resources.Load<EventDataDB>(fileName);
    }

    public EventData GetEventData()
    {
        return database[Random.Range(0, database.Count)];
    }
}
