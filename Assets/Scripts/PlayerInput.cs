using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerInputActions playerInput;
    public InputAction moveAction { get;  set; }
    public InputAction iconChangeAction { get; set; }
    public InputAction iconSelectAction { get; set; }
    public InputAction attackAction { get; set; }

    private void OnEnable()
    {
        playerInput = new PlayerInputActions();
        
        moveAction = playerInput.Player.Move;
        moveAction.Enable();

        attackAction = playerInput.Player.Attack;
        attackAction.Enable();

        iconChangeAction = playerInput.Interaction.Change;
        iconChangeAction.Enable();

        iconSelectAction = playerInput.Interaction.Select;
        iconSelectAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        attackAction.Disable();
        iconChangeAction.Disable();
        iconSelectAction.Disable();
    }
}
