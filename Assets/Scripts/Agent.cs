using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Agent : MonoBehaviour
{
    [SerializeField] private List<InputBase> inputs;
    [SerializeField] private List<OutputBase> outputs;
    private ResponseGenerator responseGenerator;

    private void Awake() {
        responseGenerator = new DialogueResponseGenerator();
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
        var text = responseGenerator.Generate(input);
        Debug.Log(text);
        foreach(var output in outputs){
            output.Execute(text);
        }
    }
    
}
