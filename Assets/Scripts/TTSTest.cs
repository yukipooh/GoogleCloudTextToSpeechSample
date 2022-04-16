using System.IO;
using Google.Cloud.TextToSpeech.V1;
using UnityEngine;

public class TTSTest : MonoBehaviour
{
    [SerializeField] private string credentialPath;
    [SerializeField] private string outputPath;
    
    private void Start()
    {
        var credential = Resources.Load<TextAsset>(credentialPath).text;

        TextToSpeechClientBuilder builder = new TextToSpeechClientBuilder()
        {
            JsonCredentials = credential
        };

        TextToSpeechClient client = builder.Build();
        
        // 読み上げる内容
        SynthesisInput input = new SynthesisInput
        {
            Text = "こんにちは。音声読み上げを行っています"
        };

        // ボイス設定
        VoiceSelectionParams voice = new VoiceSelectionParams
        {
            LanguageCode = "ja-JP",
            SsmlGender = SsmlVoiceGender.Female
        };

        // 音声ファイル設定
        AudioConfig config = new AudioConfig
        {
            AudioEncoding = AudioEncoding.Mp3
        };

        // 音声合成実行
        var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
        {
            Input = input,
            Voice = voice,
            AudioConfig = config
        });

        // MP3のファイルを生成
        using (Stream output = File.Create(outputPath))
        {
            response.AudioContent.WriteTo(output);
            Debug.Log($"succeeded : {outputPath}");
        }
    }
}
