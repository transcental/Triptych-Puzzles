using UnityEngine;

public interface IHoldInteractable : IInteractable
{
    bool IsHeld { get; }
    GameObject Holder { get; }
    
    void Update();
    void PickUp(GameObject owner);
    void Drop();
}