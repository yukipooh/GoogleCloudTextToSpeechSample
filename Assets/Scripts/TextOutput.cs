using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextOutput : OutputBase
{
    [SerializeField] private Text text;

    public override void Execute(string text){
        this.text.text = text;
    }

}
