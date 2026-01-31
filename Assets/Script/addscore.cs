using UnityEngine;

public class addscore : MonoBehaviour
{
    public void DestroyParent()
    {
        // Menambah skor ke GameManager (script sebelumnya)
        if (GameManager.instance != null)
        {
            GameManager.instance.AddScore(10);
        }

        // Hancurkan Parent dari Button ini
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
