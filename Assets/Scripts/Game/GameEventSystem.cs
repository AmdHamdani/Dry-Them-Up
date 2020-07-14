using UnityEngine;

public class GameEventSystem : SingletonBehaviour<GameEventSystem>
{
    public int score;

    public EventData currentEvent;
    public OptionResult result;
    public EventDataDB database;
    public DragAndDropComponent[] components;

    private bool isEventActive = false;
    private bool isFinish = false;
    private WeatherSystem weatherSystem;

    private float tweenSpeed = 1f;
    private bool runTween = false;
    private Color targetColor = Color.black;
    private System.Action OnTweenCompleted;

    private void Awake()
    {
        components = FindObjectsOfType<DragAndDropComponent>();
        weatherSystem = FindObjectOfType<WeatherSystem>();
    }

    private void Start()
    {
        database = EventDataDB.Load();

        ClearUIText();
    }

    private void Update()
    {
        if (!isEventActive && IsTrue(ClothState.MostlyWet))
        {
            SetEventData();
            isEventActive = true;
        }

        if (!isFinish && IsTrue(ClothState.Dry))
        {
            isFinish = true;
        }

        if (runTween)
        {
            TweenColor();
        }
    }

    public void SetEventData()
    {
        currentEvent = database.GetEventData();
        InGameUI.Instance.skyText.text = currentEvent.eventText;

        if (!string.IsNullOrEmpty(currentEvent.choiceOne.choice))
        {
            InGameUI.Instance.leftChoiceText.text = "[ " + currentEvent.choiceOne.choice;
            InGameUI.Instance.leftChoice.onClick.AddListener(() =>
            {
                result = currentEvent.choiceOne.result;
                ShowPanel(currentEvent.choiceOne.detail);
                ClearUIText();
            });
        }

        if (!string.IsNullOrEmpty(currentEvent.choiceTwo.choice))
        {
            InGameUI.Instance.rightChoiceText.text = currentEvent.choiceTwo.choice + " ]";
            InGameUI.Instance.rightChoice.onClick.AddListener(() =>
            {
                result = currentEvent.choiceTwo.result;
                ShowPanel(currentEvent.choiceTwo.detail);
                ClearUIText();
            });
        }
    }

    private bool IsTrue(ClothState threshold)
    {
        int counter = 0;
        foreach (var item in components)
        {
            if ((int)item.state >= (int)threshold)
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
            case OptionResult.RandomNum:
                int random = Random.Range(0, components.Length);
                int loop = components.Length - random;
                score += random;
                for (int i = 0; i < loop; i++)
                {
                    components[i].gameObject.SetActive(false);
                }
                break;
        }
        Debug.Log(components.Length);
        Debug.Log("Score : " + score);
    }

    private void ClearUIText()
    {
        InGameUI.Instance.skyText.text = string.Empty;
        InGameUI.Instance.leftChoiceText.text = string.Empty;
        InGameUI.Instance.rightChoiceText.text = string.Empty;
    }

    private void ShowPanel(string detail)
    {
        InGameUI.Instance.panel.SetActive(true);
        tweenSpeed = 7;
        runTween = true;

        StartCoroutine(Fun.WaitFor(.2f, () => InGameUI.Instance.panelText.text = detail));

        OnTweenCompleted = () =>
        {
            InGameUI.Instance.panelImage.color = Color.black;

            StartCoroutine(Fun.WaitFor(2f, () =>
            {
                GetResult();
                InGameUI.Instance.skyText.text = weatherSystem.GetResultText();
                InGameUI.Instance.panel.SetActive(false);
                foreach (var item in components)
                {
                    item.state = ClothState.Dry;
                }
                StartCoroutine(Fun.WaitFor(2f, () => InGameUI.Instance.skyText.text = string.Empty));
            }));
        };
    }

    private void TweenColor()
    {
        if (!(InGameUI.Instance.panelImage.color == targetColor))
        {
            InGameUI.Instance.panelImage.color = Color.Lerp(InGameUI.Instance.panelImage.color, targetColor, tweenSpeed * Time.deltaTime);
        }
        else
        {
            runTween = false;
            OnTweenCompleted?.Invoke();
        }
    }

}
