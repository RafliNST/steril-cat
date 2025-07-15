using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanics : MonoBehaviour
{
    public PlayerInput input;
    [SerializeField] GameObject shooter;
    SpriteRenderer shooterSprite;
    Action onShoot;

    private void Awake()
    {
        input = GameObject.FindGameObjectWithTag("GameInput").GetComponent<PlayerInput>();
    }

    private void Start()
    {
        //shooterSprite = shooter.GetComponentInChildren<SpriteRenderer>();

        input.attackAction.performed += Attack;
        input.attackAction.performed += (InputAction.CallbackContext ctx) => Debug.Log("dari delta function");
    }

    private void Update()
    {
        PointingAtMouse();
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        Debug.Log("attack");
    }

    float PointingAtMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        shooter.transform.localRotation = Quaternion.Euler(0, 0, rotZ);
        return rotZ;
    }
}
