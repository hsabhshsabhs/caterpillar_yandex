using System.Collections;
using UnityEngine;

public class SplashEffectController : MonoBehaviour
{
    // ��� ���� �������� � ���������� �������
    public float fadeOutTime = 1.0f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // ��������� ��������, ������� ����� ��������� �������������
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float startAlpha = spriteRenderer.color.a; // ��������� ������������ (������ 1)
        float elapsedTime = 0f;

        // ���� ����� ��������, ���� �� ������� ����� fadeOutTime
        while (elapsedTime < fadeOutTime)
        {
            // ��������� ����� ������������ � ������� Lerp ��� ���������
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutTime);

            // ��������� ����� ���� � ���������� �������������
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null; // ���� ���������� �����
        }

        // �������������� ���������� ������ ����� ���������� �����
        Destroy(gameObject);
    }
}