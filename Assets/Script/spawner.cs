using UnityEngine;

public class spawner : MonoBehaviour
{
    [Header("Settings Objek")]
    public GameObject[] objectsToSpawn;

    public float spawnInterval = 0.5f;
    private float baseSpawnInterval;

    private int currentSortingOrder = 0;
    private Camera mainCamera;

    // Ambil score dari GameManager
    private int lastSpeedIncreaseScore = 0;
    public float spawnDecreaseAmount = 0.1f; // setiap 100 poin, spawn interval berkurang

    void Start()
    {
        mainCamera = Camera.main;
        baseSpawnInterval = spawnInterval;

        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void Update()
    {
        // Ambil score dari GameManager
        int playerScore = GameManager.instance.score;

        // Cek score untuk tambah speed spawn
        if (playerScore / 100 > lastSpeedIncreaseScore / 100)
        {
            IncreaseSpawnSpeed();
            lastSpeedIncreaseScore = playerScore;
        }
    }

    void IncreaseSpawnSpeed()
    {
        spawnInterval = Mathf.Max(0.1f, spawnInterval - spawnDecreaseAmount);

        CancelInvoke(nameof(SpawnObject));
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);

        Debug.Log("Spawn speed increased! New interval: " + spawnInterval);
    }

    void SpawnObject()
    {
        if (objectsToSpawn == null || objectsToSpawn.Length == 0) return;

        int index = Random.Range(0, objectsToSpawn.Length);
        GameObject objectToSpawn = objectsToSpawn[index];

        Vector3 spawnPosition = GetRandomCameraPosition();
        GameObject newObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        newObj.transform.parent = this.transform;

        SpriteRenderer[] renderers = newObj.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.sortingOrder = currentSortingOrder;
        }

        currentSortingOrder++;
    }

    Vector3 GetRandomCameraPosition()
    {
        float spawnZ = 0f;
        float margin = 0.2f;

        float randomXViewport = Random.Range(margin, 1f - margin);
        float randomYViewport = Random.Range(margin, 1f - margin);

        Vector3 viewportPoint = new Vector3(randomXViewport, randomYViewport, 0f);
        float distance = Mathf.Abs(mainCamera.transform.position.z - spawnZ);
        viewportPoint.z = distance;

        Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPoint);
        worldPos.z = spawnZ;
        return worldPos;
    }
}
