using UnityEngine;

public class MoveWithWorld : MonoBehaviour
{
    // �������� ����� ������ ����� � ������ �������� �������
    public float moveSpeed = 0f;

    void Update()
    {
        // ������ ���� ������� ���� ������ ����� � ��������� ���������
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);
    }
}