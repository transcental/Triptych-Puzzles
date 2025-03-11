using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private PlayerController[] players;
    public PlayerController[] Players {
        get => players;
    }
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
