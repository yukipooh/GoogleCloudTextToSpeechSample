using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Agent : MonoBehaviour
{
    [SerializeField] private List<InputBase> inputs;
    [SerializeField] private List<OutputBase> outputs;
    [SerializeField] WeatherAPI weatherAPI;
    WeatherAPIJson weatherAPIJson;
    private ResponseGenerator responseGenerator;

    private void Awake() {
        responseGenerator = new DialogueResponseGenerator();
        weatherAPI.SetWeatherAPIJson();
        
            
    }

    private void Start(){
        foreach(var input in inputs){
            input.OnInput += Distribute;
        }
        
    }

    private void OnDestroy(){
        foreach(var input in inputs){
            input.OnInput -= Distribute;
        }
    }

    private void Distribute(string input){
        Debug.Log(input);
        string text;
        if(input.Contains("天気")){
            responseGenerator = new WeatherResponseGenerater();
            weatherAPIJson = CommonData.weatherAPIJson;
            text = responseGenerator.Generate(input, weatherAPIJson);
        }else{
            responseGenerator = new DialogueResponseGenerator();
            text = responseGenerator.Generate(input);
        }
        Debug.Log(text);
        foreach(var output in outputs){
            output.Execute(text);
        }
    }
    
}
