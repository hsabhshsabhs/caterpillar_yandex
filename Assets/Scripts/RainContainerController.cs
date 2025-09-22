using UnityEngine;

public class RainContainerController : MonoBehaviour
{
    // ���� ��������� �������� �����
    public GroundManager groundManager;

    // ������ �� ������, ����� ��������� �� ���
    private Transform cameraTransform;

    void Start()
    {
        // ������� � �������� ��������� ������� ������ ��� ������������������
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (groundManager == null || cameraTransform == null) return;

        // --- ������ ������������� ---

        // 1. �������� ������� �������� �����
        float speed = groundManager.GetCurrentSpeed();

        // 2. ������� ��������� (� ���� ��� �����) ����� � ���� ���������
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);

        // 3. "��������" �������: ������������� ����������� ������� ���������� �� ����������� � �������� ������
        // �� ��������� Y � Z ����������, �� ��� X ������ ����� ����� X ������.
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, transform.position.z);
    }
}