using UnityEngine;

public class SystemPenalty : MonoBehaviour
{
    public static SystemPenalty instance;

    [Header("Objects")]
    public GameObject lightGlitch;
    public GameObject heavyGlitch;
    public GameObject jumpscareObject;

    private int wrongCount = 0;
    private float lastWrongTime = 0f;

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
    }
    else
    {
        Debug.Log("JUMPSCARE BERHASIL DIAKTIFKAN");
        jumpscareObject.SetActive(true);
    }

    if (lightGlitch != null)
        lightGlitch.SetActive(false);

    if (heavyGlitch != null)
        heavyGlitch.SetActive(false);
}


    public void ResetAll()
    {
        wrongCount = 0;

        lightGlitch.SetActive(false);
        heavyGlitch.SetActive(false);
        jumpscareObject.SetActive(false);
    }
}
