using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;

public class TestScript
{
    private SentenceAnalyzer sentenceAnalyzer;
    
    [SetUp] 
    public void SetUp(){
        sentenceAnalyzer = new SentenceAnalyzer();
    }

    [TearDown]
    public void TearDown(){
        sentenceAnalyzer.Dispose();
    }

    [Test]
    public void Test1(){
        var response = sentenceAnalyzer.Analyze("今日の天気は");
        var correct = new []{"今日","の","天気","は"};
        Assert.That(response.Select(x => x.Surface).ToArray(), Is.EqualTo(correct));
    }

    [Test]
    public void Test2(){
        try{
            var response = sentenceAnalyzer.Analyze(null);
        }catch(ArgumentException){
            Assert.Pass();
        }catch(Exception){
            Assert.Fail();
        }
    }
}
