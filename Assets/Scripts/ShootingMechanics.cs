using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanics : MonoBehaviour
{
    public PlayerInput input;
    [SerializeField] GameObject shooter;
    SpriteRenderer shooterSprite;
    Action onShoot;

    private void Start()
    {
        shooterSprite = shooter.GetComponentInChildren<SpriteRenderer>();

        input.attackAction.performed += Attack;
        onShoot += Attack;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onShoot?.Invoke();
        }
    }

    void Attack()
    {
        Debug.Log("hehe");
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        Debug.Log("attack");
    }
}
