using UnityEngine;
using UnityEngine.UI;

public class GameStageManager : MonoBehaviour
{
    [Header("������ �� ������� (���������� �� ��������)")]
    public RainManager rainManager;
    public SpriteRenderer backgroundSpriteRenderer;

    [Header("��������� ���������")]
    [Tooltip("�� ������� ������ ���� ��������� ������������ ���������")]
    public float timeToMaxDifficulty = 60.0f;

    [Header("��������� ���� (�� ������ �� �����)")]
    public Color startBackgroundColor = Color.white;
    public Color endBackgroundColor = Color.black;

    [Header("��������� ����� (�� ������ �� �����)")]
    public float startSpawnInterval = 1.2f;
    public float endSpawnInterval = 0.3f;

    private float gameTimer = 0f;

    void Start()
    {
        if (rainManager == null || backgroundSpriteRenderer == null)
        {
            Debug.LogError("�� ��� ������ ��������� � GameStageManager!");
            this.enabled = false;
            return;
        }
        backgroundSpriteRenderer.color = startBackgroundColor;
        // ��������� ��������� ��������� ������ �� ����� �����, Update() ���������
    }

    void Update()
    {
        // === ������� ���������: �� ������ 'if (gameTimer < timeToMaxDifficulty)' ===
        // ������ ���� ��� �������� ������.

        // ����������� ������, �� �� ���� ��� ����� ����������
        if (gameTimer < timeToMaxDifficulty)
        {
            gameTimer += Time.deltaTime;
        }

        // ��������� ������� �������� (�� 0 �� 1). ����� 60 ��� �� ������ ����� 1.
        float progress = Mathf.Clamp01(gameTimer / timeToMaxDifficulty);

        // ������ �������� ���� ����
        backgroundSpriteRenderer.color = Color.Lerp(startBackgroundColor, endBackgroundColor, progress);

        // ������ �������� �������� ��� ���� �������� ���������
        float currentInterval = Mathf.Lerp(startSpawnInterval, endSpawnInterval, progress);
        UpdateSpawnIntervals(currentInterval);
    }

    // ��������������� ����� ��� ���������� ���� ���������
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