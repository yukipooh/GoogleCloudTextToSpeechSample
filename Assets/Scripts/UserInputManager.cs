using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInputManager : MonoBehaviour
{
    [SerializeField] private Button enterButton;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text responseText;
    [SerializeField] TTSTest ttsTest;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: inputTextを渡してレスポンスを音声合成する関数をセット
        enterButton.onClick.AddListener(OnClickEnter);
    }

    void OnClickEnter(){
        TTSTest.inputText = inputField.text;
        StartCoroutine(ttsTest.PlayAudio(true));
    }

    public void SetResponseText(string response){
        responseText.text = "Response : " + response;
    }
}
