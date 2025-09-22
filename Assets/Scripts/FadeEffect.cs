using System.Collections;
using UnityEngine;

// ���� ������ ������� ������� ���������� SpriteRenderer �� ��� �� �������.
[RequireComponent(typeof(SpriteRenderer))]
public class FadeEffect : MonoBehaviour
{
    [Header("��������� �������")]
    [Tooltip("����� � �������� �� ������� ��������� (0 = ���������)")]
    public float fadeInTime = 0.1f;

    [Tooltip("������� ������� ������ ����� ��������� �������")]
    public float duration = 0.2f;

    [Tooltip("����� � �������� �� ������� ������������ (0 = ���������)")]
    public float fadeOutTime = 0.3f;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        // �������� ��������� SpriteRenderer ���� ��� ��� �������������
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // ��������� ������� ��������� ���� �������
        StartCoroutine(LifecycleCoroutine());
    }

    private IEnumerator LifecycleCoroutine()
    {
        // --- ���� 1: ������� ��������� (Fade In) ---
        if (fadeInTime > 0)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeInTime)
            {
                // ������ ������ �����-����� �� 0 (����������) �� 1 (������������)
                float alpha = Mathf.Lerp(0f, 0.8f, elapsedTime / fadeInTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null; // ���� ���������� �����
            }
        }
        // �����������, ��� � ����� ������ ��������� �����
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

        // --- ���� 2: ����� (������ ���������) ---
        yield return new WaitForSeconds(duration);

        // --- ���� 3: ������� ������������ (Fade Out) ---
        if (fadeOutTime > 0)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fadeOutTime)
            {
                // ������ ������ �����-����� �� 1 �� 0
                float alpha = Mathf.Lerp(0.8f, 0f, elapsedTime / fadeOutTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // --- ���� 4: ����������� ---
        // ����� ���� ��� ������ ���� ��������� ����������, ���������� ��� ������� ������
        Destroy(gameObject);
    }
}