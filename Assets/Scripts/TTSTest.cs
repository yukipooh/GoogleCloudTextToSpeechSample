using System.IO;
using Google.Cloud.TextToSpeech.V1;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

public class TTSTest : MonoBehaviour
{
    /// <summary>
    /// APIキー？の保管場所
    /// </summary>
    [SerializeField] private string credentialPath;
    
    /// <summary>
    /// 合成した音声の保存先（フォルダ）
    /// </summary>
    [SerializeField] private string outputPath;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] UserInputManager userInputManager;

    /// <summary>
    /// Userが入力した文章
    /// </summary>
    public static string inputText;
    private AudioClip audioClip;
    
    /// <summary>
    /// 合成した音声のファイル名
    /// </summary>
    private string outputAudioName = "outputAudio";
    private TextToSpeechClient client;

    private async void Start()
    {
        var credential = Resources.Load<TextAsset>(credentialPath).text;
        
        TextToSpeechClientBuilder builder = new TextToSpeechClientBuilder()
        {
            JsonCredentials = credential
        };

        client = builder.Build();

    }
    
    /// <summary>
    /// 喋る内容と性別に応じて音声合成を行い結果を返す
    /// </summary>
    /// <param name="sentence">喋る内容</param>
    /// <param name="gender">性別</param>
    /// <returns></returns>
    private SynthesizeSpeechResponse GetSynthesizeSpeechResponse(string sentence, SsmlVoiceGender gender = SsmlVoiceGender.Female){
        // 読み上げる内容
        SynthesisInput input = new SynthesisInput
        {
            Text = sentence
        };

        // ボイス設定
        VoiceSelectionParams voice = new VoiceSelectionParams
        {
            LanguageCode = "ja-JP",
            SsmlGender = gender
        };

        // 音声ファイル設定
        AudioConfig config = new AudioConfig
        {
            AudioEncoding = AudioEncoding.Mp3
        };

        // 音声合成実行
        SynthesizeSpeechResponse res = client.SynthesizeSpeech(new SynthesizeSpeechRequest
        {
            Input = input,
            Voice = voice,
            AudioConfig = config
        });

        return res;
    }

    /// <summary>
    /// 音声合成ファイルを作成し音声クリップをセット
    /// </summary>
    /// <param name="audioName">作成するAudioファイルのファイル名</param>
    /// <param name="response">音声合成のレスポンス</param>
    /// <returns></returns>
    private IEnumerator CreateAudio(string audioName, SynthesizeSpeechResponse response){
        string filePath = outputPath + "/" + audioName + ".mp3";
        // MP3のファイルを生成
        using (Stream output = File.Create(filePath))
        {
            response.AudioContent.WriteTo(output);
            Debug.Log($"succeeded : {outputPath}");
        }
        AudioClip tmpClip = null;
        while(tmpClip == null){
            AssetDatabase.ImportAsset(filePath);    //fileをUnityにインポート
            tmpClip = Resources.Load<AudioClip>(audioName); //ロード
            yield return new WaitForSeconds(0.5f);
        }
        audioClip = tmpClip;
    }

    /// <summary>
    /// 合成した音声を再生
    /// </summary>
    /// <param name="response">音声合成のレスポンス</param>
    /// <param name="isDeleteFile">合成した音声ファイルを再生後削除するかどうか</param>
    /// <returns></returns>
    public IEnumerator PlayAudio(bool isDeleteFile = true){
        SynthesizeSpeechResponse response = GetSynthesizeSpeechResponse(GetSpeechSentence(), SsmlVoiceGender.Female);
        yield return CreateAudio(outputAudioName, response);
        audioSource.clip = audioClip;
        audioSource.Play();
        if(isDeleteFile){
            File.Delete(outputPath + "/" + outputAudioName + ".mp3");
        }
    }

    /// <summary>
    /// 喋る内容を決定し返す
    /// </summary>
    /// <returns>喋る内容</returns>
    public string GetSpeechSentence(){
        SentenceAnalyzer sentenceAnalyzer = new SentenceAnalyzer();
        string answerSentence = "";
        bool isDecidedAnswer = false;   //答えを決定できたかどうか
        var nodes = sentenceAnalyzer.Analyze(inputText);
        List<string> result = nodes.Select(x => x.Surface).ToArray().ToList();
        foreach(string item in result){
            Debug.Log(item);
        }
        foreach(string greeting in ConstData.greetings){
            if(result.Contains(greeting)){
                answerSentence = greeting + "。わざわざありがとうございます！";
                isDecidedAnswer = true;
            }
        }
        if(result.Contains("天気")){
            if(result.Contains("今日")){
                //ToDo　天気予報APIを用いて天気予報を伝える。
            }else if(result.Contains("明日")){

            }else if(result.Contains("明後日")){

            }else{

            }
            // isDecidedAnswer = true;
        }

        if(!isDecidedAnswer) answerSentence = "すいません。よくわかりません。";
        userInputManager.SetResponseText(answerSentence);
        return answerSentence;
    }
}
