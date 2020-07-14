using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{

    public List<Weather> weathers = new List<Weather>();

    public Weather mainWeather;
    public Weather secondaryWeather;
    public WeatherResult result;

    private WeatherDataDB weatherData;

    private void Awake()
    {
        weatherData = WeatherDataDB.Load();
    }

    private void Start()
    {
        SetWeather();
        //Debug.Log(mainWeather.GetText());
        InGameUI.Instance.skyText.text = mainWeather.GetText();

        StartCoroutine(Fun.WaitFor(1f, () => InGameUI.Instance.skyText.text = string.Empty));
    }

    public void SetWeather()
    {
        mainWeather = weathers[Random.Range(0, weathers.Count)];
        secondaryWeather = weathers[Random.Range(0, weathers.Count)];

        result = weatherData.GetResult(mainWeather.weatherName, secondaryWeather.weatherName);
    }

    public string GetResultText()
    {
        switch(result)
        {
            case WeatherResult.BlownUp: return "Your clothes is blown up by the wind . . .";
            case WeatherResult.Wet: return "Your clothes are wet . . .";
            case WeatherResult.Dry: return "Your clothers are dry now . . .";
            default: return string.Empty;
        }
    }

    public void SetSkyText(string text)
    {
        InGameUI.Instance.skyText.text = text;
    }
}
