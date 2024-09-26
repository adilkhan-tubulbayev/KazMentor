using UnityEngine;

public class HeaterController : MonoBehaviour {
    public Animator heaterAnimator;  // Ссылка на анимацию нагревателя
    public float heatingPower = 5f;  // Мощность нагрева
    public bool isHeating = false;   // Флаг активности нагревателя
    private MetalProperties currentMetal = null; // Текущий металл в зоне нагрева

    // Срабатывает, когда металл попадает в зону нагрева
    private void OnTriggerEnter2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && currentMetal == null) {
            // Если нет текущего металла, устанавливаем его
            currentMetal = metal;
        }
    }

    // Срабатывает, пока металл находится в зоне нагрева
    private void OnTriggerStay2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && metal == currentMetal && isHeating) {
            // Нагреваем только текущий металл
            currentMetal.UpdateTemperature(heatingPower * Time.deltaTime);
        }
    }

    // Срабатывает, когда металл выходит из зоны нагрева
    private void OnTriggerExit2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && metal == currentMetal) {
            // Если текущий металл покинул зону нагрева, сбрасываем его
            currentMetal = null;
        }
    }

    public void StartHeating() {
        isHeating = true;
        heaterAnimator.SetBool("IsHeating", true);  // Включаем анимацию
    }

    public void StopHeating() {
        isHeating = false;
        heaterAnimator.SetBool("IsHeating", false);  // Отключаем анимацию
    }
}
