using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image fillImage;
    [SerializeField] private GameObject container; // Opcional: para ocultar la barra cuando esta llena

    private void OnEnable()
    {
        health.OnHealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateBar;
    }

    private void UpdateBar(int current, int max)
    {
        fillImage.fillAmount = max > 0 ? (float)current / max : 0f;

        if (container != null)
            container.SetActive(current < max);
    }
}