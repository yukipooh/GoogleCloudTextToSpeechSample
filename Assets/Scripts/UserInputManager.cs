using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInputManager : MonoBehaviour
{
    [SerializeField] private Button enterButton;
    [SerializeField] private InputField inputField;
    [SerializeField] private Text responseText;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: inputTextを渡してレスポンスを音声合成する関数をセット
        enterButton.onClick.AddListener(() => {TTSTest.inputText = inputField.text;});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
