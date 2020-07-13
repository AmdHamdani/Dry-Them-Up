using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : SingletonBehaviour<GameEventSystem>
{
    public int score;

    public EventData currentEvent;
    public OptionResult result;
    public EventDataDB database;
    public DragAndDropComponent[] components;

    private bool isEventActive;
    private bool isFinish = false;

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

    private void Update()
    {
        if (!isEventActive && IsTrue(ClothState.MostlyWet))
        {
            SetEventData();
            isEventActive = true;
            Debug.Log("SET EVENT DATA");
        }

        if(!isFinish && IsTrue(ClothState.Dry))
        {
            GetResult();
        }
    }

    public void SetEventData()
    {
        currentEvent = database.GetEventData();
        InGameUI.Instance.skyText.text = currentEvent.eventText;

        if (!string.IsNullOrEmpty(currentEvent.choiceOne.choice))
        {
            InGameUI.Instance.leftChoiceText.text = "[ " + currentEvent.choiceOne.choice;
            InGameUI.Instance.leftChoice.onClick.AddListener(() => {
                result = currentEvent.choiceOne.result;
                //Debug.Log("RESULT : " + currentEvent.choiceOne.result);
            });
    }

        if (!string.IsNullOrEmpty(currentEvent.choiceTwo.choice))
        {
            InGameUI.Instance.rightChoiceText.text = currentEvent.choiceTwo.choice + " ]";
            InGameUI.Instance.rightChoice.onClick.AddListener(() => {
                result = currentEvent.choiceTwo.result;
                //Debug.Log("RESULT : " + currentEvent.choiceTwo.result);
            });
        }
    }

    private bool IsTrue(ClothState threshold)
    {
        int counter = 0;
        foreach(var item in components) {
            if((int)item.state >= (int)threshold)
            {
                counter++;
            }
        };

        return counter == components.Length;
    }

    private void GetResult()
    {
        switch (result)
        {
            case OptionResult.Num: score += components.Length; break;
        }
    }

}
