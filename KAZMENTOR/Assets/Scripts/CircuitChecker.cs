using UnityEngine;
using TMPro;

public class CircuitChecker : MonoBehaviour {
    public DropZone[] dropZones; // Ссылки на все зоны сброса
    public string[] correctTags; // Массив правильных тегов для зон сброса
    public TextMeshProUGUI resultText; // Текст для отображения результата
    public GameObject circuitResult; // Объект для отображения результата
    private Vector3[] initialPositions; // Исходные позиции перетаскиваемых объектов
    private DraggableItem[] draggableItems; // Перетаскиваемые объекты
    private bool isCircuitCorrect = false; // Флаг для отслеживания правильности схемы

    private void Awake() {
        circuitResult.SetActive(false); // Скрыть объект CircuitResult при старте игры

        // Сохраним исходные позиции всех перетаскиваемых объектов
        draggableItems = FindObjectsOfType<DraggableItem>();
        initialPositions = new Vector3[draggableItems.Length];

        for (int i = 0; i < draggableItems.Length; i++) {
            initialPositions[i] = draggableItems[i].transform.position;
        }
    }

    public void CheckCircuit() {
        bool isCorrect = true;

        // Проверка каждой зоны сброса
        for (int i = 0; i < dropZones.Length; i++) {
            bool zoneIsCorrect = dropZones[i].HasCorrectItem(correctTags[i]);
            Debug.Log($"Проверка зоны {dropZones[i].name}: {zoneIsCorrect} (ожидалось: {correctTags[i]})");
            if (!zoneIsCorrect) {
                isCorrect = false;
                break;
            }
        }

        // Отображение результата
        if (isCorrect) {
            isCircuitCorrect = true; // Устанавливаем флаг правильности схемы
            resultText.text = "Схема собрана верно!";
            Debug.Log("Схема собрана верно!");
        } else {
            isCircuitCorrect = false; // Сбрасываем флаг правильности схемы
            resultText.text = "Схема собрана неверно.";
            Debug.Log("Схема собрана неверно.");
        }

        circuitResult.SetActive(true); // Показать результат
    }

    public void ExitResult() {
        circuitResult.SetActive(false); // Отключаем окно результата

        // Возвращаем перетаскиваемые объекты на их исходные позиции только если схема неверна
        if (!isCircuitCorrect) {
            ResetDraggableItems();
        }
    }

    private void ResetDraggableItems() {
        for (int i = 0; i < draggableItems.Length; i++) {
            draggableItems[i].transform.position = initialPositions[i];
            draggableItems[i].transform.SetParent(draggableItems[i].originalParent); // Возвращаем исходного родителя
            draggableItems[i].ClearDroppedZone(); // Сбросить текущую зону сброса
        }
    }
}
