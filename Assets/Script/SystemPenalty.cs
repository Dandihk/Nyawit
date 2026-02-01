using UnityEngine;
using UnityEngine.InputSystem;

public class SystemPenalty : MonoBehaviour
{
    public static SystemPenalty instance;

    [Header("Objects")]
    public GameObject lightGlitch;
    public GameObject heavyGlitch;
    public GameObject[] jumpscareObjects;
    public GameObject gameOverPopup; 
    public Transform spawnerParent;   

    private int wrongCount = 0;
    private int correctCount = 0;
    private bool isJumpscareActive = false;
    private bool isPopupShown = false;
    public int maxPopUps = 20;

    private GameObject activeJumpscare; // Untuk melacak jumpscare yang terpilih

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
        if (!isJumpscareActive && spawnerParent != null)
        {
            if (spawnerParent.childCount >= maxPopUps)
            {
                ActivateJumpscare();
            }
        }

        if (isJumpscareActive && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShowGameOverPopup();
        }
    }

    public void WrongPressed()
    {
        wrongCount++;
        correctCount = 0;

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
            correctCount++;
            if (correctCount >= 2)
            {
                ResetAll();
            }
        }
    }

    void ActivateLightGlitch()
    {
        if (lightGlitch != null) lightGlitch.SetActive(true);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);
        // Mematikan semua jumpscare di array
        ToggleAllJumpscares(false);
    }

    void ActivateHeavyGlitch()
    {
        if (heavyGlitch != null) heavyGlitch.SetActive(true);
        if (lightGlitch != null) lightGlitch.SetActive(false);
        ToggleAllJumpscares(false);
    }

    void ActivateJumpscare()
    {
        if (jumpscareObjects == null || jumpscareObjects.Length == 0) return;

        // Pilih random
        int randomIndex = Random.Range(0, jumpscareObjects.Length);
        activeJumpscare = jumpscareObjects[randomIndex];

        if (activeJumpscare != null)
        {
            activeJumpscare.SetActive(true);
            isJumpscareActive = true;
            isPopupShown = false;
        }

        if (lightGlitch != null) lightGlitch.SetActive(false);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);

        Time.timeScale = 0f;
    }

    void ShowGameOverPopup()
    {
        if (gameOverPopup != null)
            gameOverPopup.SetActive(true);

        if (activeJumpscare != null)
            activeJumpscare.SetActive(false);

        isPopupShown = true;
        Time.timeScale = 0f;
    }

    public void ResetAll()
    {
        wrongCount = 0;
        correctCount = 0;
        isJumpscareActive = false;
        isPopupShown = false;

        if (lightGlitch != null) lightGlitch.SetActive(false);
        if (heavyGlitch != null) heavyGlitch.SetActive(false);
        
        ToggleAllJumpscares(false);

        if (gameOverPopup != null) gameOverPopup.SetActive(false);

        Time.timeScale = 1f;
    }

    // Fungsi bantuan untuk mematikan semua jumpscare dalam array
    void ToggleAllJumpscares(bool state)
    {
        foreach (GameObject js in jumpscareObjects)
        {
            if (js != null) js.SetActive(state);
        }
    }
}