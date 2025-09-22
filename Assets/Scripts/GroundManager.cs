using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject groundPrefab;
    public int initialTiles = 5;

    private float tileWidth;
    private float currentSpeed = 2f;
    private Camera mainCamera;
    [HideInInspector]
    public List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;

        if (groundPrefab == null)
        {
            Debug.LogError("Ground Prefab не назначен в GroundManager!");
            this.enabled = false;
            return;
        }

        tileWidth = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 nextSpawnPoint = transform.position;

        for (int i = 0; i < initialTiles; i++)
        {
            GameObject newTile = Instantiate(groundPrefab, nextSpawnPoint, Quaternion.identity, transform);
            newTile.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
            activeTiles.Add(newTile);
            nextSpawnPoint.x += tileWidth;
        }
    }

    void Update()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(Vector2.left * currentSpeed * Time.deltaTime, Space.World);
        }

        if (activeTiles.Count > 0)
        {
            GameObject lastTile = activeTiles[activeTiles.Count - 1];
            float rightEdgeOfScreen = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

            while (lastTile.transform.position.x < rightEdgeOfScreen + tileWidth)
            {
                SpawnTile();
                lastTile = activeTiles[activeTiles.Count - 1];
            }
        }

        if (activeTiles.Count > 0)
        {
            GameObject firstTile = activeTiles[0];
            float leftEdgeOfScreen = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - tileWidth;

            if (firstTile.transform.position.x < leftEdgeOfScreen)
            {
                activeTiles.RemoveAt(0);
                Destroy(firstTile);
            }
        }
    }

    private void SpawnTile()
    {
        Vector3 nextSpawnPoint = activeTiles[activeTiles.Count - 1].transform.position;
        nextSpawnPoint.x += tileWidth;
        GameObject newTile = Instantiate(groundPrefab, nextSpawnPoint, Quaternion.identity, transform);
        newTile.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
        activeTiles.Add(newTile);
    }

    public void SetSpeed(float newSpeed)
    {
        currentSpeed = newSpeed;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}