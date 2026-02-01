using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan

public class pindahscene : MonoBehaviour
{
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
