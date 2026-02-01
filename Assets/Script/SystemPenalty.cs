using UnityEngine;
using UnityEngine.InputSystem;
public class SystemPenalty : MonoBehaviour
{
    public static SystemPenalty instance;

    [Header("Objects")]
    public GameObject lightGlitch;
    public GameObject heavyGlitch;
    public GameObject jumpscareObject;
    public GameObject gameOverPopup; // assign panel popup Game Over


    private int wrongCount = 0;
    private float lastWrongTime = 0f;
    private bool isJumpscareActive = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
{
    Debug.Log("=== CEK ISI REFERENCE DI START ===");
    Debug.Log("LightGlitch : " + lightGlitch);
    Debug.Log("HeavyGlitch : " + heavyGlitch);
    Debug.Log("Jumpscare   : " + jumpscareObject);

    ResetAll();
}
void Update()
{
    // Cek jika jumpscare sedang aktif dan pemain mengklik layar (Mouse kiri atau Tap)
   if (isJumpscareActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShowGameOverPopup();
        }
}


    public void WrongPressed()
    {
        wrongCount++;
        lastWrongTime = Time.time;

        Debug.Log("Wrong Count: " + wrongCount);

        if (wrongCount == 1)
        {
            ActivateLightGlitch();
        }
        else if (wrongCount == 2)
        {
            ActivateHeavyGlitch();
        }
        else if (wrongCount >= 3)
        {
            ActivateJumpscare();
        }
    }

    public void CorrectPressed()
    {
        Debug.Log("Correct pressed!");

        // Kalau masih dalam 2 detik setelah salah terakhir
        if (wrongCount > 0 && Time.time - lastWrongTime <= 2f)
        {
            Debug.Log("Glitch Reset!");
            ResetAll();
        }
    }

   void ActivateLightGlitch()
{
    Debug.Log("LIGHT GLITCH AKTIF DIPANGGIL");

    if (lightGlitch == null)
    {
        Debug.LogError("LIGHT GLITCH NULL! Object belum di-assign di inspector.");
    }
    else
    {
        Debug.Log("LIGHT GLITCH BERHASIL DIAKTIFKAN");
        lightGlitch.SetActive(true);
    }

    if (heavyGlitch != null)
        heavyGlitch.SetActive(false);

    if (jumpscareObject != null)
        jumpscareObject.SetActive(false);
}


    void ActivateHeavyGlitch()
{
    Debug.Log("HEAVY GLITCH AKTIF DIPANGGIL");

    if (heavyGlitch == null)
    {
        Debug.LogError("HEAVY GLITCH NULL! Object belum di-assign di inspector.");
    }
    else
    {
        Debug.Log("HEAVY GLITCH BERHASIL DIAKTIFKAN");
        heavyGlitch.SetActive(true);
    }

    if (lightGlitch != null)
        lightGlitch.SetActive(false);

    if (jumpscareObject != null)
        jumpscareObject.SetActive(false);
}


   void ActivateJumpscare()
{
    Debug.Log("JUMPSCARE DIPANGGIL");

    if (jumpscareObject == null)
    {
        Debug.LogError("JUMPSCARE NULL! Object belum di-assign di inspector.");
        return;
    }

    jumpscareObject.SetActive(true);
    isJumpscareActive = true; // Tandai bahwa jumpscare sedang muncul
    Debug.Log("JUMPSCARE BERHASIL DIAKTIFKAN: " + jumpscareObject.name);

    if (lightGlitch != null)
        lightGlitch.SetActive(false);

    if (heavyGlitch != null)
        heavyGlitch.SetActive(false);

    if (gameOverPopup != null)
        gameOverPopup.SetActive(false);

    // Pause game jika perlu
    Time.timeScale = 0f;
}
void ShowGameOverPopup()
{
    isJumpscareActive = false; // Reset tanda agar tidak terpanggil terus menerus
    
    if (gameOverPopup != null)
        gameOverPopup.SetActive(true);

    // Pause game hanya SETELAH popup muncul
    Time.timeScale = 0f;
}



    public void ResetAll()
    {
        wrongCount = 0;
        isJumpscareActive = false;

        lightGlitch.SetActive(false);
        heavyGlitch.SetActive(false);
        jumpscareObject.SetActive(false);

         if (gameOverPopup != null)
             gameOverPopup.SetActive(false);

    Time.timeScale = 1f;
    }
}
