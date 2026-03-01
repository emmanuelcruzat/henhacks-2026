using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = 5f; // movement speed in units/sec

    void Update()
    {
        // Get input from arrow keys / WASD
        float horizontal = Input.GetAxis("Horizontal"); // Left/Right
        float vertical = Input.GetAxis("Vertical");     // Up/Down

        // Create movement vector
        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        // Move the camera
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
