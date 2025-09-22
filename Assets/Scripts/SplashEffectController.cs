using System.Collections;
using UnityEngine;

public class SplashEffectController : MonoBehaviour
{
    // Это поле появится в инспекторе префаба
    public float fadeOutTime = 1.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Запускаем корутину, которая будет управлять исчезновением
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float startAlpha = spriteRenderer.color.a; // Начальная прозрачность (обычно 1)
        float elapsedTime = 0f;

        // Цикл будет работать, пока не пройдет время fadeOutTime
        while (elapsedTime < fadeOutTime)
        {
            // Вычисляем новую прозрачность с помощью Lerp для плавности
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutTime);

            // Применяем новый цвет с измененной прозрачностью
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующего кадра
        }

        // Гарантированно уничтожаем объект после завершения цикла
        Destroy(gameObject);
    }
}