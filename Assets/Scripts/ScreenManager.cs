using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"Displays connected: {Display.displays.Length}");
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    void Update()
    {
        
    }
}
