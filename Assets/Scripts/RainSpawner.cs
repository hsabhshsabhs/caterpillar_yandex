using System.Collections;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    [Header("Объекты для спавна")]
    public GameObject raindropPrefab;
    public Sprite[] raindropSprites;

    [Header("Настройки капель")]
    public float minSize = 0.8f;
    public float maxSize = 1.2f;

    private float spawnInterval = 1.0f;
    private float halfWidth;

    void Start()
    {
        halfWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    IEnumerator SpawnRainCoroutine()
    {
        while (true)
        {
            float randomX = Random.Range(-halfWidth, halfWidth);
            Vector3 spawnPosition = transform.position + new Vector3(randomX, 0, 0);
            GameObject newRaindrop = Instantiate(raindropPrefab, spawnPosition, Quaternion.identity, transform);

            // --- НОВАЯ СТРОКА ---
            // Принудительно устанавливаем слой для каждой новой капли
            newRaindrop.GetComponent<SpriteRenderer>().sortingLayerName = "Rain";

            // --- Остальной код без изменений ---
            newRaindrop.GetComponent<SpriteRenderer>().sprite = raindropSprites[Random.Range(0, raindropSprites.Length)];
            float randomSize = Random.Range(minSize, maxSize);
            newRaindrop.transform.localScale = new Vector3(randomSize, randomSize, 1);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SetSpawnInterval(float newInterval)
    {
        if (Mathf.Abs(spawnInterval - newInterval) < 0.01f)
        {
            return;
        }

        spawnInterval = newInterval;

        StopAllCoroutines();
        StartCoroutine(SpawnRainCoroutine());
    }
}