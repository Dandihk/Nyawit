using UnityEngine;
using TMPro; // Gunakan TextMeshPro untuk teks yang lebih tajam

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton agar mudah dipanggil script lain
    
    [Header("Score Settings")]
    public int score = 0;
    public TextMeshProUGUI scoreText; // Drag objek Text UI ke sini

    void Awake()
    {
        // Setup Singleton
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // Fungsi yang akan dipanggil saat klik atau dapat poin
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score.ToString();
        }
    }
}