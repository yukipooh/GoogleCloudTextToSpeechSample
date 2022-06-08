using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class WeatherAPIJson
{
    public struct Forecasts{
        public string date;    //日付
        public string dateLabel;   //雨　とか
        public string telop;    //雨とか
        public Detail detail;
        public Temperature temperature;
        public ChanceOfRain chanceOfRain;
        public Image image;
    }
    public struct Detail{
        public string weather;
        public string wind;
        public string wave;
    }
    public struct Temperature{
        TempShape min;
        TempShape max;
    }
    public struct TempShape{
        public string celsius;
        public string fahrenheit;
    }
    public struct ChanceOfRain{
        public string T00_06;
        public string T06_12;
        public string T12_18;
        public string T18_24;
    }
    public struct Image{
        string title;
        string url;
        int width;
        int height;
    }

    public Forecasts[] forecasts;

}
