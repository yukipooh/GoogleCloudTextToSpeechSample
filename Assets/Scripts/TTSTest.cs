using System.IO;
using Google.Cloud.TextToSpeech.V1;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;

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
    public static string inputText;
    private AudioClip audioClip;
    
    /// <summary>
    /// 合成した音声のファイル名
    /// </summary>
    private string outputAudioName = "outputAudio";

    private async void Start()
    {
        var credential = Resources.Load<TextAsset>(credentialPath).text;
        Debug.Log(credential);

        TextToSpeechClientBuilder builder = new TextToSpeechClientBuilder()
        {
            JsonCredentials = credential
        };

        TextToSpeechClient client = builder.Build();
        
        // 読み上げる内容
        SynthesisInput input = new SynthesisInput
        {
            Text = "おはようございます"
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
        SynthesizeSpeechResponse response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
        {
            Input = input,
            Voice = voice,
            AudioConfig = config
        });
        
        StartCoroutine(PlayAudio(response));
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
    private IEnumerator PlayAudio(SynthesizeSpeechResponse response, bool isDeleteFile = true){
        yield return CreateAudio(outputAudioName, response);
        audioSource.clip = audioClip;
        audioSource.Play();
        if(isDeleteFile){
            File.Delete(outputPath + "/" + outputAudioName + ".mp3");
        }
    }
}
