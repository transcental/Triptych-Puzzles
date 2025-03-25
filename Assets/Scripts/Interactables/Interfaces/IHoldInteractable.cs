using UnityEngine;

public interface IHoldInteractable : IInteractable
{
    bool IsHeld { get; }
    GameObject Holder { get; }
    
    void PickUp(GameObject owner);
    void Drop();
}