using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;

public class ChatGPTManager : MonoBehaviour {
    public OnResponseEvent OnResponse;

    [System.Serializable]
    public class OnResponseEvent : UnityEvent<string> { }

    private OpenAIApi openAI = new OpenAIApi("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");
    private List<ChatMessage> messages = new List<ChatMessage>();

    // Создаем начальное сообщение для настройки контекста персонажа
    private void Start() {
        // Устанавливаем начальный промпт для AI
        ChatMessage initialMessage = new ChatMessage();
        initialMessage.Content = "You are Albert Einstein. Respond with knowledge about physics, relativity, and other scientific matters. Behave like a wise and knowledgeable scientist. Students of grades 5-7 will communicate with you. Give them moral lessons";
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
        request.Model = "gpt-3.5-turbo";

        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0) {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);

            Debug.Log(chatResponse.Content);

            OnResponse.Invoke(chatResponse.Content);
        }
    }
}
