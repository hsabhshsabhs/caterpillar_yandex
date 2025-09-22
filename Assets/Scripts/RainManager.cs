using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
    [Header("Префабы и Настройки")]
    public GameObject rainContainerPrefab;
    public int initialContainers = 3;

    [HideInInspector]
    public List<RainSpawner> activeSpawners = new List<RainSpawner>();

    private float containerWidth;
    private List<GameObject> activeContainers = new List<GameObject>();
    private Camera mainCamera;
    private float currentSpeed = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        if (rainContainerPrefab == null)
        {
            Debug.LogError("Префаб в RainManager не назначен!");
            this.enabled = false; return;
        }

        containerWidth = rainContainerPrefab.GetComponent<BoxCollider2D>().size.x;
        Vector3 nextSpawnPoint = transform.position;

        for (int i = 0; i < initialContainers; i++)
        {
            SpawnContainer(ref nextSpawnPoint);
        }
    }

    void Update()
    {
        // Двигаем контейнеры с текущей скоростью
        foreach (var container in activeContainers)
        {
            container.transform.Translate(Vector2.left * currentSpeed * Time.deltaTime, Space.World);
        }

        // Логика создания и удаления контейнеров
        if (activeContainers.Count == 0) return;

        GameObject lastContainer = activeContainers[activeContainers.Count - 1];
        float rightEdgeOfScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (lastContainer.transform.position.x < rightEdgeOfScreen + containerWidth)
        {
            Vector3 spawnPos = lastContainer.transform.position;
            spawnPos.x += containerWidth;
            GameObject newContainer = Instantiate(rainContainerPrefab, spawnPos, Quaternion.identity, transform);
            activeContainers.Add(newContainer);
            activeSpawners.Add(newContainer.GetComponent<RainSpawner>());
        }

        GameObject firstContainer = activeContainers[0];
        if (firstContainer.transform.position.x < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - containerWidth * 1.5f)
        {
            activeContainers.RemoveAt(0);
            activeSpawners.RemoveAt(0);
            Destroy(firstContainer);
        }
    }

    void SpawnContainer(ref Vector3 spawnPoint)
    {
        GameObject newContainer = Instantiate(rainContainerPrefab, spawnPoint, Quaternion.identity, transform);
        activeContainers.Add(newContainer);
        activeSpawners.Add(newContainer.GetComponent<RainSpawner>());
        spawnPoint.x += containerWidth;
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }
}