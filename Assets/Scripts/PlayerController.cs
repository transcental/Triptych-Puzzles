using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rendering")]
    
    [Tooltip("Display number to render player on. 1 is top, 2 is left, 3 is right.")]
    [SerializeField] private int displayIndex;
    private Camera playerCamera;
    [SerializeField] private GameObject lookAt;

    void Start()
    {
        var displays = Display.displays.Length;
        Debug.Log(displays);
        if (displays >= displayIndex && !Display.displays[displayIndex - 1].active && displayIndex > 0)
        {
            Debug.Log($"Display {displayIndex} activated");
            Display.displays[displayIndex - 1].Activate();
            Debug.Log($"Activated {gameObject.name} on display {displayIndex - 1}");
        }
        playerCamera = GetComponentInChildren<Camera>();
        playerCamera.targetDisplay = displayIndex - 1;

        playerCamera.transform.position = lookAt.transform.position;
        playerCamera.transform.rotation = lookAt.transform.rotation;
        var playerPos = lookAt.transform.parent.transform;
        // set the camera to face the player
        playerCamera.transform.LookAt(playerPos);
    }
}
