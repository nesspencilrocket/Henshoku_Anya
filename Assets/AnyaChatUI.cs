using UnityEngine;
using LLMUnity;
using TMPro;
using UnityEngine.UI;

public class AnyaChatUI : MonoBehaviour
{
    [Header("Settings")]
    public LLMAgent anyaCharacter; // LLMCharacterからLLMAgentに変更
    public TMP_InputField inputField;
    public TMP_Text outputText;
    public Button sendButton;

    void Start()
    {
        // ボタンのクリックイベントを登録
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(OnSendClicked);
        }
    }

    async void OnSendClicked()
    {
        // 1. 参照チェック（NullReferenceException対策）
        if (anyaCharacter == null)
        {
            Debug.LogError("AnyaCharacter (LLMAgent) がInspectorで設定されていません！");
            return;
        }

        string message = inputField.text;
        if (string.IsNullOrEmpty(message)) return;

        // 2. UIの初期化
        outputText.text = "アーニャ、かんがえちゅう...";
        inputField.text = "";
        sendButton.interactable = false; // 連打防止

        try
        {
            // 3. アーニャに話しかける (LLMAgentのChatメソッド)
            // 第2引数はストリーミング返答（一文字ずつ表示）を受け取るコールバックです
            await anyaCharacter.Chat(message, (reply) => {
                outputText.text = reply;
            });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"チャット中にエラーが発生しました: {e.Message}");
            outputText.text = "エラーがおきたみたい...";
        }
        finally
        {
            // 4. 送信ボタンを再度有効化
            sendButton.interactable = true;
        }
    }
}
