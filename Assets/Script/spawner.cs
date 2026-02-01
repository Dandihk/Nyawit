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
    // Tentukan Z objek (misal spawn di z = 0)
    float spawnZ = 0f;

    // Hitung jarak dari kamera ke plane Z spawn
    float distanceToCamera = Mathf.Abs(mainCamera.transform.position.z - spawnZ);

    // Konversi viewport ke world point
    Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, distanceToCamera));
    Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, distanceToCamera));

    float randomX = Random.Range(lowerLeft.x, upperRight.x);
    float randomY = Random.Range(lowerLeft.y, upperRight.y);

    return new Vector3(randomX, randomY, spawnZ);
}


    
}