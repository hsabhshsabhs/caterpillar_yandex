using UnityEngine;

public class RaindropController : MonoBehaviour
{
    public GameObject splashPrefab;
    public float lifetime = 5.0f; // Время жизни в секундах

    void Start()
    {
        // Уничтожить эту каплю через 'lifetime' секунд
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Instantiate(splashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player")) // Добавим логику столкновения с гусеницей
        {
            // Здесь будет логика конца игры
            Debug.Log("Game Over!");
            Destroy(gameObject); // Уничтожаем каплю
            // Time.timeScale = 0; // Например, останавливаем игру
        }
    }
}