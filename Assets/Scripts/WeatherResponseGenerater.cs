using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class WeatherResponseGenerater : ResponseGenerator
{
    private const string ErrorMessage = "よくわかりませんでした";

    public override bool IsAvailable(string input){
        return true;
    }

    public override string Generate(string input){
        return "test";
    }

    public override string Generate(string input, WeatherAPIJson weatherAPIJson){
        int dayNum = (int)DayNum.TODAY; //0:今日 1:明日
        if(input.Contains("明日") || input.Contains("あした")) dayNum = (int)DayNum.TOMORROW;
        string day = weatherAPIJson.forecasts[dayNum].dateLabel;
        string weather = weatherAPIJson.forecasts[dayNum].telop;
        string text = $"仙台市の{day}の天気は{weather}です。";
        return text;
    }

    enum DayNum{
        TODAY,
        TOMORROW,
    }
}
