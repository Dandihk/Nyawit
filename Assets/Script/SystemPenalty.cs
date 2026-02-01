using UnityEngine;
using UnityEngine.InputSystem;


public class SystemPenalty : MonoBehaviour
{
    public static SystemPenalty instance;

    [Header("Objects")]
    public GameObject lightGlitch;
    public GameObject heavyGlitch;
    public GameObject jumpscareObject;
    public GameObject gameOverPopup; // panel Game Over
    public Transform spawnerParent;   // parent spawner tombol

    private int wrongCount = 0;
    private int correctCount = 0;
    private float lastWrongTime = 0f;
    private bool isJumpscareActive = false;
    private bool isPopupShown = false;
    public int maxPopUps = 20;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetAll();
    }

    void Update()
    {
        // cek spawn terlalu banyak
        if (!isJumpscareActive && spawnerParent != null)
        {
            if (spawnerParent.childCount >= maxPopUps)
            {
                ActivateJumpscare();
            }
        }

        // cek klik untuk menampilkan popup setelah jumpscare
         if (isJumpscareActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShowGameOverPopup();
        }
    }

    public void WrongPressed()
    {
        wrongCount++;
        correctCount = 0;
        lastWrongTime = Time.time;

        if (wrongCount == 1)
            ActivateLightGlitch();
        else if (wrongCount == 2)
            ActivateHeavyGlitch();
        else if (wrongCount >= 3)
            ActivateJumpscare();
    }

    public void CorrectPressed()
    {
        if (wrongCount > 0)
        {
            correctCount++; // Tambah hitungan benar
            Debug.Log("Correct Count: " + correctCount);

            // Jika sudah benar 2x, baru hapus semua glitch
            if (correctCount >= 2)
            {
                Debug.Log("Glitch Clear!");
                ResetAll();
            }
        }
    }

    void ActivateLightGlitch()
    {
        if (lightGlitch != null) lightGlitch.SetActive(true);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);
        if (jumpscareObject != null) jumpscareObject.SetActive(false);
    }

    void ActivateHeavyGlitch()
    {
        if (heavyGlitch != null) heavyGlitch.SetActive(true);
        if (lightGlitch != null) lightGlitch.SetActive(false);
        if (jumpscareObject != null) jumpscareObject.SetActive(false);
    }

    void ActivateJumpscare()
    {
        if (jumpscareObject == null) return;

        jumpscareObject.SetActive(true);
        isJumpscareActive = true;
        isPopupShown = false; // reset flag popup

        if (lightGlitch != null) lightGlitch.SetActive(false);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);

        // langsung pause game
        Time.timeScale = 0f;
    }

    void ShowGameOverPopup()
    {
        if (gameOverPopup != null)
            gameOverPopup.SetActive(true);

        isPopupShown = true; // agar popup tidak muncul berkali-kali
        // Game tetap pause
        Time.timeScale = 0f;
    }

    public void ResetAll()
    {
        wrongCount = 0;
        isJumpscareActive = false;
        isPopupShown = false;

        if (lightGlitch != null) lightGlitch.SetActive(false);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);
        if (jumpscareObject != null) jumpscareObject.SetActive(false);
        if (gameOverPopup != null) gameOverPopup.SetActive(false);

        Time.timeScale = 1f;
    }
}
