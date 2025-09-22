using UnityEngine;

public class RainContainerController : MonoBehaviour
{
    // Сюда перетащим менеджер земли
    public GroundManager groundManager;

    // Ссылка на камеру, чтобы следовать за ней
    private Transform cameraTransform;

    void Start()
    {
        // Находим и кэшируем трансформ главной камеры для производительности
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (groundManager == null || cameraTransform == null) return;

        // --- ЛОГИКА СИНХРОНИЗАЦИИ ---

        // 1. Получаем текущую скорость земли
        float speed = groundManager.GetCurrentSpeed();

        // 2. Двигаем контейнер (и всех его детей) влево с этой скоростью
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        // 3. "ТЕЛЕПОРТ" ОБРАТНО: Принудительно выравниваем позицию контейнера по горизонтали с позицией камеры
        // Мы сохраняем Y и Z контейнера, но его X всегда будет равен X камеры.
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, transform.position.z);
    }
}