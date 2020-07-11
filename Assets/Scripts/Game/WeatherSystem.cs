using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{

    public List<Weather> weathers = new List<Weather>();

    public Weather mainWeather;
    public Weather secondaryWeather;
    public WeatherResult result;

    private InGameUIComponent inGameUI;
    private WeatherCombinationData weatherData;

    private void Awake()
    {
        inGameUI = GetComponent<InGameUIComponent>();
        weatherData = WeatherCombinationData.Load();
    }

    private void Start()
    {
        SetWeather();

        inGameUI.skyText.text = mainWeather.GetText();
    }

    public void SetWeather()
    {
        mainWeather = weathers[Random.Range(0, weathers.Count)];
        secondaryWeather = weathers[Random.Range(0, weathers.Count)];

        result = weatherData.GetResult(mainWeather.weatherName, secondaryWeather.weatherName);
    }

    public void SetSkyText(string text)
    {
        inGameUI.skyText.text = text;
    }
}
