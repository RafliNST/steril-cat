using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerInput _input;
    Rigidbody2D physicsComp;
    public float speedForce, maxSpeed;

    [SerializeField] GameObject pointerHand;

    private void Awake()
    {
        physicsComp = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        physicsComp.transform.position += Movement() * speedForce * Time.deltaTime;
        //PointingAtMouse();
    }

    private Vector3 Movement()
    {
        return _input.moveAction.ReadValue<Vector2>();
    }

    float PointingAtMouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouse);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        pointerHand.transform.localRotation = Quaternion.Euler(0,0,rotZ);
        return rotZ;
    }
}
