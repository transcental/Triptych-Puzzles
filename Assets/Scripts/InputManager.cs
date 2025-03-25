using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private GravityController gravityController;
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private float interactionRadius = 0.5f;

    private Vector2 MoveComposite;
    private Controls controls;
    private List<GameObject> players;
    
    void Start()
    {
        var playerControllers = screenManager.Players;
        players = new List<GameObject>();
        foreach (var playerController in playerControllers)
        {
            var parent = playerController.gameObject;
            players.Add(parent);
        }
    }
    
    private void OnEnable()
    {
        if (controls != null)
            return;
        
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void FixedUpdate()
    {
        Move(MoveComposite);
    }

    private void Move(Vector2 direction)
    {
        var collided = false;
        foreach (var player in players)
        {
            var origin = player.transform.position;
            var raycastDir = new Vector3(direction.x, 0, direction.y);
            // distance should be the distance the player moves in one frame
            var radius = 0.5f;
            var point1 = origin + new Vector3(0, radius, 0);
            var point2 = origin + new Vector3(0, -radius, 0);
            var hit = Physics.CapsuleCast(point1, point2, radius, raycastDir, moveSpeed);
            if (hit)
            {
                collided = true;
                break;
            }
        }
        if (collided)
            return;
        foreach (var player in players)
        {
            player.transform.Translate(new Vector3(direction.x * 0.1f, 0, direction.y * 0.1f));
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!!!!!!");
        throw new NotImplementedException();
    }
    
    public void OnCrouch(InputAction.CallbackContext context)
    {
        Debug.Log("crouch");
        throw new NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }
        foreach (var player in players)
        {
            // get object in front of player
            RaycastHit hit;
            var pos1 = player.transform.position + new Vector3(0, 0.5f, 0);
            var pos2 = player.transform.position + new Vector3(0, -0.5f, 0);
            var radius = 0.5f;
            float distanceToObstacle = 0;
            
            GameObject obj = null;
            if (Physics.CapsuleCast(pos1, pos2, radius, transform.forward, out hit, interactionRadius))
            {
                distanceToObstacle = hit.distance;
                obj = hit.collider.gameObject;
            }
            
            if (obj == null)
                continue;
            if (distanceToObstacle > interactionRadius)
                continue;

            MonoBehaviour[] allScripts = obj.GetComponents<MonoBehaviour>();
            for (int i = 0; i < allScripts.Length; i++)
            {
                if (allScripts[i] is IInteractable)
                {
                    var interactable = allScripts[i] as IInteractable;
                    interactable.Interact(player);
                    continue;
                }
            }
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        gravityController.Jump();
    }
    
    public void OnNext(InputAction.CallbackContext context)
    {
        Debug.Log("next");
        throw new NotImplementedException();
    }
    
    public void OnPrevious(InputAction.CallbackContext context)
    {
        Debug.Log("previous");
        throw new NotImplementedException();
    }
    
    public void OnSprint(InputAction.CallbackContext context)
    {
        Debug.Log("sprint");
        throw new NotImplementedException();
    }
}
