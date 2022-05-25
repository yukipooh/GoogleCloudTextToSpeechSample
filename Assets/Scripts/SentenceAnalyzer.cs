using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NMeCab.Specialized;
using System;

public class SentenceAnalyzer : IDisposable
{
    private string sentence = "今日もいい天気ですね。おはようございます";

    // 「dic/ipadicフォルダ」のパスを指定する
    private const string dicDir = @"Assets/dic/ipadic";
    private MeCabIpaDicTagger meCabIpaDicTagger;

    public SentenceAnalyzer(){
        meCabIpaDicTagger = MeCabIpaDicTagger.Create(dicDir);
    }

    public MeCabIpaDicNode[] Analyze(string sentence)
    {
        if(string.IsNullOrEmpty(sentence)) throw new ArgumentException();
        var nodes = meCabIpaDicTagger.Parse(sentence);
        return nodes;
    }

    public void Dispose(){
        meCabIpaDicTagger?.Dispose();
    }

    ~SentenceAnalyzer(){
        Dispose();
    }
}
