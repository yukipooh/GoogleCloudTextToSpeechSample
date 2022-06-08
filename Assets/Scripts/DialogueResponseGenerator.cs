using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DialogueResponseGenerator : ResponseGenerator
{
    private const string ErrorMessage = "よくわかりませんでした";

    public override bool IsAvailable(string input){
        return true;
    }

    public override string Generate(string input){
        (string,string)? target = ConstData.Dialogues.Select(x => ((string,string)?)x).FirstOrDefault(x => x.Value.Item1 == input);
        return target != null ? target.Value.Item2:ErrorMessage;
    }
}
