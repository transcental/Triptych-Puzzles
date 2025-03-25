using UnityEngine;

public class PickupCube : MonoBehaviour, IHoldInteractable
{
    private bool isHeld = false;
    private GameObject holder;
    [SerializeField] private InventoryManager inventoryManager;
    private Vector3 startingScale;
    
    public bool IsHeld => isHeld;
    public GameObject Holder => holder;
    
    void Start()
    {
        startingScale = transform.localScale;
    }

    public void Update()
    {
        if (isHeld)
        {
            transform.position = holder.transform.position + new Vector3(0, 0, 1);
        }
    }
    
    public void Interact(GameObject interacter)
    {
        if (isHeld)
        {
            inventoryManager.Drop(gameObject);
        }
        else
        {
            inventoryManager.AddItem(interacter, gameObject);
        }
    }
    
    public void PickUp(GameObject owner)
    {
        isHeld = true;
        holder = owner;
        
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.position = holder.transform.position + new Vector3(0, 0, 1);
    }
    
    public void Drop()
    {
        isHeld = false;
        holder = null;
        transform.localScale = startingScale;
        transform.position = transform.position + new Vector3(0, 0, 1);
    }
}
