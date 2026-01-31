using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public bool isCorrectButton;

    public void OnClick()
    {
        if (isCorrectButton)
        {
            GameManager.instance.AddScore(10);
            SystemPenalty.instance.CorrectPressed();

            // Hapus popup tempat tombol ini berada
            Destroy(transform.parent.gameObject);
        }
        else
        {
            SystemPenalty.instance.WrongPressed();
        }
    }
}
