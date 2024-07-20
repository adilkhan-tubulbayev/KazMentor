using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenAI;
using UnityEngine.Events;

public class ChatGPTManager : MonoBehaviour {
    public TextMeshProUGUI textComponent;
    public OnResponseEvent OnResponse;
    public float textSpeed = 0.05f; // Скорость отображения текста

    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    private OpenAIApi openAI = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    private void Start() {
        ChatMessage initialMessage = new ChatMessage();
        initialMessage.Content = "Ты - Альберт Энштейн. Говори, что ты настоящий, если спросят, настоящий ли ты. Являешься ученым и учителем для игроков-учеников одновременно. Отвечай на их вопросы кратко, но при этом мудро и загадочно. Ответы должны быть понятными и логичными для твоей целевой аудитории: 5-7 класс";
        initialMessage.Role = "system";

        messages.Add(initialMessage);
    }

    public async void AskChatGPT(string newText) {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";

        messages.Add(newMessage);

        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-4o-mini";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0) {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);

            StartCoroutine(TypeText(chatResponse.Content)); // Запуск корутины для постепенного отображения текста
        }
    }

    private IEnumerator TypeText(string text) {
        textComponent.text = string.Empty; // Очистка текста перед началом новой линии
        foreach (char c in text.ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed); // Пауза между символами
        }

        OnResponse.Invoke(text);
    }
}
