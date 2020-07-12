using UnityEngine;

public class GameEventSystem : SingletonBehaviour<GameEventSystem>
{

    public EventData currentEvent;
    public OptionResult result;
    public EventDataDB database;

    private void Start()
    {
        Debug.Log("LOAD");
        database = EventDataDB.Load();
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

}
