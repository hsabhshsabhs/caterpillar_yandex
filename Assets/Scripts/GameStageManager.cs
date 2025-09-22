using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    [Header("Ссылки на объекты (Перетащить из Иерархии)")]
    public RainManager rainManager;
    public SpriteRenderer backgroundSpriteRenderer;

    [Header("Настройки сложности")]
    [Tooltip("За сколько секунд игра достигнет максимальной сложности")]
    public float timeToMaxDifficulty = 60.0f;

    [Header("Параметры фона (от начала до конца)")]
    public Color startBackgroundColor = Color.white;
    public Color endBackgroundColor = Color.black;

    [Header("Параметры дождя (от начала до конца)")]
    public float startSpawnInterval = 1.2f;
    public float endSpawnInterval = 0.3f;

    private float gameTimer = 0f;

    void Start()
    {
        if (rainManager == null || backgroundSpriteRenderer == null)
        {
            Debug.LogError("Не все ссылки назначены в GameStageManager!");
            this.enabled = false;
            return;
        }
        backgroundSpriteRenderer.color = startBackgroundColor;
        // Начальная установка интервала больше не нужна здесь, Update() справится
    }

    void Update()
    {
        // === ГЛАВНОЕ ИЗМЕНЕНИЕ: Мы убрали 'if (gameTimer < timeToMaxDifficulty)' ===
        // Теперь этот код работает всегда.

        // Увеличиваем таймер, но не даем ему расти бесконечно
        if (gameTimer < timeToMaxDifficulty)
        {
            gameTimer += Time.deltaTime;
        }

        // Вычисляем текущий прогресс (от 0 до 1). После 60 сек он всегда будет 1.
        float progress = Mathf.Clamp01(gameTimer / timeToMaxDifficulty);

        // Плавно изменяем цвет фона
        backgroundSpriteRenderer.color = Color.Lerp(startBackgroundColor, endBackgroundColor, progress);

        // Плавно изменяем интервал для ВСЕХ активных спавнеров
        float currentInterval = Mathf.Lerp(startSpawnInterval, endSpawnInterval, progress);
        UpdateSpawnIntervals(currentInterval);
    }

    // Вспомогательный метод для обновления всех спавнеров
    void UpdateSpawnIntervals(float interval)
    {
        foreach (var spawner in rainManager.activeSpawners)
        {
            if (spawner != null)
            {
                spawner.SetSpawnInterval(interval);
            }
        }
    }
}