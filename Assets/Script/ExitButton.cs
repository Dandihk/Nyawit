using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Fungsi ini tinggal dipanggil oleh tombol UI
    public void ExitGame()
    {
        Debug.Log("Exit button clicked! Closing game...");
        Application.Quit(); // langsung close Windows build
    }
}
