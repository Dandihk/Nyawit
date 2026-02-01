using UnityEngine;

public class spawner : MonoBehaviour
{

[Header("Settings Objek")]
    public GameObject[] objectsToSpawn;
    //ubah jadi array

    public float spawnInterval = 0.5f;
    
    // Variabel untuk melacak urutan layer agar tidak overlay/tumpang tindih visual
    private int currentSortingOrder = 0;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        // Menjalankan fungsi spawn setiap 0.5 detik
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void SpawnObject()
    {
       if (objectsToSpawn == null || objectsToSpawn.Length == 0) return;
        //0. random prefab dari array
        int index = Random.Range(0, objectsToSpawn.Length); 
        GameObject objectToSpawn = objectsToSpawn[index];

        // 1. Dapatkan posisi random di dalam batas kamera
        Vector3 spawnPosition = GetRandomCameraPosition();

        // 2. Munculkan objek
        GameObject newObj = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // 3. Atur agar Hierarchy tetap rapi (masuk ke dalam folder/parent)
        newObj.transform.parent = this.transform;

        // 4. ATASI OVERLAY VISUAL (Kuncinya di sini)
        // Kita cari SpriteRenderer baik di objek utama maupun di anak-anaknya (child)
        SpriteRenderer[] renderers = newObj.GetComponentsInChildren<SpriteRenderer>();
        
        foreach (SpriteRenderer sr in renderers)
        {
            // Tambahkan nilai sorting order agar objek baru selalu tampil "paling depan"
            sr.sortingOrder = currentSortingOrder;
        }

        // Naikkan angka untuk objek berikutnya
        currentSortingOrder++;
    }

  Vector3 GetRandomCameraPosition()
{
    float spawnZ = 0f;
    
    // 1. Tentukan batas aman (0.1f berarti kasih jarak 10% dari pinggir layar)
    float margin = 0.2f; 

    float randomXViewport = Random.Range(margin, 1f - margin);
    float randomYViewport = Random.Range(margin, 1f - margin);

    // 2. Konversi dari koordinat layar (0-1) ke posisi di dunia game
    Vector3 viewportPoint = new Vector3(randomXViewport, randomYViewport, 0f);
    
    // Hitung jarak kamera ke Z target
    float distance = Mathf.Abs(mainCamera.transform.position.z - spawnZ);
    viewportPoint.z = distance;

    Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPoint);
    worldPos.z = spawnZ;

    return worldPos;
}


    
}