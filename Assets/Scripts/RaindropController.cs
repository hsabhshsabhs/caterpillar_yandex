using UnityEngine;

public class RaindropController : MonoBehaviour
{
    public GameObject splashPrefab;
    public float lifetime = 5.0f; // ����� ����� � ��������

    void Start()
    {
        // ���������� ��� ����� ����� 'lifetime' ������
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Instantiate(splashPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player")) // ������� ������ ������������ � ���������
        {
            // ����� ����� ������ ����� ����
            Debug.Log("Game Over!");
            Destroy(gameObject); // ���������� �����
            // Time.timeScale = 0; // ��������, ������������� ����
        }
    }
}