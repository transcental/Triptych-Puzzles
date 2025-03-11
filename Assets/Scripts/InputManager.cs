using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, Controls.IPlayerActions
{
    [SerializeField] private ScreenManager screenManager;
    [SerializeField] private float moveSpeed = 0.1f;
    
    private Vector2 MoveComposite;
    public Action OnJumpPerformed;
    private Controls controls;
    
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
        var playerControllers = screenManager.Players;
        // playerControllers is an array of PlayerController objects, get the parent GameObjects
        var players = new List<GameObject>();
        foreach (var playerController in playerControllers)
        {
            var parent = playerController.gameObject;
            players.Add(parent);
        }
        
        // For each player in players, raycast to make sure they won't collide before moving. if any collide, none can move. If none collide, they all move.
        
        var collided = false;
        foreach (var player in players)
        {
            var origin = player.transform.position;
            var raycastDir = new Vector3(direction.x, 0, direction.y);
            // distance should be the distance the player moves in one frame
            var hit = Physics.Raycast(origin, raycastDir, moveSpeed);
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
        Debug.Log("interact");
        throw new NotImplementedException();
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        OnJumpPerformed?.Invoke();
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
