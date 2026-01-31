using UnityEngine;
using UnityEngine.InputSystem;

public class ClickDetector : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint =
                Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit =
                Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                ButtonLogic button = hit.collider.GetComponent<ButtonLogic>();

                if (button != null)
                {
                    button.OnClick();
                }
            }
        }
    }
}
