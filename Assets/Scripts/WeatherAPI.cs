using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class WeatherAPI : MonoBehaviour
{
    public void SetWeatherAPIJson(){
        StartCoroutine(nameof(Fetch));
    }
    
    public IEnumerator Fetch(){
        // 仙台の情報
        string url = "https://weather.tsukumijima.net/api/forecast?city=040010";

        UnityWebRequest request = UnityWebRequest.Get (url);

        yield return request.Send();

        if (request.isHttpError || request.isNetworkError)
        {
            //エラー確認
            Debug.Log(request.error);
        }
        else
        {
            string jsonText = request.downloadHandler.text;
            Debug.Log(jsonText);
            WeatherAPIJson weatherObject = new WeatherAPIJson();
            weatherObject = LitJson.JsonMapper.ToObject<WeatherAPIJson>(jsonText);
            CommonData.weatherAPIJson = weatherObject;
            Debug.Log(weatherObject.forecasts[0].date);
            Debug.Log(weatherObject.forecasts[0].telop);
        }
    }
}
