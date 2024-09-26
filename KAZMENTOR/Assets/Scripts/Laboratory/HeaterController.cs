using UnityEngine;

public class HeaterController : MonoBehaviour {
    public float heatingRange = 1.5f;  // Радиус действия нагревателя
    public float heatingPower = 100f;  // Мощность нагревателя
    public bool isHeating = false;     // Флаг, активен ли нагреватель
    public Animator heaterAnimator;    // Ссылка на аниматор нагревателя

    private void Update() {
        if (isHeating) {
            // Проверяем все объекты с MetalProperties в радиусе действия нагревателя
            MetalProperties[] metals = FindObjectsOfType<MetalProperties>();
            foreach (MetalProperties metal in metals) {
                if (Vector3.Distance(transform.position, metal.transform.position) <= heatingRange) {
                    metal.UpdateTemperature(heatingPower * Time.deltaTime);  // Нагреваем металл
                }
            }
        }
    }

    public void StartHeating() {
        isHeating = true;
        heaterAnimator.SetBool("IsHeating", true);  // Включаем анимацию нагрева
    }

    public void StopHeating() {
        isHeating = false;
        heaterAnimator.SetBool("IsHeating", false);  // Выключаем анимацию нагрева
    }
}
