using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Fungsi untuk mengulang scene yang sedang berjalan
    public void ReplayCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi untuk pindah ke scene tertentu berdasarkan nama
    public void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}