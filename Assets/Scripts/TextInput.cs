using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextInput : InputBase
{
    public override Action<string> OnInput{get; set;}
    [SerializeField] private InputField inputField;

    private void Start() {
        inputField.onEndEdit.AddListener(input => OnInput?.Invoke(input));
    }
}
