using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : SingletonBehaviour<GameEventSystem>
{

    public ClothState threshold;
    public EventData currentEvent;
    public OptionResult result;
    public EventDataDB database;
    public DragAndDropComponent[] components;

    private void Awake()
    {
        components = FindObjectsOfType<DragAndDropComponent>();    
    }

    private void Start()
    {
        database = EventDataDB.Load();

        InGameUI.Instance.skyText.text = string.Empty;
        InGameUI.Instance.leftChoiceText.text = string.Empty;
        InGameUI.Instance.rightChoiceText.text = string.Empty;
    }

    public void SetEventData()
    {
        currentEvent = database.GetEventData();
        InGameUI.Instance.skyText.text = currentEvent.eventText;

        if(!string.IsNullOrEmpty(currentEvent.choiceOne.choice))
        {
            InGameUI.Instance.leftChoiceText.text = "[ " + currentEvent.choiceOne.choice;
            InGameUI.Instance.leftChoice.onClick.AddListener(() => {
                result = currentEvent.choiceOne.result;
                Debug.Log("RESULT : " + result);
            });
        }

        if(!string.IsNullOrEmpty(currentEvent.choiceTwo.choice))
        {
            InGameUI.Instance.rightChoiceText.text = currentEvent.choiceTwo.choice + " ]";
            InGameUI.Instance.rightChoice.onClick.AddListener(() => {
                result = result = currentEvent.choiceTwo.result;
                Debug.Log("RESULT : " + result);
            });
        }
    }

    private bool IsTrue()
    {
        int counter = 0;
        foreach(var item in components) {
            if(item.state == threshold)
            {
                counter++;
            }
        };

        return counter == components.Length;
    }

}
