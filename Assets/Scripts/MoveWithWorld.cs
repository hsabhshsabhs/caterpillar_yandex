using UnityEngine;

public class MoveWithWorld : MonoBehaviour
{
    // Скорость будет задана извне в момент создания эффекта
    public float moveSpeed = 0f;

    void Update()
    {
        // Каждый кадр двигаем этот объект влево с указанной скоростью
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
    }
}