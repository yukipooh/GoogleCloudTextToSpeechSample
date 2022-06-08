using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class ResponseGenerator
{
    public abstract bool IsAvailable(string input);
    public abstract string Generate(string input);
    public abstract string Generate(string input, WeatherAPIJson weatherAPIJson);
}
