using System.Collections;
using UnityEngine;

public class CaterpillarController : MonoBehaviour
{
    private enum ControlMode { Auto, Manual }
    private ControlMode currentMode;

    [Header("Ссылки на менеджеры (Обязательно!)")]
    public GroundManager groundManager;
    public RainManager rainManager;

    [Header("Ссылки на спрайты (Обязательно!)")]
    public Sprite bentSprite;
    public Sprite straightSprite;

    [Header("Эффекты (Не обязательно)")]
    public GameObject airPushEffectPrefab;
    public GameObject groundDustEffectPrefab;
    public Transform effectSpawnPoint;

    [Header("Настройки ручного толчка")]
    public float manualPushStrength = 10f;
    public float pushDuration = 0.1f;

    [Header("Настройки авто-режима")]
    public float timeUntilAuto = 1.0f;
    public float autoPushStrength = 3f;
    public float autoTimeStraight = 0.7f;
    public float autoTimeBent = 1.0f;

    private SpriteRenderer spriteRenderer;
    private bool isBent = true;
    private float idleTimer = 0f;
    private Coroutine activeCoroutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (groundManager == null || rainManager == null || bentSprite == null || straightSprite == null)
        {
            Debug.LogError("Не все обязательные поля назначены в инспекторе CaterpillarController!");
            this.enabled = false; return;
        }
        if (effectSpawnPoint == null)
        {
            Debug.LogWarning("Не назначен 'EffectSpawnPoint', эффекты будут появляться в центре гусеницы.");
            effectSpawnPoint = transform;
        }
        SwitchToAutoMode();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            idleTimer = 0f;
            if (currentMode == ControlMode.Auto && isBent)
            {
                SwitchToManualMode();
                PerformManualClick();
            }
            else if (currentMode == ControlMode.Manual)
            {
                PerformManualClick();
            }
        }
        else if (currentMode == ControlMode.Manual)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= timeUntilAuto)
            {
                SwitchToAutoMode();
            }
        }
    }

    private void SwitchToAutoMode() { StopCurrentProcess(); currentMode = ControlMode.Auto; activeCoroutine = StartCoroutine(AutoMovementLoop()); }
    private void SwitchToManualMode() { StopCurrentProcess(); currentMode = ControlMode.Manual; }

    private IEnumerator AutoMovementLoop()
    {
        if (!isBent) { spriteRenderer.sprite = bentSprite; isBent = true; yield return new WaitForSeconds(0.3f); }
        while (true)
        {
            spriteRenderer.sprite = straightSprite; isBent = false;
            SpawnEffects();
            groundManager.SetSpeed(autoPushStrength);
            rainManager.SetSpeed(autoPushStrength);
            yield return new WaitForSeconds(autoTimeStraight);
            spriteRenderer.sprite = bentSprite; isBent = true;
            groundManager.SetSpeed(0);
            rainManager.SetSpeed(0);
            yield return new WaitForSeconds(autoTimeBent);
        }
    }

    private void PerformManualClick()
    {
        if (activeCoroutine != null) return;
        if (isBent) { GameManager.Instance.AddScore(1); }
        isBent = !isBent;
        if (!isBent) { spriteRenderer.sprite = straightSprite; activeCoroutine = StartCoroutine(ManualPushImpulse()); }
        else { spriteRenderer.sprite = bentSprite; }
    }

    private IEnumerator ManualPushImpulse()
    {
        SpawnEffects();
        groundManager.SetSpeed(manualPushStrength);
        rainManager.SetSpeed(manualPushStrength);
        yield return new WaitForSeconds(pushDuration);
        groundManager.SetSpeed(0);
        rainManager.SetSpeed(0);
        activeCoroutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other) { if (other.CompareTag("Raindrop")) { this.enabled = false; GameManager.Instance.GameOver(); } }

    private void SpawnEffects()
    {
        if (airPushEffectPrefab != null)
        {
            Instantiate(airPushEffectPrefab, effectSpawnPoint.position, airPushEffectPrefab.transform.rotation);
        }

        if (groundDustEffectPrefab != null && groundManager != null)
        {
            Transform groundParent = FindGroundTileBeneath();
            if (groundParent != null)
            {
                Instantiate(groundDustEffectPrefab, effectSpawnPoint.position, groundDustEffectPrefab.transform.rotation, groundParent);
            }
            else
            {
                Instantiate(groundDustEffectPrefab, effectSpawnPoint.position, groundDustEffectPrefab.transform.rotation);
            }
        }
    }

    private Transform FindGroundTileBeneath()
    {
        foreach (var tile in groundManager.activeTiles)
        {
            float tileWidth = tile.GetComponent<SpriteRenderer>().bounds.size.x;
            if (transform.position.x >= tile.transform.position.x - tileWidth / 2 &&
                transform.position.x <= tile.transform.position.x + tileWidth / 2)
            {
                return tile.transform;
            }
        }
        return null;
    }

    private void StopCurrentProcess()
    {
        if (activeCoroutine != null) { StopCoroutine(activeCoroutine); activeCoroutine = null; }
        groundManager.SetSpeed(0);
        rainManager.SetSpeed(0);
    }
}