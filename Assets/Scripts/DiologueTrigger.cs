using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData dialogue;
    public DialogueUI ui;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || cam == null) return;

        // left click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            // checks if THIS object's collider was clicked
            Collider2D hit = Physics2D.OverlapPoint(worldPoint);
            if (hit != null && hit.gameObject == gameObject)
            {
                Debug.Log($"Clicked {gameObject.name} -> showing dialogue");
                ui.Show(dialogue);
            }
        }
    }
}