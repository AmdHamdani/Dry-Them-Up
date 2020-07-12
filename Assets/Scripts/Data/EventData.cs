using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData : ScriptableObject
{

    public string eventName;
    [TextArea]
    public string eventText;
    public OptionStr choiceOne;
    public OptionStr ChoiceTwo;
}

[System.Serializable]
public class OptionStr
{
    public string choice;
    public string result;
}