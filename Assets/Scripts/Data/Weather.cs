using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weather", menuName = "Game/New Weather")]
public class Weather : ScriptableObject
{
    public string weatherName;
    [TextArea]
    public List<string> weatherText;

    public string GetText()
    {
        return weatherText[Random.Range(0, weatherText.Count)];
    }
}
