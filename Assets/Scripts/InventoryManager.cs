using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ScreenManager screenManager;
    private List<GameObject> players;
    private Dictionary<GameObject, GameObject> inventories;
    
    void Start()
    {
        var playerControllers = screenManager.Players;
        players = new List<GameObject>();
        inventories = new Dictionary<GameObject, GameObject>();
        foreach (var playerController in playerControllers)
        {
            var parent = playerController.gameObject;
            players.Add(parent);
            inventories.Add(parent, null);
        }
    }
    
    public void AddItem(GameObject player, GameObject item)
    {
        if (!players.Contains(player))
        {
            Debug.LogError("Player not found in players list");
            return;
        }
        if (inventories[player] != null)
        {
            var oldItem = inventories[player];
            Drop(oldItem);
            inventories[player] = null;
            return;
        }
        inventories[player] = item;
        var interactable = FindIHoldInteractable(item);
        if (interactable == null)
        {
            Debug.LogError("Item does not have an IHoldInteractable script");
            return;
        }
        interactable.PickUp(player);
    }
    
    IHoldInteractable FindIHoldInteractable(GameObject obj)
    {
        MonoBehaviour[] allScripts = obj.GetComponents<MonoBehaviour>();
        for (int i = 0; i < allScripts.Length; i++)
        {
            if(allScripts[i] is IHoldInteractable) return allScripts[i] as IHoldInteractable;
        }
        
        return null;
    }
    
    public void Drop(GameObject item)
    {
        if (item == null)
        {
            Debug.LogError("Item is null");
            return;
        }
        var interactable = FindIHoldInteractable(item);
        if (interactable == null)
        {
            Debug.LogError("Item does not have an IHoldInteractable script");
            return;
        }
        interactable.Drop();
    }
}
