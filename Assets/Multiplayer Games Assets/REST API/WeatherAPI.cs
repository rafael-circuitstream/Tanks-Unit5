using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WeatherAPI : MonoBehaviour
{
    [SerializeField] private string appid;
    [SerializeField] private TextMeshProUGUI cityName;
    [SerializeField] private TextMeshProUGUI currentTemp;
    [SerializeField] private TextMeshProUGUI weatherTitle;
    [SerializeField] private TextMeshProUGUI feelsLike;
    [SerializeField] private TextMeshProUGUI humidity;

    [SerializeField] private RawImage weatherImage;

    [SerializeField] private TMP_InputField searchBar;


    public WeatherInfo info;

    //Button will call this method
    public void MakeSearch()
    {
        StartCoroutine(FetchWeatherInformation());
    }

    //This will download weather information and place on the UI
    IEnumerator FetchWeatherInformation()
    {
        string location = searchBar.text;
        string apiCall = "https://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + appid + "&units=metric";

        UnityWebRequest myRequest = UnityWebRequest.Get(apiCall);
        yield return myRequest.SendWebRequest();

        info = JsonUtility.FromJson<WeatherInfo>(myRequest.downloadHandler.text);

        //GOOGLE API FOR LOCATIONS AND ADDRESS
        cityName.text = searchBar.text;

        humidity.text = info.main.humidity.ToString() + "%";
        feelsLike.text = info.main.feels_like.ToString() + "º C";
        weatherTitle.text = info.weather[0].main;
        currentTemp.text = info.main.temp.ToString() + "º C";


        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture("http://openweathermap.org/img/wn/" + info.weather[0].icon + "@2x.png");
        yield return imageRequest.SendWebRequest();

        if (imageRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(imageRequest.error);
        }
        else
        {
            Texture myTexture = DownloadHandlerTexture.GetContent(imageRequest);
            weatherImage.texture = myTexture;
        }

    }
}

[System.Serializable]
public class WeatherInfo
{
    public int id;
    public MainWeatherInfo main;
    public WeatherCondition[] weather;
}

[System.Serializable]
public class MainWeatherInfo
{
    public float temp;
    public float feels_like;
    public float humidity;
    public float pressure;
}

[System.Serializable]
public class WeatherCondition
{
    public string main;
    public string description;
    public string icon;
}

