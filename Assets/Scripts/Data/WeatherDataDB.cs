using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherDataDB", menuName = "Game/New Weather Combination")]
public class WeatherDataDB : ScriptableObject
{
    public List<WeatherCombination> combinations = new List<WeatherCombination>();

    public static WeatherDataDB Load(string fileName = "Weather/WeatherDataDB")
    {
        return Resources.Load<WeatherDataDB>(fileName);
    }

    public WeatherResult GetResult(string main, string secondary)
    {
        WeatherCombination data = null;

        combinations.ForEach((item) => {
            if(item.main == main && item.secondary == secondary)
            {
                data = item;
            }
        });

        return data.GetResult();
    }
}

[System.Serializable]
public class WeatherCombination
{
    public string main;
    public string secondary;
    public List<WeatherResult> result = new List<WeatherResult>();

    public WeatherResult GetResult()
    {
        return result[Random.Range(0, result.Count)];
    }
}