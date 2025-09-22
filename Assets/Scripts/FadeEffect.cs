using System.Collections;
using UnityEngine;

// Этот скрипт требует наличия компонента SpriteRenderer на том же объекте.
[RequireComponent(typeof(SpriteRenderer))]
public class FadeEffect : MonoBehaviour
{
    [Header("Настройки времени")]
    [Tooltip("Время в секундах на плавное появление (0 = мгновенно)")]
    public float fadeInTime = 0.1f;

    [Tooltip("Сколько времени эффект будет полностью видимым")]
    public float duration = 0.2f;

    [Tooltip("Время в секундах на плавное исчезновение (0 = мгновенно)")]
    public float fadeOutTime = 0.3f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // Получаем компонент SpriteRenderer один раз для эффективности
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Запускаем главный жизненный цикл эффекта
        StartCoroutine(LifecycleCoroutine());
    }

    private IEnumerator LifecycleCoroutine()
    {
        // --- Фаза 1: Плавное появление (Fade In) ---
        if (fadeInTime > 0)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeInTime)
            {
                // Плавно меняем альфа-канал от 0 (прозрачный) до 1 (непрозрачный)
                float alpha = Mathf.Lerp(0f, 0.8f, elapsedTime / fadeInTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null; // Ждем следующего кадра
            }
        }
        // Гарантируем, что в конце эффект полностью видим
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        // --- Фаза 2: Пауза (полная видимость) ---
        yield return new WaitForSeconds(duration);

        // --- Фаза 3: Плавное исчезновение (Fade Out) ---
        if (fadeOutTime > 0)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeOutTime)
            {
                // Плавно меняем альфа-канал от 1 до 0
                float alpha = Mathf.Lerp(0.8f, 0f, elapsedTime / fadeOutTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // --- Фаза 4: Уничтожение ---
        // После того как эффект стал полностью прозрачным, уничтожаем его игровой объект
        Destroy(gameObject);
    }
}