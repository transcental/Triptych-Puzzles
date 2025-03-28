using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    public bool IsOpen => isOpen;
    [SerializeField] private bool startOpen = false;
    [SerializeField] private bool multiUse = false;

    public void Interact(GameObject interacter)
    {
        if (multiUse)
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
        else
        {
            if (isOpen && startOpen)
            {
                Close();
            }
            else if (!isOpen && !startOpen)
            {
                Open();
            } 
        }
    }
    
    void OnStart()
    {
        isOpen = startOpen;
    }

    public void Open()
    {
        Debug.Log("Opening door");
        isOpen = true;
        transform.position = transform.position + new Vector3(0, 2, 0);
    }

    public void Close()
    {
        Debug.Log("Closing door");
        isOpen = false;
        transform.position = transform.position + new Vector3(0, -2, 0);
    }
}
