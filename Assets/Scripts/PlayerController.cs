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
        if (displays > displayIndex && !Display.displays[displayIndex].active && displayIndex > 0)
        {
            Display.displays[displayIndex - 1].Activate();
            Debug.Log($"Activated {gameObject.name} on display {displayIndex - 1}");
        }
        playerCamera = GetComponentInChildren<Camera>();
        playerCamera.targetDisplay = displayIndex - 1;

        playerCamera.transform.position = lookAt.transform.position;
    }
}
