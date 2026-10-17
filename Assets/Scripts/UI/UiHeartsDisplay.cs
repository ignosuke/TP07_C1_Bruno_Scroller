using UnityEngine;
using UnityEngine.UI;

public class UiHeartsDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform container;   // con Horizontal Layout Group
    [SerializeField] private Image heartPrefab;

    [Header("Sprites")]
    [SerializeField] private Sprite largeHeart;     // El proximo en recibir el golpe
    [SerializeField] private Sprite smallHeart;     // Los que estan a salvo
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite shieldHeart;    // Reemplaza al grande mientras haya escudo

    private Image[] slots;

    private void Awake()
    {
        CreateSlots();
    }

    private void OnEnable()
    {
        playerHealth.OnHealthChanged += HandleHealthChanged;
        playerHealth.OnShieldChanged += Redraw;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthChanged -= HandleHealthChanged;
        playerHealth.OnShieldChanged -= Redraw;
    }

    private void CreateSlots()
    {
        slots = new Image[playerHealth.MaxHealth];

        for (int i = 0; i < slots.Length; i++)
            slots[i] = Instantiate(heartPrefab, container);
    }

    private void HandleHealthChanged(int current, int max) => Redraw();

    private void Redraw()
    {
        int current = playerHealth.CurrentHealth;
        int nextHitIndex = current - 1; // El ultimo lleno es el que se va a comer el proximo golpe
        bool hasShield = playerHealth.HasShield;

        for (int i = 0; i < slots.Length; i++)
            slots[i].sprite = ResolveSprite(i, current, nextHitIndex, hasShield);
    }

    private Sprite ResolveSprite(int index, int current, int nextHitIndex, bool hasShield)
    {
        if (index == nextHitIndex) return hasShield ? shieldHeart : largeHeart;
        if (index < current) return smallHeart;

        return emptyHeart;
    }
}